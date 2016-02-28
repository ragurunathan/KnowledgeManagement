namespace KnowledgeMatrix.Forms
{
    partial class TopMenu
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
            this.Home = new System.Windows.Forms.Button();
            this.License = new System.Windows.Forms.Button();
            this.Report = new System.Windows.Forms.Button();
            this.Help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Home
            // 
            this.Home.Location = new System.Drawing.Point(3, 3);
            this.Home.Name = "Home";
            this.Home.Size = new System.Drawing.Size(65, 23);
            this.Home.TabIndex = 0;
            this.Home.Text = "Home";
            this.Home.UseVisualStyleBackColor = true;
            this.Home.Click += new System.EventHandler(this.button_click);
            // 
            // License
            // 
            this.License.Enabled = false;
            this.License.Location = new System.Drawing.Point(70, 3);
            this.License.Name = "License";
            this.License.Size = new System.Drawing.Size(65, 23);
            this.License.TabIndex = 1;
            this.License.Text = "License";
            this.License.UseVisualStyleBackColor = true;
            this.License.Click += new System.EventHandler(this.button_click);
            // 
            // Report
            // 
            this.Report.Enabled = false;
            this.Report.Location = new System.Drawing.Point(140, 3);
            this.Report.Name = "Report";
            this.Report.Size = new System.Drawing.Size(65, 23);
            this.Report.TabIndex = 2;
            this.Report.Text = "Report";
            this.Report.UseVisualStyleBackColor = true;
            // 
            // Help
            // 
            this.Help.Enabled = false;
            this.Help.Location = new System.Drawing.Point(211, 3);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(65, 23);
            this.Help.TabIndex = 3;
            this.Help.Text = "Help";
            this.Help.UseVisualStyleBackColor = true;
            // 
            // TopMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Help);
            this.Controls.Add(this.Report);
            this.Controls.Add(this.License);
            this.Controls.Add(this.Home);
            this.Name = "TopMenu";
            this.Size = new System.Drawing.Size(308, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Home;
        private System.Windows.Forms.Button License;
        private System.Windows.Forms.Button Report;
        private System.Windows.Forms.Button Help;
    }
}
