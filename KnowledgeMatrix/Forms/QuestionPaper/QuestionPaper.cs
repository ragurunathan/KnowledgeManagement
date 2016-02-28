using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExampApp.Database;
using KnowledgeMatrix.Database;
using System;
using System.ComponentModel;
using System.IO;
using KnowledgeMatrix.Framework;
using System.Drawing;
namespace KnowledgeMatrix.Forms
{
    public partial class QuestionPaper : UserControl
    {
        public List<QuestionMast> getParent;
        public List<QuestionMast> getFirstChild;
        public List<QuestionMast> getSecondChild;
        public QuestionDetailData resultQuestions;
        public QuestionDetailData resultQuestionsFltr;
        private QuestionDetailData objQuestions = new QuestionDetailData();
        private QuestionPaperDisplay obj;
        public int rootImageIndex = 0;
        public int selectedCustomerImageIndex = 1;
        public QuestionPaperColl objQuestionPaperColl= new QuestionPaperColl();
        public QuestionsData result;
        private BindingSource bs;
        private List<int> questionMadeZero = new List<int>();
        int questions = 0, questionAvail = 0, qCompQAval = 0;
        private bool isReset = true;
        private bool isPrintPreviewTaken = false;

        public QuestionPaper()
        {
            InitializeComponent();

            this.Size = new System.Drawing.Size(Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelWidth), Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelHeight));
            ClearAll();
              
        }

        private void ClearAll()
        {
            objQuestionPaperColl.objPaper = new List<QuestionPap>();
            toolStripButton1.BackColor = Color.LightSkyBlue;
            toolStripButton2.BackColor = Color.LightGray;
            LoadTree();
            //   LoadQuestionFromXml();
            dataGridView1.AutoGenerateColumns = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private List<QuestionMast> LoadTreeInfo(int Level,int ParentId,bool includeSelf)
        {
            List<QuestionMast> q=new List<QuestionMast>();
            
            if(Level ==0)
                  q= (from QuestionMast in result.objQuestionMas
                              where QuestionMast.ParentParentQuestionNo == 0 && QuestionMast.ParentQuestionNo == 0
                              select QuestionMast).ToList();
            else
               q= (from QuestionMast in result.objQuestionMas
                 where QuestionMast.ParentQuestionNo == ParentId || (QuestionMast.QuesNo == ParentId && includeSelf)
                 select QuestionMast).ToList();
            return q;
        }

        private string GetTopicName(int QuesNo)
        {
            

            List<QuestionMast>  q = (from QuestionMast in result.objQuestionMas
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
                getParent = LoadTreeInfo(0, 0,true);
                //(from QuestionMast in result.objQuestionMas
                // where QuestionMast.ParentParentQuestionNo == i && QuestionMast.ParentQuestionNo == j
                // select QuestionMast).ToList();

                for (int prntCnt = 0; prntCnt < getParent.Count; prntCnt++)
                {
                    if ((Utility.IsAdmin()) || (!string.IsNullOrWhiteSpace(getParent[prntCnt].QuesBankGen) && (getParent[prntCnt].QuesBankGen == "Purchased") && !string.IsNullOrWhiteSpace(getParent[prntCnt].QuesBankDate) && Convert.ToDateTime(getParent[prntCnt].QuesBankDate).AddDays(KnowledgeMatrix.Properties.Settings.Default.DaysToAdd) >= System.DateTime.Now))
                    {
                    //Add to tree
                    TreeNode treeNode = new TreeNode(getParent[prntCnt].Name);
                    treeNode.Tag = getParent[prntCnt].QuesNo.ToString();
                    treeNode.ImageIndex = rootImageIndex;
                    treeNode.SelectedImageIndex = 3;

                    treeView1.Nodes.Add(treeNode);
                    //For each of them get the child and pass the node to be added if > 0

                    getFirstChild = LoadTreeInfo(1, getParent[prntCnt].QuesNo,false);
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
                        getSecondChild = LoadTreeInfo(2, getFirstChild[childCnt].QuesNo,false);
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
            
        private void LoadQuestionFromXml(string FileName)
        {
            resultQuestions = null;
            groupBox1.GroupTitle = FileName;
            if(Utility.IsAdmin())
                FileName = Application.StartupPath + Utility.FolderType() + @"QuestionBank\" + FileName + ".txt";
            else
                FileName = Application.StartupPath + Utility.FolderType() + @"QuestionPaper\" + FileName + ".txt";


            if (File.Exists(FileName))
            {
                resultQuestions = ObjectXMLSerializer<QuestionDetailData>.Load(FileName);
                resultQuestions.objQuestionDetail = (from QuestionDetailData in resultQuestions.objQuestionDetail
                                                     where (QuestionDetailData.ModuleName == Utility.MOD_ALL || QuestionDetailData.ModuleName == Utility.MOD_MOCK_TST)
                                          select QuestionDetailData).ToList();
                        
                resultQuestionsFltr = ObjectXMLSerializer<QuestionDetailData>.Load(FileName);
                resultQuestionsFltr.objQuestionDetail = (from QuestionDetailData in resultQuestionsFltr.objQuestionDetail
                                                         where (QuestionDetailData.ModuleName == Utility.MOD_ALL || QuestionDetailData.ModuleName == Utility.MOD_MOCK_TST)
                                                     select QuestionDetailData).ToList();
            }
            //Apply Filter
            if (resultQuestionsFltr != null && resultQuestionsFltr.objQuestionDetail.Count() > 0)
            {
                //QUestion Type
               // resultQuestionsFltr = resultQuestions;
             //   if (comboBox1.Text == "" && comboBox2.Text == "")
               //   resultQuestionsFltr.objQuestionDetail.Clear();

                if(comboBox1.Text != "ALL")
                    resultQuestionsFltr.objQuestionDetail=
                 (from QuestionDetail in resultQuestionsFltr.objQuestionDetail
                  where QuestionDetail.AnswerType == comboBox1.Text
                  select QuestionDetail).ToList();

                //Question Complexity
                if (comboBox2.Text != "ALL")
                    resultQuestionsFltr.objQuestionDetail =
                 (from QuestionDetail in resultQuestionsFltr.objQuestionDetail
                  where QuestionDetail.Complexity.ToLower() == comboBox2.Text.ToLower()
                  select QuestionDetail).ToList();
            }
            Utility.XML_FILE_NAME = FileName;

            //resultQuestions = ObjectXMLSerializer<QuestionDetailData>.Load(XML_FILE_NAME);
        }
        private int GetQuestionCount(int quesNo)
        {
            int i = 0;
            if (resultQuestions != null)
            {
                //Load the Question Based on the Question Number
                i = (from QuestionDetail in resultQuestions.objQuestionDetail
                         where QuestionDetail.QuesNo == quesNo
                         select QuestionDetail).ToList().Count();

            } return i;
        }

        private int GetQuestionCountWithFilter(int quesNo)
        {
            int i=0;
            if (resultQuestionsFltr != null && resultQuestionsFltr.objQuestionDetail.Count() > 0)
            {
                //Load the Question Based on the Question Number
                i = (from QuestionDetail in resultQuestionsFltr.objQuestionDetail
                     where QuestionDetail.QuesNo == quesNo
                     select QuestionDetail).ToList().Count();
            }
                return i;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (toolStripButton1.BackColor == Color.LightSkyBlue)
            {
                enablePaperButtons();
            
            isPrintPreviewTaken = false;
            if (e != null)
            {
                questionMadeZero.Clear();
                isReset = true;
            }
            bs = new BindingSource();
            bs.DataSource = typeof(QuestionPap);
             questions = 0;
             questionAvail = 0;
            qCompQAval=0;
            if (treeView1.SelectedNode != null)
            {
                //if level is 0
                if (treeView1.SelectedNode.Level == 0)
                {
                    LoadQuestionFromXml(treeView1.SelectedNode.Text);

                    objQuestionPaperColl.objPaper.Clear();

                    QuestionPap objF = new QuestionPap();
                    objF.QuestionNo = Convert.ToInt32(treeView1.SelectedNode.Tag);
                    objF.Name = treeView1.SelectedNode.Text;

                    objF.Questions = GetQuestionCount(Convert.ToInt32(treeView1.SelectedNode.Tag));

                    if (!questionMadeZero.Contains(objF.QuestionNo))
                    {
                        questions += objF.Questions;
                        objF.Select = objF.Questions > 0 ? true : false;
                        objF.Flag = objF.Questions > 0 ? "A" : "N";
                        objF.QuestionTypeAva = GetQuestionCountWithFilter(Convert.ToInt32(treeView1.SelectedNode.Tag));
                        questionAvail += objF.QuestionTypeAva;
                        objF.QCompQAval = Math.Min(objF.Questions, objF.QuestionTypeAva);
                        qCompQAval += objF.QCompQAval;
                       // objF.AlterQNo = objF.Questions;
                        objF.AlterQNo = objF.QCompQAval;

                    }
                    else
                        objF.Flag = "D";

                    objQuestionPaperColl.objPaper.Add(objF);

                    getFirstChild =
                    (from QuestionMast in result.objQuestionMas
                     where QuestionMast.ParentQuestionNo == Convert.ToInt32(treeView1.SelectedNode.Tag)
                     select QuestionMast).ToList();
                    for (int childCnt = 0; childCnt < getFirstChild.Count; childCnt++)
                    {
                        QuestionPap obj = new QuestionPap();
                        obj.QuestionNo = getFirstChild[childCnt].QuesNo;
                        obj.Name = getFirstChild[childCnt].Name;
                        if (!questionMadeZero.Contains(obj.QuestionNo))
                        {
                            obj.Questions = GetQuestionCount(getFirstChild[childCnt].QuesNo);
                            questions += obj.Questions;
                            obj.Select = obj.Questions > 0 ? true : false;
                            obj.Flag = obj.Questions > 0 ? "A" : "N";
                            obj.QuestionTypeAva = GetQuestionCountWithFilter(getFirstChild[childCnt].QuesNo);
                            questionAvail += obj.QuestionTypeAva;
                            obj.QCompQAval = Math.Min(obj.Questions, obj.QuestionTypeAva);
                            qCompQAval += obj.QCompQAval;
                            //obj.AlterQNo = obj.Questions;
                            obj.AlterQNo = obj.QCompQAval;
                        }
                        else
                            obj.Flag = "D";
                        objQuestionPaperColl.objPaper.Add(obj);

                        //Add the Subchild
                        getSecondChild =
             (from QuestionMast in result.objQuestionMas
              where QuestionMast.ParentQuestionNo == getFirstChild[childCnt].QuesNo
              select QuestionMast).ToList();
                        for (int childSecondCnt = 0; childSecondCnt < getSecondChild.Count; childSecondCnt++)
                        {
                            QuestionPap obj1 = new QuestionPap();
                            obj1.QuestionNo = getSecondChild[childSecondCnt].QuesNo;
                            obj1.Name = getSecondChild[childSecondCnt].Name;
                            if (!questionMadeZero.Contains(obj1.QuestionNo))
                            {
                                obj1.Questions = GetQuestionCount(getSecondChild[childSecondCnt].QuesNo);
                                questions += obj1.Questions;
                                obj1.Select = obj1.Questions > 0 ? true : false;
                                obj1.Flag = obj1.Questions > 0 ? "A" : "N";
                                obj1.QuestionTypeAva = GetQuestionCountWithFilter(getSecondChild[childSecondCnt].QuesNo);
                                questionAvail += obj1.QuestionTypeAva;
                                obj1.QCompQAval = Math.Min(obj1.Questions, obj1.QuestionTypeAva);
                                qCompQAval += obj1.QCompQAval;
                                obj1.AlterQNo = obj1.Questions;
                            }
                                  objQuestionPaperColl.objPaper.Add(obj1);
                        }
                    }
                    /////

                }
                else if (treeView1.SelectedNode.Level == 1 || treeView1.SelectedNode.Level == 2)
                {
                    objQuestionPaperColl.objPaper.Clear();
                
                    if (treeView1.SelectedNode.Level == 1)
                        LoadQuestionFromXml(treeView1.SelectedNode.Parent.Text);
                    else
                        LoadQuestionFromXml(treeView1.SelectedNode.Parent.Parent.Text);

                    getSecondChild = LoadTreeInfo(2, Convert.ToInt32(treeView1.SelectedNode.Tag), true);

                    for (int i = 0; i < getSecondChild.Count; i++)
                    {
                        QuestionPap obj = new QuestionPap();
                        obj.QuestionNo = getSecondChild[i].QuesNo;
                        obj.Name = getSecondChild[i].Name;
                        if (!questionMadeZero.Contains(obj.QuestionNo))
                        {
                            obj.Questions = GetQuestionCount(getSecondChild[i].QuesNo);
                            questions += obj.Questions;
                            obj.Select = obj.Questions > 0 ? true : false;
                            obj.Flag = obj.Questions > 0 ? "A" : "N";
                            obj.QuestionTypeAva = GetQuestionCountWithFilter(getSecondChild[i].QuesNo);
                            questionAvail += obj.QuestionTypeAva;
                            obj.QCompQAval = Math.Min(obj.Questions, obj.QuestionTypeAva);
                            qCompQAval += obj.QCompQAval;

                           // obj.AlterQNo = obj.Questions;
                            obj.AlterQNo = obj.QCompQAval;

                        }
                        else
                            obj.Flag = "D";
                        objQuestionPaperColl.objPaper.Add(obj);
                    }



                }

                //if level is 1
                else
                {
                    objQuestionPaperColl.objPaper.Clear();

                }
                CalculateTotal();

                
            }
            //if level is 2 do nothing
           dataGridView1.DataSource = bs;
           dataGridView1.AutoGenerateColumns = true; // create columns automatically //**
           dataGridView1.Columns[0].Visible = false;
           dataGridView1.Columns[dataGridView1.Columns.Count-1].Visible = false;
           dataGridView1.RowHeadersWidth = 25;
           dataGridView1.Columns[0].Width = 10;
           dataGridView1.Columns[1].Width = 45;
           dataGridView1.Columns[2].Width = 300;

           dataGridView1.Columns[2].HeaderText = "Syllabus / Topics";
           dataGridView1.Columns[3].HeaderText = "Total Questions";
           dataGridView1.Columns[5].HeaderText = "Available Questions";
           dataGridView1.Columns[6].HeaderText = "Planned Questions - Auto";
            dataGridView1.Columns[7].HeaderText = "Available Weightage";
            
                dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[7].Visible = false;
           
                dataGridView1.Columns[8].HeaderText = "Alter Weightage";
                dataGridView1.Columns[8].Visible = false;

           dataGridView1.Columns[9].HeaderText = "Planned Questions - Manual";

            for(int j=3;j<dataGridView1.Columns.Count;j++)
                dataGridView1.Columns[j].Width=60 ;

            if (panel1.Visible)
                panel1.SendToBack();
            }
            else
            {
                panel1.Visible = true;


                if (panel1.Controls.Count == 0)
                {

                    obj = new QuestionPaperDisplay();

                    //obj.resultQuestions.objQuestionDetail = navQuestions;
                    //obj.strSubject = label15.Text;
                    //obj.strQuestTopic = label16.Text;
                    panel1.Controls.Add(obj);
                }
                else
                {

                    //obj.resultQuestions.objQuestionDetail = navQuestions;
                    //obj.strSubject = label15.Text;
                    //obj.strQuestTopic = label16.Text;
                    obj.LoadTextData();
                }
                panel1.BringToFront();
            }
        }

        private void CalculateTotal()
        {
            //Add Footer
            if (objQuestionPaperColl.objPaper.Count > 0)
            {
                QuestionPap obj = new QuestionPap();

                obj.Name = "Total";

                obj.Questions = questions;
                obj.Weightage = string.Format("{0:0%}", 100);
                obj.QuestionTypeAva = questionAvail;

                obj.AlterQNo = qCompQAval;
                //obj.AlterQNo = questions;
                obj.QCompQAval = qCompQAval;
                if (qCompQAval > 0)
                    obj.AvaWeitage = string.Format("{0:0%}", 100);
                else
                    obj.AvaWeitage = string.Format("{0:0%}", 0);
                objQuestionPaperColl.objPaper.Add(obj);
            }

            label6.Text = questions.ToString();

            label13.Text = qCompQAval.ToString();
            if (isReset)
            {
                textBox1.Text = label13.Text;
                isReset = false;
            }
            else if (string.IsNullOrWhiteSpace(textBox1.Text))
                  textBox1.Text = label13.Text;
            else if(qCompQAval < Convert.ToInt16(textBox1.Text))
                textBox1.Text = label13.Text;


            double adjFact = (double)(Convert.ToDouble(textBox1.Text) / qCompQAval);
            int nQuestioncnt = 0;
            for (int k = 0; k < objQuestionPaperColl.objPaper.Count; k++)
            {
                if (questions > 0)
                {
                    objQuestionPaperColl.objPaper[k].Weightage = string.Format("{0:0%}", Math.Round((double)((double)objQuestionPaperColl.objPaper[k].Questions / (double)questions), 3));
                    
                }
                else
                {
                    
                    objQuestionPaperColl.objPaper[k].Weightage = string.Format("{0:0%}", 0);
                }

                if (qCompQAval > 0)
                {
                    objQuestionPaperColl.objPaper[k].AvaWeitage = string.Format("{0:0%}", Math.Round((double)(((double)objQuestionPaperColl.objPaper[k].QCompQAval / (double)qCompQAval)), 3));
                    objQuestionPaperColl.objPaper[k].QCompQAval = (int)(Math.Round((double)objQuestionPaperColl.objPaper[k].QCompQAval * adjFact));
                    if(k != objQuestionPaperColl.objPaper.Count-1)
                        nQuestioncnt = nQuestioncnt + objQuestionPaperColl.objPaper[k].QCompQAval;
                    objQuestionPaperColl.objPaper[k].AlterWeitage = objQuestionPaperColl.objPaper[k].Weightage;
                }
                else
                {
                    objQuestionPaperColl.objPaper[k].AvaWeitage = string.Format("{0:0%}", 0);
                    objQuestionPaperColl.objPaper[k].AlterWeitage = string.Format("{0:0%}", 0);
                }
                
            }

            if (nQuestioncnt != objQuestionPaperColl.objPaper[objQuestionPaperColl.objPaper.Count - 1].QCompQAval)
            {                
                objQuestionPaperColl.objPaper[objQuestionPaperColl.objPaper.Count - 1].QCompQAval = nQuestioncnt;
                for (int k = 0; k < objQuestionPaperColl.objPaper.Count-1; k++)
                {
                    objQuestionPaperColl.objPaper[k].AvaWeitage = string.Format("{0:0%}", Math.Round((double)((double)objQuestionPaperColl.objPaper[k].QCompQAval / (double)nQuestioncnt), 3));
                }
                MessageBox.Show("The planned question varies with the distribution of questions", "Info");
            }
            

            for (int k = 0; k < objQuestionPaperColl.objPaper.Count; k++)
                bs.Add(objQuestionPaperColl.objPaper[k]);
        }
    

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
           if (e.ColumnIndex > 1 && e.ColumnIndex <= 6)
                e.Cancel = true;
             //if (comboBox3.Text == "Auto" &&
           // DataGridViewCheckBoxCell obj11 = (DataGridViewCheckBoxCell)dataGridView1[e.ColumnIndex, e.RowIndex];
           if ((comboBox3.Text == "Auto" || comboBox3.Text == "Fixed") && (e.ColumnIndex == 9 || e.ColumnIndex == 8))
               e.Cancel = true;
           else
               isPrintPreviewTaken = false;
        }

    

    

        private void comboBox1_Click(object sender, EventArgs e)
        {
            treeView1_AfterSelect(null, null);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            isReset = true;
            treeView1_AfterSelect(null, null);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isReset = true;
            treeView1_AfterSelect(null, null);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {
                    //   DataGridViewDisableButtonCell buttonCell =
                    //  (DataGridViewDisableButtonCell)dataGridView1.
                    // Rows[e.RowIndex].Cells["Buttons"];

                    DataGridViewCheckBoxCell checkCell =
                        (DataGridViewCheckBoxCell)dataGridView1.
                        Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // buttonCell.Enabled = !(Boolean)checkCell.Value;

                    dataGridView1.Invalidate();
                    if ((Boolean)checkCell.Value)
                        questionMadeZero.Remove(objQuestionPaperColl.objPaper[e.RowIndex].QuestionNo);
                    else
                        questionMadeZero.Add(objQuestionPaperColl.objPaper[e.RowIndex].QuestionNo);

                    treeView1_AfterSelect(null, null);
                    //   bs.Clear();
                    // for(int mbox=3;mbox<4;mbox++)
                    //  dataGridView1[e.ColumnIndex, e.RowIndex].Value = 0;
                    //    CalculateTotal();
                    //MessageBox.Show(((Boolean)checkCell.Value).ToString());

                }
                if (e.ColumnIndex == 9 && e.RowIndex != -1)
                {
                    int subTota = 0;
                    for (int m = 0; m < dataGridView1.Rows.Count - 2; m++)
                    {
                        subTota += objQuestionPaperColl.objPaper[m].AlterQNo;
                    }
                    label6.Text = subTota.ToString();
                    for (int m = 0; m < dataGridView1.Rows.Count - 2; m++)
                    {
                        int l = Convert.ToInt16(dataGridView1[9, m].Value);
                        if (subTota > 0)
                            dataGridView1[8, m].Value = string.Format("{0:0%}", Math.Round((double)((double)l / (double)subTota), 3));
                        else
                            dataGridView1[8, m].Value = "0%";
                    }
                    dataGridView1[9, dataGridView1.Rows.Count - 2].Value = subTota;
                    if (subTota == 0)
                        dataGridView1[8, dataGridView1.Rows.Count - 2].Value = string.Format("{0:0%}", 0);
                    else
                        dataGridView1[8, dataGridView1.Rows.Count - 2].Value = "100%";

                    textBox1.Text = label6.Text;
                }
            }
        }

    

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //if(!((new System.Collections.ArrayList.ArrayListDebugView(((System.Windows.Forms.BaseCollection)(((System.Windows.Forms.DataGridView)(sender)).SelectedCells)).List)).Items[0].ToString().Contains("TextBox")))
            if (((System.Windows.Forms.DataGridView)(sender)).CurrentCellAddress.X == 1 && dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                isReset = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(this.dataGridView1.Columns[e.ColumnIndex].Name == "QuestionTypeAva")
                e.CellStyle.BackColor = Color.SandyBrown;
            //e.CellStyle.BackColor = Color.Pink;
            if ((comboBox3.Text == "Auto" || comboBox3.Text == "Fixed") && ((this.dataGridView1.Columns[e.ColumnIndex].Name == "QCompQAval") || (this.dataGridView1.Columns[e.ColumnIndex].Name == "AvaWeitage")))
            {
                e.CellStyle.BackColor = Color.Pink;
                //if (e.Value != null)
                //{
                //    if (objQuestionPaperColl.objPaper[e.RowIndex].Flag == "D")
                //    {
                //        e.CellStyle.BackColor = Color.Pink;
                //    }
                //}
            }
            else if ((comboBox3.Text == "Manual") && ((this.dataGridView1.Columns[e.ColumnIndex].Name == "AlterQNo") || (this.dataGridView1.Columns[e.ColumnIndex].Name == "AlterWeitage")))
            {
                e.CellStyle.BackColor = Color.Cyan;
            }
          /*  for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (objQuestionPaperColl.objPaper[i].Flag == "D")
                    //row.Cells[col.Index].Style.BackColor = Color.Green; //doesn't work
                    //col.Cells[row.Index].Style.BackColor = Color.Green; //doesn't work
                    dataGridView1[0, i].Style.BackColor = Color.Green; //doesn't work

            } */

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            isReset = false;
            if (comboBox3.SelectedIndex == 1)
                isReset = true;
            treeView1_AfterSelect(null, null);
            textBox1.Enabled = true;
            if (comboBox3.SelectedIndex == 1)
                textBox1.Enabled = false;
           // dataGridView1.DataSource = bs;
            if (comboBox3.SelectedIndex <= 1)
                comboBox3.BackColor = Color.Pink;
            else
                comboBox3.BackColor = Color.Cyan;
        }

        private void dataGridView1_CellBeginEdit_1(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            isReset = false;
            treeView1_AfterSelect(null, null);
        }

        private List<QuestionDetail> GetQuestionInfo(int quesNo)
        {
            //Load the Question Based on the Question Number
            List<QuestionDetail> obj = (from QuestionDetail in resultQuestionsFltr.objQuestionDetail
                     where QuestionDetail.QuesNo == quesNo
                     select QuestionDetail).ToList();
            return obj;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            SaveToListOrMock(Application.StartupPath+ Utility.FolderType() + @"QuestionPaper\QuestionInfo.txt");
        }

        private void SaveToListOrMock(string FileName)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please enter the Exam Name", "Error");
                return;
            }
            
            if (dateTimePicker1.Enabled == true)
            {
                if (dateTimePicker1.Value.TimeOfDay.ToString() == "00:00:00")
                {
                    MessageBox.Show("Please enter the Exam Duration", "Error");
                    return;
                }
            }

            if (comboBox4.SelectedIndex == 1 || comboBox4.SelectedIndex == 2)
            {
                if (textBox2.Enabled == true)
                {
                    if (string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        MessageBox.Show("Please enter the Exam Percentage", "Error");
                        return;
                    }
                }


            }
            if (!isPrintPreviewTaken)
            {
                GenerateQuestions();
                isPrintPreviewTaken = true;
            }

            //GenerateQuestions();
            if (!Directory.Exists(Application.StartupPath + @"\QuestionPaper\"))
                Directory.CreateDirectory(Application.StartupPath + @"\QuestionPaper\");

            
            //Create Object for the QuestionManagement 
            QuestionManagementColl objQuestionManagementColl;
            if (File.Exists(FileName))
                objQuestionManagementColl = ObjectXMLSerializer<QuestionManagementColl>.Load(FileName);
            else
            {
                objQuestionManagementColl = new QuestionManagementColl();
                objQuestionManagementColl.objQuestionManagement = new List<QuestionManagement>();
            }


            int i = (from QuestionManagement in objQuestionManagementColl.objQuestionManagement
                     where QuestionManagement.ExamName == textBox3.Text
                     select QuestionManagement).ToList().Count();

            if (i > 0)
            {
                MessageBox.Show("Already the exam name exists!!!", "Error");
                return;
            }

            QuestionManagement objQuestionManagement = new QuestionManagement();
            //for (int ik = 0; ik < objQuestions.objQuestionDetail.Count(); ik++)
            //{
            //    objQuestions.objQuestionDetail[ik].CategoryName = GetTopicName(objQuestions.objQuestionDetail[ik].QuesNo);
            //}

            if (FileName.Contains(@"\MockTest\"))
            {

                ObjectXMLSerializer<QuestionDetailData>.Save(objQuestions, Application.StartupPath + Utility.FolderType() + @"MockTest\" + textBox3.Text + ".txt");
                objQuestionManagement.FileName = Application.StartupPath +Utility.FolderType() +@"MockTest\" + textBox3.Text + ".txt";
                objQuestionManagement.TestTime = dateTimePicker1.Text;
            }
            else
            {
                ObjectXMLSerializer<QuestionDetailData>.Save(objQuestions, Application.StartupPath + Utility.FolderType() + @"QuestionPaper\" + textBox3.Text + ".txt");
                objQuestionManagement.FileName = Application.StartupPath + Utility.FolderType() + @"QuestionPaper\" + textBox3.Text + ".txt";
            }
            
            objQuestionManagement.QuestionMngNo = objQuestionManagementColl.objQuestionManagement.Count + 1;
            objQuestionManagement.ExamName = textBox3.Text;
            if (treeView1.SelectedNode.Level == 0)
                objQuestionManagement.QuestionTopic = treeView1.SelectedNode.Text;

            if (treeView1.SelectedNode.Level == 1)
            {
                objQuestionManagement.QuestionTopic = treeView1.SelectedNode.Parent.Text;
                objQuestionManagement.Subject = treeView1.SelectedNode.Text;
            }
            if (treeView1.SelectedNode.Level == 2)
            {
                objQuestionManagement.QuestionTopic = treeView1.SelectedNode.Parent.Parent.Text;
                objQuestionManagement.Subject = treeView1.SelectedNode.Text;
            }


            objQuestionManagement.ExamPasPercentageScore = textBox2.Text;
            objQuestionManagement.ExamMode = comboBox4.Text;
            objQuestionManagement.QuestionComplexity = comboBox2.Text;
            objQuestionManagement.QuestionType = comboBox1.Text;
            objQuestionManagement.TotalQuestions = textBox1.Text;
            
            objQuestionManagementColl.objQuestionManagement.Add(objQuestionManagement);

            ObjectXMLSerializer<QuestionManagementColl>.Save(objQuestionManagementColl, FileName);
            MessageBox.Show("Data saved successfully");
            enablePaperButtons();
        }
        private void GenerateQuestions()
        {
            string quesmode = comboBox3.Text;
            int noOfQuesUsed = 0, noOfQuesAvlb = 0;
            
            objQuestions.objQuestionDetail = new List<QuestionDetail>();

            for (int i = 0; i < objQuestionPaperColl.objPaper.Count; i++)
            {
                noOfQuesUsed = 0;
                noOfQuesAvlb = 0;
                //include which is checked
                if (objQuestionPaperColl.objPaper[i].Select)
                {
                    noOfQuesAvlb = objQuestionPaperColl.objPaper[i].QuestionTypeAva;

                    if (quesmode == "Auto" || quesmode == "Fixed")
                        noOfQuesUsed = objQuestionPaperColl.objPaper[i].QCompQAval;
                    else
                        noOfQuesUsed = objQuestionPaperColl.objPaper[i].AlterQNo;

                    List<QuestionDetail> objQuestionDetail = GetQuestionInfo(objQuestionPaperColl.objPaper[i].QuestionNo);
                    if (label13.Text == textBox1.Text && quesmode == "Fixed")
                    {
                        for (int l = 0; l < noOfQuesAvlb; l++)
                            objQuestions.objQuestionDetail.Add(objQuestionDetail[l]);
                        
                    }
                    else
                    {
                        //Use the Code to generate the list
                        List<int> questionConsidered = GetListUses(noOfQuesUsed, noOfQuesAvlb);

                        for (int l = 0; l < questionConsidered.Count; l++)
                            objQuestions.objQuestionDetail.Add(objQuestionDetail[questionConsidered[l]]);
                    }
                }

            }
            for (int ik = 0; ik < objQuestions.objQuestionDetail.Count(); ik++)
            {
                objQuestions.objQuestionDetail[ik].CategoryName = GetTopicName(objQuestions.objQuestionDetail[ik].QuesNo);
            }

        }
        public List<int> GetListUses(int noOfQuesUsed, int noOfQuesAvlb)
        {

            List<int> myValues = new List<int>();//new int[] { 0, 1, 2, 3, 4, 5, 6 });
            for (int k = 0; k < noOfQuesAvlb; k++)
                myValues.Add(k);

            IEnumerable<int> result = myValues.Shuffle().Take(noOfQuesUsed);
            // Extension methods can convert IEnumerable<int>
            List<int> list = result.ToList();
            return list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DisplayQuestion(true, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisplayQuestion(true, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisplayQuestion(false, true);
        }

        public void DisplayQuestion(bool ShowQuestion, bool ShowAnswer)
        {
            if (!isPrintPreviewTaken)
            {
                GenerateQuestions();
                isPrintPreviewTaken = true;
            }

            FrmQuestionsDisplay obj = new FrmQuestionsDisplay();
            //obj.resultQuestions.objQuestionDetail.Clear();
            obj.resultQuestions.objQuestionDetail = objQuestions.objQuestionDetail;
            if (treeView1.SelectedNode.Level == 0)
                obj.strQuestTopic = treeView1.SelectedNode.Text;

            if (treeView1.SelectedNode.Level == 1)
            {
                obj.strQuestTopic = treeView1.SelectedNode.Parent.Text;
                obj.strSubject = treeView1.SelectedNode.Text;
            }
            if (treeView1.SelectedNode.Level == 2)
            {
                obj.strQuestTopic = treeView1.SelectedNode.Parent.Parent.Text;
                obj.strSubject = treeView1.SelectedNode.Text;
            }

          //  obj.strSubject = groupBox1.GroupTitle;// "";
           // obj.strQuestTopic = "";
            obj.printAnswer = ShowAnswer;
            obj.printQuestion = ShowQuestion;
            obj.ShowDialog();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Manage Questions")
            {
                toolStripButton1.BackColor = Color.LightSkyBlue;
                toolStripButton2.BackColor = Color.LightGray;

            }
            else
            {
                toolStripButton2.BackColor = Color.LightSkyBlue;
                toolStripButton1.BackColor = Color.LightGray;
            }
            isReset = true;
            treeView1_AfterSelect(null, null);
           // LoadQuestionOnTreeSelect();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveToListOrMock(Application.StartupPath +Utility.FolderType() +@"MockTest\MockTestList.txt");
        }

        private void enablePaperButtons()
        {
            bool enableSave = true;
            if (!Utility.IsAdmin())
            {
                if (File.Exists(Application.StartupPath + Utility.FolderType() + @"QuestionPaper\QuestionInfo.txt"))
                {
                    QuestionManagementColl objQuestionManagementColl = ObjectXMLSerializer<QuestionManagementColl>.Load(Application.StartupPath + Utility.FolderType() + @"QuestionPaper\QuestionInfo.txt");
                    if (objQuestionManagementColl != null && objQuestionManagementColl.objQuestionManagement.Count() >= Convert.ToInt16(KnowledgeMatrix.Properties.Settings.Default.QuestionPaperQuestions))
                        enableSave = false;
                }
            }
            button2.Enabled = enableSave;
            button6.Enabled = enableSave;
            button3.Enabled = enableSave;
            button4.Enabled = enableSave;
            button5.Enabled = enableSave;
            button7.Enabled = enableSave;
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            enablePaperButtons();

         
            AuthorizationOperations obj1 = new AuthorizationOperations();
            button6.Enabled = true && obj1.isUserAccessible(OperationNames.TypeOfOperations.Mock_Exam); 
           // button7.Enabled=true;
            dateTimePicker1.Enabled = true;
            label4.Text = "Exam Duration(HH:MM): *";
            textBox2.Enabled = true;
            label12.Text = "Exam Pass Percentage Score: ";
            if (comboBox4.SelectedIndex == 1 || comboBox4.SelectedIndex == 2)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button7.Enabled = false;
                label12.Text = "Exam Pass Percentage Score: *";
                
                
            }
            else if (comboBox4.SelectedIndex == 3)
            {
                button6.Enabled = false;
                dateTimePicker1.Text = "00:00";
                label4.Text = "Exam Duration(HH:MM):";
                dateTimePicker1.Enabled = false;
                textBox2.Enabled = false;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DisplayQuestion(false, false);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utility.isNumeric(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  ClearAll();
            //dataGridView1.AutoGenerateColumns = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Text = "00:00";
            
        }

      
    }
    
}
