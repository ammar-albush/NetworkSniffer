using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace NetworkSniffer
{
    public partial class DeviceSniffer : Form
    {

        LibPcapLiveDevice Driver { get; set; }

        ActiveDevice ActiveDevice { get; set; }

        List<SpoofARP> SpoofARPs = new List<SpoofARP>(2);

        void init_Driver_CB()
        {

            foreach (LibPcapLiveDevice libPcapLive in LibPcapLiveDeviceList.Instance)
            {
                string adrre = String.Empty;
                foreach (var address in libPcapLive.Addresses)
                {
                    if (address.Addr.ipAddress != null)
                        if (address.Addr.ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                           
                           adrre = address.Addr.ipAddress.ToString();

                        }
                }
                drivers_comboBox.Items.Add((adrre==string.Empty)?"IP NOT DETECTED":adrre);
            }


        }

       public byte[] SubnetMask { get; set; }
        IPAddress getGatway(IPAddress NETID, byte[] subnetmask)
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                        foreach (GatewayIPAddressInformation gw in ni.GetIPProperties().GatewayAddresses)
                            if (gw.Address != null)
                                if (gw.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                    if (getNetID(subnetmask, gw.Address).Equals(getNetID(subnetmask, NETID)))
                                        return gw.Address;
            return null;
        }

        IPAddress getNetID(byte[] subnetmask, IPAddress IP)
        {

            byte[] ip = IP.GetAddressBytes();

            StringBuilder sb = new StringBuilder();

            int index = 0;
            foreach (byte Bvalue in subnetmask)
            {
                if(index == subnetmask.Length-1)
                sb.Append((ip[index] & Bvalue).ToString());
                else sb.Append(((ip[index] & Bvalue).ToString())+".");
                index++;

            }
            return IPAddress.Parse(sb.ToString());

        }

        bool Driver_on()
        {

            try
            {

                if (this.drivers_comboBox.SelectedIndex != -1)
                {

                    if((bool)spoofarp_toggle.Tag)
                    {

                        Driver.Open();

                        IPAddress target = IPAddress.Parse(ActiveDevice.IPV4Adresse);

                        IPAddress gatway = getGatway(IPAddress.Parse(this.ActiveDevice.IPV4Adresse),this.SubnetMask);

                        if (target == null || gatway == null)
                            throw new Exception("target or gatway must be not null");

                        SpoofARP spoofARPdup1 = new SpoofARP(Driver,target,gatway);
                        SpoofARP spoofARPdup2 = new SpoofARP(Driver,gatway,target);
                        if(spoofARPdup1!= null || spoofARPdup2 != null)
                        {

                            SpoofARPs.Add(spoofARPdup1);
                            SpoofARPs.Add(spoofARPdup2);
                            try
                            {
                                foreach (SpoofARP spoofARP in SpoofARPs)
                                {
                                    spoofARP.SendArpRequstsAsync();
                                    if (spoofARP.Error)
                                        throw new Exception("error while running the Thread");

                                }

                                Driver.OnPacketArrival += Driver_OnPacketArrival;

                                Driver.StartCapture();

                            }
                            catch (Exception ex)
                            {
                                Driver.Close();
                                throw new Exception(ex.Message); 
                                
                            }
                        }
                        else
                        {

                            Driver.Close();
                            throw new Exception("Packt must be not null");
                           
                        }
                        

                        

                    }
                    else
                    {

                        Driver.Open();

                        Driver.OnPacketArrival += Driver_OnPacketArrival;

                        Driver.StartCapture();

                    }

                   
                }
                else
                {

                    throw new Exception("Pease Select a Driver First and then Try again !");
                }

                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;

            }

        }

        void Driver_off()
        {

            try
            {
                if (this.Driver != null)
                {
                    if (this.Driver.Opened)
                    {
                        if (!(bool)spoofarp_toggle.Tag)
                        {

                            foreach (SpoofARP spoofARP in SpoofARPs)
                                  spoofARP.StopAsync();
                        }
                        this.Driver.StopCapture();
                        this.Driver.Close();

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }


        void Driver_OnPacketArrival(object s, PacketCapture e)
        {

            int index = 0;

            var rawCapture = e.GetPacket();

            List<DNSControl> packetIds = new List<DNSControl>();

            var packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);

            if (packet != null)
            {
                var ethernetPacket = (PacketDotNet.EthernetPacket)packet;

                if (ethernetPacket.Type == PacketDotNet.EthernetType.IPv4 || ethernetPacket.Type == PacketDotNet.EthernetType.IPv6)
                {

                    var ipPacket = packet.Extract<PacketDotNet.IPPacket>();

                    if (ipPacket != null)
                    {
                        //IpV4
                        if (ipPacket.Version == IPVersion.IPv4)
                        {

                            ipPacket = packet.Extract<PacketDotNet.IPv4Packet>();

                            if (!string.IsNullOrEmpty(this.ActiveDevice.IPV4Adresse))
                            {
                                if (ipPacket.SourceAddress.ToString() == ActiveDevice.IPV4Adresse || ipPacket.DestinationAddress.ToString() == ActiveDevice.IPV4Adresse)
                                {


                                    if (ipPacket.Protocol == ProtocolType.Udp)
                                    {

                                        var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();

                                        if (udpPacket.SourcePort == 53 || udpPacket.DestinationPort == 53)
                                        {
                                            DNSPacket DNS = new DNSPacket(udpPacket.PayloadData, "DNS");

                                            if (DNS.OP_Code == DNSPacket.OP_Code_Type.Standard_Query)
                                            {
                                                if (DNS.QR == DNSPacket.QR_Type.request)
                                                {

                                                    List<DNSControl> dNSControls = packetIds.Where(x => (x.Id == DNS.ID) && (x.Request)).ToList();


                                                    if (!dNSControls.Any())
                                                    {
                                                        DNSControl dNSControl = new DNSControl();
                                                        dNSControl.Id = DNS.ID;
                                                        dNSControl.Request = true;
                                                        packetIds.Add(dNSControl);

                                                        SafeInvoke(this.packetsListControl1, new Action(() =>
                                                        {



                                                            string DnsName = string.Empty;
                                                            string Dns_Info = string.Empty;
                                                            foreach (DNS_INFO kvp in DNS.DNS_INFO)
                                                            {

                                                                DnsName += kvp.DNS_NAME + ",";
                                                                Dns_Info += kvp.DNS_DATA + ",";

                                                            }

                                                            packetsListControl1.addpacketControl(DnsName.Substring(0, Math.Min(DnsName.Length, 25))+"...", Dns_Info.Substring(0, Math.Min(Dns_Info.Length, 30)), false, packetIds.Count, packet, PacketControl_Click);



                                                        }));

                                                    }



                                                }
                                                else
                                                {

                                                    List<DNSControl> dNSControls = packetIds.Where(x => (x.Id == DNS.ID) && (!x.Request)).ToList();

                                                    if (!dNSControls.Any())
                                                    {
                                                        DNSControl dNSControl = new DNSControl();
                                                        dNSControl.Id = DNS.ID;
                                                        dNSControl.Request = false;
                                                        packetIds.Add(dNSControl);

                                                        SafeInvoke(this.packetsListControl1, new Action(() =>
                                                        {



                                                            string DnsName = string.Empty;
                                                            string Dns_Info = string.Empty;
                                                            foreach (DNS_INFO kvp in DNS.DNS_INFO)
                                                            {

                                                                DnsName += kvp.DNS_NAME + ",";
                                                                Dns_Info += kvp.DNS_DATA + ",";

                                                            }

                                                            packetsListControl1.addpacketControl(DnsName.Substring(0, Math.Min(DnsName.Length, 25))+"...", Dns_Info.Substring(0, Math.Min(Dns_Info.Length, 30)), true, packetIds.Count, packet, PacketControl_Click);



                                                        }));

                                                    }


                                                }
                                            }
                                        }
                                    }


                                }
                            }


                            else if (ipPacket.Version == IPVersion.IPv6)
                            {

                                ipPacket = packet.Extract<PacketDotNet.IPv6Packet>();

                                if (this.ActiveDevice.IPV6Adresse.Any())
                                {
                                    if (this.ActiveDevice.IPV6Adresse.Contains(ipPacket.SourceAddress.ToString()) || this.ActiveDevice.IPV6Adresse.Contains(ipPacket.DestinationAddress.ToString()))
                                    {


                                        if (ipPacket.Protocol == ProtocolType.Udp)
                                        {



                                            var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();

                                            if (udpPacket.SourcePort == 53 || udpPacket.DestinationPort == 53)
                                            {

                                                //DNS _dNS = new DNS(udpPacket.PayloadData);
                                                DNSPacket DNS = new DNSPacket(udpPacket.PayloadData, "DNS");

                                                if (DNS.OP_Code == DNSPacket.OP_Code_Type.Standard_Query)
                                                {
                                                    if (DNS.QR == DNSPacket.QR_Type.request)
                                                    {
                                                        List<DNSControl> dNSControls = packetIds.Where(x => (x.Id == DNS.ID) && (!x.Request)).ToList();


                                                        if (!dNSControls.Any())
                                                        {
                                                            DNSControl dNSControl = new DNSControl();
                                                            dNSControl.Id = DNS.ID;
                                                            dNSControl.Request = false;
                                                            packetIds.Add(dNSControl);

                                                            SafeInvoke(this.packetsListControl1, new Action(() =>
                                                            {



                                                                string DnsName = string.Empty;
                                                                string Dns_Info = string.Empty;
                                                                foreach (DNS_INFO kvp in DNS.DNS_INFO)
                                                                {

                                                                    DnsName += kvp.DNS_NAME + ",";
                                                                    Dns_Info += kvp.DNS_DATA + ",";

                                                                }

                                                                packetsListControl1.addpacketControl(DnsName.Substring(0, Math.Min(DnsName.Length, 25))+"...", Dns_Info.Substring(0, Math.Min(Dns_Info.Length, 30)), false, packetIds.Count, packet, PacketControl_Click);



                                                            }));

                                                        }

                                                    }
                                                    else
                                                    {

                                                        List<DNSControl> dNSControls = packetIds.Where(x => (x.Id == DNS.ID) && (!x.Request)).ToList();

                                                        if (!dNSControls.Any())
                                                        {
                                                            DNSControl dNSControl = new DNSControl();
                                                            dNSControl.Id = DNS.ID;
                                                            dNSControl.Request = false;
                                                            packetIds.Add(dNSControl);

                                                            SafeInvoke(this.packetsListControl1, new Action(() =>
                                                            {



                                                                string DnsName = string.Empty;
                                                                string Dns_Info = string.Empty;
                                                                foreach (DNS_INFO kvp in DNS.DNS_INFO)
                                                                {

                                                                    DnsName += kvp.DNS_NAME + ",";
                                                                    Dns_Info += kvp.DNS_DATA + ",";

                                                                }

                                                                packetsListControl1.addpacketControl(DnsName.Substring(0, Math.Min(DnsName.Length, 25))+"...", Dns_Info.Substring(0, Math.Min(Dns_Info.Length, 30)), true, packetIds.Count, packet, PacketControl_Click);



                                                            }));

                                                        }


                                                    }
                                                }
                                            }

                                        }
                                    }

                                }

                            }
                        }
                    }

                }
            }


        }

        private void PacketControl_Click(object sender, EventArgs e)
        {

            Button PacketControl = (Button)sender;

            Packet packet = (Packet)PacketControl.Tag;

            SafeInvoke(analyse_textBox, new Action(() => { analyse_textBox.Text = (packet.PrintHex() + Environment.NewLine); }));
            SafeInvoke(analyse_treeView, new Action(() => { getPacketInfoINTreeView(packet); }));

        }

        void getPacketInfoINTreeView(Packet packet)
        {

            this.analyse_treeView.Nodes.Clear();
            this.analyse_treeView.Nodes.Add("Ethernet", "Ethernet");
            var ethernetPacket = (PacketDotNet.EthernetPacket)packet;
            this.analyse_treeView.Nodes["Ethernet"].Nodes.Add("SHA", "Source Hardware Address");
            this.analyse_treeView.Nodes["Ethernet"].Nodes["SHA"].Nodes.Add(ethernetPacket.SourceHardwareAddress.ToString(), ethernetPacket.SourceHardwareAddress.ToString());
            this.analyse_treeView.Nodes["Ethernet"].Nodes.Add("DHA", "Destination Hardware Address");
            this.analyse_treeView.Nodes["Ethernet"].Nodes["DHA"].Nodes.Add(ethernetPacket.DestinationHardwareAddress.ToString(), ethernetPacket.DestinationHardwareAddress.ToString());
            this.analyse_treeView.Nodes["Ethernet"].Nodes.Add("IP-Payload", "IP-Payload");
            this.analyse_treeView.Nodes.Add("Info", "Important Info");
            if (ethernetPacket.Type == PacketDotNet.EthernetType.IPv4 || ethernetPacket.Type == PacketDotNet.EthernetType.IPv6)
            {
                var ipPacket = packet.Extract<PacketDotNet.IPPacket>();


                if (ipPacket.Version == IPVersion.IPv4)
                {

                   var ipv4Packet = packet.Extract<PacketDotNet.IPv4Packet>();

                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes.Add("IPV4","IPV4");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("TOS", "Type OF Service");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["TOS"].Nodes.Add(ipv4Packet.TypeOfService.ToString(), ipv4Packet.TypeOfService.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("TL", "Total Length");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["TL"].Nodes.Add(ipv4Packet.TotalLength.ToString(), ipv4Packet.TotalLength.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("ID", "Identification");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["ID"].Nodes.Add(ipv4Packet.Id.ToString(), ipv4Packet.Id.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("Flags", "Flags");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["Flags"].Nodes.Add(ipv4Packet.FragmentFlags.ToString(), ipv4Packet.FragmentFlags.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("FO", "Fragment Offset");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["FO"].Nodes.Add(ipv4Packet.FragmentOffset.ToString(), ipv4Packet.FragmentOffset.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("TTL", "Time To Live");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["TTL"].Nodes.Add(ipv4Packet.TimeToLive.ToString(), ipv4Packet.TimeToLive.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("Protocol", "Protocol");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["Protocol"].Nodes.Add(ipv4Packet.Protocol.ToString(), ipv4Packet.Protocol.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("Check_Sum", "Check Sum");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["Check_Sum"].Nodes.Add(ipv4Packet.Checksum.ToString(), ipv4Packet.Checksum.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("Source_Addresse", "Source Addresse") ;
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["Source_Addresse"].Nodes.Add(ipv4Packet.SourceAddress.ToString(),ipv4Packet.SourceAddress.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("Destination_Addresse", "Destination Addresse");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["Destination_Addresse"].Nodes.Add(ipv4Packet.DestinationAddress.ToString(), ipv4Packet.DestinationAddress.ToString());

                    if (ipPacket.Protocol == ProtocolType.Udp)
                    {

                        var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes.Add("UDP", "UDP");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes.Add("Source_Port", "Source Port");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["Source_Port"].Nodes.Add(udpPacket.SourcePort.ToString(), udpPacket.SourcePort.ToString());
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes.Add("Destination_Port", "Destionation Port");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["Destination_Port"].Nodes.Add(udpPacket.DestinationPort.ToString(), udpPacket.DestinationPort.ToString());
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes.Add("Length", "Lenfth");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["length"].Nodes.Add(udpPacket.Length.ToString(), udpPacket.Length.ToString());
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes.Add("Check_Sum", "Check Sum");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["Check_Sum"].Nodes.Add(udpPacket.Checksum.ToString(), udpPacket.Checksum.ToString());

                        if (udpPacket.SourcePort == 53 || udpPacket.DestinationPort == 53)
                        {

                            var DNSPacket = new DNSPacket(udpPacket.PayloadData,"DNS");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes.Add("DNS", "DNS");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("ID","Identification");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["ID"].Nodes.Add(DNSPacket.ID,DNSPacket.ID);
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("QR", "QR");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["QR"].Nodes.Add(DNSPacket.QR.ToString(), DNSPacket.QR.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("OP_Code", "OP Code");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["OP_Code"].Nodes.Add(DNSPacket.OP_Code.ToString(), DNSPacket.OP_Code.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("AA", "Authoritative Answer");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["AA"].Nodes.Add(DNSPacket.Authoritative_Answer.ToString(), DNSPacket.Authoritative_Answer.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("TC", "Truncation");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["TC"].Nodes.Add(DNSPacket.Truncation.ToString(), DNSPacket.Truncation.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("RD", "Recursion Desired");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["RD"].Nodes.Add(DNSPacket.Recursion_Desired.ToString(), DNSPacket.Recursion_Desired.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("RA", "Recursion Available");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["RA"].Nodes.Add(DNSPacket.Recursion_Available.ToString(), DNSPacket.Recursion_Available.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("Z", "Zero");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["Z"].Nodes.Add(DNSPacket.Zero.ToString(), DNSPacket.Zero.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("RCode", "Response Code");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["RCode"].Nodes.Add(DNSPacket.Response_Code.ToString(), DNSPacket.Response_Code.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("NOQ", "Number of Questions");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["NOQ"].Nodes.Add(DNSPacket.Number_of_Questions.ToString(), DNSPacket.Number_of_Questions.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("NOAn", "Number of Answer");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["NOAn"].Nodes.Add(DNSPacket.Number_of_Answer.ToString(), DNSPacket.Number_of_Answer.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("NOAu", "Number of Authority");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["NOAu"].Nodes.Add(DNSPacket.Number_of_Authority.ToString(), DNSPacket.Number_of_Authority.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("NOAd", "Number of Additional");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["NOAd"].Nodes.Add(DNSPacket.Number_of_Additional.ToString(), DNSPacket.Number_of_Additional.ToString());

                            for (int i =0;i< DNSPacket.DNS_INFO.Count;i++)
                            {

                                this.analyse_treeView.Nodes["Info"].Nodes.Add(i.ToString(),i.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Class","Class");
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["Class"].Nodes.Add(DNSPacket.DNS_INFO[i].DNS_CLASS.ToString(), DNSPacket.DNS_INFO[i].DNS_CLASS.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Type", "Type");
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["Type"].Nodes.Add(DNSPacket.DNS_INFO[i].DNS_TYPE.ToString(), DNSPacket.DNS_INFO[i].DNS_TYPE.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("RRs_Type", "RRs Type");
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["RRs_Type"].Nodes.Add(DNSPacket.DNS_INFO[i].RRs_Type.ToString(), DNSPacket.DNS_INFO[i].RRs_Type.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("DNS_NAME", "DNS NAME");
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["DNS_NAME"].Nodes.Add(DNSPacket.DNS_INFO[i].DNS_NAME.ToString(), DNSPacket.DNS_INFO[i].DNS_NAME.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("DNS_Data", "DNS Data");
                                if(DNSPacket.DNS_INFO[i].DNS_DATA!=null)
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["DNS_Data"].Nodes.Add(DNSPacket.DNS_INFO[i].DNS_DATA.ToString(), DNSPacket.DNS_INFO[i].DNS_DATA.ToString());

                            }

                        }

                    }


                }

                if (ipPacket.Version == IPVersion.IPv6)
                {

                   var ipv6Packet = packet.Extract<PacketDotNet.IPv6Packet>();
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes.Add("IPV6", "IPV6");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes.Add("Tc", "Traffic Class");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["TC"].Nodes.Add(ipv6Packet.TrafficClass.ToString(), ipv6Packet.TrafficClass.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes.Add("FL", "Flow Label");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["FL"].Nodes.Add(ipv6Packet.FlowLabel.ToString(), ipv6Packet.FlowLabel.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes.Add("PL", "Payload Length");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["PL"].Nodes.Add(ipv6Packet.PayloadLength.ToString(), ipv6Packet.PayloadLength.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes.Add("NH", "Next Header");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["NH"].Nodes.Add(ipv6Packet.NextHeader.ToString(), ipv6Packet.NextHeader.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes.Add("HL", "Hope Limit");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["HL"].Nodes.Add(ipv6Packet.HopLimit.ToString(), ipv6Packet.HopLimit.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes.Add("SA", "Source Addresse");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["SA"].Nodes.Add(ipv6Packet.SourceAddress.ToString(), ipv6Packet.SourceAddress.ToString());
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes.Add("DA", "Destination Addresse");
                    this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["DA"].Nodes.Add(ipv6Packet.DestinationAddress.ToString(), ipv6Packet.DestinationAddress.ToString());

                    if (ipPacket.Protocol == ProtocolType.Udp)
                    {

                        var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes.Add("UDP", "UDP");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["UDP"].Nodes.Add("Source_Port", "Source Port");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["UDP"].Nodes["Source_Port"].Nodes.Add(udpPacket.SourcePort.ToString(), udpPacket.SourcePort.ToString());
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["UDP"].Nodes.Add("Destination_Port", "Destionation Port");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["UDP"].Nodes["Destination_Port"].Nodes.Add(udpPacket.DestinationPort.ToString(), udpPacket.DestinationPort.ToString());
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["UDP"].Nodes.Add("Length", "Lenfth");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["UDP"].Nodes["length"].Nodes.Add(udpPacket.Length.ToString(), udpPacket.Length.ToString());
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["UDP"].Nodes.Add("Check_Sum", "Check Sum");
                        this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV6"].Nodes["UDP"].Nodes["Check_Sum"].Nodes.Add(udpPacket.Checksum.ToString(), udpPacket.Checksum.ToString());

                        if (udpPacket.SourcePort == 53 || udpPacket.DestinationPort == 53)
                        {

                            var DNSPacket = new DNSPacket(udpPacket.PayloadData, "DNS");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes.Add("DNS", "DNS");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("ID", "Identification");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["ID"].Nodes.Add(DNSPacket.ID, DNSPacket.ID);
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("QR", "QR");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["QR"].Nodes.Add(DNSPacket.QR.ToString(), DNSPacket.QR.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("OP_Code", "OP Code");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["OP_Code"].Nodes.Add(DNSPacket.OP_Code.ToString(), DNSPacket.OP_Code.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("AA", "Authoritative Answer");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["AA"].Nodes.Add(DNSPacket.Authoritative_Answer.ToString(), DNSPacket.Authoritative_Answer.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("TC", "Truncation");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["TC"].Nodes.Add(DNSPacket.Truncation.ToString(), DNSPacket.Truncation.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("RD", "Recursion Desired");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["RD"].Nodes.Add(DNSPacket.Recursion_Desired.ToString(), DNSPacket.Recursion_Desired.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("RA", "Recursion Available");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["RA"].Nodes.Add(DNSPacket.Recursion_Available.ToString(), DNSPacket.Recursion_Available.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("Z", "Zero");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["Zero"].Nodes.Add(DNSPacket.Zero.ToString(), DNSPacket.Zero.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("RCode", "Response Code");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["RCode"].Nodes.Add(DNSPacket.Response_Code.ToString(), DNSPacket.Response_Code.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("NOQ", "Number of Questions");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["NOQ"].Nodes.Add(DNSPacket.Number_of_Questions.ToString(), DNSPacket.Number_of_Questions.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("NOAn", "Number of Answer");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["NOAn"].Nodes.Add(DNSPacket.Number_of_Answer.ToString(), DNSPacket.Number_of_Answer.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("NOAu", "Number of Authority");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["NOAu"].Nodes.Add(DNSPacket.Number_of_Authority.ToString(), DNSPacket.Number_of_Authority.ToString());
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes.Add("NOAd", "Number of Additional");
                            this.analyse_treeView.Nodes["Ethernet"].Nodes["IP-Payload"].Nodes["IPV4"].Nodes["UDP"].Nodes["DNS"].Nodes["NOAd"].Nodes.Add(DNSPacket.Number_of_Additional.ToString(), DNSPacket.Number_of_Additional.ToString());

                            for (int i = 0; i < DNSPacket.DNS_INFO.Count; i++)
                            {

                                this.analyse_treeView.Nodes["Info"].Nodes.Add(i.ToString(), i.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Class", "Class");
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["Class"].Nodes.Add(DNSPacket.DNS_INFO[i].DNS_CLASS.ToString(), DNSPacket.DNS_INFO[i].DNS_CLASS.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Type", "Type");
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["Type"].Nodes.Add(DNSPacket.DNS_INFO[i].DNS_TYPE.ToString(), DNSPacket.DNS_INFO[i].DNS_TYPE.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("RRs_Type", "RRs Type");
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["RRs_Type"].Nodes.Add(DNSPacket.DNS_INFO[i].RRs_Type.ToString(), DNSPacket.DNS_INFO[i].RRs_Type.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("DNS_NAME", "DNS NAME");
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["DNS_NAME"].Nodes.Add(DNSPacket.DNS_INFO[i].DNS_NAME.ToString(), DNSPacket.DNS_INFO[i].DNS_NAME.ToString());
                                this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes.Add("DNS_Data", "DNS Data");
                                if (DNSPacket.DNS_INFO[i].DNS_DATA != null)
                                    this.analyse_treeView.Nodes["Info"].Nodes[i.ToString()].Nodes["DNS_Data"].Nodes.Add(DNSPacket.DNS_INFO[i].DNS_DATA.ToString(), DNSPacket.DNS_INFO[i].DNS_DATA.ToString());
                            }
                        }
     
                    }
                }

            }

        }
        public DeviceSniffer(ActiveDevice device)
        {

            InitializeComponent();
            this.ActiveDevice = device;
            this.form_title.Text = "Watching :" + ActiveDevice.HostName + " , " + ActiveDevice.IPV4Adresse;
            init_Driver_CB();

        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            if (this.Driver != null)
            {
                if (!((bool)this.Start_button.Tag))
                {
                    if (!this.Driver.Opened)
                    {


                        if(Driver_on())
                        {

                            this.Start_button.Text = "Stop";
                            this.Start_button.BackColor = Color.Red;
                            this.Start_button.Tag = true;
                            this.drivers_comboBox.Enabled = false;
                            this.spoofarp_toggle.Enabled = false;

                        }
                       

                    }

                    else
                    {

                        MessageBox.Show("Driver Allready Open!!");

                    }
                }
                else
                {

                    Driver_off();
                    this.Start_button.Text = "Start";
                    this.Start_button.BackColor = Color.LimeGreen;
                    this.Start_button.Tag = false;
                    this.drivers_comboBox.Enabled = true;
                    this.spoofarp_toggle.Enabled = true;



                }
            }


        }


        public void SafeInvoke(Control uiElement, Action updater)
        {
            if (uiElement == null)
            {
                throw new ArgumentNullException("uiElement");
            }

            if (uiElement.InvokeRequired)
            {


                uiElement.BeginInvoke((Action)delegate { SafeInvoke(uiElement, updater); });

            }
            else
            {
                if (uiElement.IsDisposed)
                {
                    throw new ObjectDisposedException("Control is already disposed.");
                }

                updater();
            }
        }

        private void drivers_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            Driver = LibPcapLiveDeviceList.Instance[this.drivers_comboBox.SelectedIndex];

        }

        private void spoofarp_toggle_Click(object sender, EventArgs e)
        {

            if (!((bool)this.spoofarp_toggle.Tag))
            {

                this.spoofarp_toggle.Image = global::NetworkSniffer.Properties.Resources.icons8_schalter_an_40;
                this.spoofarp_toggle.Tag = true;

            }
            else
            {

                this.spoofarp_toggle.Image = global::NetworkSniffer.Properties.Resources.icons8_schalter_aus_40;
                this.spoofarp_toggle.Tag = false;

            }

            
        }
    }
}
