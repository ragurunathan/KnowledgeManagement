namespace KnowledgeMatrix.Forms
{
    partial class QuestionPaper
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestionPaper));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new CodeVendor.Controls.Grouper();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new CodeVendor.Controls.Grouper();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new CodeVendor.Controls.Grouper();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.questionPaperBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.questionPaperBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.questionPaperBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionPaperBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 550);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(259, 495);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "red.jpg");
            this.imageList1.Images.SetKeyName(1, "yellow.png");
            this.imageList1.Images.SetKeyName(2, "green.jpg");
            this.imageList1.Images.SetKeyName(3, "black.png");
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 469);
            this.panel1.TabIndex = 9;
            this.panel1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(736, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.BackColor = System.Drawing.Color.DarkGray;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(110, 22);
            this.toolStripButton1.Text = "Manage Questions";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(147, 22);
            this.toolStripButton2.Text = "Archived Question Papers";
            // 
            // groupBox3
            // 
            this.groupBox3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.groupBox3.BackgroundGradientColor = System.Drawing.Color.White;
            this.groupBox3.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.groupBox3.BorderColor = System.Drawing.Color.Black;
            this.groupBox3.BorderThickness = 1F;
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox3.GroupImage = null;
            this.groupBox3.GroupTitle = "";
            this.groupBox3.Location = new System.Drawing.Point(4, 435);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox3.PaintGroupBox = false;
            this.groupBox3.RoundCorners = 10;
            this.groupBox3.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox3.ShadowControl = false;
            this.groupBox3.ShadowThickness = 3;
            this.groupBox3.Size = new System.Drawing.Size(631, 60);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = global::KnowledgeMatrix.Properties.Resources.QP_Save;
            this.button7.Location = new System.Drawing.Point(405, 14);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(60, 40);
            this.button7.TabIndex = 17;
            this.button7.Tag = "QuestionPaper";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = global::KnowledgeMatrix.Properties.Resources.QP_Mock_test;
            this.button6.Location = new System.Drawing.Point(468, 14);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(60, 40);
            this.button6.TabIndex = 16;
            this.button6.Tag = "QuestionPaper";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.BackgroundImage = global::KnowledgeMatrix.Properties.Resources.QP_Generate_OMR_sheet;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(339, 14);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 40);
            this.button5.TabIndex = 15;
            this.button5.Tag = "QuestionPaper";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = global::KnowledgeMatrix.Properties.Resources.QP_Generate_Answer;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Location = new System.Drawing.Point(273, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 40);
            this.button4.TabIndex = 14;
            this.button4.Tag = "QuestionPaper";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = global::KnowledgeMatrix.Properties.Resources.QP_Generate_Q___A;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Location = new System.Drawing.Point(206, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 40);
            this.button3.TabIndex = 13;
            this.button3.Tag = "QuestionPaper";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::KnowledgeMatrix.Properties.Resources.QP_Generate_Question;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(140, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 40);
            this.button2.TabIndex = 12;
            this.button2.Tag = "QuestionPaper";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Image = global::KnowledgeMatrix.Properties.Resources.QP_Clear;
            this.button1.Location = new System.Drawing.Point(74, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 40);
            this.button1.TabIndex = 11;
            this.button1.Tag = "QuestionPaper";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundColor = System.Drawing.Color.White;
            this.groupBox2.BackgroundGradientColor = System.Drawing.Color.White;
            this.groupBox2.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.groupBox2.BorderColor = System.Drawing.Color.Black;
            this.groupBox2.BorderThickness = 1F;
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox2.GroupImage = null;
            this.groupBox2.GroupTitle = "";
            this.groupBox2.Location = new System.Drawing.Point(1, 142);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox2.PaintGroupBox = false;
            this.groupBox2.RoundCorners = 10;
            this.groupBox2.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox2.ShadowControl = false;
            this.groupBox2.ShadowThickness = 3;
            this.groupBox2.Size = new System.Drawing.Size(634, 300);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(633, 285);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.BackgroundGradientColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.BackwardDiagonal;
            this.groupBox1.BorderColor = System.Drawing.Color.DarkGray;
            this.groupBox1.BorderThickness = 1F;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.comboBox4);
            this.groupBox1.Controls.Add(this.comboBox3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox1.GroupImage = null;
            this.groupBox1.GroupTitle = "";
            this.groupBox1.Location = new System.Drawing.Point(3, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox1.PaintGroupBox = false;
            this.groupBox1.RoundCorners = 8;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 3;
            this.groupBox1.Size = new System.Drawing.Size(632, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Exam Duration(HH:MM):";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(446, 93);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(177, 20);
            this.dateTimePicker1.TabIndex = 24;
            this.dateTimePicker1.Value = new System.DateTime(2014, 8, 31, 0, 0, 0, 0);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(532, 51);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 18);
            this.label15.TabIndex = 23;
            this.label15.Text = "Vs";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(137, 69);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(139, 20);
            this.textBox3.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(554, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 18);
            this.label13.TabIndex = 21;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "Exam Name: *";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.White;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(259, 130);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox5.Size = new System.Drawing.Size(192, 10);
            this.textBox5.TabIndex = 19;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(445, 70);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(178, 20);
            this.textBox2.TabIndex = 17;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(280, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Exam Pass Percentage Score: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Exam Mode: *";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(430, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Generation Type:";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownHeight = 50;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.IntegralHeight = false;
            this.comboBox4.ItemHeight = 13;
            this.comboBox4.Items.AddRange(new object[] {
            "Both",
            "CBT--Single User",
            "CBT--Multiple User",
            "Paper Based"});
            this.comboBox4.Location = new System.Drawing.Point(137, 93);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(139, 21);
            this.comboBox4.TabIndex = 13;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.BackColor = System.Drawing.Color.Pink;
            this.comboBox3.DropDownHeight = 50;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.IntegralHeight = false;
            this.comboBox3.ItemHeight = 13;
            this.comboBox3.Items.AddRange(new object[] {
            "Auto",
            "Fixed",
            "Manual"});
            this.comboBox3.Location = new System.Drawing.Point(525, 22);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(98, 21);
            this.comboBox3.TabIndex = 12;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.Color.SandyBrown;
            this.comboBox2.DropDownHeight = 50;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.IntegralHeight = false;
            this.comboBox2.ItemHeight = 13;
            this.comboBox2.Items.AddRange(new object[] {
            "ALL",
            "Simple",
            "Medium",
            "Complex"});
            this.comboBox2.Location = new System.Drawing.Point(319, 22);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(104, 21);
            this.comboBox2.TabIndex = 11;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.SandyBrown;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ALL",
            "Single Choice",
            "Multi Choice"});
            this.comboBox1.Location = new System.Drawing.Point(91, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(108, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Snow;
            this.textBox1.Location = new System.Drawing.Point(136, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Question Type:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(210, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Question Complexity:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "No of Questions Planned: *";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(448, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 5;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(280, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Questions used vs Available:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Subject /Exam Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Standard:";
            // 
            // questionPaperBindingSource
            // 
            this.questionPaperBindingSource.DataSource = typeof(KnowledgeMatrix.Forms.QuestionPaper);
            // 
            // questionPaperBindingSource1
            // 
            this.questionPaperBindingSource1.DataSource = typeof(KnowledgeMatrix.Forms.QuestionPaper);
            // 
            // QuestionPaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "QuestionPaper";
            this.Size = new System.Drawing.Size(1000, 550);
            this.Tag = "QuestionPaper";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.questionPaperBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionPaperBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private CodeVendor.Controls.Grouper groupBox1;
        private CodeVendor.Controls.Grouper groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource questionPaperBindingSource;
        private System.Windows.Forms.BindingSource questionPaperBindingSource1;
        private CodeVendor.Controls.Grouper groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
    }
}
