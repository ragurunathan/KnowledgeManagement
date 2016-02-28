using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExampApp.Database;
using KnowledgeMatrix.Database;
using System;
using System.IO;
using KnowledgeMatrix.Framework;

namespace KnowledgeMatrix.Forms
{
    public partial class eTutor : UserControl
    {
        public List<QuestionMast> getParent;
        public List<QuestionMast> getFirstChild;
        public List<QuestionMast> getSecondChild;
        public QuestionDetailData resultQuestions;
        public QuestionsData result;
        public int rootImageIndex = 0;
        public int selectedCustomerImageIndex = 1;
        public eTutorCollData objeTutorCollData;
        private bool isDataEdited;

        public eTutor()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelWidth), Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelHeight));
            if (Utility.IsAdmin())
            {
                button2.Visible = true;
                exRichTextBox1.ReadOnly = false;
                exRichTextBox2.ReadOnly = false;
            }
            else
                button2.Visible = false;

            LoadTree();
            Clear();
        }

        private List<QuestionMast> LoadTreeInfo(int Level, int ParentId, bool includeSelf)
        {
            List<QuestionMast> q = new List<QuestionMast>();

            if (Level == 0)
                q = (from QuestionMast in result.objQuestionMas
                     where QuestionMast.ParentParentQuestionNo == 0 && QuestionMast.ParentQuestionNo == 0
                     select QuestionMast).ToList();
            else
                q = (from QuestionMast in result.objQuestionMas
                     where QuestionMast.ParentQuestionNo == ParentId || (QuestionMast.QuesNo == ParentId && includeSelf)
                     select QuestionMast).ToList();
            return q;
        }
        private void LoadTree()
        {
            treeView1.Nodes.Clear();
            if (Utility.IsAdmin())
            {
                if (File.Exists(Utility.XML_QUESTION_NAME))
                    result = ObjectXMLSerializer<QuestionsData>.Load(KnowledgeMatrix.Framework.Utility.XML_QUESTION_NAME);
                else
                {
                    MessageBox.Show("Kindly validate the license. Contact System Administrator");
                    return;
                }
            }
            else
            {
                if (File.Exists(Utility.XML_QUESTION_NAME))
                    result = ObjectXMLSerializer<QuestionsData>.Load(KnowledgeMatrix.Framework.Utility.XML_QUESTION_NAME);
                else
                {
                    MessageBox.Show("Kindly validate the license. Contact System Administrator");
                    return;
                }
            }
            
            if (result != null && result.objQuestionMas != null && result.objQuestionMas.Count > 0)
            {
                //Get the First Level Node using Parent Node is null
                getParent = LoadTreeInfo(0, 0, true);
                //(from QuestionMast in result.objQuestionMas
                // where QuestionMast.ParentParentQuestionNo == i && QuestionMast.ParentQuestionNo == j
                // select QuestionMast).ToList();

                for (int prntCnt = 0; prntCnt < getParent.Count; prntCnt++)
                {
                    if ((Utility.IsAdmin()) || (!string.IsNullOrWhiteSpace(getParent[prntCnt].eTutor) && (getParent[prntCnt].eTutor == "Purchased") && !string.IsNullOrWhiteSpace(getParent[prntCnt].QuesBankDate) && Convert.ToDateTime(getParent[prntCnt].QuesBankDate).AddDays(KnowledgeMatrix.Properties.Settings.Default.DaysToAdd) >= System.DateTime.Now))
                    {
                        //Add to tree
                        TreeNode treeNode = new TreeNode(getParent[prntCnt].Name);
                        treeNode.Tag = getParent[prntCnt].QuesNo.ToString();
                        treeNode.ImageIndex = rootImageIndex;
                        treeNode.SelectedImageIndex = 3;

                        treeView1.Nodes.Add(treeNode);
                        //For each of them get the child and pass the node to be added if > 0

                        getFirstChild = LoadTreeInfo(1, getParent[prntCnt].QuesNo, false);
                        //(from QuestionMast in result.objQuestionMas
                        // where QuestionMast.ParentQuestionNo == getParent[prntCnt].QuesNo
                        // select QuestionMast).ToList();
                        for (int childCnt = 0; childCnt < getFirstChild.Count; childCnt++)
                        {
                            TreeNode childtreeNode = new TreeNode(getFirstChild[childCnt].Name);
                            childtreeNode.Tag = getFirstChild[childCnt].QuesNo.ToString();
                            childtreeNode.ImageIndex = selectedCustomerImageIndex;
                            childtreeNode.SelectedImageIndex = 3;

                            treeNode.Nodes.Add(childtreeNode);

                            //Add the Subchild
                            getSecondChild = LoadTreeInfo(2, getFirstChild[childCnt].QuesNo, false);
                            //(from QuestionMast in result.objQuestionMas
                            // where QuestionMast.ParentQuestionNo == getFirstChild[childCnt].QuesNo
                            // select QuestionMast).ToList();
                            for (int childSecondCnt = 0; childSecondCnt < getSecondChild.Count; childSecondCnt++)
                            {
                                TreeNode childsecondtreeNode = new TreeNode(getSecondChild[childSecondCnt].Name);
                                childsecondtreeNode.Tag = getSecondChild[childSecondCnt].QuesNo.ToString();
                                childsecondtreeNode.ImageIndex = rootImageIndex + 2;
                                childsecondtreeNode.SelectedImageIndex = 3;

                                childtreeNode.Nodes.Add(childsecondtreeNode);

                                //Add the Subchild
                            }
                        }
                    }
                }




            }



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void launcheTutor()
        {
            frmeTutorManage q = new frmeTutorManage();
            q.objeTutorMast = (from eTutorMast in objeTutorCollData.eTutorlst
                               where eTutorMast.QuesNo == Convert.ToInt32(treeView1.SelectedNode.Tag)
                               select eTutorMast).ToList()[0];
            q.ShowDialog();
            if (q.isUpdate)
            {
                for (int i = 0; i < objeTutorCollData.eTutorlst.Count; i++)
                {
                    if (objeTutorCollData.eTutorlst[i].QuesNo == q.objeTutorMast.QuesNo)
                    {
                        objeTutorCollData.eTutorlst[i].Status = q.objeTutorMast.Status;
                        objeTutorCollData.eTutorlst[i].Result = q.objeTutorMast.Result;
                        ObjectXMLSerializer<eTutorCollData>.Save(objeTutorCollData, Application.StartupPath+ Utility.FolderType() + @"eTutor\" + treeView1.SelectedNode.Parent.Parent.Text + ".txt");
                    }
                }
            }
        }

        private bool isTreeSecondLevel()
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 2)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please select the last level to modify/view them");
                return false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(isTreeSecondLevel())
                launcheTutor();
            /*eTutorCollData obj = new eTutorCollData();

            eTutorMast objetutor = new eTutorMast();
            objetutor.NoOfQuestions = 10;

            PageDet objPageDet = new PageDet();
            objPageDet.PageInfo = "Ranga";

            objetutor.QuestionDetailData=ObjectXMLSerializer<QuestionDetailData>.Load("Second Standard English - IEO.txt");
            objetutor.Pages.Add(objPageDet);
            objetutor.Pages.Add(objPageDet);

            obj.eTutorlst.Add(objetutor);
            objetutor.NoOfQuestions = 20;
            obj.eTutorlst.Add(objetutor);
            ObjectXMLSerializer<eTutorCollData>.Save(obj, "eTutor.txt");*/
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Filter based on the treeView1.SelectedNode.Text
            Clear();
            grouper1.GroupTitle = treeView1.SelectedNode.Text;
            if (treeView1.SelectedNode.Level == 2)
            {
                objeTutorCollData = ObjectXMLSerializer<eTutorCollData>.Load(Application.StartupPath+ Utility.FolderType() +@"eTutor\" + treeView1.SelectedNode.Parent.Parent.Text + ".txt");
                //Locate using the tag
                if (objeTutorCollData != null)
                {
                    List<eTutorMast> objeTutorMast =
                     (from eTutorMast in objeTutorCollData.eTutorlst
                      where eTutorMast.QuesNo == Convert.ToInt32(treeView1.SelectedNode.Tag)
                      select eTutorMast).ToList();
                    if (objeTutorMast != null && objeTutorMast.Count > 0)
                    {
                        setTextForRich(exRichTextBox1,objeTutorMast[0].Summary);
                       setTextForRich(exRichTextBox2, objeTutorMast[0].Description);
                       isDataEdited = false;
                        if (string.IsNullOrEmpty(objeTutorMast[0].Status))
                        {
                            btnLaunch.Visible = true;
                        }
                        else
                        {
                            btnRelaunch.Visible = true;
                            label4.Text = objeTutorMast[0].Status + " Test percentage is –" + objeTutorMast[0].Result;
                        }
                    }
                    else
                    {
                        Clear();
                    }
                }
                else
                {
                    
                        objeTutorCollData = new eTutorCollData();
                        ObjectXMLSerializer<eTutorCollData>.Save(objeTutorCollData, Application.StartupPath+Utility.FolderType() +@"eTutor\"+ treeView1.SelectedNode.Parent.Parent.Text + ".txt");
                    
                    Clear();
                }
            }
            else
            {
                Clear();
            }
        }
        private void setTextForRich(Khendys.Controls.ExRichTextBox obj, string question)
        {
            // obj.Visible = true;
            //if (question == null || question == "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\n}\n")
            //{
            //    obj.Visible = false;
            //else
            if (question != null && Utility.Base64Decode(question).Contains("\\rtf1\\ansi"))
                obj.Rtf = Utility.Base64Decode(question);
            else if (question != null && question.Length > 0)
                obj.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang16393{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}\viewkind4\uc1\pard\f0\fs20" + question + @"}";

            //  if (obj.TextLength == 0)
            //    obj.Visible = false;


        }
        private void Clear()
        {
            objeTutorCollData = null;
            exRichTextBox1.Text = "";
            exRichTextBox2.Text = "";
            label4.Text = "Not taken the course";
            btnLaunch.Visible = false;
            btnRelaunch.Visible = false;            
        }
        private List<eTutorMast> FindDetails()
        {
            List<eTutorMast> objeTutorMast=
                 (from eTutorMast in objeTutorCollData.eTutorlst
                  where eTutorMast.QuesNo == Convert.ToInt32(treeView1.SelectedNode.Tag)
                  select eTutorMast).ToList();
            return objeTutorMast;
        }

        private void btnRelaunch_Click(object sender, EventArgs e)
        {
            if (isTreeSecondLevel())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure to re-launch the e-Tutor?", "Confirm", MessageBoxButtons.YesNo))
                {
                    launcheTutor();
                }
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (isTreeSecondLevel())
            {
                bool isRecFnd = false;
                int i = 0;

                //Question append

                int noOfQuesUsed = Convert.ToInt16(KnowledgeMatrix.Properties.Settings.Default.eTutorQuestions);

                QuestionDetailData objQuestionDetailData = ObjectXMLSerializer<QuestionDetailData>.Load(Application.StartupPath + Utility.FolderType() + @"QuestionBank\" + treeView1.SelectedNode.Parent.Parent.Text + ".txt");

                objQuestionDetailData.objQuestionDetail = (from QuestionDetailData in objQuestionDetailData.objQuestionDetail
                                                           where QuestionDetailData.QuesNo == Convert.ToInt32(treeView1.SelectedNode.Tag)
                                                           select QuestionDetailData).ToList();

                QuestionPaper obj = new QuestionPaper();
                List<int> questionConsidered = obj.GetListUses(noOfQuesUsed, objQuestionDetailData.objQuestionDetail.Count());

                QuestionDetailData objQuestionDetailData1 = new QuestionDetailData();
                for (int l = 0; l < questionConsidered.Count; l++)
                            objQuestionDetailData1.objQuestionDetail.Add(objQuestionDetailData.objQuestionDetail[questionConsidered[l]]);
                




                if (objeTutorCollData != null)
                {
                    // objeTutorCollData = new eTutorCollData();
                    //ObjectXMLSerializer<eTutorCollData>.Save(objeTutorCollData, treeView1.SelectedNode.Parent.Parent.Text + ".txt");

                    for ( i = 0; i < objeTutorCollData.eTutorlst.Count(); i++)
                    {
                        if (objeTutorCollData.eTutorlst[i].QuesNo == Convert.ToInt32(treeView1.SelectedNode.Tag))
                        {
                            isRecFnd = true;
                            objeTutorCollData.eTutorlst[i].Summary = exRichTextBox1.Rtf;
                            objeTutorCollData.eTutorlst[i].Description = exRichTextBox2.Rtf;
                            objeTutorCollData.eTutorlst[i].QuestionDetailData = objQuestionDetailData1;
                        }
                    }
                }
                if (!isRecFnd)
                {
                    objeTutorCollData = ObjectXMLSerializer<eTutorCollData>.Load(Application.StartupPath +Utility.FolderType()+ @"eTutor\" + treeView1.SelectedNode.Parent.Parent.Text + ".txt");
                    eTutorMast objeTutorMast = new eTutorMast();
                    objeTutorMast.Summary = exRichTextBox1.Rtf;
                    objeTutorMast.Description = exRichTextBox2.Rtf;
                    objeTutorMast.QuesNo = Convert.ToInt32(treeView1.SelectedNode.Tag);
                    objeTutorMast.QuestionDetailData = objQuestionDetailData1;
                    objeTutorCollData.eTutorlst.Add(objeTutorMast);
                }


                


                ObjectXMLSerializer<eTutorCollData>.Save(objeTutorCollData, Application.StartupPath +Utility.FolderType()+ @"eTutor\" + treeView1.SelectedNode.Parent.Parent.Text + ".txt");
                MessageBox.Show("Data Saved!!!");
                treeView1_AfterSelect(null, null);
            }
            
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (isDataEdited)
            {
                System.Windows.Forms.DialogResult answer = MessageBox.Show("There are some data modified. Do you want to save?", "eTutor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer == System.Windows.Forms.DialogResult.Yes)
                    button2_Click_1(null, null);
                    
            }
            
        }

        private void exRichTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDataEdited = true;
        }

        private void exRichTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDataEdited = true;
        }

    }
}
