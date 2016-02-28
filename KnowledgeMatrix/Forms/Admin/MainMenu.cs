using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KnowledgeMatrix.Framework;

namespace KnowledgeMatrix.Forms
{
    public partial class MainMenu : UserControl
    {

        
      

        public MainMenu()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelWidth), Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelHeight));
        }

        private void button_Click(object sender, System.EventArgs e)
        {
            Util.Animate((Button)sender, Util.Effect.Roll, 50, 160);
            //For FAQ and Contact US
            if (((Button)sender).Tag.ToString() == "FAQ" || ((Button)sender).Tag.ToString() == "ContactUs")
                ((System.Windows.Forms.UserControl)(this)).Text = ((Button)sender).Tag.ToString();
            //((Button)sender).Click += new EventHandler(MainWindow_UserControlEvent);
            else if(Utility.IsAdmin())
                ((System.Windows.Forms.UserControl)(this)).Text = ((Button)sender).Tag.ToString();
            

           else if (Utility.isAppValidated() && ((Button)sender).Tag.ToString() == "LicenseManagement")
            {
                ((System.Windows.Forms.UserControl)(this)).Text = "PurchaseManagement";
                this.ParentForm.Text = "KNOWLEDGE MATRIX - Purchase Management";
            }
            else if (!Utility.isAppValidated() && ((Button)sender).Tag.ToString() == "LicenseManagement")
            {
                ((System.Windows.Forms.UserControl)(this)).Text = ((Button)sender).Tag.ToString();
                this.ParentForm.Text = "KNOWLEDGE MATRIX - License Management";
            }
            else
            {
                if (Utility.isAppValidated())
                        ((System.Windows.Forms.UserControl)(this)).Text = ((Button)sender).Tag.ToString();
                else
                    {
                        Util.Animate((Button)sender, Util.Effect.Center, 150, 180);
                        MessageBox.Show("Kindly validate the license. Contact System Administrator");
                        
                        return;
            }       }

        
            
            this.InvokeOnClick(this, e);
            Util.Animate((Button)sender, Util.Effect.Center, 150, 180);
        }

    
        
    }
}
