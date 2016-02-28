using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KnowledgeMatrix.Database;
using ExampApp.Database;
using System.IO;
using KnowledgeMatrix.Framework;
using ExtendedRichTextBox;
//using ExamApp;

namespace KnowledgeMatrix.Forms
{
    //public class ListOfQuestions
    //{
    //}
    public partial class QuestionBank : UserControl
    {
       // private  string XML_FILE_NAME = Application.StartupPath + @"\Physics.xml";
        //private  string XML_QUESTION_NAME = Application.StartupPath + @"\QuestionMaster.xml";
        public int rootImageIndex = 0;
        public int selectedCustomerImageIndex = 1;
        public List<QuestionMast> getParent;
        public List<QuestionMast> getFirstChild;
        public List<QuestionMast> getSecondChild;
        public QuestionsData result;
        public QuestionDetailData resultQuestions;
        public List<QuestionDetail> navQuestions;
        public int currRecord = 0;
        private frmMain1 obj;
        private string PrevFileName;
        private bool isShowAnsweChecked = false;

        public QuestionBank()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelWidth), Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelHeight));
           
            AnswerInfo.Visible = checkBox5.Checked;
            Answer.Visible = checkBox5.Checked;
            
            First.Enabled = false;
            Previous.Enabled = false;
            Next.Enabled = false;
            Last.Enabled = false;

            toolStripButton1.BackColor = Color.LightSkyBlue;
            toolStripButton2.BackColor = Color.LightGray;
            // Identify that the first link is visited already. 
            this.linkLabel1.Links[0].Visited = true;
            //groupBox4.SendToBack();
            toolStrip1.Visible = false;
            groupBox2.Visible = false;
            checkBox6.Checked = false;
            // Set up how the form should be displayed and add the controls to the form. 
            //this.ClientSize = new System.Drawing.Size(292, 266);
           // this.Controls.AddRange(new System.Windows.Forms.Control[] { this.linkLabel1 });
           // this.Text = "Link Label Example";

            
            // start off by adding a base treeview node
           /* TreeNode mainNode = new TreeNode();
            mainNode.Name = "mainNode";
            mainNode.Text = "Main";
            this.treeView1.Nodes.Add(mainNode);*/
           LoadTree();
            //if (File.Exists(Utility.XML_QUESTION_NAME))
            //    result = ObjectXMLSerializer<QuestionsData>.Load(Utility.XML_QUESTION_NAME);
            //else
            //{
            //    MessageBox.Show("Kindly validate the license. Contact System Administrator");
            //    return;
            //}
           // LoadQuestionFromXml();
            //this.Load += new EventHandler(Form1_Load);
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("First Time");
        }
        private void setLinkLabel()
        {
            // Set the Text property to a string. 
            this.linkLabel1.Text = "";

            int i = navQuestions.Count;
            // Create new links using the Add method of the LinkCollection class. 
            // Underline the appropriate words in the LinkLabel's Text property. 
            // The words 'Register', 'Microsoft', and 'MSN' will  
            // all be underlined and behave as hyperlinks. 
            this.linkLabel1.Links.Clear();
            // First check that the Text property is long enough to accommodate 
            // the desired hyperlinked areas.  If it's not, don't add hyperlinks. 
            if (this.linkLabel1.Text.Length <= 100)
            {
                //this.linkLabel1.Links[0].LinkData = "Register";
                int j = 0;
                while (j < i)
                {
                    this.linkLabel1.Links.Add(j, 1, j);
                    this.linkLabel1.Text =   this.linkLabel1.Text+  (j+1).ToString() ;
                    j++;

                }//this.linkLabel1.Links.Add(0, 2, "www.microsoft.com");
                //this.linkLabel1.Links.Add(2, 2, "www.msn.com");
                //  The second link is disabled and will appear as red. 
                // this.linkLabel1.Links[1].Enabled = false;
            }
        }

        
        private void LoadQuestionFromXml(string FileName)
        {

            if (string.IsNullOrWhiteSpace(PrevFileName) || (PrevFileName != FileName))
            {
                PrevFileName = FileName;

                resultQuestions = null;
                if (Utility.IsAdmin())
                    FileName = Application.StartupPath + Utility.FolderType() + @"\QuestionBank\" + FileName + ".txt";
                else
                    FileName = Application.StartupPath + Utility.FolderType() + @"\QuestionBank\" + FileName + ".txt";
                if (File.Exists(FileName))
                    resultQuestions = ObjectXMLSerializer<QuestionDetailData>.Load(FileName);
                Utility.XML_FILE_NAME = FileName;
            }
        }
        private void LoadTree()
        {
            /*XmlDocument contentxml = new XmlDocument();
            contentxml.Load(@"D:\Ranga\Consultant\Exam App .NET\Code\ExamApp\ExamApp\XML\QuestionMaster.xml");
            //contentxml.Load(Application.StartupPath + @"\QuestionMaster.xml");
            string xml = contentxml.OuterXml;*/
            if (File.Exists(Utility.XML_QUESTION_NAME))
                result = ObjectXMLSerializer<QuestionsData>.Load(Utility.XML_QUESTION_NAME);
            else
            {
                MessageBox.Show("Kindly validate the license. Contact System Administrator");
                return;
            }
           

           /*var xml =
@"<?xml version=""1.0"" ?>
<CustomerQueryRs>
 <QuestionMaster>
   <QuesNo>1</QuesNo>
   <Name>Physics</Name>
   <isLeaf>true</isLeaf>
 </QuestionMaster>
 <QuestionMaster>
   <QuesNo>2</QuesNo>
   <Name>Gravity</Name>
<isLeaf>true</isLeaf>
<ParentQuestionNo>1</ParentQuestionNo>
</QuestionMaster>
 <QuestionMaster>
   <QuesNo>3</QuesNo>
   <Name>Newton</Name>
<isLeaf>true</isLeaf>
<ParentQuestionNo>2</ParentQuestionNo>
<ParentParentQuestionNo>1</ParentParentQuestionNo>
<TotalQuestions>20</TotalQuestions>
</QuestionMaster>
</CustomerQueryRs>";

           
           var serializer = new XmlSerializer(typeof(QuestionsData), new XmlRootAttribute("CustomerQueryRs"));
           using (var stringReader = new StringReader(xml))
           using (var reader = XmlReader.Create(stringReader))
           {*/
           //result = (QuestionsData)serializer.Deserialize(reader);
           int i = 0, j = 0;         
           if (result != null && result.objQuestionMas != null && result.objQuestionMas.Count > 0)
           { 
                //Get the First Level Node using Parent Node is null
                getParent =
                (from QuestionMast in result.objQuestionMas
                 where QuestionMast.ParentParentQuestionNo == i && QuestionMast.ParentQuestionNo == j
                 select QuestionMast).ToList();

                for (int prntCnt = 0; prntCnt < getParent.Count; prntCnt++)
                {
                    //Check if the date---It should not cross expiry time
                    if ((Utility.IsAdmin()) || (!string.IsNullOrWhiteSpace(getParent[prntCnt].QuesBank) && (getParent[prntCnt].QuesBank == "Purchased") && !string.IsNullOrWhiteSpace(getParent[prntCnt].QuesBankDate) && Convert.ToDateTime(getParent[prntCnt].QuesBankDate).AddDays(KnowledgeMatrix.Properties.Settings.Default.DaysToAdd) >= System.DateTime.Now))
                    {

                        //Add to tree
                        TreeNode treeNode = new TreeNode(getParent[prntCnt].Name);
                        treeNode.Tag = getParent[prntCnt].QuesNo.ToString();
                        treeNode.ImageIndex = rootImageIndex;

                        treeNode.SelectedImageIndex = 3;

                        treeView1.Nodes.Add(treeNode);
                        //For each of them get the child and pass the node to be added if > 0

                        getFirstChild =
                   (from QuestionMast in result.objQuestionMas
                    where QuestionMast.ParentQuestionNo == getParent[prntCnt].QuesNo
                    select QuestionMast).ToList();
                        for (int childCnt = 0; childCnt < getFirstChild.Count; childCnt++)
                        {
                            TreeNode childtreeNode = new TreeNode(getFirstChild[childCnt].Name);
                            childtreeNode.Tag = getFirstChild[childCnt].QuesNo.ToString();
                            childtreeNode.ImageIndex = selectedCustomerImageIndex;
                            childtreeNode.SelectedImageIndex = 3;

                            treeNode.Nodes.Add(childtreeNode);

                            //Add the Subchild
                            getSecondChild =
                 (from QuestionMast in result.objQuestionMas
                  where QuestionMast.ParentQuestionNo == getFirstChild[childCnt].QuesNo
                  select QuestionMast).ToList();
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


           //if (treeView1.Nodes.Count == 0)
           //    MessageBox.Show("Kindly purchase under the Product Purchasing cart");
        }

#region Add and Remove Nodes

        /// <summary>
        /// Add a Treeview node using a dialog box
        /// forcing the user to set the name and text properties
        /// of the node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuAddNode_Click(object sender, EventArgs e)
        {
            
                if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 2)
                    return;
                NewNode n = new NewNode();
                n.ShowDialog();
                if (n.NewNodeName != null)
                {
                    TreeNode nod = new TreeNode();
                    nod.Name = n.NewNodeName.ToString();
                    nod.Text = nod.Name;
                    //nod.Text = n.NewNodeText.ToString();
                    //nod.Tag = n.NewNodeTag.ToString();



                    QuestionMast obj = new QuestionMast();
                    obj.Name = nod.Name;
                    obj.QuesNo = result.objQuestionMas.Count() + 1;
                    nod.Tag = obj.QuesNo.ToString();
                    obj.TotalQuestions = 0;
                    n.Close();


                    if (treeView1.SelectedNode.Level == 0)
                        //do nothing
                        obj.ParentQuestionNo = Convert.ToInt32(treeView1.SelectedNode.Tag);
                    else if (treeView1.SelectedNode.Level == 1)
                    {
                        obj.ParentQuestionNo = Convert.ToInt32(treeView1.SelectedNode.Tag);
                        obj.ParentParentQuestionNo = Convert.ToInt16(treeView1.SelectedNode.Parent.Tag);
                    }
                    else if (treeView1.SelectedNode.Level == 2)
                    {
                        obj.ParentQuestionNo = Convert.ToInt32(treeView1.SelectedNode.Parent.Tag);
                        obj.ParentParentQuestionNo = Convert.ToInt16(treeView1.SelectedNode.Parent.Parent.Tag);
                    }

                    result.objQuestionMas.Add(obj);


                    //QuestionsData obj1 = new QuestionsData();
                    //obj1.objQuestionMas = result;
                    ObjectXMLSerializer<QuestionsData>.Save(result, Utility.XML_QUESTION_NAME);


                    obj = null;

                    treeView1.SelectedNode.Nodes.Add(nod);
                    treeView1.SelectedNode.ExpandAll();
                }
            }
        



        /// <summary>
        /// Remove the selected node and it children
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuRemoveNode_Click(object sender, EventArgs e)
        {
            for(int i =0;i<result.objQuestionMas.Count;i++)
            {
                if (treeView1.SelectedNode.Text == result.objQuestionMas[i].Name)
                {
                    result.objQuestionMas.RemoveAt(i);
                    break;
                }
            }
            ObjectXMLSerializer<QuestionsData>.Save(result, Utility.XML_QUESTION_NAME);
            treeView1.SelectedNode.Remove();
        }

        
#endregion


       
#region Treeview Event Handlers

        /// <summary>
        /// Display information about the selected node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
               // clearPDF();
                txtName.Text = "";
                txtParentName.Text = "";
                txtText.Text = "";
                txtTag.Text = "";
                

               if (treeView1.SelectedNode.Level == 0)
                {
                    LoadQuestionFromXml(treeView1.SelectedNode.Text);
                    label14.Text = treeView1.SelectedNode.Text.Split(' ')[0] + " " + treeView1.SelectedNode.Text.Split(' ')[1];
                    userInfo.GroupTitle = label14.Text;
                    label15.Text = treeView1.SelectedNode.Text;
                   Int32 strindex = 0;
                    List<QuestionDetail> obj; 

                   /////
                    if (navQuestions != null)
                        navQuestions.Clear();
                    else
                        navQuestions = new List<QuestionDetail>();
                    getFirstChild =
                 (from QuestionMast in result.objQuestionMas
                  where QuestionMast.ParentQuestionNo == Convert.ToInt32( treeView1.SelectedNode.Tag)
                  select QuestionMast).ToList();
                    if (getFirstChild.Count != 0)
                        cmnuRemoveNode.Enabled = false;
                    for (int childCnt = 0; childCnt < getFirstChild.Count; childCnt++)
                    {
                        strindex = (getFirstChild[childCnt].QuesNo);
                        if (resultQuestions != null)
                        {
                            obj = (from QuestionDetail in resultQuestions.objQuestionDetail
                                   where QuestionDetail.QuesNo == strindex
                                   orderby QuestionDetail.QuesDetSNo
                                   select QuestionDetail).ToList();
                            //MessageBox.Show(node.Tag.ToString());
                            foreach (QuestionDetail objj in obj)
                                navQuestions.Add(objj);
                        }
                        //TreeNode childtreeNode = new TreeNode(getFirstChild[childCnt].Name);
                        //childtreeNode.Tag = getFirstChild[childCnt].QuesNo.ToString();
                        //childtreeNode.ImageIndex = selectedCustomerImageIndex;
                        //childtreeNode.SelectedImageIndex = 3;

                        //treeNode.Nodes.Add(childtreeNode);

                        //Add the Subchild
                        getSecondChild =
             (from QuestionMast in result.objQuestionMas
              where QuestionMast.ParentQuestionNo == getFirstChild[childCnt].QuesNo
              select QuestionMast).ToList();


                        for (int childSecondCnt = 0; childSecondCnt < getSecondChild.Count; childSecondCnt++)
                        {
                            strindex = (getSecondChild[childSecondCnt].QuesNo);
                            if (resultQuestions != null)
                            {
                                obj = (from QuestionDetail in resultQuestions.objQuestionDetail
                                       where QuestionDetail.QuesNo == strindex
                                       orderby QuestionDetail.QuesDetSNo
                                       select QuestionDetail).ToList();
                                //MessageBox.Show(node.Tag.ToString());
                                foreach (QuestionDetail objj in obj)
                                    navQuestions.Add(objj);
                            }
                                //TreeNode childsecondtreeNode = new TreeNode(getSecondChild[childSecondCnt].Name);
                            //childsecondtreeNode.Tag = getSecondChild[childSecondCnt].QuesNo.ToString();
                            //childsecondtreeNode.ImageIndex = rootImageIndex + 2;
                            //childsecondtreeNode.SelectedImageIndex = 3;

                            //childtreeNode.Nodes.Add(childsecondtreeNode);

                            //Add the Subchild
                        }
                    }
                   /////

                    
                /*    
                   foreach (TreeNode node in treeView1.SelectedNode.Nodes)
                    {
                        strindex = Convert.ToInt16(node.Tag);
                        obj = (from QuestionDetail in resultQuestions.objQuestionDetail
                               where QuestionDetail.QuesNo == strindex
                               select QuestionDetail).ToList();
                        //MessageBox.Show(node.Tag.ToString());
                        foreach (QuestionDetail objj in obj)
                            navQuestions.Add(objj);
                    }


                    label15.Text = treeView1.SelectedNode.Text;
                    cmnuAddNode.Enabled = true;
                    //Load the Question Based on the Question Number
                    navQuestions = (from QuestionDetail in resultQuestions.objQuestionDetail
                                    //where ParentParentQuestionNo = Convert.ToInt16(treeView1.SelectedNode.Tag)
                                    select QuestionDetail).ToList();*/
                }
               else if (treeView1.SelectedNode.Level == 1 )
                {
                    LoadQuestionFromXml(treeView1.SelectedNode.Parent.Text);

                    label14.Text = treeView1.SelectedNode.Parent.Text.Split(' ')[0] +" "+ treeView1.SelectedNode.Parent.Text.Split(' ')[1];
                    userInfo.GroupTitle = label14.Text; 
                   Int16 strindex=0;
                    if (navQuestions != null)
                        navQuestions.Clear();
                    else
                        navQuestions = new List<QuestionDetail>();
                    List<QuestionDetail> obj;
                    
                        label15.Text = treeView1.SelectedNode.Parent.Text;
                    foreach (TreeNode node in treeView1.SelectedNode.Nodes)
                    {
                        strindex= Convert.ToInt16(node.Tag);
                        if (resultQuestions != null)
                        {
                            obj = (from QuestionDetail in resultQuestions.objQuestionDetail
                                   where QuestionDetail.QuesNo == strindex
                                   orderby QuestionDetail.QuesDetSNo
                                   select QuestionDetail).ToList();
                            
                            //MessageBox.Show(node.Tag.ToString());
                            foreach (QuestionDetail objj in obj)
                                navQuestions.Add(objj);
                        }
                    }
                    //Load the Question Based on the Question Number
                 //   navQuestions = (from QuestionDetail in resultQuestions.objQuestionDetail
                   //                 where QuestionDetail.QuesNo <= strindex
                     //               select QuestionDetail).ToList();
                    AuthorizationOperations obj1 = new AuthorizationOperations();
                    cmnuAddNode.Enabled = true && obj1.isUserAccessible(OperationNames.TypeOfOperations.Question_Manage);
                    if (navQuestions.Count() != 0)
                        cmnuRemoveNode.Enabled = false;
                    
                }
                else if (treeView1.SelectedNode.Level == 2)
                {
                    LoadQuestionFromXml(treeView1.SelectedNode.Parent.Parent.Text);
                    label14.Text = treeView1.SelectedNode.Parent.Parent.Text.Split(' ')[0] +" "+ treeView1.SelectedNode.Parent.Parent.Text.Split(' ')[1];
                    userInfo.GroupTitle = label14.Text; 
                    label15.Text = treeView1.SelectedNode.Parent.Parent.Text;
                    cmnuAddNode.Enabled = false;

                      AuthorizationOperations obj = new AuthorizationOperations();
                    //  obj.isUserAccessible(OperationNames.TypeOfOperations.Question_Manage);

                      cmnuRemoveNode.Enabled = true && obj.isUserAccessible(OperationNames.TypeOfOperations.Question_Manage);
                    //Load the Question Based on the Question Number
                    if(resultQuestions != null)
                    navQuestions = (from QuestionDetail in resultQuestions.objQuestionDetail
                                    where QuestionDetail.QuesNo == Convert.ToInt32(treeView1.SelectedNode.Tag)
                                    orderby QuestionDetail.QuesDetSNo
                                    select QuestionDetail).ToList();
                }
                label16.Text = treeView1.SelectedNode.Text;

                
                currRecord = 0;
                if(navQuestions != null)
                    label18.Text= (currRecord).ToString() +" of " + navQuestions.Count.ToString();
                else
                    label18.Text = "0 of 0";
              //  label18.Text = currRecord.ToString();
                LoadQuestionOnTreeSelect();
                //
                txtName.Text = treeView1.SelectedNode.Name.ToString();
                txtText.Text = treeView1.SelectedNode.Text.ToString();
                txtTag.Text = treeView1.SelectedNode.Tag.ToString();
                if (treeView1.SelectedNode.Level == 0)
                    txtParentName.Text = treeView1.SelectedNode.Text.ToString();
                else
                     txtParentName.Text = treeView1.SelectedNode.Parent.Text.ToString();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }
        }

        private void setTextForRich(RichTextBoxPrintCtrl obj, string question,bool isOverride )
        {
            obj.Visible = true;
            //if (question == null || question == "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\n}\n")
            //{
            //    obj.Visible = false;
            //else
            if (question.Length == 0)
                obj.Text = "";
            else if (question != null && Utility.Base64Decode(question).Contains("\\rtf1\\ansi") )
                obj.Rtf = Utility.Base64Decode(question);
            else if (question != null && question.Length > 0)
                obj.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang16393{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}\viewkind4\uc1\pard\f0\fs20" + question +@"}";

            if(obj.TextLength == 0 && ! isOverride)
                obj.Visible = false;
            

        }
        private void LoadQuestionOnTreeSelect()
        {
            //setLinkLabel();
           // clearPDF();
            if (navQuestions.Count > 0)
            {
                if (toolStripButton1.BackColor == Color.LightSkyBlue)
                {
                    //LogEntry.WriteLog(" LoadQuestionOnTreeSelect startt : ", System.DateTime.Now.ToLocalTime().ToString());

                    //label21.Text = navQuestions[currRecord].Question;
                    
                    //if(navQuestions[currRecord].Question.Contains("\rtf1\ansi"))
                     //    richTextBoxPrintCtrl1.Rtf = navQuestions[currRecord].Question;
                    //else
                      

                    //textBox8.Text= navQuestions[currRecord].Question;
                    groupBox2.Visible = true;

                    radioPanel.Visible = false;
                    checkboxPanel.Visible = false;
                    
                    First.Enabled = true;
                    Previous.Enabled = true;
                    Next.Enabled = true;
                    Last.Enabled = true;


                   // label18.Text = (currRecord + 1).ToString() ;
                    label18.Text = (currRecord+1).ToString() + " of " + navQuestions.Count.ToString();
                    

                    //label17.Text = navQuestions[currRecord].Pattern;//TO DO && label14
                    Complexity.Text = navQuestions[currRecord].Complexity;

                    //Answer.Text = navQuestions[currRecord].Answer;
                    //CorrectAnswerDetails.Text = navQuestions[currRecord].CorrectAnswerDetails;

//                    LogEntry.WriteLog(" Text Rich startt : ", System.DateTime.Now.ToLocalTime().ToString());
                    setTextForRich(richTextBoxPrintCtrl1, navQuestions[currRecord].Question,false);
                    setTextForRich(richTextBoxPrintCtrl2, navQuestions[currRecord].CorrectAnswerDetails,true);
                    setTextForRich(richTextBoxPrintCtrl3, navQuestions[currRecord].AnswerConcept,true);
                    
                   // textBox6.Text = navQuestions[currRecord].CorrectAnswerDetails;
                   // concept.Text = navQuestions[currRecord].AnswerConcept;
                   // textBox7.Text = navQuestions[currRecord].AnswerConcept;



                    setTextForRich(richTextBoxPrintCtrl4, navQuestions[currRecord].OptionOne,false);

                    setTextForRich(richTextBoxPrintCtrl5,navQuestions[currRecord].OptionTwo,false);
                    
                    setTextForRich(richTextBoxPrintCtrl6,navQuestions[currRecord].OptionThree,false);

                    setTextForRich(richTextBoxPrintCtrl7,navQuestions[currRecord].OptionFour,false);
  //                  LogEntry.WriteLog(" Text Rich Ends : ", System.DateTime.Now.ToLocalTime().ToString());

                    if (navQuestions[currRecord].AnswerType == "Multi Choice" || navQuestions[currRecord].AnswerType == "Multi-Image")
                    {
                        checkboxPanel.Visible = true;
                       // checkBox1.Text = navQuestions[currRecord].QuestionOptions.Split(';')[0];
                        //checkBox2.Text = navQuestions[currRecord].QuestionOptions.Split(';')[1];
                        //checkBox3.Text = navQuestions[currRecord].QuestionOptions.Split(';')[2];
                        //checkBox4.Text = navQuestions[currRecord].QuestionOptions.Split(';')[3];

                       // pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = pictureBox5.Visible = false;
                     //   checkBox1.Visible = checkBox2.Visible = checkBox3.Visible = checkBox4.Visible = true;
                        checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;

                        checkBox1.Visible=richTextBoxPrintCtrl4.TextLength > 0? true:false;
                        checkBox2.Visible = richTextBoxPrintCtrl5.TextLength > 0 ? true : false;
                        checkBox3.Visible = richTextBoxPrintCtrl6.TextLength > 0 ? true : false;
                        checkBox4.Visible = richTextBoxPrintCtrl7.TextLength > 0 ? true : false;
                    }
                    else if (navQuestions[currRecord].AnswerType == "Single Choice" || navQuestions[currRecord].AnswerType == "Single-Image")
                    {
                        radioPanel.Visible = true;
                        //radioButton1.Text = navQuestions[currRecord].QuestionOptions.Split(';')[0];
                        //radioButton2.Text = navQuestions[currRecord].QuestionOptions.Split(';')[1];
                        //radioButton3.Text = navQuestions[currRecord].QuestionOptions.Split(';')[2];
                        //radioButton4.Text = navQuestions[currRecord].QuestionOptions.Split(';')[3];
                       // radioButton1.Visible = radioButton2.Visible = radioButton3.Visible = radioButton4.Visible = true;
                        radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
                        radioButton1.Visible = richTextBoxPrintCtrl4.TextLength > 0 ? true : false;
                        radioButton2.Visible = richTextBoxPrintCtrl5.TextLength > 0 ? true : false;
                        radioButton3.Visible = richTextBoxPrintCtrl6.TextLength > 0 ? true : false;
                        radioButton4.Visible = richTextBoxPrintCtrl7.TextLength > 0 ? true : false;
                        //pictureBox6.Image = (Image)navQuestions[currRecord].Picture;
                        //pictureBox7.Image = (Image)navQuestions[currRecord].Picture1;
                        //pictureBox8.Image = (Image)navQuestions[currRecord].Picture2;
                        //pictureBox9.Image = (Image)navQuestions[currRecord].Picture3;
    //                    pictureBox6.Visible = pictureBox7.Visible = pictureBox8.Visible = pictureBox9.Visible = false;

                    }
                    else if (navQuestions[currRecord].AnswerType == "Multi-Image")
                    {
                        checkboxPanel.Visible = true;
                       // checkBox1.Text = checkBox2.Text = checkBox3.Text = checkBox4.Text = "";
                        //checkBox1.Visible = checkBox2.Visible = checkBox3.Visible = checkBox4.Visible = false;
                      //  pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = pictureBox5.Visible = true;
                        /*pictureBox2.Image = (Image)navQuestions[currRecord].Picture;
                        pictureBox3.Image = (Image)navQuestions[currRecord].Picture1;
                        pictureBox4.Image = (Image)navQuestions[currRecord].Picture2;
                        pictureBox5.Image = (Image)navQuestions[currRecord].Picture3;*/

                        checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;
                    }
                    else if (navQuestions[currRecord].AnswerType == "Single-Image")
                    {
                        radioPanel.Visible = true;
                        //radioButton1.Text = navQuestions[currRecord].QuestionOptions.Split(';')[0];
                        //radioButton2.Text = navQuestions[currRecord].QuestionOptions.Split(';')[1];
                        //radioButton3.Text = navQuestions[currRecord].QuestionOptions.Split(';')[2];
                        //radioButton4.Text = navQuestions[currRecord].QuestionOptions.Split(';')[3];
                        radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
                       /* pictureBox6.Visible = pictureBox7.Visible = pictureBox8.Visible = pictureBox9.Visible = true;
                        pictureBox6.Image = (Image)navQuestions[currRecord].Picture;
                        pictureBox7.Image = (Image)navQuestions[currRecord].Picture1;
                        pictureBox8.Image = (Image)navQuestions[currRecord].Picture2;
                        pictureBox9.Image = (Image)navQuestions[currRecord].Picture3;*/
                    }
                    if (navQuestions[currRecord].AnswerType == "Multi-Text" || navQuestions[currRecord].AnswerType == "Single-Text")
                    {
                      /*  textBox2.Text = navQuestions[currRecord].QuestionOptions.Split(';')[0];
                        textBox3.Text = navQuestions[currRecord].QuestionOptions.Split(';')[1];
                        textBox4.Text = navQuestions[currRecord].QuestionOptions.Split(';')[2];
                        textBox5.Text = navQuestions[currRecord].QuestionOptions.Split(';')[3];
                        textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = true;*/
                    }
                    else
                    {
                      //  textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = false;
                    }
                    if (currRecord == 0)
                    {
                        First.Enabled = false;
                        Previous.Enabled = false;
                    }
                    if (currRecord + 1 == navQuestions.Count)
                    {
                        Last.Enabled = false;
                        Next.Enabled = false;
                    }
      //              LogEntry.WriteLog(" LoadQuestionOnTreeSelect enddss : ", System.DateTime.Now.ToLocalTime().ToString());
                    if (panel1.Visible)
                        panel1.SendToBack();
                   
                }
                else
                {
                    panel1.Visible = true;
                    
                   /* foreach (Control item in panel1.Controls)
                    {
                        panel1.Controls.Remove(item);
                    }*/
                    for (int nav = 0; nav < navQuestions.Count; nav++)
                        navQuestions[nav].CategoryName = GetTopicName(navQuestions[nav].QuesNo);
                    if (panel1.Controls.Count == 0)
                    {
                     //   obj = new RichText(navQuestions);
                      //  obj.isLoadedFirst = true;
                        obj = new frmMain1();
                        //obj.resultQuestions.objQuestionDetail.Clear();
                        
                        obj.resultQuestions.objQuestionDetail = navQuestions;
                        obj.strSubject = label15.Text;
                        obj.strQuestTopic = label16.Text;
                        panel1.Controls.Add(obj);
                    }
                    else
                    {
                       // obj.resultQuestions.objQuestionDetail.Clear();
                        obj.resultQuestions.objQuestionDetail = navQuestions;
                        obj.strSubject = label15.Text;
                        obj.strQuestTopic = label16.Text;
                       // obj.Redirect();
                      //  obj.isLoadedFirst = false;
                        obj.LoadTextData();
                    }
                        panel1.BringToFront();
                }
        //        LogEntry.WriteLog(" LoadQuestionOnTreeSelect startt 2 : ", System.DateTime.Now.ToLocalTime().ToString());
                checkBox5_CheckedChanged(null,null);
                HomePage.SendToBack();
                toolStrip1.Visible = true;
          //      LogEntry.WriteLog(" LoadQuestionOnTreeSelect endd 2 : ", System.DateTime.Now.ToLocalTime().ToString());
            }
            else
            {
                groupBox2.Visible = false;
                toolStrip1.Visible = false;
                HomePage.BringToFront();
            }
        }

        private string GetTopicName(int QuesNo)
        {


            List<QuestionMast> q = (from QuestionMast in result.objQuestionMas
                                    where QuestionMast.QuesNo == QuesNo
                                    select QuestionMast).ToList();

            //q = (from QuestionMast in result.objQuestionMas
            //                      where QuestionMast.QuesNo == q[0].ParentQuestionNo
            //                    select QuestionMast).ToList();
            if (q != null && q.Count() > 0)
                return q[0].Name;
            else
                return "OTHERS";


        }

        /// <summary>
        /// Clear nodes marked by the find functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_Click(object sender, EventArgs e)
        {
            //ClearBackColor();
        }

#endregion

        private void clearPDF()
        {
            if (panel1.Controls.Count > 0)
            {
                //obj.Redirect();
            }
        }
        


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //int target = e.Link.LinkData as short;
            currRecord = Convert.ToInt16(e.Link.LinkData);
            LoadQuestionOnTreeSelect();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            LogEntry.WriteLog("<br/> Next Button start : ", System.DateTime.Now.ToLocalTime().ToString());
            ResetValues();
            currRecord++;
            LogEntry.WriteLog("<br/> LoadQuestionOnTreeSelect start : ", System.DateTime.Now.ToLocalTime().ToString());
            LoadQuestionOnTreeSelect();
            LogEntry.WriteLog("<br/> LoadQuestionOnTreeSelect ends : ", System.DateTime.Now.ToLocalTime().ToString());
            LogEntry.WriteLog("<br/> Next Button ends : ", System.DateTime.Now.ToLocalTime().ToString());
        }

        private void Last_Click(object sender, EventArgs e)
        {
            LogEntry.WriteLog("<br/> Last Button start : ", System.DateTime.Now.ToLocalTime().ToString());
            ResetValues();
            currRecord=navQuestions.Count-1;
            LogEntry.WriteLog("<br/> LoadQuestionOnTreeSelect start : ", System.DateTime.Now.ToLocalTime().ToString());
            LoadQuestionOnTreeSelect();
            LogEntry.WriteLog("<br/> LoadQuestionOnTreeSelect ends : ", System.DateTime.Now.ToLocalTime().ToString());
            LogEntry.WriteLog("<br/> Last Button ends : ", System.DateTime.Now.ToLocalTime().ToString());
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            LogEntry.WriteLog("<br/> Previous Button start : ", System.DateTime.Now.ToLocalTime().ToString());
            ResetValues();
            currRecord--;
            LogEntry.WriteLog("<br/> LoadQuestionOnTreeSelect start : ", System.DateTime.Now.ToLocalTime().ToString());
            LoadQuestionOnTreeSelect();
            LogEntry.WriteLog("<br/> LoadQuestionOnTreeSelect ends : ", System.DateTime.Now.ToLocalTime().ToString());
            LogEntry.WriteLog("<br/> Previous Button ends : ", System.DateTime.Now.ToLocalTime().ToString());

        }

        private void First_Click(object sender, EventArgs e)
        {
            LogEntry.WriteLog("<br/> Previous Button start : ", System.DateTime.Now.ToLocalTime().ToString());
            ResetValues();
            currRecord = 0;
            LogEntry.WriteLog("<br/> LoadQuestionOnTreeSelect start : ", System.DateTime.Now.ToLocalTime().ToString());
            LoadQuestionOnTreeSelect();
            LogEntry.WriteLog("<br/> LoadQuestionOnTreeSelect ends : ", System.DateTime.Now.ToLocalTime().ToString());
            LogEntry.WriteLog("<br/> First Button ends : ", System.DateTime.Now.ToLocalTime().ToString());
        }

        private void ResetValues()
        {
            textBox1.Text = "";
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox5_CheckedChanged(null, null);
            checkBox6_Click(null, null);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            AnswerInfo.Visible = checkBox5.Checked;
            if (checkBox5.Checked)
            {
                if (checkBox6.Checked == true)
                    isShowAnsweChecked = true;
                else
                    isShowAnsweChecked = false;
                checkBox6.Checked = true;
                checkBox6_Click(null, null);
                checkBox6.Enabled = false;
            }
            else
            {
                if (!isShowAnsweChecked)
                {
                    isShowAnsweChecked = false;
                    checkBox6.Checked = false;
                    checkBox6_Click(null, null);
                }
                checkBox6.Enabled = true;
            }
                // CorrectAnswer.Visible=;
          //CheckBox ckb = this.Controls.OfType<CheckBox>()
               //    .Where(c => c.AccessibleName.Equals(checkName)).First();
          
           // Answer.Visible = checkBox5.Checked;
        }

        private void CorrectAnswerTick()
        {
            if (navQuestions[currRecord].AnswerType == "Multi Choice" || navQuestions[currRecord].AnswerType == "Multi-Image")
            {
                string sAnswer = navQuestions[currRecord].Answer;
                for (int i = 0; i < sAnswer.Split(',').Count(); i++)
                {
                    int x = Convert.ToInt16(sAnswer.Split(',')[i]);
                    switch (x)
                    {
                        case 1:
                            CorrectAnswer.Visible = !checkBox1.Checked;
                            pictureBox13.Visible = checkBox1.Checked;
                            break;

                        case 2:
                            pictureBox10.Visible = !checkBox2.Checked;
                            pictureBox14.Visible = checkBox2.Checked;
                            break;

                        case 3:
                            pictureBox11.Visible = !checkBox3.Checked;
                            pictureBox15.Visible = checkBox3.Checked;
                            break;

                        case 4:
                            pictureBox12.Visible = !checkBox4.Checked;
                            pictureBox16.Visible = checkBox4.Checked;
                            break;

                    }
                }
            }
            else if (navQuestions[currRecord].AnswerType == "Single Choice" || navQuestions[currRecord].AnswerType == "Single-Image")
            {
                
                int x = Convert.ToInt16(navQuestions[currRecord].Answer);
                switch (x)
                {
                    case 1:
                        CorrectAnswer.Visible = !radioButton1.Checked;
                        pictureBox13.Visible = radioButton1.Checked;
                        break;

                    case 2:
                        pictureBox10.Visible = !radioButton2.Checked;
                        pictureBox14.Visible = radioButton2.Checked;
                        break;

                    case 3:
                        pictureBox11.Visible = !radioButton3.Checked;
                        pictureBox15.Visible = radioButton3.Checked;
                        break;

                    case 4:
                        pictureBox12.Visible = !radioButton4.Checked;
                        pictureBox16.Visible = radioButton4.Checked;
                        break;

                }

            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Questions")
            {
                toolStripButton1.BackColor = Color.LightSkyBlue;
                toolStripButton2.BackColor = Color.LightGray;

            }
            else
            {
                toolStripButton2.BackColor = Color.LightSkyBlue;
                toolStripButton1.BackColor = Color.LightGray;
            }
            LoadQuestionOnTreeSelect();
        }

        public void Delete_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 2)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure to delete the question?", "Delete", MessageBoxButtons.YesNo))
                {
                    if (resultQuestions != null)
                    {
                        resultQuestions.objQuestionDetail.Remove(navQuestions[currRecord]);
                        navQuestions.RemoveAt(currRecord);
                        ObjectXMLSerializer<QuestionDetailData>.Save(resultQuestions, Utility.XML_FILE_NAME);
                        RefreshView();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select atleast one leaf node");
            }
        }

        public void Edit_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 2)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure to edit the question?", "Edit", MessageBoxButtons.YesNo))
                {
                          frmMain q = new frmMain();
                       // QuestionsForm q = new QuestionsForm();
                        q.obj = navQuestions[currRecord];
                        q.strSubject = label15.Text;
                        q.strQuestTopic = label16.Text;
                        LogEntry.WriteLog("<br/> Edit start : ", DateTime.Now.ToLongTimeString());
                        q.ShowDialog();
                        navQuestions[currRecord] = q.obj;
                        ObjectXMLSerializer<QuestionDetailData>.Save(resultQuestions, Utility.XML_FILE_NAME);
                        LoadQuestionOnTreeSelect();
                        //RefreshView();
                }
            }
            else
            {
                MessageBox.Show("Select atleast one leaf node");
            }
        }

        public void Add_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 2)
            {
                frmMain q = new frmMain();
               // QuestionsForm q = new QuestionsForm();
                q.obj = new QuestionDetail();
               // q.obj.QuestionOptions = ";;;";
                q.strSubject = label15.Text;
                q.strQuestTopic = label16.Text;

                q.ShowDialog();
                //   q.obj.QuesNo = navQuestions[currRecord].QuesNo;
                if (!string.IsNullOrEmpty(q.obj.Question))
                {
                    q.obj.QuesNo = Convert.ToInt32(treeView1.SelectedNode.Tag);
                    if (resultQuestions != null)
                        q.obj.QuesDetSNo = (resultQuestions.objQuestionDetail.Count + 1);
                    else
                    {
                        q.obj.QuesDetSNo = 1;
                        resultQuestions = new QuestionDetailData();
                        resultQuestions.objQuestionDetail = new List<QuestionDetail>();
                        navQuestions = new List<QuestionDetail>();
                    }

                    //for (int iter = 0; iter < 500; iter++)
                    //{
                        resultQuestions.objQuestionDetail.Add(q.obj);
                        navQuestions.Add(q.obj);
                    //}
                    //  QuestionDetailData obj = new QuestionDetailData();
                    // obj.objQuestionDetail = navQuestions;
                    ObjectXMLSerializer<QuestionDetailData>.Save(resultQuestions, Utility.XML_FILE_NAME);
                    Utility.compress(Utility.XML_FILE_NAME);
                    currRecord = navQuestions.Count-1;
                    LoadQuestionOnTreeSelect();
                    //RefreshView();
                }
            }
            else
            {
                MessageBox.Show("Select atleast one leaf node");
            }

        }
        private void RefreshView()
        {
            //label19.Text = navQuestions.Count.ToString();

            currRecord = 0;
            LoadQuestionOnTreeSelect();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //navQuestions[currRecord].Question = textBox1.Text;
            //groupBox2.Visible = true;
            //groupBox4.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            //groupBox4.Visible = false;
        }

        #region Find By Name

        /// <summary>
        /// Use the treeview's built-in find function
        /// to search for a node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindNode_Click(object sender, EventArgs e)
        {
            ClearBackColor();

            try
            {
                TreeNode[] tn = treeView1.Nodes[0].Nodes.Find(txtNodeSearch.Text, true);
                for (int i = 0; i < tn.Length; i++)
                {
                    treeView1.SelectedNode = tn[i];
                    treeView1.SelectedNode.BackColor = Color.Yellow;
                }
            }
            catch { }
        }

        #endregion

        #region Remove BackColor

        // recursively move through the treeview nodes
        // and reset backcolors to white
        private void ClearBackColor()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                ClearRecursive(n);
            }
        }

        // called by ClearBackColor function
        private void ClearRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.BackColor = Color.White;
                ClearRecursive(tn);
            }
        }

        #endregion

        #region Find By Text

        /// <summary>
        /// Searching for nodes by text requires a special function
        /// this function recursively scans the treeview and
        /// marks matching items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNodeTextSearch_Click(object sender, EventArgs e)
        {
            ClearBackColor();
            FindByText();
        }


        private void FindByText()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursive(n);
            }
        }


        private void FindRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // if the text properties match, color the item
                if (tn.Text == this.txtNodeTextSearch.Text)
                    tn.BackColor = Color.Yellow;

                FindRecursive(tn);
            }
        }

        #endregion

        #region Find By Tag

        /// <summary>
        /// Searching for nodes by tag requires a special function
        /// this function recursively scans the treeview and
        /// marks matching items.  Tags can be object; in this
        /// case they are just used to contain strings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNodeTagSearch_Click(object sender, EventArgs e)
        {
            ClearBackColor();
            FindByTag();
        }


        private void FindByTag()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursiveTag(n);
            }
        }


        private void FindRecursiveTag(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // if the text properties match, color the item
                if (tn.Tag.ToString() == this.txtTagSearch.Text)
                    tn.BackColor = Color.Yellow;

                FindRecursiveTag(tn);
            }
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    if (Convert.ToInt16(textBox1.Text) <= navQuestions.Count)
                    {
                        currRecord = Convert.ToInt16(textBox1.Text) - 1;
                        LoadQuestionOnTreeSelect();

                    }
                    else
                        MessageBox.Show("Please enter valid question number");
                }
                else
                    MessageBox.Show("Please enter valid question number");
            }
            catch (Exception ex)
            {
                LogEntry.WriteLog(ex, "Thread Exception");
                MessageBox.Show("Please enter valid question number");
            }
        }

        private void checkBox6_Click(object sender, EventArgs e)
        {
            CorrectAnswer.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;

            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox15.Visible = false;
            pictureBox16.Visible = false;

            if (checkBox6.Checked)
            {
                CorrectAnswerTick();

            }
            else
            {

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utility.isNumeric(e.KeyChar);
        }

       

    }
}