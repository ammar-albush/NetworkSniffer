namespace Network_Sniffer
{
    partial class PacketsListControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.packetlist = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // packetlist
            // 
            this.packetlist.AutoScroll = true;
            this.packetlist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.packetlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetlist.Location = new System.Drawing.Point(0, 0);
            this.packetlist.Name = "packetlist";
            this.packetlist.Size = new System.Drawing.Size(796, 216);
            this.packetlist.TabIndex = 0;
            // 
            // PacketsListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.packetlist);
            this.Name = "PacketsListControl";
            this.Size = new System.Drawing.Size(796, 216);
            this.ResumeLayout(false);

        }

        #endregion

        private PacketControl packetcontrol;
        private Panel packetlist;
    }
}
