using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExampApp.Database;
using System.IO;
using KnowledgeMatrix.Database;
using ExtendedRichTextBox;
using System.Collections.Generic;
using KnowledgeMatrix.Framework;
using ShanuNestedDataGridView;
using System.Data;
using System.Reflection;
using ShanuNestedDataGridView.DataClass;
using ShanuNestedDataGridView;
using ShanuNestedDataGridView.Helper;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace KnowledgeMatrix.Forms
{
    public partial class MockTestHome : UserControl
    {
        #region Variables
        // Declared for the Master grid
        DataGridView Master_shanuDGV = new DataGridView();
        // Declared for the Detail grid
        DataGridView Detail_shanuDGV = new DataGridView();
        List<ResultList> objResultList = new List<ResultList>();
        List<int> lstNumericTextBoxColumns;

        ShanuNestedDataGridView.Helper.ShanuDGVHelper objshanudgvHelper = new ShanuNestedDataGridView.Helper.ShanuDGVHelper();
        public int ColumnIndex;
        DataTable dtName = new DataTable();
        # endregion

        // Declared for the Master grid
       // DataGridView Master_shanuDGV = new DataGridView();
        // Declared for the Detail grid
        //DataGridView Detail_shanuDGV = new DataGridView();
        //ShanuNestedDataGridView.Helper.ShanuDGVHelper objshanudgvHelper = new ShanuNestedDataGridView.Helper.ShanuDGVHelper();

        public int currRecord = 0;
        private int nRowIndex = 0;
        QuestionManagementColl objQuestionManagementColl = null;
        // Because we have not specified a namespace, this
        // will be a System.Windows.Forms.Timer instance
        private Timer _timer1;
        // The last time the timer was started
        private DateTime _startTime = DateTime.MinValue;
        // Time between now and when the timer was started last
        private TimeSpan _currentElapsedTime = TimeSpan.Zero;

        // Time between now and the first time the timer was started after a reset
        private TimeSpan _totalElapsedTime = TimeSpan.Zero;

        // Whether or not the timer is currently running
        private bool _timerRunning = false;

        private BindingSource bs;
        private bool isManualTabSecond = false;
        private bool isManualTabThird = false;

        public QuestionDetailData resultQuestions;

        public MockTestHome()
        {
            InitializeComponent();
            // To bind the Master data to List 
            Master_BindData();

            // To bind the Detail data to List 
            Detail_BindData();


            MasterGrid_Initialize();

            DetailGrid_Initialize();



           // InitialiseGrid();
            InitialiseGridSecond();
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
            //dataGridView3.AutoGenerateColumns = false;
            //InitialiseResultGrid();
           // // Set up a timer and fire the Tick event once per second (1000 ms)
            _timer1 = new Timer();
            _timer1.Interval = 1000;
            _timer1.Tick += new EventHandler(timer1_Tick);
            pictureBox1.Image = imageList1.Images[0];
            pictureBox2.Image = imageList1.Images[1];
            pictureBox3.Image = imageList1.Images[2];
            pictureBox4.Image = imageList1.Images[3];
            
        }
        #region Methods
        private void Master_BindData()
        {
            objResultList.Clear();
            if (File.Exists(Application.StartupPath +Utility.FolderType() +@"MockTest\MockTestList.txt"))
                objQuestionManagementColl = ObjectXMLSerializer<QuestionManagementColl>.Load(Application.StartupPath+ Utility.FolderType()+ @"MockTest\MockTestList.txt");
            if (objQuestionManagementColl != null)
            {
                int nRowCount = 0;
                QuestionManagementColl objQuestionManagementColl1 = new QuestionManagementColl();
                objQuestionManagementColl1.objQuestionManagement = new List<QuestionManagement>();


               
                for (int tt = 0; tt < objQuestionManagementColl.objQuestionManagement.Count; tt++)
                {
                    if ((Utility.IsAdmin()) || (!string.IsNullOrWhiteSpace(objQuestionManagementColl.objQuestionManagement[tt].MockTestDate) && Convert.ToDateTime(objQuestionManagementColl.objQuestionManagement[tt].MockTestDate).AddDays(KnowledgeMatrix.Properties.Settings.Default.DaysToAdd) >= System.DateTime.Now))
                    {
                        objQuestionManagementColl1.objQuestionManagement.Add(objQuestionManagementColl.objQuestionManagement[tt]);
                        nRowCount++;
                    
                      for (int tt1 = 0; tt1 < objQuestionManagementColl.objQuestionManagement[tt].objResultList.Count; tt1++)
                      {
                          objQuestionManagementColl.objQuestionManagement[tt].objResultList[tt1].ExamName = objQuestionManagementColl.objQuestionManagement[tt].ExamName;
                          objResultList.Add(objQuestionManagementColl.objQuestionManagement[tt].objResultList[tt1]);
                       }
                    }
                }
                objQuestionManagementColl = objQuestionManagementColl1;

            }
            ShanuNestedDataGridView.DataClass.OrderMasterBindClass.objMasterDGVBind.Clear();
            for (int i = 0; i < objQuestionManagementColl.objQuestionManagement.Count; i++)
            {
                ShanuNestedDataGridView.DataClass.OrderMasterBindClass obj1 = new ShanuNestedDataGridView.DataClass.OrderMasterBindClass("", objQuestionManagementColl.objQuestionManagement[i].ExamName, objQuestionManagementColl.objQuestionManagement[i].TotalQuestions, objQuestionManagementColl.objQuestionManagement[i].TestStatus, DateTime.Now, objQuestionManagementColl.objQuestionManagement[i].TestResult, objQuestionManagementColl.objQuestionManagement[i].ExamPasPercentageScore, objQuestionManagementColl.objQuestionManagement[i].ExamMode,objQuestionManagementColl.objQuestionManagement[i].TestResult,objQuestionManagementColl.objQuestionManagement[i].TestStatus);
                ShanuNestedDataGridView.DataClass.OrderMasterBindClass.objMasterDGVBind.Add(obj1);
            }
        }

        private void Detail_BindData()
        {
            ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Clear();
            for (int j = 0; j < objResultList.Count; j++)
            {
                ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj1 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass(objResultList[j].UserName, objResultList[j].ExamName, objResultList[j].TestMark, objResultList[j].TestSatus, 150, 4);
                ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj1);
            }
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj1 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_001", "Order_001", "Burger Set", "With double chees", 150, 4);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj2 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_002", "Order_001", "Chicken Fry", "Spicy", 120, 2);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj3 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_003", "Order_001", "Fruit Salad", "WithIce cream", 75, 2);

            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj4 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_004", "Order_002", "Bibimbap", "Hot", 450, 2);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj5 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_005", "Order_002", "Sundubu", "Spicy", 390, 1);

            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj6 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_006", "Order_003", "Pizza", "Hot and served fast", 235, 1);

            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj7 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_007", "Order_005", "Kimchi jjigae", "Spicy", 650, 4);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj8 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_008", "Order_005", "Chicken Fry", "Spicy", 120, 2);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj9 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_009", "Order_005", "Fruit Salad", "WithIce cream", 75, 2);

            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj10 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_010", "Order_006", "chicken kebab", "Spicy", 250, 3);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass obj11 = new ShanuNestedDataGridView.DataClass.OrderDetailBindClass("Ord_dtl_011", "Order_006", "Lamb kebab", "Spicy", 300, 2);


            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj1);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj2);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj3);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj4);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj5);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj6);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj7);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj8);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj9);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj10);
            //ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj11);


        }

        // to generate Master Datagridview with your coding
        public void MasterGrid_Initialize()
        {

            //First generate the grid Layout Design
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Layouts(Master_shanuDGV, Color.LightSteelBlue, Color.AliceBlue, Color.WhiteSmoke, false, Color.Brown, false, false, false);
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.obj = this;
            //Set Height,width and add panel to your selected control
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Generategrid(Master_shanuDGV, pnlShanuGrid, 850, 430, 10, 10);

            Master_shanuDGV.ColumnHeadersHeight = 40;
            
            
            Master_shanuDGV.BorderStyle = BorderStyle.Fixed3D;


            
            // Color Image Column creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.ImageColumn, "img", "", "", true, 26, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Order_No", "Test Name", "Test Name", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Table_ID", "No Of Questions", "No Of Questions", true, 100, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Description", "Test Status", "Test Status", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Order_DATE", "Order DATE", "Order DATE", false, 140, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Exam_Mode", "Exam Mode", "Exam Mode", true, 100, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Pass_Percentage", "Pass Percentage", "Pass Percentage", true, 100, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Waiter_ID", "Test Result", "Test Result", true, 120, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.Button, "TestStatus", "Test Status", "Test", true, 120, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.Button, "TestResult", "View Results", "View Results", true, 120, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            //Convert the List to DataTable
            DataTable detailTableList = ListtoDataTable(ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind);

            // Image Colum Click Event - In  this method we create an event for cell click and we will display the Detail grid with result.

            objshanudgvHelper.DGVMasterGridClickEvents(Master_shanuDGV, Detail_shanuDGV, Master_shanuDGV.Columns["img"].Index, ShanuEventTypes.cellContentClick, ShanuControlTypes.ImageColumn, detailTableList, "Order_No");
   
            Master_shanuDGV.DataSource = null;
            // Bind data to DGV.
            Master_shanuDGV.DataSource = ShanuNestedDataGridView.DataClass.OrderMasterBindClass.objMasterDGVBind;
         /*  for (int i = 0; i < objQuestionManagementColl.objQuestionManagement.Count; i++)
            {
                if (objQuestionManagementColl.objQuestionManagement[i].ExamMode == "CBT--Single User")
                {
                    Master_shanuDGV.Rows[i].DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.LightBlue };
                }
                else
                {
                    Master_shanuDGV.Rows[i].DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.MediumAquamarine };
                    //  dataGridView1.Rows[i].Cells[1].Style = dataGridView1.DefaultCellStyle;
                }
            }
            Master_shanuDGV.Update();// = buttonColumn3;*/

        }
        //List to Data Table Convert
        private static DataTable ListtoDataTable<T>(IEnumerable<T> DetailList)
        {
            Type type = typeof(T);
            var typeproperties = type.GetProperties();

            DataTable listToDT = new DataTable();
            foreach (PropertyInfo propInfo in typeproperties)
            {
                listToDT.Columns.Add(new DataColumn(propInfo.Name, propInfo.PropertyType));
            }

            foreach (T ListItem in DetailList)
            {
                object[] values = new object[typeproperties.Length];
                for (int i = 0; i < typeproperties.Length; i++)
                {
                    values[i] = typeproperties[i].GetValue(ListItem, null);
                }

                listToDT.Rows.Add(values);
            }

            return listToDT;
        }


        // to generate Detail Datagridview with your coding
        public void DetailGrid_Initialize()
        {

            //First generate the grid Layout Design
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Layouts(Detail_shanuDGV, Color.Peru, Color.Wheat, Color.Tan, false, Color.Sienna, false, false, false);

            //Set Height,width and add panel to your selected control
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Generategrid(Detail_shanuDGV, pnlShanuGrid, 800, 200, 10, 10);

            // Color Dialog Column creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Order_Detail_No", "Name Of The Student", "Name Of The Student", true, 290, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.Button, "Order_No", "Order NO", "Order NO", false, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Blue, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Item_Name", "Test Score", "Test Score", true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Notes", "Test Status", "Test Status", true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.Button, "Action", "Action", "Action", true, 70, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            //ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "QTY", "QTY", "QTY", true, 40, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            objshanudgvHelper.DGVDetailGridClickEvents(Detail_shanuDGV);


        }
        # endregion
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            tabPage2.BackColor = Color.LightGray;
            
        }
        private void setTextForRich(RichTextBoxPrintCtrl obj, string question)
        {
            obj.Visible = true;
            //if (question == null || question == "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\n}\n")
            //{
            //    obj.Visible = false;
            //else
            if (question != null && question.Contains("\\rtf1\\ansi"))
                obj.Rtf = question;
            else if (question != null && question.Length > 0)
                obj.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang16393{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}\viewkind4\uc1\pard\f0\fs20" + question + @"}";
            if (obj.TextLength == 0)
                obj.Visible = false;
        }
        #region First Tab
        private void InitialiseGrid()
        {
            /*   DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
               col0.ReadOnly = true;
               col0.HeaderText = "Test Name";
               col0.Name = "ExamName";
               dataGridView1.Columns.Add(col0);
               this.dataGridView1.Columns[0].Width = 280;

               DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
               col1.ReadOnly = true;
               col1.HeaderText = "No Of Questions";
               col1.Name = "NpOfQuestions";
               dataGridView1.Columns.Add(col1);
               this.dataGridView1.Columns[1].Width = 95;

               DataGridViewTextBoxColumn col11 = new DataGridViewTextBoxColumn();
               col11.ReadOnly = true;
               col11.HeaderText = "Duration (HH:MM)";
               col11.Name = "Duration";
               dataGridView1.Columns.Add(col11);
               this.dataGridView1.Columns[2].Width = 90;

               DataGridViewTextBoxColumn buttonColumn =
                  new DataGridViewTextBoxColumn();
               buttonColumn.ReadOnly = true;
               buttonColumn.HeaderText = "Test Status";
               buttonColumn.Name = "TestStatus";
              // buttonColumn.Text = "TestStatus";
               buttonColumn.Tag = "Question";
               dataGridView1.Columns.Add(buttonColumn);


               DataGridViewTextBoxColumn buttonColumn1 =
   new DataGridViewTextBoxColumn();
               buttonColumn1.ReadOnly = true;
               buttonColumn1.HeaderText = "Test Result";
               buttonColumn1.Name = "TestResult";
               buttonColumn1.Tag = "TestResult";

              // buttonColumn1.Text = "Generate";
               //buttonColumn1.UseColumnTextForButtonValue = false;

               dataGridView1.Columns.Add(buttonColumn1);


               DataGridViewDisableButtonColumn buttonColumn2 =
            new DataGridViewDisableButtonColumn();
               buttonColumn2.ReadOnly = true;
               buttonColumn2.HeaderText = "Online Test";
               buttonColumn2.Name = "Action";
               buttonColumn2.Tag = "Action";
               buttonColumn2.Text = "Generate";

            
               //buttonColumn2.UseColumnTextForButtonValue = false;            
               dataGridView1.Columns.Add(buttonColumn2);
            

               this.dataGridView1.Columns[3].Width = 80;

               DataGridViewDisableButtonColumn buttonColumn4 =
            new DataGridViewDisableButtonColumn();
               buttonColumn4.ReadOnly = true;
               buttonColumn4.HeaderText = "View Result";
               buttonColumn4.Name = "Result";
               buttonColumn4.Tag = "Result";
               buttonColumn4.Text = "Result";
            
               dataGridView1.Columns.Add(buttonColumn4);
              // dataGridView1.ScrollBars = ScrollBars.Both;
              // dataGridView1.AutoSize = true;
               dataGridView1.AllowUserToAddRows = false;
               dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                   DataGridViewContentAlignment.MiddleCenter;
               dataGridView1.Update();// = buttonColumn3;
             //  dataGridView1.Sort(col0, ListSortDirection.Ascending);
              // dataGridView1.Dock = DockStyle.Left;
               dataGridView1.BackgroundColor = Color.LightGray;
           //    dataGridView1.BorderStyle = BorderStyle.Fixed3D;

               // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
               // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
               dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
               //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Red;
               // Set the selection background color for all the cells.
            //   dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
             //  dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                //Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
       // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
       //dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

               // Set the background color for all rows and for alternating rows.  
               // The value for alternating rows overrides the value for all rows. 
      

               LoadTextData();
               dataGridView1.CellClick +=
               new DataGridViewCellEventHandler(dataGridView1_CellClick);*/
            //RANGA STARTS
            //First generate the grid Layout Design
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Layouts(Master_shanuDGV, Color.LightSteelBlue, Color.AliceBlue, Color.WhiteSmoke, false, Color.SteelBlue, false, false, false);

            //Set Height,width and add panel to your selected control
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Generategrid(Master_shanuDGV, pnlShanuGrid, 850, 380, 10, 10);

            Master_shanuDGV.ScrollBars = ScrollBars.Both;
            // Color Image Column creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.ImageColumn, "img", "", "", true, 26, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "ExamName", "Test Name", "Test Name", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "TotalQuestions", "No Of Questions", "No Of Questions", true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "TestStatus", "Test Status", "Test Status", true, 320, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "TestResult", "Test Result", "Test Result", true, 140, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
         //   ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Waiter_ID", "Waiter_ID", "Waiter_ID", true, 120, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            //Convert the List to DataTable
           // DataTable detailTableList = ListtoDataTable(ShanuNestedDataGridView.DataClass.OrderDetailBindClass.objDetailDGVBind);

            // Image Colum Click Event - In  this method we create an event for cell click and we will display the Detail grid with result.

          //  objshanudgvHelper.DGVMasterGridClickEvents(Master_shanuDGV, Detail_shanuDGV, Master_shanuDGV.Columns["img"].Index, ShanuEventTypes.cellContentClick, ShanuControlTypes.ImageColumn, detailTableList, "Order_No");

            

            LoadTextData();
            // Bind data to DGV.
           // Master_shanuDGV.DataSource = DataClass.OrderMasterBindClass.objMasterDGVBind;
            

            //RANGA ENDS
        }
        
        private void InitialiseGridSecond()
        {

            DataGridViewTextBoxColumn col00 = new DataGridViewTextBoxColumn();
            col00.ReadOnly = true;
            col00.HeaderText = "Question No";
            col00.Name = "QuesNo";
            dataGridView2.Columns.Add(col00);
            this.dataGridView2.Columns[0].Width = 65;

            DataGridViewTextBoxColumn col112 = new DataGridViewTextBoxColumn();
            col112.ReadOnly = true;
            col112.HeaderText = "Your Response";
            col112.Name = "Response";
            col112.Visible = false;
            dataGridView2.Columns.Add(col112);
            this.dataGridView2.Columns[1].Width = 72;
            

            
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Your Response ";
            //Add twice the padding for the left and  
            //right sides of the cell.
            imageColumn.Width = 75;
            imageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                imageColumn.ImageLayout= DataGridViewImageCellLayout.Normal;
            imageColumn.Name = "StatusImage";    
            
            dataGridView2.Columns.Add(imageColumn);
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dataGridView1.Rows.Count > 0)
            {
                nRowIndex = 0;
                button9.Enabled = button6.Enabled = button7.Enabled = button8.Enabled = true;
            }
            
        }

        private void DisplayStatus()
        {
            dataGridView2.RowCount = resultQuestions.objQuestionDetail.Count();
            for (int nm = 0; nm < resultQuestions.objQuestionDetail.Count(); nm++)
            {
                Bitmap unMarked = new Bitmap(global::KnowledgeMatrix.Properties.Resources.MT_notattempted1);// @"notattempted1.png");
                dataGridView2.Rows[nm].Cells["QuesNo"].Value = Convert.ToString(nm + 1);
                dataGridView2.Rows[nm].Cells["Response"].Value = resultQuestions.objQuestionDetail[nm].AnsRespType == null ? "Yet To Attend" : resultQuestions.objQuestionDetail[nm].AnsRespType;
                if(dataGridView2.Rows[nm].Cells["Response"].Value.ToString() =="Yet To Attend")
                    unMarked=(Bitmap)imageList1.Images[1];
                   // unMarked = new Bitmap(Application.StartupPath + @"\notattempted.png");
                else if (dataGridView2.Rows[nm].Cells["Response"].Value.ToString() == "Skip")
                    unMarked=(Bitmap)imageList1.Images[2];
                   
                else if (dataGridView2.Rows[nm].Cells["Response"].Value.ToString() == "Mark for review")
                    unMarked = (Bitmap)imageList1.Images[3];
                 else if (dataGridView2.Rows[nm].Cells["Response"].Value.ToString() == "Completed")
                    unMarked = (Bitmap)imageList1.Images[0];
                
                
                 dataGridView2.Rows[nm].Cells["StatusImage"].Value = unMarked;

            }
        }
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nRowIndex = e.RowIndex;
            if(nRowIndex >= 0)
                 button9.Enabled = button6.Enabled = button7.Enabled = button8.Enabled = true;

            if (e.ColumnIndex == 8)
            {
                //TEST Launch
                //DataGridViewDisableButtonCell buttonCell;
                //buttonCell =
                //            (DataGridViewDisableButtonCell)Master_shanuDGV.
                //            Rows[e.RowIndex].Cells["TestStatus"];

                

                CaptureMockDetails obj = new CaptureMockDetails();
                obj.mode = ShanuNestedDataGridView.DataClass.OrderMasterBindClass.objMasterDGVBind[e.RowIndex].TestStatus;//buttonCell.Value.ToString();
                if (objQuestionManagementColl.objQuestionManagement[e.RowIndex].ExamMode == "CBT--Single User" && obj.mode.ToString() == "Take Re-Test")
                    obj.strName = objQuestionManagementColl.objQuestionManagement[e.RowIndex].objResultList[0].UserName;
                obj.ShowDialog();
                if (obj.mode.ToString() == "Take Re-Test")
                {
                    //isManualTabThird = true;
                    //tabControl1.SelectedTab = tabPage3;
                    //LoadResultGrid();
                    
                    if(obj.mode=="OK")


                    //if (DialogResult.Yes == MessageBox.Show("Taking re-test will erase the result of previous attempt?", "Delete", MessageBoxButtons.YesNo))
                    {
                        txtName.Text = obj.strName;
                        label39.Text = obj.strName;
                        LoadQuestionFromXml(objQuestionManagementColl.objQuestionManagement[e.RowIndex].FileName);
                        for (int i = 0; i < resultQuestions.objQuestionDetail.Count(); i++)
                        {
                            resultQuestions.objQuestionDetail[i].AnswerResponse = null;
                            resultQuestions.objQuestionDetail[i].AnsRespType = null;
                        }
                        ObjectXMLSerializer<QuestionDetailData>.Save(resultQuestions, objQuestionManagementColl.objQuestionManagement[nRowIndex].FileName);
                    }
                    else
                    {
                        return;
                    }
                }

                //else
                {

                    if (obj.mode == "OK")
                    {
                        isManualTabSecond = true;
                        tabControl1.SelectedTab = tabPage2;
                        label39.Text=txtName.Text = obj.strName;


                        //Load the Question Paper
                        label14.Text = objQuestionManagementColl.objQuestionManagement[e.RowIndex].ExamName;
                        label15.Text = objQuestionManagementColl.objQuestionManagement[e.RowIndex].ExamPasPercentageScore;
                        label1.Text = objQuestionManagementColl.objQuestionManagement[e.RowIndex].QuestionTopic;
                        label17.Text = objQuestionManagementColl.objQuestionManagement[e.RowIndex].TestTime;
                        LoadQuestionFromXml(objQuestionManagementColl.objQuestionManagement[e.RowIndex].FileName);
                        nRowIndex = e.RowIndex;
                        currRecord = 0;

                        DisplayQuestions();




                        //Start The Timer
                        _timerRunning = false;
                        // Reset the elapsed time TimeSpan objects
                        _totalElapsedTime = TimeSpan.Zero;
                        _currentElapsedTime = TimeSpan.Zero;
                        startButton_Click();
                    }
                }
            }
            else if (e.ColumnIndex == 6)
            {
                //RESULT Launch
                DataGridViewDisableButtonCell buttonCell;
                buttonCell =
                            (DataGridViewDisableButtonCell)dataGridView1.
                            Rows[e.RowIndex].Cells["Result"];
                if (buttonCell.Value.ToString() != "No Result")
                {
                    //CaptureMockDetails obj = new CaptureMockDetails();
                    ViewMockResult obj = new ViewMockResult();
                    
                    if (objQuestionManagementColl.objQuestionManagement[e.RowIndex].objResultList != null &&
                        objQuestionManagementColl.objQuestionManagement[e.RowIndex].objResultList.Count() > 0)
                    {
                        obj.objQuestionManagement = objQuestionManagementColl.objQuestionManagement[e.RowIndex];
                     //   obj.nameColl = new Object[objQuestionManagementColl.objQuestionManagement[e.RowIndex].objResultList.Count()];
                       // for(int i=0;i<objQuestionManagementColl.objQuestionManagement[e.RowIndex].objResultList.Count() ;i++)
                        //obj.nameColl[i]=objQuestionManagementColl.objQuestionManagement[e.RowIndex].objResultList[i].UserName;
                    }
                  //  obj.mode = "View";
                    obj.ShowDialog();
                    
                   if (obj.RowChoosen != -1)
                    {
                        label39.Text= txtName.Text = objQuestionManagementColl.objQuestionManagement[e.RowIndex].objResultList[obj.RowChoosen].UserName;
                        LoadResultGrid(objQuestionManagementColl.objQuestionManagement[e.RowIndex].objResultList[obj.RowChoosen].FileName, true);

                        isManualTabThird = true;
                        tabControl1.SelectedTab = tabPage3;
                    }
                  //  LoadResultGrid();
                }
            }
            
        }

        public void NavigateToResult(int gridMstRwIdx, int gridChildRwIdx)
        {
            label39.Text = txtName.Text = objQuestionManagementColl.objQuestionManagement[gridMstRwIdx].objResultList[gridChildRwIdx].UserName;
            LoadResultGrid(objQuestionManagementColl.objQuestionManagement[gridMstRwIdx].objResultList[gridChildRwIdx].FileName, true);

            isManualTabThird = true;
            tabControl1.SelectedTab = tabPage3;
        }
        private void DisplayQuestions()
        {
            label18.Text = (currRecord+1).ToString() + " of " + resultQuestions.objQuestionDetail.Count.ToString();

            setTextForRich(richTextBoxPrintCtrl1, resultQuestions.objQuestionDetail[currRecord].Question);

            setTextForRich(richTextBoxPrintCtrl4, resultQuestions.objQuestionDetail[currRecord].OptionOne);

            setTextForRich(richTextBoxPrintCtrl5, resultQuestions.objQuestionDetail[currRecord].OptionTwo);

            setTextForRich(richTextBoxPrintCtrl6, resultQuestions.objQuestionDetail[currRecord].OptionThree);

            setTextForRich(richTextBoxPrintCtrl7, resultQuestions.objQuestionDetail[currRecord].OptionFour);
            radioPanel.Visible = false;
            checkboxPanel.Visible = false;

            if (resultQuestions.objQuestionDetail[currRecord].AnswerType == "Multi Choice" || resultQuestions.objQuestionDetail[currRecord].AnswerType == "Multi-Image")
            {
                checkboxPanel.Visible = true;
                checkBox1.Visible = checkBox2.Visible = checkBox3.Visible = checkBox4.Visible = true;
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;

                //checkBox1.Visible = richTextBoxPrintCtrl4.TextLength > 0 ? true : false;
                checkBox2.Visible = richTextBoxPrintCtrl5.TextLength > 0 ? true : false;
                checkBox3.Visible = richTextBoxPrintCtrl6.TextLength > 0 ? true : false;
                checkBox4.Visible = richTextBoxPrintCtrl7.TextLength > 0 ? true : false;

                string sAnswer = resultQuestions.objQuestionDetail[currRecord].AnswerResponse;
                if (!string.IsNullOrWhiteSpace(sAnswer))
                {
                    for (int i = 0; i < sAnswer.Split(',').Count(); i++)
                    {
                        
                        int x = Convert.ToInt16(sAnswer.Split(',')[i]);
                        switch (x)
                        {
                            case 1:
                                checkBox1.Checked = true;
                                break;

                            case 2:
                                checkBox2.Checked = true;
                                break;

                            case 3:
                                checkBox3.Checked = true;
                                break;

                            case 4:
                                checkBox4.Checked = true;
                                break;

                        }
                    }
                }
            }
            else if (resultQuestions.objQuestionDetail[currRecord].AnswerType == "Single Choice" || resultQuestions.objQuestionDetail[currRecord].AnswerType == "Single-Image")
            {
                radioPanel.Visible = true;
                radioButton1.Visible = radioButton2.Visible = radioButton3.Visible = radioButton4.Visible = true;
                radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
                radioButton1.Visible = richTextBoxPrintCtrl4.TextLength > 0 ? true : false;
                radioButton2.Visible = richTextBoxPrintCtrl5.TextLength > 0 ? true : false;
                radioButton3.Visible = richTextBoxPrintCtrl6.TextLength > 0 ? true : false;
                radioButton4.Visible = richTextBoxPrintCtrl7.TextLength > 0 ? true : false;
                pictureBox6.Visible = pictureBox7.Visible = pictureBox8.Visible = pictureBox9.Visible = false;

                if (!string.IsNullOrWhiteSpace(resultQuestions.objQuestionDetail[currRecord].AnswerResponse))
                {
                    int x = Convert.ToInt16(resultQuestions.objQuestionDetail[currRecord].AnswerResponse);
                    switch (x)
                    {
                        case 1:
                            radioButton1.Checked = true;

                            break;

                        case 2:
                            radioButton2.Checked = true;

                            break;

                        case 3:
                            radioButton3.Checked = true;

                            break;

                        case 4:
                            radioButton4.Checked = true;

                            break;

                    }
                }

            }
            else if (resultQuestions.objQuestionDetail[currRecord].AnswerType == "Multi-Image")
            {
                checkboxPanel.Visible = true;
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;
            }
            else if (resultQuestions.objQuestionDetail[currRecord].AnswerType == "Single-Image")
            {
                radioPanel.Visible = true;
                radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
            }

            First.Enabled = true;
            Last.Enabled = true;
            DisplayStatus();
            if (currRecord+1 == resultQuestions.objQuestionDetail.Count())
                Last.Enabled = false;
            if (currRecord == 0)
                First.Enabled = false;
        }
        public void LoadTextData()
        {
         
   
            
            if (File.Exists(Application.StartupPath +Utility.FolderType() +@"MockTest\MockTestList.txt"))
                objQuestionManagementColl = ObjectXMLSerializer<QuestionManagementColl>.Load(Application.StartupPath+ Utility.FolderType()+ @"MockTest\MockTestList.txt");
            if (objQuestionManagementColl != null)
            {
                int nRowCount = 0;
                QuestionManagementColl objQuestionManagementColl1 = new QuestionManagementColl();   
                objQuestionManagementColl1.objQuestionManagement = new List<QuestionManagement>();
                  List<ResultList> objResultList = new List<ResultList>();
                

                for (int tt = 0; tt < objQuestionManagementColl.objQuestionManagement.Count; tt++)
                {
                    if ((Utility.IsAdmin()) || (!string.IsNullOrWhiteSpace(objQuestionManagementColl.objQuestionManagement[tt].MockTestDate) && Convert.ToDateTime(objQuestionManagementColl.objQuestionManagement[tt].MockTestDate).AddDays(KnowledgeMatrix.Properties.Settings.Default.DaysToAdd) >= System.DateTime.Now))
                    {
                        objQuestionManagementColl1.objQuestionManagement.Add(objQuestionManagementColl.objQuestionManagement[tt]);
                        nRowCount++;
                    }
                    for (int tt1 = 0; tt1 < objQuestionManagementColl.objQuestionManagement[tt].objResultList.Count; tt1++)
                    {
                        objQuestionManagementColl.objQuestionManagement[tt].objResultList[tt1].ExamName = objQuestionManagementColl.objQuestionManagement[tt].ExamName;
                        objResultList.Add(objQuestionManagementColl.objQuestionManagement[tt].objResultList[tt1]);
                    }
                }
                objQuestionManagementColl = objQuestionManagementColl1;

            //RANGA STARTS
                Master_shanuDGV.DataSource = objQuestionManagementColl.objQuestionManagement;
                //Convert the List to DataTable

                DataTable detailTableList = ListtoDataTable(objResultList);
                objshanudgvHelper.DGVMasterGridClickEvents(Master_shanuDGV, Detail_shanuDGV, Master_shanuDGV.Columns["img"].Index, ShanuEventTypes.cellContentClick, ShanuControlTypes.ImageColumn, detailTableList, "ExamName");
                //
                //First generate the grid Layout Design
                ShanuNestedDataGridView.Helper.ShanuDGVHelper.Layouts(Detail_shanuDGV, Color.Peru, Color.Wheat, Color.Tan, false, Color.Sienna, false, false, false);

                //Set Height,width and add panel to your selected control
                ShanuNestedDataGridView.Helper.ShanuDGVHelper.Generategrid(Detail_shanuDGV, pnlShanuGrid, 800, 200, 10, 10);

                // Color Dialog Column creation
                ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "UserName", "Name of the student", "Name of the student", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, detailTableList, "UserName", "UserName", Color.Black);

                // BoundColumn creation
                ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.Button, "ExamName", "Test Score", "Test Score", true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Blue, null, "", "", Color.Black);

                // BoundColumn creation
                ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "TestSatus", "Test Status", "Test Status", true, 160, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


                // BoundColumn creation
                // ShanuNestedDataGridView.Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Notes", "Notes", "Notes", true, 260, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


                // BoundColumn creation
                //Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Price", "Price", "Price", true, 70, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


                // BoundColumn creation
                //Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "QTY", "QTY", "QTY", true, 40, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


                objshanudgvHelper.DGVDetailGridClickEvents(Detail_shanuDGV);

                //
               // Detail_shanuDGV.DataSource = objResultList;
                //RANGA ENDS

                   /*dataGridView1.RowCount = objQuestionManagementColl.objQuestionManagement.Count;
               // dataGridView1.RowCount = nRowCount;
                for (int i = 0; i < objQuestionManagementColl.objQuestionManagement.Count; i++)
                {
                 //   if ((Utility.IsAdmin()) || (!string.IsNullOrWhiteSpace(objQuestionManagementColl.objQuestionManagement[i].MockTestDate) && Convert.ToDateTime(objQuestionManagementColl.objQuestionManagement[i].MockTestDate).AddDays(Properties.Settings.Default.DaysToAdd) >= System.DateTime.Now))
                   // {
                    dataGridView1.Rows[i].Cells["Result"].Value = objQuestionManagementColl.objQuestionManagement[i].TestResult == null ? "No Result" : " View Results";//: objQuestionManagementColl.objQuestionManagement[i].TestResult + "%";
                    
                        dataGridView1.Rows[i].Cells["TestResult"].Value = objQuestionManagementColl.objQuestionManagement[i].TestResult == null ? "" : objQuestionManagementColl.objQuestionManagement[i].TestResult + "%";
                    objQuestionManagementColl.objQuestionManagement[i].TestStatus = objQuestionManagementColl.objQuestionManagement[i].TestStatus == null ? "Not Taken" : objQuestionManagementColl.objQuestionManagement[i].TestStatus;
                        dataGridView1.Rows[i].Cells["TestStatus"].Value = objQuestionManagementColl.objQuestionManagement[i].TestStatus;
                        dataGridView1.Rows[i].Cells["Action"].Value = objQuestionManagementColl.objQuestionManagement[i].TestStatus == "Not Taken" ? "Take a Test"  : "Take Re-Test";
                        dataGridView1.Rows[i].Cells["ExamName"].Value = objQuestionManagementColl.objQuestionManagement[i].ExamName;
                        dataGridView1.Rows[i].Cells["NpOfQuestions"].Value = objQuestionManagementColl.objQuestionManagement[i].TotalQuestions;
                        dataGridView1.Rows[i].Cells["Duration"].Value = objQuestionManagementColl.objQuestionManagement[i].TestTime;
                        //dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightBlue;
                        //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.MediumAquamarine;

                        if (objQuestionManagementColl.objQuestionManagement[i].ExamMode == "CBT--Single User")
                        {
                            dataGridView1.Rows[i].DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.LightBlue };
                        }
                        else
                        {
                            dataGridView1.Rows[i].DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.MediumAquamarine };
                          //  dataGridView1.Rows[i].Cells[1].Style = dataGridView1.DefaultCellStyle;
                        }

                    //}

                }*/
            }

        }
        #endregion
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        #region Second Tab
        private void LoadQuestionFromXml(string FileName)
        {
            resultQuestions = null;
           // FileName = Application.StartupPath + @"\" + FileName + ".txt";
            if (File.Exists(FileName))
                resultQuestions = ObjectXMLSerializer<QuestionDetailData>.Load(FileName);
            else
                MessageBox.Show("Contact Admin");
           
        }


        /// <summary>
        /// Handle Start/Stop button click
        /// </summary>
        /// <param name="sender">The Button control</param>
        /// <param name="e">EventArgs object</param>
        private void startButton_Click()
        {
            // If the timer isn't already running
            if (!_timerRunning)
            {
                // Set the start time to Now
                _startTime = DateTime.Now;

                // Store the total elapsed time so far
                _totalElapsedTime = _currentElapsedTime;

                _timer1.Start();
                _timerRunning = true;
            }
            else // If the timer is already running
            {
                if(_timer1 != null)
                    _timer1.Stop();
                _timerRunning = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            // We do this to chop off any stray milliseconds resulting from 
            // the Timer's inherent inaccuracy, with the bonus that the 
            // TimeSpan.ToString() method will now show correct HH:MM:SS format
            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,timeSinceStartTime.Minutes,timeSinceStartTime.Seconds);

            // The current elapsed time is the time since the start button was
            // clicked, plus the total time elapsed since the last reset
            //_currentElapsedTime = timeSinceStartTime + _totalElapsedTime;

            // These are just two Label controls which display the current 
            // elapsed time and total elapsed time
            //_totalElapsedTimeDisplay.Text = _currentElapsedTime.ToString();
            label16.Text = timeSinceStartTime.ToString();

            //if (label16.Text == "00:00:10")
              //  _timer1.Stop();
            if (label16.Text.StartsWith(label17.Text))
            {
                _timer1.Stop();
                button3_Click(null, null);
            }
            //label16.Text = DateTime.Now.TimeOfDay.ToString();
        }
        #endregion

        private void Last_Click(object sender, EventArgs e)
        {
            LogAnswer();
            currRecord = resultQuestions.objQuestionDetail.Count()-1;
            DisplayQuestions();
        }

        
        private void First_Click(object sender, EventArgs e)
        {
            LogAnswer();
            currRecord = 0;
            DisplayQuestions();
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            
            LogAnswer();
            if (string.IsNullOrWhiteSpace(resultQuestions.objQuestionDetail[currRecord].AnswerResponse))
            {
                MessageBox.Show("Choose the option to continue");
                return;
            }
            resultQuestions.objQuestionDetail[currRecord].AnsRespType = "Completed";
            if (currRecord != resultQuestions.objQuestionDetail.Count() - 1)
            {
                currRecord++;
                DisplayQuestions();
            }
            else
            {
                DisplayStatus();
                button3_Click(null, null);
            }
            
        }

        private void LogAnswer()
        {
            if (resultQuestions.objQuestionDetail[currRecord].AnswerType == "Multi Choice" || resultQuestions.objQuestionDetail[currRecord].AnswerType == "Multi-Image")
            {
                StringBuilder strBld = new StringBuilder();
                strBld.Append(checkBox1.Checked ? "1," : "");
                strBld.Append(checkBox2.Checked ? "2," : "");
                strBld.Append(checkBox3.Checked ? "3," : "");
                strBld.Append(checkBox4.Checked ? "4," : "");
                if(strBld.Length > 0)
                    strBld.Remove(strBld.Length-1, 1);
                resultQuestions.objQuestionDetail[currRecord].AnswerResponse = strBld.ToString();                
            }
            else
            {
                resultQuestions.objQuestionDetail[currRecord].AnswerResponse = radioButton1.Checked ? "1" : (radioButton2.Checked ? "2" : (radioButton3.Checked ? "3" : (radioButton4.Checked? "4":"")));
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            
            LogAnswer();
            if (string.IsNullOrWhiteSpace(resultQuestions.objQuestionDetail[currRecord].AnswerResponse))
            {
                MessageBox.Show("Choose the option to continue");
                return;
            }
            resultQuestions.objQuestionDetail[currRecord].AnsRespType = "Mark for review";
            if (currRecord != resultQuestions.objQuestionDetail.Count() - 1)
            {
                currRecord++;
                DisplayQuestions();
            }
            else
            {
                DisplayStatus();
                button3_Click(null, null);
            }
            //currRecord++;
            //DisplayQuestions();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultQuestions.objQuestionDetail[currRecord].AnsRespType = "Skip";
            LogAnswer();
            if (string.IsNullOrWhiteSpace(resultQuestions.objQuestionDetail[currRecord].AnswerResponse))
            {
                MessageBox.Show("You are proceeding without providing options","Info");
                  }
            if (currRecord != resultQuestions.objQuestionDetail.Count() - 1)
            {
                currRecord++;


                DisplayQuestions();
            }
            else
            {
                DisplayStatus();
                button3_Click(null, null);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            isManualTabThird = true;
            LogAnswer();
            //Check if any record is marked for Review

            List<QuestionDetail>  q = (from QuestionDetail in resultQuestions.objQuestionDetail
                                       where QuestionDetail.AnsRespType == "Mark for review" || QuestionDetail.AnsRespType == "Skip" 
                                     select QuestionDetail).ToList();
            System.Windows.Forms.DialogResult answer;
            if (q != null && q.Count() > 0)
                answer = MessageBox.Show("There are some answers marked for review/skipped. Are you sure to mark exam complete?", "Exam Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            else            
                answer = MessageBox.Show("Are you sure to mark exam complete?", "Exam Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == System.Windows.Forms.DialogResult.Yes)
            {
                objQuestionManagementColl.objQuestionManagement[nRowIndex].TestStatus = "Test taken";

                ResultList objResultList = new ResultList();
                objResultList.UserName = txtName.Text;
                objResultList.DateTaken = DateTime.Now.ToLocalTime().ToString();
                Utility.validateAndCreateDirectory( @"\MockTest\" + objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamName);

                objResultList.FileName = Application.StartupPath + Utility.FolderType() + @"MockTest\" + objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamName +@"\"+ txtName.Text.Replace(" ", "") + ".txt";


                if (objQuestionManagementColl.objQuestionManagement[nRowIndex].objResultList == null)
                    objQuestionManagementColl.objQuestionManagement[nRowIndex].objResultList = new List<ResultList>();
                if(objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamMode == "CBT--Single User")
                    objQuestionManagementColl.objQuestionManagement[nRowIndex].objResultList = new List<ResultList>();
                objQuestionManagementColl.objQuestionManagement[nRowIndex].objResultList.Add(objResultList);


                int totalAnswer = 0;
                for (int i = 0; i < resultQuestions.objQuestionDetail.Count(); i++)
                {
                    if (resultQuestions.objQuestionDetail[i].AnswerResponse == resultQuestions.objQuestionDetail[i].Answer)
                        totalAnswer++;
                }
              //  totalAnswer = (totalAnswer *100) / resultQuestions.objQuestionDetail.Count;

                //if (Convert.ToDouble((totalAnswer*100 ) / resultQuestions.objQuestionDetail.Count) >= Convert.ToDouble(objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamPasPercentageScore))
                //    objQuestionManagementColl.objQuestionManagement[nRowIndex].TestResult = "Pass - " + ((totalAnswer*100) / resultQuestions.objQuestionDetail.Count).ToString();
                    
                //else
                //    objQuestionManagementColl.objQuestionManagement[nRowIndex].TestResult = "Fail - " + ((totalAnswer * 100) / resultQuestions.objQuestionDetail.Count).ToString();
                    

                

                ObjectXMLSerializer<QuestionManagementColl>.Save(objQuestionManagementColl, Application.StartupPath+ Utility.FolderType() + @"MockTest\MockTestList.txt");

                 ObjectXMLSerializer<QuestionDetailData>.Save(resultQuestions, objQuestionManagementColl.objQuestionManagement[nRowIndex].FileName);
                // if (objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamMode != "CBT--Single User") 
                    ObjectXMLSerializer<QuestionDetailData>.Save(resultQuestions, objResultList.FileName);

                 LoadResultGrid(objQuestionManagementColl.objQuestionManagement[nRowIndex].FileName,false);
                 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            tabPage1.BackColor = Color.LightGray;
            if (tabControl1.SelectedIndex == 0)
            {
                Master_shanuDGV = new DataGridView();
                // Declared for the Detail grid
                Detail_shanuDGV = new DataGridView();
                pnlShanuGrid.Controls.Clear();
                Master_BindData();

                // To bind the Detail data to List 
                Detail_BindData();

                MasterGrid_Initialize();

                DetailGrid_Initialize();
                //LoadTextData();
                _timerRunning = true;
                startButton_Click();
            }
            //tabPage3.BackColor = System.Control;
        }
        private void InitialiseResultGrid()
        {
            DataGridViewTextBoxColumn col00 = new DataGridViewTextBoxColumn();
            col00.ReadOnly = true;
            col00.HeaderText = "Test Result";
            col00.Name = "TestResult";
            dataGridView3.Columns.Add(col00);
            this.dataGridView3.Columns[0].Width = 300;

            DataGridViewTextBoxColumn col01= new DataGridViewTextBoxColumn();
            col01.ReadOnly = true;
            col01.HeaderText = "Number of Questions";
            col01.Name = "NumberofQuestions";
            dataGridView3.Columns.Add(col01);
            this.dataGridView3.Columns[1].Width = 100;

            DataGridViewTextBoxColumn col02 = new DataGridViewTextBoxColumn();
            col02.ReadOnly = true;
            col02.HeaderText = "No of Correct Answered";
            col02.Name = "NoofCorrectAnswered";
            dataGridView3.Columns.Add(col02);
            this.dataGridView3.Columns[2].Width = 100;

            DataGridViewTextBoxColumn col03 = new DataGridViewTextBoxColumn();
            col03.ReadOnly = true;
            col03.HeaderText = "Correct Answer %";
            col03.Name = "CorrectAnswer";
            dataGridView3.Columns.Add(col03);
            this.dataGridView3.Columns[3].Width = 170;

           
        }

        private void LoadResultGrid(string FileName,bool skip)       
        {

            label25.Text = objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamName;
            label27.Text = objQuestionManagementColl.objQuestionManagement[nRowIndex].TestTime;
            label28.Text = objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamPasPercentageScore;
            label1.Text = objQuestionManagementColl.objQuestionManagement[nRowIndex].QuestionTopic;
            label35.Text = txtName.Text;
            dataGridView3.Invalidate();
            //InitialiseResultGrid();
            bs = new BindingSource();
            bs.DataSource = typeof(MockResult);
           

            tabControl1.SelectedTab = tabPage3;
            //dataGridView3.DataSource = null;

            LoadQuestionFromXml(FileName);

            label26.Text = resultQuestions.objQuestionDetail.Count().ToString();
            var categories = 
             from p in resultQuestions.objQuestionDetail
             group p by p.CategoryName into g
             select new
             {
                TestResult = g.Key,
                QuestionAvlbl = g.Count()
             };
            foreach (var v in categories)
            {
                TestResults objTestResults = new TestResults();
                objTestResults.TestResult = v.TestResult;


            }

            var categories1 =
          from p in resultQuestions.objQuestionDetail
          where p.AnswerResponse == p.Answer
          group p by p.CategoryName into g
          select new
          {
              CatName = g.Key,
              QuesCnt = g.Count()
          };


            var query = from v1 in categories
                        join v2 in categories1 on v1.TestResult equals v2.CatName into gj
                        from subpet in gj.DefaultIfEmpty()
                        select new { v1.TestResult, v1.QuestionAvlbl, QuesAnsw = (subpet == null ? 0 : subpet.QuesCnt) };
            
           // dataGridView3.RowCount = resultQuestions.objQuestionDetail.Count();
            int nm=0,rwcnt=0;
            foreach (var v in query)
            {
                MockResult objMockResult = new MockResult();
                objMockResult.TestResult = v.TestResult;
                objMockResult.NumberofQuestions = v.QuestionAvlbl;
                objMockResult.NoofCorrectAnswered = v.QuesAnsw;
                objMockResult.CorrectAnswer = (v.QuesAnsw * 100) / v.QuestionAvlbl;
                nm = nm + objMockResult.CorrectAnswer;
              //  rwcnt = rwcnt + objMockResult.NumberofQuestions;
                bs.Add(objMockResult);

                //dataGridView3.Rows[nm].Cells["TestResult"].Value = v.TestResult;
                //dataGridView3.Rows[nm].Cells["NumberofQuestions"].Value = v.QuestionAvlbl;
                //dataGridView3.Rows[nm].Cells["NoofCorrectAnswered"].Value = v.QuesAnsw;
                //dataGridView3.Rows[nm].Cells["BCorrectAnswer"].Value = (v.QuesAnsw*100) / v.QuestionAvlbl;
               // nm++;
                rwcnt++;
            }
           
            
            dataGridView3.AutoGenerateColumns = true; // create columns automatically //**
            dataGridView3.DataSource = bs;
            dataGridView3.Columns[0].Width = 400;
            dataGridView3.Columns[3].Width = 150;
            dataGridView3.Columns[0].HeaderText = "Topic Name";
            dataGridView3.Columns[1].HeaderText = "Total No Of Questions";
            dataGridView3.Columns[2].HeaderText = "No Correctly Answered ";
            dataGridView3.Columns[3].HeaderText = "Correct Answer Percentage";
            dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            label32.Text = (Convert.ToDouble(nm / rwcnt)).ToString();
            if (Convert.ToDouble(nm / rwcnt) >= Convert.ToDouble(objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamPasPercentageScore))
            {
                label31.Text = "Pass";
                objQuestionManagementColl.objQuestionManagement[nRowIndex].TestResult = "Pass - " + label32.Text ;
            }
            else
            {
                label31.Text = "Fail";
                objQuestionManagementColl.objQuestionManagement[nRowIndex].TestResult = "Fail - " + label32.Text;
            }
            if (!skip)
            {
                int n=objQuestionManagementColl.objQuestionManagement[nRowIndex].objResultList.Count();
                objQuestionManagementColl.objQuestionManagement[nRowIndex].objResultList[n-1].TestMark=label32.Text + "%";
                objQuestionManagementColl.objQuestionManagement[nRowIndex].objResultList[n-1].TestSatus=label31.Text;
                ObjectXMLSerializer<QuestionManagementColl>.Save(objQuestionManagementColl, Application.StartupPath + Utility.FolderType() + @"MockTest\MockTestList.txt");
            }
                label32.Text = label32.Text + "%";
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPage2 && !isManualTabSecond)
            {
                e.Cancel = true;
            }
            else
                isManualTabSecond = false;

            if ((e.TabPage == tabPage3 ) && !isManualTabThird)
            {
                e.Cancel = true;
            }
            else
                isManualTabThird = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                if (resultQuestions.objQuestionDetail[e.RowIndex].AnsRespType == "Mark for review" || resultQuestions.objQuestionDetail[e.RowIndex].AnsRespType == "Skip")
                {
                    LogAnswer();
                    currRecord = e.RowIndex;
                    DisplayQuestions();
                }
            }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DisplayQuestion(true, false);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DisplayQuestion(true, true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DisplayQuestion(false, true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DisplayQuestion(false, false);
        }
        private void DisplayQuestion(bool ShowQuestion, bool ShowAnswer)
        {
            LoadQuestionFromXml(objQuestionManagementColl.objQuestionManagement[nRowIndex].FileName);
            FrmQuestionsDisplay obj = new FrmQuestionsDisplay();
            //obj.resultQuestions.objQuestionDetail.Clear();
            obj.resultQuestions.objQuestionDetail = resultQuestions.objQuestionDetail;
           /* if (treeView1.SelectedNode.Level == 0)
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
            */
            //  obj.strSubject = groupBox1.GroupTitle;// "";
            // obj.strQuestTopic = "";
            obj.printAnswer = ShowAnswer;
            obj.printQuestion = ShowQuestion;
            obj.ShowDialog();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == tabControl1.SelectedIndex)
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    new Font(tabControl1.Font, FontStyle.Regular),
                    Brushes.Brown,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    tabControl1.Font,
                    Brushes.LightGray,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }

        }
        //List to Data Table Convert
            }
}

public class MockResult
{
    public string TestResult { get; set; }
    public int NumberofQuestions { get; set; }
    public int NoofCorrectAnswered { get; set; }
    public int CorrectAnswer{ get; set; }
}
public class MockResultColl
{
}
