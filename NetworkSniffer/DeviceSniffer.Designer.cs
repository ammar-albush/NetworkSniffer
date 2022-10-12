namespace NetworkSniffer
{
    partial class DeviceSniffer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.form_title = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.analyse_treeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.analyse_textBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.spoofarp_toggle = new System.Windows.Forms.Button();
            this.Start_button = new System.Windows.Forms.Button();
            this.drivers_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.packetsListControl1 = new Network_Sniffer.PacketsListControl();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.form_title);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 646);
            this.panel1.TabIndex = 0;
            // 
            // form_title
            // 
            this.form_title.BackColor = System.Drawing.Color.White;
            this.form_title.FlatAppearance.BorderSize = 0;
            this.form_title.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.form_title.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.form_title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form_title.Location = new System.Drawing.Point(12, 11);
            this.form_title.Name = "form_title";
            this.form_title.Size = new System.Drawing.Size(834, 40);
            this.form_title.TabIndex = 4;
            this.form_title.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.analyse_treeView);
            this.groupBox2.Controls.Add(this.analyse_textBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 359);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(834, 273);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Packet Analyse";
            // 
            // analyse_treeView
            // 
            this.analyse_treeView.ImageIndex = 0;
            this.analyse_treeView.ImageList = this.imageList1;
            this.analyse_treeView.Location = new System.Drawing.Point(6, 22);
            this.analyse_treeView.Name = "analyse_treeView";
            this.analyse_treeView.SelectedImageIndex = 0;
            this.analyse_treeView.Size = new System.Drawing.Size(376, 223);
            this.analyse_treeView.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // analyse_textBox
            // 
            this.analyse_textBox.Location = new System.Drawing.Point(388, 22);
            this.analyse_textBox.Multiline = true;
            this.analyse_textBox.Name = "analyse_textBox";
            this.analyse_textBox.ReadOnly = true;
            this.analyse_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.analyse_textBox.Size = new System.Drawing.Size(427, 223);
            this.analyse_textBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.spoofarp_toggle);
            this.groupBox1.Controls.Add(this.Start_button);
            this.groupBox1.Controls.Add(this.drivers_comboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.packetsListControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(834, 283);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet Traffic";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = " Arp Spoofing";
            // 
            // spoofarp_toggle
            // 
            this.spoofarp_toggle.FlatAppearance.BorderSize = 0;
            this.spoofarp_toggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.spoofarp_toggle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.spoofarp_toggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.spoofarp_toggle.Image = global::NetworkSniffer.Properties.Resources.icons8_schalter_aus_40;
            this.spoofarp_toggle.Location = new System.Drawing.Point(108, 119);
            this.spoofarp_toggle.Name = "spoofarp_toggle";
            this.spoofarp_toggle.Size = new System.Drawing.Size(75, 30);
            this.spoofarp_toggle.TabIndex = 7;
            this.spoofarp_toggle.Tag = false;
            this.spoofarp_toggle.UseVisualStyleBackColor = true;
            this.spoofarp_toggle.Click += new System.EventHandler(this.spoofarp_toggle_Click);
            // 
            // Start_button
            // 
            this.Start_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.Start_button.Location = new System.Drawing.Point(94, 187);
            this.Start_button.Name = "Start_button";
            this.Start_button.Size = new System.Drawing.Size(89, 33);
            this.Start_button.TabIndex = 6;
            this.Start_button.Tag = false;
            this.Start_button.Text = "Start";
            this.Start_button.UseVisualStyleBackColor = false;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // drivers_comboBox
            // 
            this.drivers_comboBox.FormattingEnabled = true;
            this.drivers_comboBox.Location = new System.Drawing.Point(13, 77);
            this.drivers_comboBox.Name = "drivers_comboBox";
            this.drivers_comboBox.Size = new System.Drawing.Size(170, 23);
            this.drivers_comboBox.TabIndex = 5;
            this.drivers_comboBox.SelectedIndexChanged += new System.EventHandler(this.drivers_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select Driver :";
            // 
            // packetsListControl1
            // 
            this.packetsListControl1.Location = new System.Drawing.Point(202, 38);
            this.packetsListControl1.Name = "packetsListControl1";
            this.packetsListControl1.Size = new System.Drawing.Size(613, 216);
            this.packetsListControl1.TabIndex = 0;
            // 
            // DeviceSniffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 646);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceSniffer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeviceSniffer";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox2;
        private TreeView analyse_treeView;
        private TextBox analyse_textBox;
        private GroupBox groupBox1;
        private Network_Sniffer.PacketsListControl packetsListControl1;
        private Button form_title;
        private ImageList imageList1;
        private Button Start_button;
        private ComboBox drivers_comboBox;
        private Label label1;
        private Label label2;
        private Button spoofarp_toggle;
    }
}