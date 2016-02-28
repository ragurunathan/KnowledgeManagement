using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KnowledgeMatrix.Forms
{
    public partial class CaptureMockDetails : Form
    {
        public string strName;
        public object[] nameColl;
        public string mode;
        public CaptureMockDetails()
        {
            InitializeComponent();
        }

        private void CaptureMockDetails_Load(object sender, EventArgs e)
        {
            if (mode == "Take Re-Test")
            {
                
                if (!string.IsNullOrWhiteSpace(strName))
                {
                    txtName.Text = strName;
                    txtName.Enabled = false;
                    label1.Visible = true;
                }
            }
            if (mode == "View")
            {
                txtName.SendToBack();
                label34.Text = "Choose the name of student:*";
                comboBox1.Items.AddRange(nameColl);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mode == "View")
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Select the student name the view result");
                    return;
                }
                txtName.Text = comboBox1.SelectedIndex.ToString();
            }
            else if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please  enter the name to start the test !!");
                return;
            }
            strName = txtName.Text;
            mode="OK";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mode="Cancel";
            this.Close();
        }
    }
}
