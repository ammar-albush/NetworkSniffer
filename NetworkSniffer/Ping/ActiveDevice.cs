using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSniffer
{
    public class ActiveDevice
    {

       public string IPV4Adresse { get; set; }
       public List<string> IPV6Adresse { get; set; }
       public string MACAdresse { get; set; }
       public string HostName { get; set; }

        public ActiveDevice(string ipv4,List<string> ipv6,string mac,string host) {
            
            this.IPV4Adresse = ipv4;
            this.IPV6Adresse = ipv6;
            this.MACAdresse = mac;
            this.HostName = host;
        
        }
        public ActiveDevice(string ipv4)
        {

            this.IPV4Adresse = ipv4;
            this.IPV6Adresse = getIPV6Addr(ipv4);
            this.MACAdresse = GetMacAddress(ipv4);
            this.HostName = GetHostName(ipv4);

        }
        public List<string> getIPV6Addr(string ipv4)
        {
            try
            {
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(ipv4);
                IPAddress[] addr = ipEntry.AddressList;
                List<string> foundIPs = new List<string>();
                foreach (IPAddress iPAddress in addr)
                {
                    if (iPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        foundIPs.Add(iPAddress.ToString());

                    }

                }
                return foundIPs;
            }
            catch (Exception ex) { return null; }

            return null;

        }
        public string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipAddress);
                if (entry != null)
                {
                    return entry.HostName;
                }
            }
            catch (SocketException)
            {
                // MessageBox.Show(e.Message.ToString());
            }

            return null;
        }


        //Get MAC address
        public string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            System.Diagnostics.Process Process = new System.Diagnostics.Process();
            Process.StartInfo.FileName = "arp";
            Process.StartInfo.Arguments = "-a " + ipAddress;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.CreateNoWindow = true;
            Process.Start();
            string strOutput = Process.StandardOutput.ReadToEnd();
            string[] substrings = strOutput.Split('-');
            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                         + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                         + "-" + substrings[7] + "-"
                         + substrings[8].Substring(0, 2);
                return macAddress;
            }

            else
            {
                return "OWN Machine";
            }
        }


    }
}
