using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Text;

namespace KnowledgeMatrix.Framework
{
    class ManageNavigation
    {
        private System.Windows.Forms.Panel _objPanelDetail;
        private System.Windows.Forms.Panel _objPanelMenu;
        private System.Windows.Forms.ToolStripMenuItem _questionBankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _deleteToolStripMenuItem;
        private System.Windows.Forms.Label _label1;
        private System.Windows.Forms.Panel _objPictureBox1;
        private List<string> lstObjInStack=new List<string>();
        private const string strNameSpace="KnowledgeMatrix.Forms.";
        private System.Windows.Forms.Label _lblHeader;
        private System.Windows.Forms.Label _lblFooter;
        private System.Windows.Forms.Label _label2;

        public ManageNavigation(System.Windows.Forms.Panel objPanelDetail, System.Windows.Forms.Panel objPanelMenu, ToolStripMenuItem questionBankToolStripMenuItem, ToolStripMenuItem addToolStripMenuItem,
            ToolStripMenuItem editToolStripMenuItem, ToolStripMenuItem deleteToolStripMenuItem, Label label1, System.Windows.Forms.Panel objPictureBox1, Label lblHeader,
            Label lblFooter,Label lbllabel2)
        {
             _objPanelDetail = objPanelDetail;
            _objPictureBox1 = objPictureBox1;
           // _objPanelDetail.Location=System.Drawing.Point(0, 0);
            //_objPanelDetail.Location.Y = 0;
            _objPanelMenu = objPanelMenu;
            _questionBankToolStripMenuItem = questionBankToolStripMenuItem;
            _addToolStripMenuItem = addToolStripMenuItem;
            _editToolStripMenuItem = editToolStripMenuItem;
            _deleteToolStripMenuItem = deleteToolStripMenuItem;
            _label1 = label1;
            _label2 = lbllabel2;
            _lblHeader = lblHeader;
            _lblFooter = lblFooter;
            setLabelForAppExpiry();
            //var control = (Control)Activator.CreateInstance(Type.GetType("KnowledgeMatrix.Forms.TopMenu"));
            //_objPanelMenu.Controls.Add(control);
            EnableDisableMenu();
            //_objPanelMenu.Hide();
        }

        public void AddControls(string strUsrCtrlName)
        {
           // System.Windows.Forms.UserControl obj = new System.Windows.Forms.UserControl();
            var control = (Control)Activator.CreateInstance(Type.GetType(strNameSpace + strUsrCtrlName));
            //if(strUsrCtrlName != "MainMenu")
            //control.on += new EventHandler<> (MainWindow_myevent);
             control.Click += new EventHandler( MainWindow_UserControlEvent);
            
           // _addToolStripMenuItem.Click+=new EventHandler(control.FindForm() (test));
             control.Size = new System.Drawing.Size(Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelWidth), Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelHeight));
            _objPanelDetail.Controls.Add(control);
            _objPanelDetail.Parent.Text = "KNOWLEDGE MATRIX";
             switch (strUsrCtrlName)
            {
                case "eTutor": _objPanelDetail.Parent.Text = "KNOWLEDGE MATRIX - Knowledge Tutor";
                    break;
                case "QuestionBank": _objPanelDetail.Parent.Text = "KNOWLEDGE MATRIX - Knowledge Base";
                    break;
                case "QuestionPaper": _objPanelDetail.Parent.Text = "KNOWLEDGE MATRIX - Knowledge Assessment";
                    break;
                case "MockTestHome": _objPanelDetail.Parent.Text = "KNOWLEDGE MATRIX - Knowledge Evaluator";
                    break;
                case "FAQ": _objPanelDetail.Parent.Text = "KNOWLEDGE MATRIX - FAQ";
                    break;
                case "ContactUs": _objPanelDetail.Parent.Text = "KNOWLEDGE MATRIX - Contact Us";
                    break;
                
            }

           // _objPanelDetail.Parent.Text = "SMILE";
            control.BringToFront();
            Form parentForm = _objPanelMenu.FindForm();
            lstObjInStack.Add( strUsrCtrlName);
            EnableDisableMenu();
            

        }
        public void MainWindow_UserControlEvent(object sender, System.EventArgs e)
        {
            if (((System.Windows.Forms.UserControl)(sender)).Text != null && ((System.Windows.Forms.UserControl)(sender)).Text.Length > 0 && sender.ToString().EndsWith("MainMenu"))
            {
                AddControls(((System.Windows.Forms.UserControl)(sender)).Text);
                ((System.Windows.Forms.UserControl)(sender)).Text = "";
            }
            else
            {
                if (((System.Windows.Forms.UserControl)(sender)).Text != null && ((System.Windows.Forms.UserControl)(sender)).Text.Length > 0)
                {
                    Control activectrl = ((System.Windows.Forms.ContainerControl)(sender)).ActiveControl;
                    RemoveControl();
                    //Check to see the license is valudated
                    if (((System.Windows.Forms.UserControl)(sender)).Name == "LicenseManagement" &&
                        activectrl != null &&
                         activectrl.Text == "Activate")
                    {
                        setLabelForAppExpiry();
                        AddControls("PurchaseManagement");
                    }
                }
            }
        }

        private void setLabelForAppExpiry()
        {
            if (Utility.isAppValidated())
            {
                _label1.Text = "License Activated Date:  " + KnowledgeMatrix.Properties.Settings.Default.DateOfActivation.ToString();
                if (!string.IsNullOrWhiteSpace(KnowledgeMatrix.Properties.Settings.Default.OrganizationName))
                    _label2.Text = "Client: " +KnowledgeMatrix.Properties.Settings.Default.OrganizationName;

                _lblHeader.Text = KnowledgeMatrix.Properties.Settings.Default.Header;
                //else
                  //  _lblHeader.Text = KnowledgeMatrix.Properties.Settings.Default.Header + KnowledgeMatrix.Properties.Settings.Default.OrganizationName;
                //_lblHeader.Text = Properties.Settings.Default.Header;
                _lblFooter.Text = KnowledgeMatrix.Properties.Settings.Default.Footer;
               
            }
        }

        private void EnableDisableMenu()
        {
            if (lstObjInStack != null && lstObjInStack.Count > 0 &&
                (lstObjInStack[lstObjInStack.Count - 1] == "LicenseManagement" || lstObjInStack[lstObjInStack.Count - 1] == "PurchaseManagement"
                || lstObjInStack[lstObjInStack.Count - 1] == "ContactUs" || lstObjInStack[lstObjInStack.Count - 1] == "FAQ"))
            {
                _objPanelMenu.Hide();
                _objPictureBox1.Show();
            }
            else if (_objPanelDetail.Controls.Count > 1)
            {
                if (lstObjInStack != null && lstObjInStack.Count > 0 &&
                (lstObjInStack[lstObjInStack.Count - 1] == "QuestionBank"))
                {
                    //_questionBankToolStripMenuItem.Checked = true;
                    _addToolStripMenuItem.Visible = true;
                    _editToolStripMenuItem.Visible = true;
                    _deleteToolStripMenuItem.Visible = true;

                    AuthorizationOperations obj = new AuthorizationOperations();

                    _addToolStripMenuItem.Enabled = obj.isUserAccessible(OperationNames.TypeOfOperations.Question_Manage);
                    _editToolStripMenuItem.Enabled = obj.isUserAccessible(OperationNames.TypeOfOperations.Question_Manage);
                    _deleteToolStripMenuItem.Enabled = obj.isUserAccessible(OperationNames.TypeOfOperations.Question_Manage);
                }
                else
                {
                    //_questionBankToolStripMenuItem.Enabled = true;
                    _addToolStripMenuItem.Visible = false;
                    _editToolStripMenuItem.Visible = false;
                    _deleteToolStripMenuItem.Visible = false;
                }
                _objPanelMenu.Show();
                _objPictureBox1.Hide();
            }
            else
            {
                _objPanelMenu.Hide();
                _objPictureBox1.Show();
            }
        }
        public void RemoveControl(string strUsrCtrlName)
        {
            if (strUsrCtrlName != "MainMenu")
            {
                foreach (Control item in _objPanelDetail.Controls.OfType<UserControl>())
                {
                    if (item.Name == strUsrCtrlName)
                    {
                        _objPanelDetail.Controls.Remove(item);
                        lstObjInStack.Remove(strUsrCtrlName);
                        var control = (Control)Activator.CreateInstance(Type.GetType(strNameSpace + strUsrCtrlName));
                        control.Click -= new EventHandler(MainWindow_UserControlEvent);
                        strUsrCtrlName = null;
                        EnableDisableMenu();
                        break;
                    }
                }
            }

          //var control = (Control)Activator.CreateInstance(Type.GetType( strUsrCtrlName));
            //_objPanel.Controls.Remove(control);
            
        }
        public void RemoveControl()
        {
            _objPanelDetail.Parent.Text = "KNOWLEDGE MATRIX";
            RemoveControl(lstObjInStack[lstObjInStack.Count - 1]);
            

        }
        public string GetLastControl()
        {
            return lstObjInStack[lstObjInStack.Count - 1];
            


        }
    }
}
