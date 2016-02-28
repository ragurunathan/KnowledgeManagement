using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//using KnowledgeMatrix.Forms.QuestionBank;
using KnowledgeMatrix.Framework;
using System.IO;
using KnowledgeMatrix.Database;
using KnowledgeMatrix.Properties;

namespace KnowledgeMatrix
{
    public partial class Form1 : Form
    {
        private ManageNavigation objMngNav;
        private Timer _bannertimer1;
        private int currImg = 0;
        private List<string> objBannerList = new List<string>();

        private void _bannertimer1_Tick(object sender, EventArgs e)
        {
            
            if (currImg+1 == objBannerList.Count())
                currImg = 0;
            else
                currImg++;

            pictureBox1.Image = Image.FromFile( objBannerList[currImg]);
           // pictureBox1.Invalidate();
           // if(currImg == 0)
          //  pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Banner\" + objBannerList[currImg]);
            //else
             //   pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Banner\KM HeaderOne.png");
            
        }
        public Form1()
        {
            InitializeComponent();
            // Set up a timer and fire the Tick event once per second (1000 ms)
            //TO DO 
            //Read all the image names from the Banner folder and associate them to an array so that any new image can be added
            if (Directory.Exists(Application.StartupPath + @"\Banner\"))
            {
                objBannerList = Directory.GetFiles(Application.StartupPath + @"\Banner\", "*.png").ToList<string>();
                if (objBannerList.Count() != 0)
                {
                    _bannertimer1 = new Timer();
                    _bannertimer1.Interval = 5000;
                    _bannertimer1.Tick += new EventHandler(_bannertimer1_Tick);
                    _bannertimer1.Start();

                }
            }
            label3.Text = "Version: " + Settings.Default.Version;
           // this.addToolStripMenuItem.Click += KnowledgeMatrix.Forms.QuestionBank.HandleLabelTextChanged;
           // if(string.IsNullOrWhiteSpace(KnowledgeMatrix.Properties.Settings.Default.OrganizationName))
             lblHeader.Text = KnowledgeMatrix.Properties.Settings.Default.Header;
            //else
              //  lblHeader.Text = KnowledgeMatrix.Properties.Settings.Default.Header + KnowledgeMatrix.Properties.Settings.Default.OrganizationName;
            lblFooter.Text = KnowledgeMatrix.Properties.Settings.Default.Footer;
           

            LogEntry.LogEntryInitialise();
            LogEntry.InsertEntry("Home");


            objMngNav = new ManageNavigation(panel1, panel2, questionBankToolStripMenuItem,addToolStripMenuItem, editToolStripMenuItem, deleteToolStripMenuItem, label1, panel6, lblHeader, lblFooter, label2);
            objMngNav.AddControls("MainMenu");

            //For Admin
            if (Utility.IsAdmin())
            {
                label2.Text = "ROLE: Admin";
                if (File.Exists(Application.StartupPath + @"\DemoT.s3db"))
                    MessageBox.Show("Sqlite available");
                else
                {
                    AdminDatabaseMgmt obj = new AdminDatabaseMgmt();
                    try
                    {
                        obj.CreateTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
            }
            else
                label2.Text = "ROLE: Client";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            objMngNav.RemoveControl();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            objMngNav.RemoveControl();
        }

        private void Help_Click(object sender, EventArgs e)
        {

        }

        private void eTutor_Click(object sender, EventArgs e)
        {
            
            
            if (sender.GetType().Name == "Button")
            {
                if (((Button)sender).Tag.ToString() == objMngNav.GetLastControl())
                       return;

                objMngNav.RemoveControl();
                objMngNav.AddControls(((Button)sender).Tag.ToString());
            }
            else if (sender.GetType().Name == "ToolStripButton")
            {

                if (((ToolStripButton)sender).Tag.ToString() == objMngNav.GetLastControl())
                      return;

                objMngNav.RemoveControl();
                if(((ToolStripButton)sender).Tag.ToString() != "Dashboard")
                objMngNav.AddControls(((ToolStripButton)sender).Tag.ToString());
            }
            else
            {
                if (((ToolStripMenuItem)sender).Tag.ToString() == objMngNav.GetLastControl())
                                           return;

                objMngNav.RemoveControl();
             if(((ToolStripMenuItem)sender).Tag.ToString() != "Dashboard")
                objMngNav.AddControls(((ToolStripMenuItem)sender).Tag.ToString());
            }
           // (KnowledgeMatrix.Forms.QuestionBank.AddListItem);
            //.Add_Click(null, null);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls.OfType<UserControl>())
            {
                if (item.Name == "QuestionBank")
                {
                    ((KnowledgeMatrix.Forms.QuestionBank)(item)).Add_Click(null,null);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls.OfType<UserControl>())
            {
                if (item.Name == "QuestionBank")
                {
                    ((KnowledgeMatrix.Forms.QuestionBank)(item)).Edit_Click(null, null);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls.OfType<UserControl>())
            {
                if (item.Name == "QuestionBank")
                {
                    ((KnowledgeMatrix.Forms.QuestionBank)(item)).Delete_Click(null, null);
                }
            }
        }

        private void aboutKnowledgeMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 n = new AboutBox1();
            n.ShowDialog();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AboutBox1 n = new AboutBox1();
            n.ShowDialog();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Thanks for using application. Are you sure to exit the application?", "Exit", MessageBoxButtons.YesNo))
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked Image" + currImg.ToString());
        }
    }
}
