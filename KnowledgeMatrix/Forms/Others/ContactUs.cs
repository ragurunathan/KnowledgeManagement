using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace KnowledgeMatrix.Forms
{
    public partial class ContactUs : UserControl
    {
        public ContactUs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:KMSALES@KNOWLEDGEMATRIX.ORG");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:KMSUPPORT@KNOWLEDGEMATRIX.ORG");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("WWW.KNOWLEDGEMATRIX.ORG");
            Process.Start(sInfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.UserControl)(this)).Text = "Done";
            this.InvokeOnClick(this, e);
        }
    }
}
