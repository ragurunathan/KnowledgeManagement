using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KnowledgeMatrix.Database;

namespace KnowledgeMatrix.Forms
{
    public partial class LicenseDetail : Form
    {
        public LicenseDetailInfo objLicenseDetail = new LicenseDetailInfo();
        public LicenseDetail()
        {

            InitializeComponent();
            this.Load += new System.EventHandler(this.QuestionsForm_Load);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void QuestionsForm_Load(object sender, EventArgs e)
        {
            textBox5.Text = objLicenseDetail.ProductName;
            textBox1.Text = objLicenseDetail.ProductType;
            textBox2.Text = objLicenseDetail.DateOfPurchase;
            textBox3.Text = objLicenseDetail.DateOfExpiry;
        }
    }
}
