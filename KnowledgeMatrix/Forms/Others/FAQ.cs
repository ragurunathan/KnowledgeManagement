using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace KnowledgeMatrix.Forms
{
    public partial class FAQ : UserControl
    {
        public FAQ()
        {
            InitializeComponent();
            if (Directory.Exists(Application.StartupPath + @"\Help\"))
            {
                Uri uri = new Uri(Application.StartupPath + @"\Help\index.htm");
                webBrowser1.Navigate(uri);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.UserControl)(this)).Text = "Done";
            this.InvokeOnClick(this, e);
        }
    }
}
