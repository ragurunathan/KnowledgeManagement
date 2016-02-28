using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KnowledgeMatrix.Database;
using KnowledgeMatrix.Framework;
using ExtendedRichTextBox;

namespace KnowledgeMatrix.Forms
{
    public partial class frmeTutorManage : Form
    {
        public eTutorMast objeTutorMast;
        public bool isUpdate = false;
        private int currRec = 0;
        private int currRecord = 0;
        private Timer _timer1;
        private int timePeriod = 1;
        private RichTextBoxPrintCtrl rtbDoc1 = new RichTextBoxPrintCtrl();
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (currRec+1 == objeTutorMast.Pages.Count())
                currRec = -1;
            button7_Click(null, null);
        }
        public frmeTutorManage()
        {
            InitializeComponent();
            pictureBox5.Image = imageList1.Images[0];
            pictureBox2.Image = imageList1.Images[1];
            pictureBox3.Image = imageList1.Images[2];
            pictureBox4.Image = imageList1.Images[3];
            panel2.SendToBack();

            if (Utility.IsAdmin())
                panel3.Visible = false;
            else
                panel3.Visible = true;

            txtTimePeriod.Text = timePeriod.ToString();
            
            
            DataGridViewTextBoxColumn col00 = new DataGridViewTextBoxColumn();
            col00.ReadOnly = true;
            col00.HeaderText = "Ques No";
            col00.Name = "QuesNo";
            dataGridView2.Columns.Add(col00);
            this.dataGridView2.Columns[0].Width = 75;

            DataGridViewTextBoxColumn col11 = new DataGridViewTextBoxColumn();
            col11.ReadOnly = true;
            col11.HeaderText = "Response";
            col11.Name = "Response";
            col11.Visible = false;
            dataGridView2.Columns.Add(col11);
            this.dataGridView2.Columns[1].Width = 75;



            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Response ";
            //Add twice the padding for the left and  
            //right sides of the cell.
            imageColumn.Width = 100;

            imageColumn.ImageLayout = DataGridViewImageCellLayout.Normal;
            imageColumn.Name = "StatusImage";

            dataGridView2.Columns.Add(imageColumn);
        }

        private void frmeTutorManage_Load(object sender, EventArgs e)
        {
            label35.Text = string.IsNullOrEmpty(objeTutorMast.CategoryName)? "Knowledge Tutor ": "Knowledge Tutor - "  + objeTutorMast.CategoryName;
            EnableDisableButtons();
            comboBox1.Visible= label36.Visible = txtTimePeriod.Visible = false;
            comboBox1.SelectedIndex = 1;
            this.BringToFront();
        }
        private void setTextForRich(RichTextBoxPrintCtrl obj, string question)
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
        private void setTextForRich(Khendys.Controls.ExRichTextBox obj, string question)
        {
            obj.Clear();
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

        private void EnableDisableButtons()
        {
            if (objeTutorMast.Pages != null && objeTutorMast.Pages.Count()>0)
            {
                setTextForRich(exRichTextBox1, objeTutorMast.Pages[currRec].PageInfo);
                label37.Text = (currRec + 1).ToString() + " of " + objeTutorMast.Pages.Count().ToString();

                button9.Enabled = button8.Enabled = button7.Enabled = button6.Enabled = true;
                button1.Enabled = false;

                if (currRec + 1 == objeTutorMast.Pages.Count())
                {
                    button7.Enabled = button6.Enabled = false;
                    button1.Enabled = true;
                }

                if (currRec == 0 && objeTutorMast.Pages.Count() > 0)
                    button1.Enabled = button9.Enabled = button8.Enabled = false;

                if (objeTutorMast.Pages.Count() == 1)
                {
                    button9.Enabled = button8.Enabled = button7.Enabled = button6.Enabled = false;
                    button1.Enabled = true;
                }
            }
            else
            {
                label37.Text = "";
                button9.Enabled = button8.Enabled = button7.Enabled = button6.Enabled = false;
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            currRec = 0;
            txtPage.Text = "";
            EnableDisableButtons();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currRec--;
            txtPage.Text = "";
            EnableDisableButtons();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtPage.Text = "";
            currRec++;
            EnableDisableButtons();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtPage.Text = "";
            currRec = objeTutorMast.Pages.Count()-1;
            EnableDisableButtons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            DisplayQuestions();
        }

        private void DisplayQuestions()
        {
            label15.Text = "60";
            if (objeTutorMast.QuestionDetailData.objQuestionDetail != null && objeTutorMast.QuestionDetailData.objQuestionDetail.Count()>0)
            {

                label18.Text = (currRecord + 1).ToString() + " of " + objeTutorMast.QuestionDetailData.objQuestionDetail.Count.ToString();
                setTextForRich(richTextBoxPrintCtrl1, objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].Question);
                setTextForRich(richTextBoxPrintCtrl4, objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].OptionOne);

                setTextForRich(richTextBoxPrintCtrl5, objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].OptionTwo);

                setTextForRich(richTextBoxPrintCtrl6, objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].OptionThree);

                setTextForRich(richTextBoxPrintCtrl7, objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].OptionFour);
                radioPanel.Visible = false;
                checkboxPanel.Visible = false;

                if (objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerType == "Multi Choice" || objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerType == "Multi-Image")
                {
                    checkboxPanel.Visible = true;
                    checkBox1.Visible = checkBox2.Visible = checkBox3.Visible = checkBox4.Visible = true;
                    //checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;

                    //checkBox1.Visible = richTextBoxPrintCtrl4.TextLength > 0 ? true : false;
                    checkBox2.Visible = richTextBoxPrintCtrl5.TextLength > 0 ? true : false;
                    checkBox3.Visible = richTextBoxPrintCtrl6.TextLength > 0 ? true : false;
                    checkBox4.Visible = richTextBoxPrintCtrl7.TextLength > 0 ? true : false;

                    string sAnswer = objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerResponse;
                    if (!string.IsNullOrWhiteSpace(sAnswer))
                    {
                        for (int i = 0; i < sAnswer.Split(',').Count() - 1; i++)
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
                else if (objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerType == "Single Choice" || objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerType == "Single-Image")
                {
                    radioPanel.Visible = true;
                    radioButton1.Visible = radioButton2.Visible = radioButton3.Visible = radioButton4.Visible = true;
                    radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
                    radioButton1.Visible = richTextBoxPrintCtrl4.TextLength > 0 ? true : false;
                    radioButton2.Visible = richTextBoxPrintCtrl5.TextLength > 0 ? true : false;
                    radioButton3.Visible = richTextBoxPrintCtrl6.TextLength > 0 ? true : false;
                    radioButton4.Visible = richTextBoxPrintCtrl7.TextLength > 0 ? true : false;
                    pictureBox6.Visible = pictureBox7.Visible = pictureBox8.Visible = pictureBox9.Visible = false;

                    if (!string.IsNullOrWhiteSpace(objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerResponse))
                    {
                        int x = Convert.ToInt16(objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerResponse);
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
                else if (objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerType == "Multi-Image")
                {
                    checkboxPanel.Visible = true;
                    checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;
                }
                else if (objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerType == "Single-Image")
                {
                    radioPanel.Visible = true;
                    radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
                }

                First.Enabled = true;
                Last.Enabled = true;
                DisplayStatus();
                if (currRecord == objeTutorMast.QuestionDetailData.objQuestionDetail.Count())
                    Last.Enabled = false;
                if (currRecord == 0)
                    First.Enabled = false;
            }
        }

        private void DisplayStatus()
        {
            dataGridView2.RowCount = objeTutorMast.QuestionDetailData.objQuestionDetail.Count();
            for (int nm = 0; nm < objeTutorMast.QuestionDetailData.objQuestionDetail.Count(); nm++)
            {
                Bitmap unMarked = new Bitmap(global::KnowledgeMatrix.Properties.Resources.MT_notattempted1);
                dataGridView2.Rows[nm].Cells["QuesNo"].Value = Convert.ToString(nm + 1);
                dataGridView2.Rows[nm].Cells["Response"].Value = objeTutorMast.QuestionDetailData.objQuestionDetail[nm].AnsRespType == null ? "Yet To Attend" : objeTutorMast.QuestionDetailData.objQuestionDetail[nm].AnsRespType;
                if (dataGridView2.Rows[nm].Cells["Response"].Value.ToString() == "Yet To Attend")
                    unMarked = (Bitmap)imageList1.Images[1];
                // unMarked = new Bitmap(Application.StartupPath + @"\notattempted.png");
                else if (dataGridView2.Rows[nm].Cells["Response"].Value.ToString() == "Skip")
                    unMarked = (Bitmap)imageList1.Images[2];

                else if (dataGridView2.Rows[nm].Cells["Response"].Value.ToString() == "Mark for review")
                    unMarked = (Bitmap)imageList1.Images[3];
                else if (dataGridView2.Rows[nm].Cells["Response"].Value.ToString() == "Completed")
                    unMarked = (Bitmap)imageList1.Images[0];


                dataGridView2.Rows[nm].Cells["StatusImage"].Value = unMarked;

            }
        }

        private void First_Click(object sender, EventArgs e)
        {
            LogAnswer();
            currRecord = 0;
            DisplayQuestions();
        }
        private void LogAnswer()
        {
            if (objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerType == "Multi Choice" || objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerType == "Multi-Image")
            {
                StringBuilder strBld = new StringBuilder();
                strBld.Append(checkBox1.Checked ? "1," : "");
                strBld.Append(checkBox2.Checked ? "2," : "");
                strBld.Append(checkBox3.Checked ? "3," : "");
                strBld.Append(checkBox4.Checked ? "4," : "");
                objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerResponse = strBld.ToString();
            }
            else
            {
                objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnswerResponse = radioButton1.Checked ? "1" : (radioButton2.Checked ? "2" : (radioButton3.Checked ? "3" : (radioButton4.Checked ? "4" : "")));
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnsRespType = "Completed";
            LogAnswer();
            if (currRecord != objeTutorMast.QuestionDetailData.objQuestionDetail.Count() - 1)
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

        private void Next_Click(object sender, EventArgs e)
        {
            objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnsRespType = "Mark for review";
            LogAnswer();
            if (currRecord != objeTutorMast.QuestionDetailData.objQuestionDetail.Count() - 1)
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

        private void Last_Click(object sender, EventArgs e)
        {
            LogAnswer();
            currRecord = objeTutorMast.QuestionDetailData.objQuestionDetail.Count() - 1;
            DisplayQuestions();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            objeTutorMast.QuestionDetailData.objQuestionDetail[currRecord].AnsRespType = "Skip";
            LogAnswer();
            if (currRecord != objeTutorMast.QuestionDetailData.objQuestionDetail.Count() - 1)
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
             LogAnswer();
            //Check if any record is marked for Review

            List<QuestionDetail> q = (from QuestionDetail in objeTutorMast.QuestionDetailData.objQuestionDetail
                                      where QuestionDetail.AnsRespType == "Mark for review"
                                      select QuestionDetail).ToList();
            System.Windows.Forms.DialogResult answer;
            if (q != null && q.Count() > 0)
                answer = MessageBox.Show("There are some answers marked for review. Are you sure to mark exam complete?", "Exam Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            else
                answer = MessageBox.Show("Are you sure to mark exam complete?", "Exam Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == System.Windows.Forms.DialogResult.Yes)
            {
                isUpdate = true;
                objeTutorMast.Status = "Course taken and test taken – The ";
                int totalAnswer = 0;
                for (int i = 0; i < objeTutorMast.QuestionDetailData.objQuestionDetail.Count(); i++)
                {
                    if (objeTutorMast.QuestionDetailData.objQuestionDetail[i].AnswerResponse == objeTutorMast.QuestionDetailData.objQuestionDetail[i].Answer)
                        totalAnswer++;
                }
                //  totalAnswer = (totalAnswer *100) / resultQuestions.objQuestionDetail.Count;

                //if (Convert.ToDouble((totalAnswer*100 ) / resultQuestions.objQuestionDetail.Count) >= Convert.ToDouble(objQuestionManagementColl.objQuestionManagement[nRowIndex].ExamPasPercentageScore))
                //    objQuestionManagementColl.objQuestionManagement[nRowIndex].TestResult = "Pass - " + ((totalAnswer*100) / resultQuestions.objQuestionDetail.Count).ToString();

                //else
                //    objQuestionManagementColl.objQuestionManagement[nRowIndex].TestResult = "Fail - " + ((totalAnswer * 100) / resultQuestions.objQuestionDetail.Count).ToString();

                objeTutorMast.Result = ((100 * totalAnswer) / objeTutorMast.QuestionDetailData.objQuestionDetail.Count()).ToString() + "%";
                MessageBox.Show("The eTutor for " +label35.Text+  " is completed!!!");
                this.Close();
            }
 
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPage.Text))
            {
                if (Convert.ToInt16(txtPage.Text) <= objeTutorMast.Pages.Count())
                {
                    currRec = Convert.ToInt16(txtPage.Text) - 1;
                    EnableDisableButtons();

                }
            }
        }
        private void ManageTimer()
        {
            if(_timer1 != null) 
                _timer1.Dispose();
            _timer1 = new Timer();
            _timer1.Interval = timePeriod  * 1000;
            _timer1.Tick += new EventHandler(timer1_Tick);
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                comboBox1.Visible= label36.Visible= txtTimePeriod.Visible = true;
                panel2.BringToFront();
                button9_Click(null, null);
                ManageTimer();
                _timer1.Start();
            }
            else
            {
                comboBox1.Visible= label36.Visible= txtTimePeriod.Visible  = false;
                panel2.SendToBack();
                if(_timer1 != null)
                    _timer1.Stop();
            }
        }

        private void txtTimePeriod_TextChanged(object sender, EventArgs e)
        {
            timePeriod =  Convert.ToInt32( txtTimePeriod.Text);
            checkBox5_CheckedChanged(null, null);
        }

        private void exRichTextBox1_Enter(object sender, EventArgs e)
        {
            rtbDoc = exRichTextBox1;
        }

        private void tbrBold_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Bold;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tbrItalic_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Italic;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tbrUnderline_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tbrLeft_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void tbrCenter_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void tbrRight_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void tspColor_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.ForeColor;
                if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionColor = ColorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tbrFont_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    FontDialog1.Font = rtbDoc.SelectionFont;
                }
                else
                {
                    FontDialog1.Font = null;
                }
                FontDialog1.ShowApply = true;
                if (FontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionFont = FontDialog1.Font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.CanUndo)
                {
                    rtbDoc.Undo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void mnuRedo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.CanRedo)
                {
                    rtbDoc.Redo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Copy();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to copy document content.", "RTE - Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Cut();
            }
            catch
            {
                MessageBox.Show("Unable to cut document content.", "RTE - Cut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Paste();
            }
            catch
            {
                MessageBox.Show("Unable to copy clipboard content to document.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.Title = "RTE - Insert Image File";
            OpenFileDialog1.DefaultExt = "rtf";
            OpenFileDialog1.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif";
            OpenFileDialog1.FilterIndex = 1;
            OpenFileDialog1.ShowDialog();

            if (OpenFileDialog1.FileName == "")
            {
                return;
            }

            try
            {
                // If file is an icon
                if (OpenFileDialog1.FileName.ToUpper().EndsWith(".ICO"))
                {
                    // Create a new icon, get it's handle, and create a bitmap from
                    // its handle
                    this.rtbDoc.InsertImage(Bitmap.FromHicon((new Icon(OpenFileDialog1.FileName)).Handle));
                }
                else
                {
                    // Create a bitmap from the filename
                    this.rtbDoc.InsertImage(Image.FromFile(OpenFileDialog1.FileName));
                }
                //string strImagePath = OpenFileDialog1.FileName;
                //Image img;
                //img = Image.FromFile(strImagePath);
                //Clipboard.SetDataObject(img);
                //DataFormats.Format df;
                //df = DataFormats.GetFormat(DataFormats.Bitmap);
                //if (this.rtbDoc.CanPaste(df))
                //{
                //    this.rtbDoc.Paste(df);
                //}
            }
            catch
            {
                MessageBox.Show("Unable to insert image format selected.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    FontDialog1.Font = rtbDoc.SelectionFont;
                }
                else
                {
                    FontDialog1.Font = null;
                }
                FontDialog1.ShowApply = true;
                if (FontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionFont = FontDialog1.Font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void FontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.ForeColor;
                if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionColor = ColorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Bold;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void ItalicToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Italic;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void UnderlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;
                    newFontStyle = FontStyle.Regular;
                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void PageColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog1.Color = rtbDoc.BackColor;
                if (ColorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.BackColor = ColorDialog1.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }

        }

        private void mnuIndent0_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void mnuIndent5_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void mnuIndent10_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 10;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void mnuIndent15_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 15;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void mnuIndent20_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionIndent = 20;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void LeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void CenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void RightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void AddBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.BulletIndent = 10;
                rtbDoc.SelectionBullet = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void RemoveBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionBullet = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(objeTutorMast.Pages != null && objeTutorMast.Pages.Count==0)
            {
                PageDet objPageDet = new PageDet();
                objeTutorMast.Pages.Add(objPageDet);
            }
             objeTutorMast.Pages[currRec].PageInfo = exRichTextBox1.Rtf;
            isUpdate = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult answer = MessageBox.Show("Are you sure to delete page?", "eTutor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == System.Windows.Forms.DialogResult.Yes)
            {
                objeTutorMast.Pages.RemoveAt(currRec);
                currRec = 0;
                EnableDisableButtons();
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            PageDet objPgDet = new PageDet();
            objeTutorMast.Pages.Add(objPgDet);
           // exRichTextBox1.Clear();
            currRec = objeTutorMast.Pages.Count()-1;
            EnableDisableButtons();
            exRichTextBox1.Focus();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            isUpdate = false;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                timePeriod = 15*(Convert.ToInt32(comboBox1.SelectedIndex)+1);
                checkBox5_CheckedChanged(null, null);
            }
        }
    }
}
