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
    public partial class ViewMockResult : Form
    {
        public QuestionManagement objQuestionManagement;
        public int RowChoosen = -1;
        public ViewMockResult()
        {
            InitializeComponent();
        }

        private void InitialiseGrid()
        {
            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.ReadOnly = true;
            col0.HeaderText = "Name of the Student";
            col0.Name = "UserName";
            dataGridView1.Columns.Add(col0);
            this.dataGridView1.Columns[0].Width = 100;

            DataGridViewTextBoxColumn col01 = new DataGridViewTextBoxColumn();
            col01.ReadOnly = true;
            col01.HeaderText = "Date Taken";
            col01.Name = "DateTaken";
            dataGridView1.Columns.Add(col01);
            this.dataGridView1.Columns[01].Width = 100;

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.ReadOnly = true;
            col1.HeaderText = "Test Score";
            col1.Name = "TestMark";
            dataGridView1.Columns.Add(col1);
            this.dataGridView1.Columns[1].Width = 60;

            DataGridViewTextBoxColumn buttonColumn =
               new DataGridViewTextBoxColumn();
            buttonColumn.ReadOnly = true;
            buttonColumn.HeaderText = "Test Status";
            buttonColumn.Name = "TestSatus";
            // buttonColumn.Text = "TestStatus";
            
            dataGridView1.Columns.Add(buttonColumn);
            this.dataGridView1.Columns[2].Width = 60;

            DataGridViewDisableButtonColumn buttonColumn1 =
new DataGridViewDisableButtonColumn();
            buttonColumn1.ReadOnly = true;
            buttonColumn1.HeaderText = "Action";
            buttonColumn1.Name = "Action";
            buttonColumn1.Tag = "Proceed";
            buttonColumn1.DefaultCellStyle.BackColor = Color.Red;
            // buttonColumn1.Text = "Generate";
            //buttonColumn1.UseColumnTextForButtonValue = false;

            dataGridView1.Columns.Add(buttonColumn1);


            this.dataGridView1.Columns[3].Width = 60;

           
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Update();// = buttonColumn3;


            dataGridView1.RowCount = objQuestionManagement.objResultList.Count();

            for (int i = 0; i < objQuestionManagement.objResultList.Count; i++)
            {
                dataGridView1.Rows[i].Cells["UserName"].Value = objQuestionManagement.objResultList[i].UserName;
                dataGridView1.Rows[i].Cells["TestMark"].Value = objQuestionManagement.objResultList[i].TestMark;
                dataGridView1.Rows[i].Cells["TestSatus"].Value = objQuestionManagement.objResultList[i].TestSatus;
                dataGridView1.Rows[i].Cells["DateTaken"].Value = objQuestionManagement.objResultList[i].DateTaken;
                dataGridView1.Rows[i].Cells["Action"].Value = "Summary";
            }
            
            dataGridView1.CellClick +=
            new DataGridViewCellEventHandler(dataGridView1_CellClick);

        }
        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                RowChoosen = e.RowIndex;
                this.Close();
            }
        }

        private void ViewMockResult_Load(object sender, EventArgs e)
        {
            label25.Text = objQuestionManagement.ExamName;
            label26.Text = objQuestionManagement.TotalQuestions;
            label27.Text = objQuestionManagement.TestTime;
            label28.Text = objQuestionManagement.ExamPasPercentageScore;
            InitialiseGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
