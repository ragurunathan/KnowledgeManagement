namespace KnowledgeMatrix.Forms
{
    partial class FrmQuestionsDisplay
    {
        //Bstring rtbDoc="";
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuestionsDisplay));
            this.FontDialog1 = new System.Windows.Forms.FontDialog();
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            this.PageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
            this.PrintPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.rtbDoc = new ExtendedRichTextBox.RichTextBoxPrintCtrl();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.button2 = new System.Windows.Forms.Button();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FontDialog1
            // 
            this.FontDialog1.Color = System.Drawing.SystemColors.ControlText;
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // PrintDialog1
            // 
            this.PrintDialog1.UseEXDialog = true;
            // 
            // PrintPreviewDialog1
            // 
            this.PrintPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.PrintPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.PrintPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.PrintPreviewDialog1.Enabled = true;
            this.PrintPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintPreviewDialog1.Icon")));
            this.PrintPreviewDialog1.Name = "PrintPreviewDialog1";
            this.PrintPreviewDialog1.Visible = false;
            // 
            // PrintDocument1
            // 
            this.PrintDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintDocument1_BeginPrint);
            this.PrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // rtbDoc
            // 
            this.rtbDoc.BackColor = System.Drawing.Color.White;
            this.rtbDoc.Location = new System.Drawing.Point(12, 68);
            this.rtbDoc.Name = "rtbDoc";
            this.rtbDoc.ReadOnly = true;
            this.rtbDoc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbDoc.Size = new System.Drawing.Size(642, 386);
            this.rtbDoc.TabIndex = 2;
            this.rtbDoc.Text = "";
            this.rtbDoc.SelectionChanged += new System.EventHandler(this.rtbDoc_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Question Name:";
            this.label1.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::KnowledgeMatrix.Properties.Resources.Print_75X23;
            this.button1.Location = new System.Drawing.Point(486, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 32;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.ToolStripSeparator4,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripSeparator5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(683, 25);
            this.ToolStrip1.TabIndex = 1;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Font";
            this.toolStripButton1.Click += new System.EventHandler(this.tbrFont_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Color";
            this.toolStripButton2.Click += new System.EventHandler(this.tspColor_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Left";
            this.toolStripButton3.Click += new System.EventHandler(this.tbrLeft_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Center";
            this.toolStripButton4.Click += new System.EventHandler(this.tbrCenter_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Right";
            this.toolStripButton5.Click += new System.EventHandler(this.tbrRight_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "Bold";
            this.toolStripButton6.Click += new System.EventHandler(this.tbrBold_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "Italic";
            this.toolStripButton7.Click += new System.EventHandler(this.tbrItalic_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "Underline";
            this.toolStripButton8.Click += new System.EventHandler(this.tbrUnderline_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::KnowledgeMatrix.Properties.Resources.Cancel;
            this.button2.Location = new System.Drawing.Point(567, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 33;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // FrmQuestionsDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 466);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbDoc);
            this.Controls.Add(this.ToolStrip1);
            this.Name = "FrmQuestionsDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Question Paper Print";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.FontDialog FontDialog1;
        internal System.Windows.Forms.ColorDialog ColorDialog1;
        internal System.Windows.Forms.PageSetupDialog PageSetupDialog1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.PrintDialog PrintDialog1;
        internal System.Windows.Forms.PrintPreviewDialog PrintPreviewDialog1;
        internal System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        internal ExtendedRichTextBox.RichTextBoxPrintCtrl rtbDoc;
        internal System.Drawing.Printing.PrintDocument PrintDocument1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        internal System.Windows.Forms.ToolStripButton toolStripButton3;
        internal System.Windows.Forms.ToolStripButton toolStripButton4;
        internal System.Windows.Forms.ToolStripButton toolStripButton5;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        internal System.Windows.Forms.ToolStripButton toolStripButton6;
        internal System.Windows.Forms.ToolStripButton toolStripButton7;
        internal System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.Button button2;

    }
}