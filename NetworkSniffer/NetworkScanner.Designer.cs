namespace NetworkSniffer
{
    partial class NetworkScanner
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkScanner));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.devices_listView = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.found_ip_treeView = new System.Windows.Forms.TreeView();
            this.label6 = new System.Windows.Forms.Label();
            this.Start_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.timeout_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.log_textBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ip_class_textBox = new System.Windows.Forms.TextBox();
            this.recommend = new System.Windows.Forms.Label();
            this.start_ip_textBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.end_ip_textBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.net_id_textBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.net_id_count_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.max_host_textBox = new System.Windows.Forms.TextBox();
            this.mask_bits_trackBar = new System.Windows.Forms.TrackBar();
            this.mask_bits_richTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mask_textBox = new System.Windows.Forms.TextBox();
            this.Ip_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Ip_textBox = new System.Windows.Forms.TextBox();
            this.choose_ip_radioButton = new System.Windows.Forms.RadioButton();
            this.Enter_ip_radioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeout_numericUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mask_bits_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 906);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(843, 906);
            this.panel2.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panel3);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.Start_button);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.timeout_numericUpDown);
            this.groupBox5.Location = new System.Drawing.Point(24, 453);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(807, 224);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Process";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.devices_listView);
            this.panel3.Controls.Add(this.found_ip_treeView);
            this.panel3.Location = new System.Drawing.Point(28, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(546, 163);
            this.panel3.TabIndex = 17;
            // 
            // devices_listView
            // 
            this.devices_listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.devices_listView.LargeImageList = this.imageList1;
            this.devices_listView.Location = new System.Drawing.Point(324, 20);
            this.devices_listView.Name = "devices_listView";
            this.devices_listView.Size = new System.Drawing.Size(199, 125);
            this.devices_listView.TabIndex = 1;
            this.devices_listView.UseCompatibleStateImageBehavior = false;
            this.devices_listView.DoubleClick += new System.EventHandler(this.devices_listView_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-wired-netzwerkverbindung-50.png");
            this.imageList1.Images.SetKeyName(1, "icons8-mein-computer-50.png");
            this.imageList1.Images.SetKeyName(2, "icons8-mac-desktop-50.png");
            this.imageList1.Images.SetKeyName(3, "icons8-ipv6-50.png");
            this.imageList1.Images.SetKeyName(4, "icons8-hostname-50.png");
            // 
            // found_ip_treeView
            // 
            this.found_ip_treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.found_ip_treeView.ImageIndex = 0;
            this.found_ip_treeView.ImageList = this.imageList1;
            this.found_ip_treeView.Location = new System.Drawing.Point(14, 20);
            this.found_ip_treeView.Name = "found_ip_treeView";
            this.found_ip_treeView.SelectedImageIndex = 0;
            this.found_ip_treeView.Size = new System.Drawing.Size(304, 125);
            this.found_ip_treeView.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(28, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Found Devices :";
            // 
            // Start_button
            // 
            this.Start_button.BackColor = System.Drawing.Color.LightGreen;
            this.Start_button.FlatAppearance.BorderSize = 0;
            this.Start_button.Location = new System.Drawing.Point(648, 124);
            this.Start_button.Name = "Start_button";
            this.Start_button.Size = new System.Drawing.Size(129, 44);
            this.Start_button.TabIndex = 15;
            this.Start_button.Text = "Start";
            this.Start_button.UseVisualStyleBackColor = false;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(580, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Timeout :";
            // 
            // timeout_numericUpDown
            // 
            this.timeout_numericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timeout_numericUpDown.Location = new System.Drawing.Point(657, 42);
            this.timeout_numericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.timeout_numericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timeout_numericUpDown.Name = "timeout_numericUpDown";
            this.timeout_numericUpDown.Size = new System.Drawing.Size(120, 23);
            this.timeout_numericUpDown.TabIndex = 0;
            this.timeout_numericUpDown.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.progressBar);
            this.groupBox4.Controls.Add(this.log_textBox);
            this.groupBox4.Location = new System.Drawing.Point(24, 683);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(807, 211);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Log";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(28, 31);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(742, 41);
            this.progressBar.TabIndex = 1;
            // 
            // log_textBox
            // 
            this.log_textBox.Location = new System.Drawing.Point(28, 79);
            this.log_textBox.Multiline = true;
            this.log_textBox.Name = "log_textBox";
            this.log_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.log_textBox.Size = new System.Drawing.Size(742, 122);
            this.log_textBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.ip_class_textBox);
            this.groupBox1.Controls.Add(this.recommend);
            this.groupBox1.Controls.Add(this.start_ip_textBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.end_ip_textBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.net_id_textBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.net_id_count_textBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.max_host_textBox);
            this.groupBox1.Controls.Add(this.mask_bits_trackBar);
            this.groupBox1.Controls.Add(this.mask_bits_richTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.mask_textBox);
            this.groupBox1.Controls.Add(this.Ip_comboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Ip_textBox);
            this.groupBox1.Controls.Add(this.choose_ip_radioButton);
            this.groupBox1.Controls.Add(this.Enter_ip_radioButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(24, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(807, 403);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Net ID :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(14, 363);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 20);
            this.label13.TabIndex = 39;
            this.label13.Text = "IP CLass :";
            // 
            // ip_class_textBox
            // 
            this.ip_class_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_class_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ip_class_textBox.Location = new System.Drawing.Point(152, 362);
            this.ip_class_textBox.Name = "ip_class_textBox";
            this.ip_class_textBox.ReadOnly = true;
            this.ip_class_textBox.Size = new System.Drawing.Size(232, 27);
            this.ip_class_textBox.TabIndex = 38;
            this.ip_class_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // recommend
            // 
            this.recommend.AutoSize = true;
            this.recommend.BackColor = System.Drawing.Color.White;
            this.recommend.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.recommend.ForeColor = System.Drawing.Color.White;
            this.recommend.Location = new System.Drawing.Point(482, 363);
            this.recommend.Name = "recommend";
            this.recommend.Size = new System.Drawing.Size(0, 20);
            this.recommend.TabIndex = 37;
            // 
            // start_ip_textBox
            // 
            this.start_ip_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.start_ip_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.start_ip_textBox.Location = new System.Drawing.Point(538, 268);
            this.start_ip_textBox.Name = "start_ip_textBox";
            this.start_ip_textBox.ReadOnly = true;
            this.start_ip_textBox.Size = new System.Drawing.Size(232, 27);
            this.start_ip_textBox.TabIndex = 36;
            this.start_ip_textBox.Text = "0.0.0.0";
            this.start_ip_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(400, 318);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "End IP : ";
            // 
            // end_ip_textBox
            // 
            this.end_ip_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.end_ip_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.end_ip_textBox.Location = new System.Drawing.Point(538, 317);
            this.end_ip_textBox.Name = "end_ip_textBox";
            this.end_ip_textBox.ReadOnly = true;
            this.end_ip_textBox.Size = new System.Drawing.Size(232, 27);
            this.end_ip_textBox.TabIndex = 34;
            this.end_ip_textBox.Text = "0.0.0.0";
            this.end_ip_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(400, 268);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 20);
            this.label10.TabIndex = 33;
            this.label10.Text = "Start IP  :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(400, 222);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 20);
            this.label11.TabIndex = 31;
            this.label11.Text = "Net ID :";
            // 
            // net_id_textBox
            // 
            this.net_id_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.net_id_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.net_id_textBox.Location = new System.Drawing.Point(538, 222);
            this.net_id_textBox.Name = "net_id_textBox";
            this.net_id_textBox.ReadOnly = true;
            this.net_id_textBox.Size = new System.Drawing.Size(232, 27);
            this.net_id_textBox.TabIndex = 30;
            this.net_id_textBox.Text = "0.0.0.0";
            this.net_id_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(14, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 20);
            this.label8.TabIndex = 29;
            this.label8.Text = "Max ID Count :";
            // 
            // net_id_count_textBox
            // 
            this.net_id_count_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.net_id_count_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.net_id_count_textBox.Location = new System.Drawing.Point(152, 314);
            this.net_id_count_textBox.Name = "net_id_count_textBox";
            this.net_id_count_textBox.ReadOnly = true;
            this.net_id_count_textBox.Size = new System.Drawing.Size(232, 27);
            this.net_id_count_textBox.TabIndex = 28;
            this.net_id_count_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(14, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 20);
            this.label7.TabIndex = 27;
            this.label7.Text = "Max Hosts Count  :";
            // 
            // max_host_textBox
            // 
            this.max_host_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.max_host_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.max_host_textBox.Location = new System.Drawing.Point(152, 264);
            this.max_host_textBox.Name = "max_host_textBox";
            this.max_host_textBox.ReadOnly = true;
            this.max_host_textBox.Size = new System.Drawing.Size(232, 27);
            this.max_host_textBox.TabIndex = 26;
            this.max_host_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mask_bits_trackBar
            // 
            this.mask_bits_trackBar.Enabled = false;
            this.mask_bits_trackBar.LargeChange = 1;
            this.mask_bits_trackBar.Location = new System.Drawing.Point(12, 174);
            this.mask_bits_trackBar.Maximum = 32;
            this.mask_bits_trackBar.Name = "mask_bits_trackBar";
            this.mask_bits_trackBar.Size = new System.Drawing.Size(636, 45);
            this.mask_bits_trackBar.TabIndex = 21;
            this.mask_bits_trackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.mask_bits_trackBar.Scroll += new System.EventHandler(this.mask_bits_trackBar_Scroll);
            // 
            // mask_bits_richTextBox
            // 
            this.mask_bits_richTextBox.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mask_bits_richTextBox.Location = new System.Drawing.Point(33, 118);
            this.mask_bits_richTextBox.Name = "mask_bits_richTextBox";
            this.mask_bits_richTextBox.ReadOnly = true;
            this.mask_bits_richTextBox.Size = new System.Drawing.Size(615, 50);
            this.mask_bits_richTextBox.TabIndex = 20;
            this.mask_bits_richTextBox.Text = "00000000000000000000000000000000";
            this.mask_bits_richTextBox.TextChanged += new System.EventHandler(this.mask_bits_richTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(14, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Subnet Mask  :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(664, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 31);
            this.label5.TabIndex = 22;
            this.label5.Text = "/ 0";
            // 
            // mask_textBox
            // 
            this.mask_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mask_textBox.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mask_textBox.Location = new System.Drawing.Point(152, 219);
            this.mask_textBox.Name = "mask_textBox";
            this.mask_textBox.ReadOnly = true;
            this.mask_textBox.Size = new System.Drawing.Size(232, 27);
            this.mask_textBox.TabIndex = 23;
            this.mask_textBox.Text = "0.0.0.0";
            this.mask_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Ip_comboBox
            // 
            this.Ip_comboBox.Enabled = false;
            this.Ip_comboBox.FormattingEnabled = true;
            this.Ip_comboBox.Location = new System.Drawing.Point(214, 63);
            this.Ip_comboBox.Name = "Ip_comboBox";
            this.Ip_comboBox.Size = new System.Drawing.Size(232, 23);
            this.Ip_comboBox.TabIndex = 5;
            this.Ip_comboBox.SelectedIndexChanged += new System.EventHandler(this.Ip_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(140, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP  :";
            // 
            // Ip_textBox
            // 
            this.Ip_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ip_textBox.Location = new System.Drawing.Point(214, 31);
            this.Ip_textBox.Name = "Ip_textBox";
            this.Ip_textBox.Size = new System.Drawing.Size(232, 23);
            this.Ip_textBox.TabIndex = 1;
            this.Ip_textBox.TextChanged += new System.EventHandler(this.Ip_textBox_TextChanged);
            // 
            // choose_ip_radioButton
            // 
            this.choose_ip_radioButton.AutoSize = true;
            this.choose_ip_radioButton.Location = new System.Drawing.Point(12, 65);
            this.choose_ip_radioButton.Name = "choose_ip_radioButton";
            this.choose_ip_radioButton.Size = new System.Drawing.Size(78, 19);
            this.choose_ip_radioButton.TabIndex = 2;
            this.choose_ip_radioButton.TabStop = true;
            this.choose_ip_radioButton.Text = "Choose IP";
            this.choose_ip_radioButton.UseVisualStyleBackColor = true;
            // 
            // Enter_ip_radioButton
            // 
            this.Enter_ip_radioButton.AutoSize = true;
            this.Enter_ip_radioButton.Checked = true;
            this.Enter_ip_radioButton.Location = new System.Drawing.Point(12, 31);
            this.Enter_ip_radioButton.Name = "Enter_ip_radioButton";
            this.Enter_ip_radioButton.Size = new System.Drawing.Size(65, 19);
            this.Enter_ip_radioButton.TabIndex = 3;
            this.Enter_ip_radioButton.TabStop = true;
            this.Enter_ip_radioButton.Text = "Enter IP";
            this.Enter_ip_radioButton.UseVisualStyleBackColor = true;
            this.Enter_ip_radioButton.CheckedChanged += new System.EventHandler(this.Enter_ip_radioButton_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(140, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP :";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // NetworkScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 906);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NetworkScanner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Scanner";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeout_numericUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mask_bits_trackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private GroupBox groupBox5;
        private Label label6;
        private Button Start_button;
        private Label label4;
        private TreeView found_ip_treeView;
        private NumericUpDown timeout_numericUpDown;
        private GroupBox groupBox4;
        private ProgressBar progressBar;
        private TextBox log_textBox;
        private GroupBox groupBox1;
        private Label label8;
        private TextBox net_id_count_textBox;
        private Label label7;
        private TextBox max_host_textBox;
        private TrackBar mask_bits_trackBar;
        private RichTextBox mask_bits_richTextBox;
        private Label label3;
        private Label label5;
        private TextBox mask_textBox;
        private ComboBox Ip_comboBox;
        private Label label1;
        private TextBox Ip_textBox;
        private RadioButton choose_ip_radioButton;
        private RadioButton Enter_ip_radioButton;
        private Label label2;
        private Label label9;
        private TextBox end_ip_textBox;
        private Label label10;
        private Label label11;
        private TextBox net_id_textBox;
        private TextBox start_ip_textBox;
        private Label recommend;
        private Label label13;
        private TextBox ip_class_textBox;
        private ImageList imageList1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panel3;
        private ListView devices_listView;
    }
}