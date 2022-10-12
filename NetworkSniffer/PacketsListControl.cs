using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using SharpPcap;

namespace Network_Sniffer
{
    public partial class PacketsListControl : UserControl
    {
        public PacketsListControl()
        {
            InitializeComponent();
        }

        public void addpacketControl(string largtext,string smalltext,bool up ,int Index,Packet Packet, Action<object, EventArgs> clickEvent)
        {

            PacketControl packet1 = new PacketControl();
            // 
            // packet1
            // 

            packet1.Location = new System.Drawing.Point(3, 3);
            packet1.Name = "packet1" + (Index + 1).ToString(); ;
            packet1.Size = new System.Drawing.Size(416, 71);
            packet1.TabIndex = 0;
            packet1.Dock = System.Windows.Forms.DockStyle.Top;
            packet1.larg_text.Text = largtext;
            packet1.small_text.Text = smalltext;
            packet1.button1.Click += new EventHandler(clickEvent);
            packet1.button1.Tag = Packet;
          if(up)
            {
                packet1.up.Visible = true;
                packet1.down.Visible = false;

            }
            else
            {
                packet1.up.Visible = false;
                packet1.down.Visible = true;
            }

            this.packetlist.Controls.Add(packet1);

        }
    }
}
