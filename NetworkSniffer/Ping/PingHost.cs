using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSniffer
{

    public enum PingHostStatus
    {
        Pending,
        Completed,
        InvalidHost,
        Timeout
    }

    public delegate void PingHostCompletedEventHandler(Object sender, PingHostCompletedEventArgs e);

    public class ResolveState
    {
        public ResolveState(string hostName)
        {
            if (string.IsNullOrWhiteSpace(hostName))
                throw new ArgumentNullException(nameof(hostName));
            _hostName = hostName;
        }

        readonly string _hostName;

        public PingHostStatus Result { get; set; } = PingHostStatus.Pending;

        public string HostName => _hostName;

    }
    public class PingHost
    {


        public event PingHostCompletedEventHandler PingCompleted;
        public PingHost()
        {


        }


        public async void SendAsync(string hostNameOrAddress, int millisecond_time_out)
        {
            {
                new Thread(async delegate ()
                {

                    PingHostCompletedEventArgs args = new PingHostCompletedEventArgs();
                    PingHostCompletedEventHandler handler = this.PingCompleted;
                    try
                    {
                        ResolveState ioContext = new ResolveState(hostNameOrAddress);
                        var result = await Dns.GetHostEntryAsync(hostNameOrAddress);

                        if (result == null)
                        {

                            args.Status = PingHostStatus.Timeout;
                            args.IP = null;
                            if (handler != null)
                                handler(this, args);

                        }
                        else
                        {

                            args.Status = PingHostStatus.Completed;
                            args.IP = hostNameOrAddress;
                            if (handler != null)
                                handler(this, args);

                        }
                    }
                    catch (Exception ex)
                    {

                        args.Status = PingHostStatus.Timeout;
                        args.IP = null;
                        if (handler != null)
                            handler(this, args);


                    }


                }).Start();


            }




        }
    }
}

