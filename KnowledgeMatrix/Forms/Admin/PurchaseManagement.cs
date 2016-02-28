using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using KnowledgeMatrix.Database;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using KnowledgeMatrix.Cryptography;
using System.Reflection;
using System.Text;
using KnowledgeMatrix.Framework;
using ExampApp.Database;
using System.Drawing;
namespace KnowledgeMatrix.Forms
{
    public partial class PurchaseManagement : UserControl
    {
     
       private List<QuestionMast> getParent;
       private string XML_FILE_NAME = Application.StartupPath + @"\QuestionMaster.txt";
       private string XML_FILE_NAME1 = Application.StartupPath + @"\QuestionMaster1.txt";
       private int RowIndex = 0;
       private QuestionsData result;
       private int UserId;
        public PurchaseManagement()
        {
            InitializeComponent();
            #region DataGrid Definition
            //   getParent = ObjectXMLSerializer<QuestionsData>.Load(XML_QUESTION_NAME); 
            label1.Text = "System IP : " + EntropyGenerator.GetIPForMachine();
            this.Size = new System.Drawing.Size(Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelWidth), Int32.Parse(KnowledgeMatrix.Properties.Settings.Default.PanelHeight));
            if (File.Exists(Utility.XML_QUESTION_NAME))
                result = ObjectXMLSerializer<QuestionsData>.Load(Utility.XML_QUESTION_NAME);
            else
            {
                MessageBox.Show("Kindly validate the license. Contact System Administrator");
                return;
            }
            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.ReadOnly = true;
            col0.HeaderText = "Product Name";
            col0.Name = "ProductName";
            dataGridView1.Columns.Add(col0);
            this.dataGridView1.Columns[0].Width = 445;

            DataGridViewDisableButtonColumn buttonColumn =
              new DataGridViewDisableButtonColumn();
            buttonColumn.HeaderText = "Km-Knowledge Tutor";
            buttonColumn.Name = "eTutor";
            buttonColumn.Text = "Purchase";
            buttonColumn.Tag = "eTutor";
            dataGridView1.Columns.Add(buttonColumn);


            DataGridViewDisableButtonColumn buttonColumn1 =
new DataGridViewDisableButtonColumn();
            buttonColumn1.HeaderText = "Km- Knowledge Base";
            buttonColumn1.Name = "QuestionBank";
            buttonColumn1.Tag = "QuestionBank";
            buttonColumn1.Text = "Purchase";
            //buttonColumn1.UseColumnTextForButtonValue = false;

            dataGridView1.Columns.Add(buttonColumn1);


            DataGridViewDisableButtonColumn buttonColumn2 =
         new DataGridViewDisableButtonColumn();
            buttonColumn2.HeaderText = "Km-Knowledge Assessment";
            buttonColumn2.Name = "QuestionPaper";
            buttonColumn2.Tag = "QuestionBankGeneration";
            buttonColumn2.Text = "Purchase";
            //buttonColumn2.UseColumnTextForButtonValue = false;            
            dataGridView1.Columns.Add(buttonColumn2);
            this.dataGridView1.Columns[3].Width = 100;

            DataGridViewDisableButtonColumn buttonColumn3 =
         new DataGridViewDisableButtonColumn();
            buttonColumn3.HeaderText = "Km-Knowledge Evaluator";
            buttonColumn3.Name = "MockTest";
            buttonColumn3.Tag = "MockTest";
            buttonColumn3.Text = "Purchase";
            // buttonColumn3.UseColumnTextForButtonValue = false;

            dataGridView1.Columns.Add(buttonColumn3);


            // dataGridView1.Columns.Add(column0);
            
          //  dataGridView1.RowCount = 2;
           // dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Update();// = buttonColumn3;
#endregion
            dataGridView1.CellClick +=
                new DataGridViewCellEventHandler(dataGridView1_CellClick);

          //  dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.BackgroundColor = Color.LightGray;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            // Set the selection background color for all the cells.
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            //Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            // Set the background color for all rows and for alternating rows.  
            // The value for alternating rows overrides the value for all rows. 
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.MediumAquamarine;

            //// Set the text for each button.
            if (result != null && result.objQuestionMas.Count > 0)
            {
                //Get the First Level Node using Parent Node is null
                getParent =
                (from QuestionMast in result.objQuestionMas
                 where QuestionMast.ParentParentQuestionNo == 0 && QuestionMast.ParentQuestionNo == 0
                 select QuestionMast).ToList();
                dataGridView1.RowCount = getParent.Count;
                for (int prntCnt = 0; prntCnt < getParent.Count; prntCnt++)
                {
                    dataGridView1.Rows[prntCnt].Cells["ProductName"].Value = getParent[prntCnt].Name;

                    if (Utility.IsAdmin())
                    {
                        dataGridView1.Rows[prntCnt].Cells["eTutor"].Value = "Export";
                        dataGridView1.Rows[prntCnt].Cells["QuestionBank"].Value = "Export";
                        dataGridView1.Rows[prntCnt].Cells["QuestionPaper"].Value = "Export";
                        dataGridView1.Rows[prntCnt].Cells["MockTest"].Value = "Export";
                    }
                    else
                    {
                        dataGridView1.Rows[prntCnt].Cells["eTutor"].Value = getParent[prntCnt].eTutor;
                        dataGridView1.Rows[prntCnt].Cells["QuestionBank"].Value = getParent[prntCnt].QuesBank;
                        dataGridView1.Rows[prntCnt].Cells["QuestionPaper"].Value = getParent[prntCnt].QuesBankGen;
                        dataGridView1.Rows[prntCnt].Cells["MockTest"].Value = getParent[prntCnt].MockTest;
                    }
                }
            }

            if (Utility.IsAdmin())
            {
                //button3.Visible = true;
                textBox1.Visible = true;
                button5.Enabled = false;
                //textBox1.Text = EntropyGenerator.GetIPForMachine();
                label2.Visible = true;
            }
            else
            {
                button3.Visible = false;
                button5.Visible =button6 .Visible = textBox1.Visible = false;

                label2.Visible = false;
            }
        }


        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
            #region admin related
            if ( e.ColumnIndex > 0 && Utility.IsAdmin())
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please choose Client before to proceed");
                    return;
                }
                folderBrowserDialog1.Description = @"Select the folder to Export file";
                DialogResult re=new DialogResult();
                if(Utility.isStandard())
                     re = folderBrowserDialog1.ShowDialog();

                //eTutor
             //   if (e.ColumnIndex == 1)
                //{
                //    string FileName = Application.StartupPath + @"\" + getParent[RowIndex].Name + ".txt";

                //}

                //Admin with Question Bank and Question Paper
                 if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 1)
                {
                    string FileName;
                    string text;
                    if (e.ColumnIndex == 1)
                    {
                        FileName = Application.StartupPath+ Utility.FolderType() + @"eTutor\" + getParent[RowIndex].Name + ".txt";
                        text = System.IO.File.ReadAllText(FileName);
                    }
                    else
                    {
                        FileName = Application.StartupPath + Utility.FolderType() + @"QuestionBank\" + getParent[RowIndex].Name + ".txt";

                        QuestionDetailData obj1 = ObjectXMLSerializer<QuestionDetailData>.Load(FileName);
                       // ObjectXMLSerializer<QuestionDetailData>.Save(obj1, "NewDoc111.txt");
                        if (e.ColumnIndex == 2)
                        {
                            obj1.objQuestionDetail = (from QuestionDetailData in obj1.objQuestionDetail
                                                      where (QuestionDetailData.ModuleName == Utility.MOD_ALL || QuestionDetailData.ModuleName == Utility.MOD_QUEST_BANK)
                                                      select QuestionDetailData).ToList();
                            obj1.QuestionType = "QUESTION BANK";

                        }
                        else
                            obj1.QuestionType = "QUESTION PAPER";


                        ObjectXMLSerializer<QuestionDetailData>.Save(obj1, "NewDoc.txt");
                        text = System.IO.File.ReadAllText(@"NewDoc.txt");
                        System.IO.File.Delete(@"NewDoc.txt");
                    }
                    FileCryptography.entropy = null;

                    
                    string extn = ""; ;
                    if (e.ColumnIndex == 2)
                        extn = @"_QB.txt";
                    else if (e.ColumnIndex == 3)
                        extn = @"_QP.txt";
                    else if (e.ColumnIndex == 1)
                        extn = @"_eTutor.txt";

                        if (!Utility.isStandard() || DialogResult.OK == re)
                        {

                            string strDestFile = null;
                            if (Utility.isStandard())
                                strDestFile = folderBrowserDialog1.SelectedPath + @"\" + getParent[RowIndex].Name + "_" + textBox1.Text + extn;
                            else
                            {
                                if (e.ColumnIndex == 2)
                                    strDestFile = KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\QuestionBank\" + getParent[RowIndex].Name + "_" + textBox1.Text + extn;
                                else if (e.ColumnIndex == 3)
                                    strDestFile = KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\QuestionPaper\" + getParent[RowIndex].Name + "_" + textBox1.Text + extn;
                                else if (e.ColumnIndex == 1)
                                    strDestFile = KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\eTutor\" + getParent[RowIndex].Name + "_" + textBox1.Text + extn;
                                
                            }

                            if (File.Exists(folderBrowserDialog1.SelectedPath + @"\" + getParent[RowIndex].Name  +extn))
                                System.IO.File.Delete(folderBrowserDialog1.SelectedPath + @"\" + getParent[RowIndex].Name + extn);


                            //FileCryptography.DoEncrypt(text, strDestFile, textBox1.Text);
                            FileCryptography.DoEncrypt(text, strDestFile, textBox1.Tag.ToString());
                            if (e.ColumnIndex == 2)
                                AuditPurchase(getParent[RowIndex].Name, "Km- Knowledge Base");
                            else if (e.ColumnIndex == 3)
                                AuditPurchase(getParent[RowIndex].Name, "Km-Knowledge Assessment");
                            else if (e.ColumnIndex == 1)
                                AuditPurchase(getParent[RowIndex].Name, "Km-Knowledge eTutor");
                            MessageBox.Show("Data Exported to " + strDestFile);
                        }
                }
                else if (e.ColumnIndex == 4)
                {
                    //Mock Test
                    QuestionManagementColl objQuestionManagementColl=null;
                    QuestionDetailData resultQuestions;
                    QuestionManagementWithQuesColl objQuestionManagementWithQuesColl;
                    objQuestionManagementWithQuesColl = new QuestionManagementWithQuesColl();
                    if (File.Exists(Application.StartupPath +Utility.FolderType() +@"MockTest\MockTestList.txt"))
                        objQuestionManagementColl = ObjectXMLSerializer<QuestionManagementColl>.Load(Application.StartupPath +Utility.FolderType() +@"MockTest\MockTestList.txt");

                    if (objQuestionManagementColl != null)
                    {
                
                        for (int i = 0; i < objQuestionManagementColl.objQuestionManagement.Count; i++)
                        {
                            if (objQuestionManagementColl.objQuestionManagement[i].QuestionTopic == getParent[RowIndex].Name)
                            {
                                QuestionManagementWithQues objQuestionManagement = new QuestionManagementWithQues();
                                objQuestionManagement.ExamName = objQuestionManagementColl.objQuestionManagement[i].ExamName;
                                objQuestionManagement.ExamPasPercentageScore = objQuestionManagementColl.objQuestionManagement[i].ExamPasPercentageScore;
                                objQuestionManagement.ExamMode = objQuestionManagementColl.objQuestionManagement[i].ExamMode;
                                objQuestionManagement.QuestionComplexity = objQuestionManagementColl.objQuestionManagement[i].QuestionComplexity;
                                objQuestionManagement.QuestionType = objQuestionManagementColl.objQuestionManagement[i].QuestionType;
                                objQuestionManagement.TotalQuestions = objQuestionManagementColl.objQuestionManagement[i].TotalQuestions;
                                objQuestionManagement.FileName = objQuestionManagementColl.objQuestionManagement[i].FileName ;
                                objQuestionManagement.QuestionTopic = objQuestionManagementColl.objQuestionManagement[i].QuestionTopic;
                                objQuestionManagement.Subject = objQuestionManagementColl.objQuestionManagement[i].Subject;
                                objQuestionManagement.TestStatus = objQuestionManagementColl.objQuestionManagement[i].TestStatus;
                                objQuestionManagement.TestResult = objQuestionManagementColl.objQuestionManagement[i].TestResult;
                                objQuestionManagement.TestTime = objQuestionManagementColl.objQuestionManagement[i].TestTime;
                                if (File.Exists(objQuestionManagementColl.objQuestionManagement[i].FileName))
                                {
                                    resultQuestions = ObjectXMLSerializer<QuestionDetailData>.Load(objQuestionManagementColl.objQuestionManagement[i].FileName);
                                    objQuestionManagement.objQuestionDetail = new List<QuestionDetail>();
                                    objQuestionManagement.objQuestionDetail.AddRange(resultQuestions.objQuestionDetail);
                                }
                                objQuestionManagementWithQuesColl.objQuestionManagement.Add(objQuestionManagement);
                            }
                            //objQuestionManagementWithQuesColl.objQuestionManagement[].objQuestionDetail.Add(
                        }
                        if (!Utility.isStandard() || DialogResult.OK == re)
                        {
                            ObjectXMLSerializer<QuestionManagementWithQuesColl>.Save(objQuestionManagementWithQuesColl, "NewDoc.txt");
                            string text = System.IO.File.ReadAllText(@"NewDoc.txt");
                            System.IO.File.Delete(@"NewDoc.txt");
                            FileCryptography.entropy = null;

                            string strMockFileName = null;
                            if (Utility.isStandard())
                                strMockFileName = folderBrowserDialog1.SelectedPath + @"\" + getParent[RowIndex].Name + "_" + textBox1.Text + "_Mock.txt";
                            else
                            {
                                
                                strMockFileName = KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\" + @"MockTest\" + getParent[RowIndex].Name + "_" + textBox1.Text + "_Mock.txt";
                            }
                            if (File.Exists(strMockFileName))
                                System.IO.File.Delete(strMockFileName);

                           // FileCryptography.DoEncrypt(text, strMockFileName, textBox1.Text);
                            FileCryptography.DoEncrypt(text, strMockFileName, textBox1.Tag.ToString());
                            AuditPurchase(getParent[RowIndex].Name, "Km-Knowledge Evaluator");
                            MessageBox.Show("Data Exported to " + strMockFileName);

                           // ObjectXMLSerializer<QuestionManagementWithQuesColl>.Save(objQuestionManagementWithQuesColl, folderBrowserDialog1.SelectedPath + @"\" + getParent[RowIndex].Name + "_Mock.txt");

                        }
                

                    }
                    
           // FileName = Application.StartupPath + @"\" + FileName + ".txt";
            
            

                }

            }
            
            #endregion
            else if (e.ColumnIndex > 0)
            {
                // MessageBox.Show(dataGridView1.Columns[e.ColumnIndex].Name);
                if (dataGridView1.Columns[e.ColumnIndex].Name == "QuestionBank" || dataGridView1.Columns[e.ColumnIndex].Name == "QuestionPaper" || dataGridView1.Columns[e.ColumnIndex].Name == "eTutor")
                {
                    string strDestFile = null;

                   

                    DataGridViewDisableButtonCell buttonCell;
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "QuestionBank")
                    {
                     buttonCell =
                            (DataGridViewDisableButtonCell)dataGridView1.
                            Rows[e.RowIndex].Cells["QuestionBank"];
                        if(!Utility.isStandard())
                            strDestFile = KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\QuestionBank\" + getParent[RowIndex].Name + "_" + EntropyGenerator.GetIPForMachine() +@"_QB.txt";
                    }
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "eTutor")
                    {
                        buttonCell =
                               (DataGridViewDisableButtonCell)dataGridView1.
                               Rows[e.RowIndex].Cells["eTutor"];
                            if(!Utility.isStandard())
                                strDestFile = KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\eTutor\" + getParent[RowIndex].Name + "_" + EntropyGenerator.GetIPForMachine() + @"_eTutor.txt";
                        }
                            else
                    {
                        buttonCell =
                            (DataGridViewDisableButtonCell)dataGridView1.
                            Rows[e.RowIndex].Cells["QuestionPaper"];
                            if(!Utility.isStandard())
                                strDestFile = KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\QuestionPaper\" + getParent[RowIndex].Name + "_" + EntropyGenerator.GetIPForMachine() + @"_QP.txt";
                        }


                    //if (dataGridView1.Rows[e.RowIndex].Cells["QuestionBank"].Value.ToString() == "Purchase")
                    if(buttonCell.Value.ToString() == "Purchase")
                    {
                        //DataGridViewDisableButtonCell buttonCell =
                        //    (DataGridViewDisableButtonCell)dataGridView1.
                        //    Rows[e.RowIndex].Cells["QuestionBank"];

                        //Check whether we can open the file
                        //OpenFileDialog objFile = new OpenFileDialog();

                        //DialogResult resultdia = objFile.ShowDialog(); // Show the dialog.
                        //if (resultdia == DialogResult.OK) // Test result.
                        //{
                        //    string file = objFile.FileName;
                        //    FileCryptography.DoDecrypt(file, null);
                        //    string decryptedData = FileCryptography.decryptedData;
                        //    if (string.IsNullOrEmpty(decryptedData))
                        //    {
                        //        MessageBox.Show("File is invalid");
                        //    }
                        //    else
                        string decryptedData = ValidateImport(strDestFile);
                        if (decryptedData != null)
                        {
                            QuestionDetailData result1=new QuestionDetailData();
                            eTutorCollData objetutor =new eTutorCollData();
                            try
                            {
                                string FileName = null;
                                if (dataGridView1.Columns[e.ColumnIndex].Name == "QuestionBank")
                                    FileName = Application.StartupPath +Utility.FolderType() + @"QuestionBank\" + getParent[e.RowIndex].Name + ".txt";
                                else if (dataGridView1.Columns[e.ColumnIndex].Name == "eTutor")
                                    FileName = Application.StartupPath + Utility.FolderType() + @"eTutor\" + getParent[e.RowIndex].Name + ".txt";
                                else
                                    FileName = Application.StartupPath + Utility.FolderType() + @"QuestionPaper\" + getParent[e.RowIndex].Name + ".txt";

                                if (File.Exists(FileName))
                                    File.Delete(FileName);

                                FileCryptography.encryptedData = decryptedData;
                                FileCryptography.FileName = FileName;
                                FileCryptography.entropy = UnicodeEncoding.ASCII.GetBytes(KnowledgeMatrix.Properties.Settings.Default.ProductKey);
                                FileCryptography.DoEncrypt();

                                if (dataGridView1.Columns[e.ColumnIndex].Name == "eTutor")
                                {
                                    objetutor = ObjectXMLSerializer<eTutorCollData>.Load(FileName);
                                    if (objetutor == null && objetutor.eTutorlst.Count() == 0)
                                    {
                                        MessageBox.Show("No data available to import. Please contact Sales team");
                                        return;
                                    }
                                }
                                else if (dataGridView1.Columns[e.ColumnIndex].Name == "QuestionBank" || dataGridView1.Columns[e.ColumnIndex].Name == "QuestionPaper")
                                {
                                    result1 = ObjectXMLSerializer<QuestionDetailData>.Load(FileName);

                                    bool isValid = true;

                                    if (dataGridView1.Columns[e.ColumnIndex].Name == "QuestionBank" && result1.QuestionType != "QUESTION BANK")
                                        isValid = false;
                                    else if (dataGridView1.Columns[e.ColumnIndex].Name == "QuestionPaper" && result1.QuestionType != "QUESTION PAPER")
                                        isValid = false;

                                        else if (result1 == null && result1.objQuestionDetail.Count() == 0)
                                        {
                                            isValid = false;
                                        }
                                    if(!isValid)
                                    {
                                            File.Delete(FileName);
                                            MessageBox.Show("No data available to import. Please contact Sales team");
                                            return;
                                        }
                                }
                                
                                {
                                    //ObjectXMLSerializer<QuestionDetailData>.Save(result1, FileName);

                                    MessageBox.Show("Data imported Successfully");
                                    buttonCell.Value = "Purchased";


                                    buttonCell.Enabled = false;

                                    //Update the Product Catalog
                                    int idx = result.objQuestionMas.FindIndex(
                                                     delegate(QuestionMast bk)
                                                     {
                                                         return bk.QuesNo == getParent[e.RowIndex].QuesNo;
                                                     }
                                                     );
                                    if (dataGridView1.Columns[e.ColumnIndex].Name == "QuestionBank")
                                     result.objQuestionMas[idx].QuesBank = "Purchased";
                                    else if (dataGridView1.Columns[e.ColumnIndex].Name == "eTutor")
                                        result.objQuestionMas[idx].eTutor = "Purchased";
                                    else
                                        result.objQuestionMas[idx].QuesBankGen = "Purchased";
                                    result.objQuestionMas[idx].QuesBankDate = System.DateTime.Now.ToString();

                                    ObjectXMLSerializer<QuestionsData>.Save(result, KnowledgeMatrix.Framework.Utility.XML_QUESTION_NAME);

                                  //  FileCryptography.encryptedData = File.ReadAllText(XML_FILE_NAME1);
                                   // FileCryptography.FileName = KnowledgeMatrix.Framework.Utility.XML_QUESTION_NAME;
                                    //FileCryptography.DoEncrypt();
                                    //File.Delete(XML_FILE_NAME1);
                                    //Update the Product Catalog
                                }
                            }
                            catch (Exception ex)
                            {
                                LogEntry.WriteLog(ex, "Thread Exception");
                                MessageBox.Show("File incorrect. Please contact Sales team");
                            }
                        }

                        //}
                    }
                    else if (dataGridView1.Rows[e.RowIndex].Cells["QuestionBank"].Value.ToString() == "Purchased" || dataGridView1.Rows[e.RowIndex].Cells["QuestionPaper"].Value.ToString() == "Purchased")
                    {
                        LicenseDetail obj = new LicenseDetail();
                        int idx = result.objQuestionMas.FindIndex(
                                                         delegate(QuestionMast bk)
                                                         {
                                                             return bk.QuesNo == getParent[e.RowIndex].QuesNo;
                                                         }
                                                         );


                        obj.objLicenseDetail.ProductName = result.objQuestionMas[idx].Name;
                        obj.objLicenseDetail.ProductType = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                        obj.objLicenseDetail.DateOfPurchase = result.objQuestionMas[idx].QuesBankDate;
                        obj.objLicenseDetail.DateOfExpiry = Convert.ToDateTime(result.objQuestionMas[idx].QuesBankDate).AddDays(365).ToString();
                        obj.ShowDialog();
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "MockTest")
                {
                    DataGridViewDisableButtonCell buttonCell =(DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["MockTest"];
                    //NEW REQ
                    if(buttonCell.Value.ToString() != "Purchases")
                    {

                        string strMockFileName = null;
                        if (!Utility.isStandard())
                            strMockFileName = KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\" + @"MockTest\" + getParent[RowIndex].Name + "_" + EntropyGenerator.GetIPForMachine() +"_Mock.txt";
                        

                        string decryptedData = ValidateImport(strMockFileName);
                    if (decryptedData != null)
                    {
                        string FileName = Application.StartupPath + Utility.FolderType() + @"MockTest\MockList" + ".txt";
                        if (File.Exists(FileName))
                            File.Delete(FileName);

                        FileCryptography.encryptedData = decryptedData;
                        FileCryptography.FileName = FileName;
                        FileCryptography.entropy = UnicodeEncoding.ASCII.GetBytes(KnowledgeMatrix.Properties.Settings.Default.ProductKey);
                        FileCryptography.DoEncrypt();
                        QuestionManagementWithQuesColl result1 = ObjectXMLSerializer<QuestionManagementWithQuesColl>.Load(FileName);
                        if (result1 == null || result1.objQuestionManagement.Count() == 0)
                        {
                            File.Delete(FileName);
                            MessageBox.Show("No data available to import. Please contact Sales team");
                        }
                        else
                        {
                            FileName = Application.StartupPath + Utility.FolderType() + @"MockTest\MockTestList.txt";

                            QuestionManagementColl objQuestionManagementColl;
                            if (File.Exists(FileName))
                                objQuestionManagementColl = ObjectXMLSerializer<QuestionManagementColl>.Load(FileName);
                            else
                            {
                                objQuestionManagementColl = new QuestionManagementColl();
                                objQuestionManagementColl.objQuestionManagement = new List<QuestionManagement>();
                            }
                            for (int ik = 0; ik < result1.objQuestionManagement.Count(); ik++)
                            {
                                 int i = (from QuestionManagement in objQuestionManagementColl.objQuestionManagement
                     where QuestionManagement.ExamName == result1.objQuestionManagement[ik].ExamName
                     select QuestionManagement).ToList().Count();

                                 if (i == 0)
                                 {

                                     QuestionManagement objQuestionManagement = new QuestionManagement();// =(QuestionManagement) result1.objQuestionManagement[ik];
                                     objQuestionManagement.ExamName = result1.objQuestionManagement[ik].ExamName;
                                     objQuestionManagement.ExamPasPercentageScore = result1.objQuestionManagement[ik].ExamPasPercentageScore;
                                     objQuestionManagement.TestTime = result1.objQuestionManagement[ik].TestTime;
                                     objQuestionManagement.ExamMode = result1.objQuestionManagement[ik].ExamMode;
                                     objQuestionManagement.QuestionComplexity = result1.objQuestionManagement[ik].QuestionComplexity;
                                     objQuestionManagement.QuestionType = result1.objQuestionManagement[ik].QuestionType;
                                     objQuestionManagement.TotalQuestions = result1.objQuestionManagement[ik].TotalQuestions;
                                     objQuestionManagement.TestTime = result1.objQuestionManagement[ik].TestTime;

                                     QuestionDetailData objQuestions = new QuestionDetailData();
                                     objQuestions.objQuestionDetail = new List<QuestionDetail>();
                                     objQuestions.objQuestionDetail.AddRange(result1.objQuestionManagement[ik].objQuestionDetail);
                                     if (File.Exists(XML_FILE_NAME1))
                                         File.Delete(XML_FILE_NAME1);

                                     ObjectXMLSerializer<QuestionDetailData>.Save(objQuestions, Application.StartupPath + Utility.FolderType() + @"MockTest\" + result1.objQuestionManagement[ik].ExamName + ".txt");

                                     //FileCryptography.encryptedData = File.ReadAllText(XML_FILE_NAME1);
                                     //FileCryptography.FileName = Application.StartupPath + @"\QuestionPaper\" + result1.objQuestionManagement[ik].ExamName + ".txt";
                                     //FileCryptography.DoEncrypt();
                                     //File.Delete(XML_FILE_NAME1);

                                     //ObjectXMLSerializer<QuestionDetailData>.Save(objQuestions, Application.StartupPath + @"\QuestionPaper\" + result1.objQuestionManagement[ik].ExamName + ".txt");

                                     objQuestionManagement.FileName = Application.StartupPath + Utility.FolderType() + @"MockTest\" + result1.objQuestionManagement[ik].ExamName + ".txt";
                                     objQuestionManagement.QuestionTopic = result1.objQuestionManagement[ik].QuestionTopic;
                                     objQuestionManagement.Subject = result1.objQuestionManagement[ik].Subject;
                                     objQuestionManagement.TestStatus = result1.objQuestionManagement[ik].TestStatus;
                                     objQuestionManagement.TestResult = result1.objQuestionManagement[ik].TestResult;
                                     objQuestionManagement.MockTestDate = System.DateTime.Now.ToString();
                                     objQuestionManagementColl.objQuestionManagement.Add(objQuestionManagement);
                                 }
                            }

                            if (File.Exists(FileName))
                                File.Delete(FileName);

                            ObjectXMLSerializer<QuestionManagementColl>.Save(objQuestionManagementColl, FileName);

                            buttonCell.Value = "Purchased";


                            buttonCell.Enabled = false;

                            //Update the Product Catalog
                            int idx = result.objQuestionMas.FindIndex(
                                             delegate(QuestionMast bk)
                                             {
                                                 return bk.QuesNo == getParent[e.RowIndex].QuesNo;
                                             }
                                             );

                            result.objQuestionMas[idx].MockTest = "Purchased";
                            result.objQuestionMas[idx].QuesBankDate = System.DateTime.Now.ToString();

                            ObjectXMLSerializer<QuestionsData>.Save(result, KnowledgeMatrix.Framework.Utility.XML_QUESTION_NAME);


                            //FileCryptography.encryptedData = File.ReadAllText(XML_FILE_NAME1);
                            //FileCryptography.FileName = FileName;
                            //FileCryptography.DoEncrypt();
                            //File.Delete(XML_FILE_NAME1);

                            //ObjectXMLSerializer<QuestionManagementColl>.Save(objQuestionManagementColl, FileName);

                            MessageBox.Show("Data imported Successfully");
                        }
                    }
                    
                        
                        
                    }
                    else
                    {
                        LicenseDetail obj = new LicenseDetail();
                        int idx = result.objQuestionMas.FindIndex(
                                                         delegate(QuestionMast bk)
                                                         {
                                                             return bk.QuesNo == getParent[e.RowIndex].QuesNo;
                                                         }
                                                         );


                        obj.objLicenseDetail.ProductName = result.objQuestionMas[idx].Name;
                        obj.objLicenseDetail.ProductType = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                        obj.objLicenseDetail.DateOfPurchase = result.objQuestionMas[idx].QuesBankDate;
                        obj.objLicenseDetail.DateOfExpiry = Convert.ToDateTime(result.objQuestionMas[idx].QuesBankDate).AddDays(365).ToString();
                        obj.ShowDialog();
                    }
                }

            }
            
        }

        private string ValidateImport(string strDestFile)
        {
            string decryptedData = null;
            //Check whether we can open the file
                        OpenFileDialog objFile = new OpenFileDialog();

                        DialogResult resultdia = new DialogResult();
                        if (strDestFile == null)
                            resultdia = objFile.ShowDialog(); // Show the dialog.
                        else
                            objFile.FileName = strDestFile;
                        if (strDestFile != null || resultdia == DialogResult.OK) // Test result.
                        {
                            string file = objFile.FileName;
                            //TO DO
                            FileCryptography.DoDecrypt(file, KnowledgeMatrix.Properties.Settings.Default.ProductKey);
                            decryptedData = FileCryptography.decryptedData;
                            if (string.IsNullOrEmpty(decryptedData))
                            {
                                MessageBox.Show("File is invalid");
                            }
                        }
                        return decryptedData;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Type t = typeof(System.Windows.Forms.SystemInformation);
            PropertyInfo[] pi = t.GetProperties();
            StringBuilder str=new StringBuilder();
            for (int i = 0; i < pi.Length; i++)
            {
                str.Append(pi[i].Name);
                str.Append("\r\n");
            }
                //Replace with scroll bar
                MessageBox.Show("The SystemInformation class has " + pi.Length.ToString() + " properties.\r\n"+str.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Utility.ResetActivation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.UserControl)(this)).Text = "Done";
            this.InvokeOnClick(this, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

        // MessageBox.Show(getParent[RowIndex].Name);
         string FileName = Application.StartupPath + @"\" + getParent[RowIndex].Name + ".txt";
             

         /*List<QuestionDetail> navQuestions;
         navQuestions = new List<QuestionDetail>();
         resultQuestions = ObjectXMLSerializer<QuestionDetailData>.Load(XML_QUESTION_NAME); 
            getFirstChild =
              (from QuestionMast in result.objQuestionMas
               where QuestionMast.ParentQuestionNo == Convert.ToInt16(getParent[RowIndex].QuesNo)
               select QuestionMast).ToList();
            for (int childCnt = 0; childCnt < getFirstChild.Count; childCnt++)
            {
                strindex = (getFirstChild[childCnt].QuesNo);
                obj = (from QuestionDetail in resultQuestions.objQuestionDetail
                       where QuestionDetail.QuesNo == strindex
                       select QuestionDetail).ToList();
                //MessageBox.Show(node.Tag.ToString());
                foreach (QuestionDetail objj in obj)
                    navQuestions.Add(objj);

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
                    obj = (from QuestionDetail in resultQuestions.objQuestionDetail
                           where QuestionDetail.QuesNo == strindex
                           select QuestionDetail).ToList();
                    //MessageBox.Show(node.Tag.ToString());
                    foreach (QuestionDetail objj in obj)
                        navQuestions.Add(objj);
                }
            }
        */
           // QuestionDetailData resultQuestions=ObjectXMLSerializer<QuestionDetailData>.Load( FileName);
            QuestionDetailData obj1 = ObjectXMLSerializer<QuestionDetailData>.Load(FileName); 
          //  obj1.objQuestionDetail = resultQuestions;
           
            
            ObjectXMLSerializer<QuestionDetailData>.Save(obj1, "NewDoc.txt");
            string text = System.IO.File.ReadAllText(@"NewDoc.txt");
            System.IO.File.Delete(@"NewDoc.txt");
            FileCryptography.entropy = null;

            folderBrowserDialog1.Description = @"Select the folder to Export file";
            DialogResult re = folderBrowserDialog1.ShowDialog();
            if (DialogResult.OK == re)
            {

                if (File.Exists(folderBrowserDialog1.SelectedPath + @"\"+getParent[RowIndex].Name + @"QB.txt"))
                    System.IO.File.Delete(folderBrowserDialog1.SelectedPath + @"\" + getParent[RowIndex].Name + @"_QB.txt");


                FileCryptography.DoEncrypt(text, folderBrowserDialog1.SelectedPath + @"\" + getParent[RowIndex].Name + @"_QB.txt", textBox1.Text);
                //FileCryptography.entropy = null;
                //FileCryptography.DoDecrypt("NewDocEnc.xml", null);
                //string decryptedData = FileCryptography.decryptedData;
                AuditPurchase(getParent[RowIndex].Name, "Question Bank");
                MessageBox.Show("Data Exported to " + folderBrowserDialog1.SelectedPath + @"\" + getParent[RowIndex].Name + @"_QB.txt");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SearchUser obj = new SearchUser();
            obj.ShowDialog();
            if (!string.IsNullOrWhiteSpace(obj.PublicKey))
            {
                textBox1.Text = obj.UserName + " - " + obj.IP;
                textBox1.Tag = obj.PublicKey;
                UserId = Convert.ToInt32(obj.ID);
                button5.Enabled = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
                button5.Enabled = true;
            else
                button5.Enabled = false;
        }

        private void AuditPurchase(string ProductName,string ProductType)
        {
            LicenseDetailInfo obj = new LicenseDetailInfo();
            obj.ProductName = ProductName;
            obj.ProductType = ProductType;
            obj.IP = textBox1.Text;
            obj.LicenseMasterID = UserId;

            AdminDatabaseMgmt objAdmin = new AdminDatabaseMgmt();
            objAdmin.AddProductPurchase(obj);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SearchUser obj = new SearchUser();
            obj.ID = Convert.ToString(UserId);
            obj.ShowDialog();
                
        }
    }
      
}
