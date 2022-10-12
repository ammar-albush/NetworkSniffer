using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkSniffer
{
    public partial class NetworkScanner : Form
    {
        public NetworkScanner()
        {
            InitializeComponent();
            GetLocalIPAddress();
        }

        List<ActiveDevice> FoundDvice = new List<ActiveDevice>();
        string IP
        {
            get
            {

                if (Enter_ip_radioButton.Checked)
                    return Ip_textBox.Text;
                else
                    if (Ip_comboBox.SelectedIndex != -1)
                    return (Ip_comboBox.SelectedItem.ToString());
                else return string.Empty;
            }
        }

        public void GetLocalIPAddress()
        {
            log_textBox.AppendText("Get ALL Local IP Address" + Environment.NewLine);
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {

                    this.Ip_comboBox.Items.Add(ip.ToString());


                }
            }


        }

        private void mask_bits_richTextBox_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = 0;

            int selectionLength = 0;

            for (int i = 0; i < this.mask_bits_richTextBox.Text.Length; i++)
            {

                if (mask_bits_richTextBox.Text[i] == '1')
                {

                    selectionLength++;

                }

            }

            if (!(selectionLength <= 0))
            {

                mask_bits_richTextBox.Select(selectionStart, selectionLength);
                mask_bits_richTextBox.SelectionColor = Color.White;
                mask_bits_richTextBox.SelectionBackColor = Color.Red;


            }

            string subnetmaskstr = string.Empty;

            int len = this.mask_bits_richTextBox.Text.Length / 8;

            int currentpies = 0;
            for (int i = 0; i < len; i++)
            {

                string targetstr = this.mask_bits_richTextBox.Text.Substring(currentpies, 8);

                string result = Convert.ToInt32(targetstr, 2).ToString();

                if (i == len - 1)
                {

                    if (result == string.Empty)
                        subnetmaskstr += "0";
                    else subnetmaskstr += result;

                }

                else
                {

                    if (result == string.Empty)
                        subnetmaskstr += "0.";
                    else subnetmaskstr += result + ".";
                }
                this.mask_textBox.Text = subnetmaskstr;
                currentpies += 8;

            }
        }



        private void mask_bits_trackBar_Scroll(object sender, EventArgs e)
        {


            int trackbarvalue = mask_bits_trackBar.Value;

            int submask = getSubnetMask();

            if (submask > trackbarvalue)
            {
                mask_bits_trackBar.Value = submask;
            }
            else
            {

                label5.Text = @"/ " + trackbarvalue.ToString();

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < trackbarvalue; i++)
                {

                    sb.Append("1");

                }

                int max = mask_bits_trackBar.Maximum;

                for (int i = trackbarvalue; i < max; i++)
                {

                    sb.Append("0");

                }

                mask_bits_richTextBox.Text = sb.ToString();
                if (mask_bits_trackBar.Enabled)
                {
                    //  getStartIPAddresse();


                    StringBuilder sb2 = new StringBuilder();


                    string[] subMaskValues = this.mask_textBox.Text.Split('.');

                    string[] subIpValues = IP.Split('.');


                    for (int i = 0; i < subMaskValues.Length; i++)
                    {
                        if (i == subMaskValues.Length - 1)
                            sb2.Append((int.Parse(subMaskValues[i]) & int.Parse(subIpValues[i])).ToString());
                        else
                        {
                            int maskvalue = int.Parse(subMaskValues[i]);
                            int ipvalue = int.Parse(subIpValues[i]);
                            string str = ((maskvalue & ipvalue).ToString());
                            str += ".";
                            sb2.Append(str);
                        }

                    }
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    net_id_textBox.Text = sb2.ToString();
                    string[] ipaddress;
                    if (Enter_ip_radioButton.Checked)
                    {
                        ipaddress = this.Ip_textBox.Text.Split('.');
                    }
                    else ipaddress = this.Ip_comboBox.SelectedItem.ToString().Split('.');
                    string[] lastipaddress = this.getLastIPAdress().Split('.');
                    int j = ipaddress.Length - 1;
                    for (int i = lastipaddress.Length - 1; i >= 0; i--)
                    {

                        ipaddress[j] = lastipaddress[i];
                        j--;

                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < ipaddress.Length; i++)
                        if (i == ipaddress.Length - 1) stringBuilder.Append(ipaddress[i]);
                        else stringBuilder.Append(ipaddress[i] + ".");

                    this.end_ip_textBox.Text = stringBuilder.ToString();

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    net_id_textBox.Text = sb2.ToString();
                    if (Enter_ip_radioButton.Checked)
                    {
                        ipaddress = this.Ip_textBox.Text.Split('.');
                    }
                    else ipaddress = this.Ip_comboBox.SelectedItem.ToString().Split('.');
                    string[] firstipaddress = this.getFirstIPAdress().Split('.');
                    j = ipaddress.Length - 1;
                    for (int i = firstipaddress.Length - 1; i >= 0; i--)
                    {

                        ipaddress[j] = firstipaddress[i];
                        j--;

                    }
                    stringBuilder = new StringBuilder();
                    for (int i = 0; i < ipaddress.Length; i++)
                        if (i == ipaddress.Length - 1) stringBuilder.Append(ipaddress[i]);
                        else stringBuilder.Append(ipaddress[i] + ".");

                    this.start_ip_textBox.Text = stringBuilder.ToString();
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    this.max_host_textBox.Text = getHostcount().ToString();
                    this.net_id_count_textBox.Text = getNetcount().ToString();

                }

            }

        }

        private void Enter_ip_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Enter_ip_radioButton.Checked)
            {

                this.Ip_textBox.Enabled = true;
                this.Ip_comboBox.Enabled = false;
                this.mask_bits_trackBar.Value = 0;
                if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
                    this.mask_bits_trackBar.Enabled = true;
                else this.mask_bits_trackBar.Enabled = false;
                mask_bits_trackBar_Scroll(sender, e);

            }
            else
            {

                this.Ip_textBox.Enabled = false;
                this.Ip_comboBox.Enabled = true;
                this.mask_bits_trackBar.Value = 0;
                if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
                    this.mask_bits_trackBar.Enabled = true;
                else this.mask_bits_trackBar.Enabled = false;
                mask_bits_trackBar_Scroll(sender, e);

            }
        }

        private void Ip_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
            {
                try
                {

                    IPAddress.Parse(IP);

                    this.mask_bits_trackBar.Enabled = true;
                    mask_bits_trackBar.Value = getSubnetMask();
                    mask_bits_trackBar_Scroll(sender, e);
                    IPClass iPClass = getIPClass();
                    this.ip_class_textBox.Text = iPClass.ToString();
                
                    if (iPClass == IPClass.C)
                    {

                        this.recommend.Text = "Recommended";
                        this.recommend.BackColor = Color.Green;

                    }
                    if (iPClass == IPClass.B)
                    {

                        this.recommend.Text = "Recommended but little Slowe";
                        this.recommend.BackColor = Color.Gold;

                    }

                    if (iPClass == IPClass.A)
                    {

                        this.recommend.Text = "Not Recommended";
                        this.recommend.BackColor = Color.Red;

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }


            }
            else this.mask_bits_trackBar.Enabled = false;
        }

        private void Ip_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
            {
                this.mask_bits_trackBar.Enabled = true;
                mask_bits_trackBar.Value = getSubnetMask();
                mask_bits_trackBar_Scroll(sender, e);
                IPClass iPClass = getIPClass();
                this.ip_class_textBox.Text = iPClass.ToString();
                if (iPClass == IPClass.C)
                {

                    this.recommend.Text = "Recommended";
                    this.recommend.BackColor = Color.Green;

                }
                if (iPClass == IPClass.B)
                {

                    this.recommend.Text = "Recommended but little Slowe";
                    this.recommend.BackColor = Color.Gold;

                }

                if (iPClass == IPClass.A)
                {

                    this.recommend.Text = "Not Recommended";
                    this.recommend.BackColor = Color.Red;

                }
            }
            else this.mask_bits_trackBar.Enabled = false;

        }

        public enum IPClass { A, B, C, D, E, notDetected }

        IPClass getIPClass()
        {

            if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
            {

                string ipclassstr = IP.Split('.').First();

                int ipclasssnum = int.Parse(ipclassstr);

                if (1 <= ipclasssnum && ipclasssnum <= 126)
                {

                    return IPClass.A;

                }


                if (128 <= ipclasssnum && ipclasssnum <= 191)
                {

                    return IPClass.B;

                }


                if (192 <= ipclasssnum && ipclasssnum <= 223)
                {

                    return IPClass.C;

                }


                if (224 <= ipclasssnum && ipclasssnum <= 239)
                {

                    return IPClass.D;

                }
                if (240 <= ipclasssnum && ipclasssnum <= 255)
                {

                    return IPClass.E;

                }




            }
            else return IPClass.notDetected;

            return IPClass.notDetected;
        }

        int getSubnetMask()
        {

            IPClass iPClass = getIPClass();

            if (iPClass == IPClass.A)
                return 8;
            if (iPClass == IPClass.B)
                return 16;
            if (iPClass == IPClass.C)
                return 24;
            if (iPClass == IPClass.D)
                return 31;
            if (iPClass == IPClass.E)
                return 32;

            return -1;


        }

        /// <summary>
        /// get the Net id Count 
        /// Net id  = (2 ^ (currentvaluesubnetmask - submaskint))-2
        /// </summary>
        /// <returns>Net Count </returns>
        int getNetcount()
        {

            int submaskint = getSubnetMask();

            int currentvaluesubnetmask = mask_bits_trackBar.Value;

            int resultbits = Math.Max((int)Math.Pow(2, currentvaluesubnetmask - submaskint), 0);

            return resultbits;
        }

        /// <summary>
        /// get Host count per Net id
        /// (2^( total pit count - currentvaluesubnetmask))-1 
        /// </summary>
        /// <returns>host count</returns>
        int getHostcount()
        {

            int currentvaluesubnetmask = mask_bits_trackBar.Value;

            double resultbits = Math.Max(Math.Pow(2, (32 - (currentvaluesubnetmask))) - 2, 0);

            return (int)resultbits;
        }

        string getLastIPAdress()
        {

            int submaskint = getSubnetMask();

            int currentvaluesubnetmask = mask_bits_trackBar.Value;

            int rest = currentvaluesubnetmask - submaskint;

            string submaskbits = mask_bits_richTextBox.Text;

            int idValue = mask_bits_trackBar.Value;

            int hostValue = 32 - idValue;

            string gethostpart = submaskbits.Substring(idValue, hostValue);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < gethostpart.Length; i++)
            {
                if (i == gethostpart.Length - 1) stringBuilder.Append("0");
                else stringBuilder.Append("1");
            }

            gethostpart = stringBuilder.ToString();

            if (rest != 0)
            {
                gethostpart = gethostpart.PadLeft(rest + hostValue, '0');

            }



            int length = gethostpart.Length / 8;

            string[] ipparts = new string[length];
            int j = 0;
            for (int i = 0; i < length; i++)
            {
                ipparts[i] = gethostpart.Substring(j, 8);
                j = j + 8;
            }

            StringBuilder stringBuilder1 = new StringBuilder();

            for (int i = 0; i < ipparts.Length; i++)
            {
                if (i == ipparts.Length - 1)
                    stringBuilder1.Append((Convert.ToInt64(ipparts[i], 2)).ToString());
                else stringBuilder1.Append(Convert.ToInt64(ipparts[i], 2).ToString() + ".");
            }
            return stringBuilder1.ToString();


        }

        string getFirstIPAdress()
        {

            int submaskint = getSubnetMask();

            int currentvaluesubnetmask = mask_bits_trackBar.Value;

            int rest = currentvaluesubnetmask - submaskint;

            string submaskbits = mask_bits_richTextBox.Text;

            int idValue = mask_bits_trackBar.Value;

            int hostValue = 32 - idValue;

            string gethostpart = submaskbits.Substring(idValue, hostValue);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < gethostpart.Length; i++)
            {
                if (i == gethostpart.Length - 1) stringBuilder.Append("1");
                else stringBuilder.Append("0");
            }
            gethostpart = stringBuilder.ToString();

            if (rest != 0)
            {
                gethostpart = gethostpart.PadLeft(rest + hostValue, '0');

            }



            int length = gethostpart.Length / 8;

            string[] ipparts = new string[length];
            int j = 0;
            for (int i = 0; i < length; i++)
            {
                ipparts[i] = gethostpart.Substring(j, 8);
                j = j + 8;
            }

            StringBuilder stringBuilder1 = new StringBuilder();

            for (int i = 0; i < ipparts.Length; i++)
            {
                if (i == ipparts.Length - 1)
                    stringBuilder1.Append(Convert.ToInt64(ipparts[i], 2).ToString());
                else stringBuilder1.Append(Convert.ToInt64(ipparts[i], 2).ToString() + ".");
            }
            return stringBuilder1.ToString();


        }

        public void Ping_ALL_Hosts(string Gateway)
        {
             //Extracting and pinging all other ip's.
            string[] array = Gateway.Split('.');

            IPClass iPClass = getIPClass();

            if (iPClass == IPClass.D)
                MessageBox.Show("Cannot Ping Multicast Address");

            if(iPClass ==IPClass.E)
                MessageBox.Show("Cannot Ping Address From Class E");



            if (iPClass == IPClass.A)
            {
                SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + 0 + "." + 0 + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                for (int a = 1; a < 255; a++)
                {
                    SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + a + "." + 0 + "." + 0+ " avileable Devices " + Environment.NewLine); }));

                    for (int b = 1; b < 255; b++)
                    {
                        SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + a + "." + b + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                        for (int c = 0; c < 255; c++)
                        {
                            string ping_var = array[0] + "." + a  + "." + b + "." + c;
                            Ping_Host(ping_var, (int)this.timeout_numericUpDown.Value);

                        }
                    }

                }

            }

            if (iPClass == IPClass.B)
            {
                SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + array[1] + "." + 0 + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                for (int a = 1; a < 255; a++)
                {
                    SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + array[1] + "." + a + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                    for (int b = 1; b < 255; b++)
                    {
                        
                            string ping_var = array[0] + "." + array[1] + "." +a + "." + b;
                            Ping_Host(ping_var,(int)this.timeout_numericUpDown.Value);


                    }

                }

            }

            if (iPClass == IPClass.C)
            {
                SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + array[1] + "." + array[2] + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                for (int a = 1; a < 255; a++)
                {
                   
                  string ping_var = array[0] + "." + array[1] + "." + array[2] + "." + a;
                    Ping_Host(ping_var, (int)this.timeout_numericUpDown.Value);


                }

            }

            //time in milliseconds           
            //  Ping_Host(ping_var, 4000);

        }

        public void Ping_Host(string host, int time_out)
        {

            new Thread(delegate ()
            {
                try
                {
                    PingHost ping = new PingHost();
                    ping.PingCompleted += new PingHostCompletedEventHandler(Ping_Completed);
                    ping.SendAsync(host, time_out);
                }
                catch
                {
                    // Do nothing and let it try again until the attempts are exausted.
                    // Exceptions are thrown for normal ping failurs like address lookup
                    // failed.  For this reason we are supressing errors.
                }
            }).Start();

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

        int PingCompletedCount = 0;
        private void Ping_Completed(object sender, PingHostCompletedEventArgs e)
        {

            SafeInvoke(this,
                          new Action(() =>
                          {
                              PingCompletedCount++;
                          }));

            if (e.IP != null)
            {

                string ip = (string)e.IP;
                if (e.Status == PingHostStatus.Completed)
                {
                    ActiveDevice activeDevice = new ActiveDevice(ip, e.Ipv6, e.MAC, e.Host);


                    FoundDvice.Add(activeDevice);

                    SafeInvoke(found_ip_treeView,
                         new Action(() =>
                         {

                             found_ip_treeView.Nodes.Add(e.Host,e.Host,1);
                             found_ip_treeView.Nodes[e.Host].Tag = activeDevice;

                             devices_listView.Items.Add(ip,e.Host,0);
                             devices_listView.Items[ip].Tag = activeDevice;


                             found_ip_treeView.Nodes[e.Host].Nodes.Add(ip.ToString(), ip.ToString(), 0);
                             found_ip_treeView.Nodes[e.Host].Nodes[ip].Tag = activeDevice;

                             found_ip_treeView.Nodes[e.Host].Nodes[ip.ToString()].Nodes.Add(activeDevice.HostName, activeDevice.HostName, 4);
                             found_ip_treeView.Nodes[e.Host].Nodes[ip].Nodes[activeDevice.HostName].Tag = activeDevice;

                             found_ip_treeView.Nodes[e.Host].Nodes[ip.ToString()].Nodes.Add(activeDevice.MACAdresse, activeDevice.MACAdresse, 2);
                             found_ip_treeView.Nodes[e.Host].Nodes[ip].Nodes[activeDevice.MACAdresse].Tag = activeDevice;
                             if (activeDevice.IPV6Adresse != null)
                             { 
                                 foreach (string ip_v6 in activeDevice.IPV6Adresse)
                                 {
                                     found_ip_treeView.Nodes[e.Host].Nodes[ip.ToString()].Nodes.Add(ip_v6, ip_v6, 3);
                                     found_ip_treeView.Nodes[e.Host].Nodes[ip.ToString()].Nodes[ip_v6].Tag = activeDevice;

                                 }
                             }
                         }));
                }

            }
            else
            {
                // MessageBox.Show(e.Reply.Status.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                int netcont = int.Parse(this.net_id_count_textBox.Text);
                int hostcont = int.Parse(this.max_host_textBox.Text);
                SafeInvoke(this,
                       new Action(() =>
                       {

                           if (this.Enter_ip_radioButton.Checked)
                               Ping_ALL_Hosts(this.Ip_textBox.Text);
                           else Ping_ALL_Hosts(this.Ip_comboBox.SelectedItem.ToString());
                       }));
               
                while (true)
                {
                   

                    SafeInvoke(progressBar, new Action(() => { progressBar.Value = ((((PingCompletedCount + 1) * 100) / (hostcont*netcont))); }));
                    if(this.PingCompletedCount== (hostcont * netcont))
                    {
                        SafeInvoke(progressBar, new Action(() => { progressBar.Value =0; }));
                        PingCompletedCount = 0; 
                        break;
                    }

                }
              
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                found_ip_treeView.Nodes.Clear();
                backgroundWorker1.RunWorkerAsync();
            } 
        }

        private void devices_listView_DoubleClick(object sender, EventArgs e)
        {

            ListViewItem listViewItem = devices_listView.SelectedItems[0];

            if (listViewItem != null)
            {

                DeviceSniffer deviceSniffer = new DeviceSniffer((ActiveDevice)listViewItem.Tag);
                deviceSniffer.SubnetMask = IPAddress.Parse(this.mask_textBox.Text).GetAddressBytes();
                deviceSniffer.Show();


            }

        }
    }
   

}
