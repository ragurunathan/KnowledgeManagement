using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KnowledgeMatrix.Database;
using ExampApp.Database;
using System.IO;
using KnowledgeMatrix.Framework;

namespace KnowledgeMatrix.Forms
{
    public partial class QuestionPaperDisplay : UserControl
    {
        QuestionManagementColl objQuestionManagementColl = null;
        public QuestionPaperDisplay()
        {
            InitializeComponent();


            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.ReadOnly = true;
            col0.HeaderText = "Exam Name";
            col0.Name = "ExamName";
            dataGridView1.Columns.Add(col0);
            //this.dataGridView1.Columns[0].Width = 175;

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
           col1.ReadOnly = true;
            col1.HeaderText = "No of Questions";
            col1.Name = "Subject";
            dataGridView1.Columns.Add(col1);
            this.dataGridView1.Columns[0].Width = 200;

            DataGridViewDisableButtonColumn buttonColumn =
               new DataGridViewDisableButtonColumn();
            buttonColumn.HeaderText = "Question";
            buttonColumn.Name = "Question";
            buttonColumn.Text = "Generate";
            buttonColumn.Tag = "Question";
            this.dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns.Add(buttonColumn);


            DataGridViewDisableButtonColumn buttonColumn1 =
new DataGridViewDisableButtonColumn();
            buttonColumn1.HeaderText = "Q and A";
            buttonColumn1.Name = "QandA";
            buttonColumn1.Tag = "QandA";
            buttonColumn1.Text = "Generate";
            //buttonColumn1.UseColumnTextForButtonValue = false;
            this.dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns.Add(buttonColumn1);


            DataGridViewDisableButtonColumn buttonColumn2 =
         new DataGridViewDisableButtonColumn();
            buttonColumn2.HeaderText = "Answer";
            buttonColumn2.Name = "Answer";
            buttonColumn2.Tag = "Answer";
            buttonColumn2.Text = "Generate";
            //buttonColumn2.UseColumnTextForButtonValue = false;            
            dataGridView1.Columns.Add(buttonColumn2);
            this.dataGridView1.Columns[3].Width = 60;

            DataGridViewDisableButtonColumn buttonColumn4 =
         new DataGridViewDisableButtonColumn();
            buttonColumn4.HeaderText = "OMR";
            buttonColumn4.Name = "OMR";
            buttonColumn4.Tag = "OMR";
            buttonColumn4.Text = "Generate";
            //buttonColumn2.UseColumnTextForButtonValue = false;            
            dataGridView1.Columns.Add(buttonColumn4);
            this.dataGridView1.Columns[4].Width = 60;

            DataGridViewDisableButtonColumn buttonColumn3 =
         new DataGridViewDisableButtonColumn();
            buttonColumn3.HeaderText = "Delete";
            buttonColumn3.Name = "Delete";
            buttonColumn3.Tag = "Delete";
            buttonColumn3.Text = "Delete";
            //buttonColumn2.UseColumnTextForButtonValue = false;            
            dataGridView1.Columns.Add(buttonColumn3);
            this.dataGridView1.Columns[5].Width = 60;
            this.dataGridView1.Columns[6].Width = 60;
            
          //  dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Update();// = buttonColumn3;

            

            dataGridView1.CellClick +=
                new DataGridViewCellEventHandler(dataGridView1_CellClick);
            dataGridView1.Dock = DockStyle.Fill;
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

            LoadTextData();
            
        }
        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure to delete from list?", "Delete", MessageBoxButtons.YesNo))
                {
                    File.Delete(objQuestionManagementColl.objQuestionManagement[e.RowIndex].FileName);
                    objQuestionManagementColl.objQuestionManagement.RemoveAt(e.RowIndex);
                    ObjectXMLSerializer<QuestionManagementColl>.Save(objQuestionManagementColl, Application.StartupPath +Utility.FolderType() +@"QuestionPaper\QuestionInfo.txt");
                    LoadTextData();
                }
            }
            else if (e.ColumnIndex > 1 && (e.ColumnIndex < 6))
            {
                QuestionDetailData objQuestions = ObjectXMLSerializer<QuestionDetailData>.Load(objQuestionManagementColl.objQuestionManagement[e.RowIndex].FileName);

                FrmQuestionsDisplay obj = new FrmQuestionsDisplay();
                //obj.resultQuestions.objQuestionDetail.Clear();
                obj.resultQuestions.objQuestionDetail = objQuestions.objQuestionDetail;
                obj.strSubject = objQuestionManagementColl.objQuestionManagement[e.RowIndex].Subject;
                obj.strQuestTopic = objQuestionManagementColl.objQuestionManagement[e.RowIndex].QuestionTopic;
                if (e.ColumnIndex == 2)
                {
                    obj.printAnswer = false;
                    obj.printQuestion = true;
                }
                else if (e.ColumnIndex == 3)
                {
                    obj.printAnswer = true;
                    obj.printQuestion = true;
                }
                else if (e.ColumnIndex == 4)
                {
                    obj.printAnswer = true;
                    obj.printQuestion = false;
                }
                else if (e.ColumnIndex == 5)
                {
                    obj.printAnswer = false;
                    obj.printQuestion = false;
                }
                obj.ShowDialog();
            }
        }
        public void LoadTextData()
        {
            
            if (File.Exists(Application.StartupPath+ Utility.FolderType() + @"QuestionPaper\QuestionInfo.txt"))
                objQuestionManagementColl = ObjectXMLSerializer<QuestionManagementColl>.Load(Application.StartupPath+Utility.FolderType() + @"QuestionPaper\QuestionInfo.txt");
            if (objQuestionManagementColl != null)
            {
                dataGridView1.RowCount = objQuestionManagementColl.objQuestionManagement.Count;
                for (int i = 0; i < objQuestionManagementColl.objQuestionManagement.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["Answer"].Value = "Generate";
                    dataGridView1.Rows[i].Cells["QandA"].Value = "Generate";
                    dataGridView1.Rows[i].Cells["Question"].Value = "Generate";
                    dataGridView1.Rows[i].Cells["OMR"].Value = "Generate";
                    dataGridView1.Rows[i].Cells["Delete"].Value = "Delete";
                    dataGridView1.Rows[i].Cells["ExamName"].Value = objQuestionManagementColl.objQuestionManagement[i].ExamName;
                    dataGridView1.Rows[i].Cells["Subject"].Value = objQuestionManagementColl.objQuestionManagement[i].TotalQuestions;

                }
            }

        }
    }
}
