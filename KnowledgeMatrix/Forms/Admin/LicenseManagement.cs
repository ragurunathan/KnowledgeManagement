using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KnowledgeMatrix.Framework;
using KnowledgeMatrix.Cryptography;
using System.IO;
using KnowledgeMatrix.BusinessObjects;
using KnowledgeMatrix.Database;
using System.Text.RegularExpressions;
namespace KnowledgeMatrix.Forms
{
    public partial class LicenseManagement : UserControl
    {
        private bool isToValid = false;
        public LicenseManagement()
        {
            InitializeComponent();
            txtName.Focus();
            this.Size = new System.Drawing.Size(Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelWidth), Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelHeight));
           // serialBox1.Text = "1234567890ABCDEFGHIJKLMNO";
            
            if (Utility.isAppValidated())
            {                
                button1.Text = "Activated!!!";
                button1.Enabled = false;
            }
            
            if (Utility.IsAdmin())
            {
                button1.Visible = false;
                button3.Visible = true;
                btnRegister.Visible = false;
                txtIP.Text = EntropyGenerator.GetIPForMachine();
                ImpLicFile.Visible = true;
                txtIP.ReadOnly = false;
                serialBox1.Text = Guid.NewGuid().ToString().Replace("-", "").Remove(20);
            }
            else
            {
                ImpLicFile.Visible = false;
                serialBox1.Text = "";
                button1.Visible = true;
                button3.Visible = false;
                txtIP.Text = EntropyGenerator.GetIPForMachine();
                txtIP.ReadOnly = true;
                serialBox1.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.UserControl)(this)).Text = "Done";
            this.InvokeOnClick(this, e);
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            isToValid = true;
            if (ValidateData())
            {
                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.z
                {
                    string file = openFileDialog1.FileName;
                    KnowledgeMatrix.BusinessObjects.License obj = new BusinessObjects.License();
                    obj.name = txtName.Text;
                    obj.email = txtEmail.Text;
                    obj.filename = file;
                    obj.licensekey = serialBox1.Text;
                    if (obj.ValidateLicense())
                    {
                        //MessageBox.Show(Properties.Settings.Default.ProductKey);
                        if (!string.IsNullOrWhiteSpace(txtOrganisation.Text))
                        {
                        KnowledgeMatrix.Properties.Settings.Default.OrganizationName = txtOrganisation.Text;
                        //    Properties.Settings.Default.Footer = "Pathways Foundation, Digital Warriors.Copyright Reserved";
                            KnowledgeMatrix.Properties.Settings.Default.Save();
                        }
                        MessageBox.Show("Succesfully Activated.");
                        ((System.Windows.Forms.UserControl)(this)).Text = "Done";
                        this.InvokeOnClick(this, e);
                    }
                    else
                    {

                        MessageBox.Show("Kindly provide correct License information");

                    }
                    try
                    {
                        // string text = File.ReadAllText(file);
                        //size = text.Length;
                    }
                    catch (Exception)
                    {
                    }
                }
                // MessageBox.Show(serialBox1.Text);

            }
            
        }

        private void ImpLicFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.z
            {
                string file = openFileDialog1.FileName;
                FileCryptography.DoDecrypt(file, KnowledgeMatrix.Properties.Settings.Default.RegistrationKey);
                string decryptedData = FileCryptography.decryptedData;
                string[] prodCatalog = decryptedData.Split('|');
                txtName.Text = prodCatalog[0];
                txtEmail.Text = prodCatalog[1];
                txtOrganisation.Text = prodCatalog[2];
                txtPhone.Text = prodCatalog[3];
                txtIP.Text = prodCatalog[5];
            }
           
        }

        private bool ValidateData()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please Enter ame");
                txtName.Focus();
                return false;
            }
           if (string.IsNullOrWhiteSpace(txtOrganisation.Text))
            {
                MessageBox.Show("Please enter School/Organization");
                txtOrganisation.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter Email");
                txtEmail.Focus();
                return false;
            }
            Regex reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase); ///Object initialization for Regex 
            if (!reg.IsMatch(txtEmail.Text))
            {
                MessageBox.Show("Please enter valid Email");
                txtEmail.Focus();
                return false;
            }   //valid email
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please enter Phone number");
                txtPhone.Focus();
                return false;
            }
            if (isToValid && (string.IsNullOrWhiteSpace(serialBox1.Text) || serialBox1.Text.Length < 20))
            {
                MessageBox.Show("Please enter License Key");
                serialBox1.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtIP.Text))
            {
                MessageBox.Show("Please enter IP");
                txtIP.Focus();
                return false;
            }
         
           
         
            return isValid;
        }
        
        private void btnGenerateLicense_Click(object sender, EventArgs e)
        {
            isToValid = true;
            if (ValidateData())
            {
                //Validate whether record exist in the DB
                //For Admin Generate Database
                AdminDatabaseMgmt objAdmin = new AdminDatabaseMgmt();
                if (!string.IsNullOrWhiteSpace(txtIP.Text))
                    objAdmin.IP = txtIP.Text;
                objAdmin.name = txtName.Text;
                objAdmin.email = txtEmail.Text;
                bool isRecordAvlbl = objAdmin.ValidateRecord();
               
                if (isRecordAvlbl)
                {
                    if (DialogResult.No == MessageBox.Show("The license for the combination (name, email and IP) is already created. Do you like create new one?", "License Available", MessageBoxButtons.YesNo))
                        return;
                }

                //Create License
                KnowledgeMatrix.BusinessObjects.License obj = new BusinessObjects.License();
                obj.name = txtName.Text;
                obj.email = txtEmail.Text;
                obj.LicenseKey = serialBox1.Text;
                if (!string.IsNullOrWhiteSpace(txtIP.Text))
                    obj.IP = txtIP.Text;
                
                if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                    obj.phoneNumber = txtPhone.Text;

                folderBrowserDialog1.Description = @"Select the folder to save license file";
                DialogResult re = folderBrowserDialog1.ShowDialog();
                if (DialogResult.OK == re)
                {
                    obj.filename = folderBrowserDialog1.SelectedPath + @"\Data_" + txtPhone.Text + @"_" + DateTime.Now.ToShortDateString().Replace(@"/","_") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "License.txt";

                    
                    //obj.filename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\DataLicense.txt";
                    obj.GenerateLicense();

                    //MessageBox.Show("License File is made available at" + obj.filename);

                    if (DialogResult.No == MessageBox.Show("License File is made available at " + obj.filename + " . Do you want to create one more new license ?", "License Available", MessageBoxButtons.YesNo))
                    {
                        ((System.Windows.Forms.UserControl)(this)).Text = "Done";
                        this.InvokeOnClick(this, e);
                    }
                    else
                        btnClear_Click(null, null);

                }
            }           

        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Utility.ResetActivation();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            if(Utility.IsAdmin())
                txtIP.Text = "";
            txtName.Text = "";
            txtOrganisation.Text = "";
            txtPhone.Text = "";
           // serialBox1.Text = "";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            isToValid = false;
            if (ValidateData())
            {
                KnowledgeMatrix.BusinessObjects.License obj = new BusinessObjects.License();
                obj.name = txtName.Text;
                obj.email = txtEmail.Text;
                if (!string.IsNullOrWhiteSpace(txtOrganisation.Text))
                    obj.organisationName = txtOrganisation.Text;



                if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                    obj.phoneNumber = txtPhone.Text;

                folderBrowserDialog1.Description = @"Select the folder to save reg info file";
                DialogResult re = folderBrowserDialog1.ShowDialog();
                if (DialogResult.OK == re)
                {
                    obj.filename = folderBrowserDialog1.SelectedPath + @"\RegInfo_" + txtPhone.Text + @"_" + DateTime.Now.ToShortDateString().Replace(@"/", "_") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".txt";


                    //obj.filename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\DataLicense.txt";
                    obj.RegisterUser();

                    MessageBox.Show("Registration info File is made available at" + obj.filename);
                }
            }
        }
    }
}
