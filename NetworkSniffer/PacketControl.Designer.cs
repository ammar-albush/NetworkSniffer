namespace Network_Sniffer
{
    partial class PacketControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.down = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.up = new System.Windows.Forms.PictureBox();
            this.small_text = new System.Windows.Forms.Label();
            this.larg_text = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.up)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.down);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.up);
            this.panel1.Controls.Add(this.small_text);
            this.panel1.Controls.Add(this.larg_text);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 71);
            this.panel1.TabIndex = 5;
            // 
            // down
            // 
            this.down.Image = global::NetworkSniffer.Properties.Resources.icons8_herunterladen_32;
            this.down.Location = new System.Drawing.Point(446, 14);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(31, 37);
            this.down.TabIndex = 10;
            this.down.TabStop = false;
            this.down.Visible = false;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::NetworkSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
            this.button1.Location = new System.Drawing.Point(520, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 37);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // up
            // 
            this.up.Image = global::NetworkSniffer.Properties.Resources.icons8_hochladen_32;
            this.up.Location = new System.Drawing.Point(483, 14);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(31, 37);
            this.up.TabIndex = 8;
            this.up.TabStop = false;
            this.up.Visible = false;
            // 
            // small_text
            // 
            this.small_text.AutoSize = true;
            this.small_text.Location = new System.Drawing.Point(80, 39);
            this.small_text.Name = "small_text";
            this.small_text.Size = new System.Drawing.Size(56, 15);
            this.small_text.TabIndex = 7;
            this.small_text.Text = "Text Here";
            // 
            // larg_text
            // 
            this.larg_text.AutoSize = true;
            this.larg_text.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.larg_text.Location = new System.Drawing.Point(64, 14);
            this.larg_text.Name = "larg_text";
            this.larg_text.Size = new System.Drawing.Size(94, 25);
            this.larg_text.TabIndex = 6;
            this.larg_text.Text = "Text Here";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::NetworkSniffer.Properties.Resources.icons8_internet_30;
            this.pictureBox2.Location = new System.Drawing.Point(26, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 37);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // PacketControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PacketControl";
            this.Size = new System.Drawing.Size(585, 71);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.up)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Panel panel1;
        public  PictureBox up;
        public Label small_text;
        public Label larg_text;
        private PictureBox pictureBox2;
        public Button button1;
        public PictureBox down;
    }
}
