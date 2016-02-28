using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using ExtendedRichTextBox;
using KnowledgeMatrix.Database;
using ExampApp.Database;
using KnowledgeMatrix.Framework;
using Khendys.Controls;

namespace KnowledgeMatrix.Forms
{


    public partial class frmMain : Form
    {
      //  public QuestionDetail obj = new QuestionDetail();
        public QuestionDetail obj;
        QuestionDetailData resultQuestions = new QuestionDetailData();
        public string strSubject;
        public string strQuestTopic;
        private RichTextBoxPrintCtrl rtbDoc1= new RichTextBoxPrintCtrl();
        // constructor
        public frmMain()
        {
            LogEntry.WriteLog("<br/> Edit form start : ", DateTime.Now.ToLongTimeString());
            InitializeComponent();

            currentFile = "";
            ApplicableTo.Items.AddRange(new object[] {
           Utility.MOD_ALL,
            Utility.MOD_QUEST_BANK,
            Utility.MOD_MOCK_TST});

          //  richTextBoxPrintCtrl5.Text = this.Text;

        }


       
#region "Declaration"

        private string currentFile;
        private int checkPrint;

#endregion
        


#region "Menu Methods"


        private void NewToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (rtbDoc.Modified == true)
                {
                    System.Windows.Forms.DialogResult answer;
                    answer = MessageBox.Show("Save current document before creating new document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == System.Windows.Forms.DialogResult.No)
                    {
                        currentFile = "";
                        this.Text = "Editor: New Document";
                        rtbDoc.Modified = false;
                        rtbDoc.Clear();
                        return;
                    }
                    else
                    {
                        SaveToolStripMenuItem_Click(this, new EventArgs());
                        rtbDoc.Modified = false;
                        rtbDoc.Clear();
                        currentFile = "";
                        this.Text = "Editor: New Document";
                        return;
                    }
                }
                else
                {
                    currentFile = "";
                    this.Text = "Editor: New Document";
                    rtbDoc.Modified = false;
                    rtbDoc.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        


        private void OpenToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (rtbDoc.Modified == true)
                {
                    System.Windows.Forms.DialogResult answer;
                    answer = MessageBox.Show("Save current file before opening another document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == System.Windows.Forms.DialogResult.No)
                    {
                        rtbDoc.Modified = false;
                        OpenFile();
                    }
                    else
                    {
                        SaveToolStripMenuItem_Click(this, new EventArgs());
                        OpenFile();
                    }
                }
                else
                {
                    OpenFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        


        private void OpenFile()
        {
            try
            {
                OpenFileDialog1.Title = "RTE - Open File";
                OpenFileDialog1.DefaultExt = "rtf";
                OpenFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                OpenFileDialog1.FilterIndex = 1;
                OpenFileDialog1.FileName = string.Empty;

                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if (OpenFileDialog1.FileName == "")
                    {
                        return;
                    }

                    string strExt;
                    strExt = System.IO.Path.GetExtension(OpenFileDialog1.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        rtbDoc.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.StreamReader txtReader;
                        txtReader = new System.IO.StreamReader(OpenFileDialog1.FileName);
                        rtbDoc.Text = txtReader.ReadToEnd();
                        txtReader.Close();
                        txtReader = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    currentFile = OpenFileDialog1.FileName;
                    rtbDoc.Modified = false;
                    this.Text = "Editor: " + currentFile.ToString();
                }
                else
                {
                    MessageBox.Show("Open File request cancelled by user.", "Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        


        private void SaveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (currentFile == string.Empty)
                {
                    SaveAsToolStripMenuItem_Click(this, e);
                    return;
                }

                try
                {
                    string strExt;
                    strExt = System.IO.Path.GetExtension(currentFile);
                    strExt = strExt.ToUpper();
                    if (strExt == ".RTF")
                    {
                        rtbDoc.SaveFile(currentFile);
                    }
                    else
                    {
                        System.IO.StreamWriter txtWriter;
                        txtWriter = new System.IO.StreamWriter(currentFile);
                        txtWriter.Write(rtbDoc.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    this.Text = "Editor: " + currentFile.ToString();
                    rtbDoc.Modified = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "File Save Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }

            
        }

        
        private void SaveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

            try
            {
                SaveFileDialog1.Title = "RTE - Save File";
                SaveFileDialog1.DefaultExt = "rtf";
                SaveFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                SaveFileDialog1.FilterIndex = 1;

                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if (SaveFileDialog1.FileName == "")
                    {
                        return;
                    }

                    string strExt;
                    strExt = System.IO.Path.GetExtension(SaveFileDialog1.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        rtbDoc.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.StreamWriter txtWriter;
                        txtWriter = new System.IO.StreamWriter(SaveFileDialog1.FileName);
                        txtWriter.Write(rtbDoc.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    currentFile = SaveFileDialog1.FileName;
                    rtbDoc.Modified = false;
                    this.Text = "Editor: " + currentFile.ToString();
                    MessageBox.Show(currentFile.ToString() + " saved.", "File Save");
                }
                else
                {
                    MessageBox.Show("Save File request cancelled by user.", "Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }



        
        private void ExitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (rtbDoc.Modified == true)
                {
                    System.Windows.Forms.DialogResult answer;
                    answer = MessageBox.Show("Save this document before closing?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        rtbDoc.Modified = false;
                        Application.Exit();
                    }
                }
                else
                {
                    rtbDoc.Modified = false;
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        


        private void SelectAllToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                rtbDoc.SelectAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to select all document content.", "RTE - Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        
        private void CopyToolStripMenuItem_Click(object sender, System.EventArgs e)
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

        


        private void CutToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void PasteToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void SelectFontToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void FontColorToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void BoldToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void ItalicToolStripMenuItem_Click(object sender, System.EventArgs e)
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





        private void UnderlineToolStripMenuItem_Click(object sender, System.EventArgs e)
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





        private void NormalToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void PageColorToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void mnuUndo_Click(object sender, System.EventArgs e)
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




        private void mnuRedo_Click(object sender, System.EventArgs e)
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




        private void LeftToolStripMenuItem_Click_1(object sender, System.EventArgs e)
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




        private void CenterToolStripMenuItem_Click_1(object sender, System.EventArgs e)
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




        private void RightToolStripMenuItem_Click_1(object sender, System.EventArgs e)
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




        private void AddBulletsToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void RemoveBulletsToolStripMenuItem_Click(object sender, System.EventArgs e)
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




        private void mnuIndent0_Click(object sender, System.EventArgs e)
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




        private void mnuIndent5_Click(object sender, System.EventArgs e)
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




        private void mnuIndent10_Click(object sender, System.EventArgs e)
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




        private void mnuIndent15_Click(object sender, System.EventArgs e)
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




        private void mnuIndent20_Click(object sender, System.EventArgs e)
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




        private void FindToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                frmFind f = new frmFind(this);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }




        private void FindAndReplaceToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                frmReplace f = new frmReplace(this);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }




        private void PreviewToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                PrintPreviewDialog1.Document = PrintDocument1;
                PrintPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }




        private void PrintToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                PrintDialog1.Document = PrintDocument1;
                if (PrintDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PrintDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }




        private void mnuPageSetup_Click(object sender, System.EventArgs e)
        {
            try
            {
                PageSetupDialog1.Document = PrintDocument1;
                PageSetupDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }




        private void InsertImageToolStripMenuItem_Click(object sender, System.EventArgs e)
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

        
        private void rtbDoc_SelectionChanged(object sender, EventArgs e)
        {
            tbrBold.Checked = rtbDoc.SelectionFont.Bold;
            tbrItalic.Checked = rtbDoc.SelectionFont.Italic;
            tbrUnderline.Checked = rtbDoc.SelectionFont.Underline;
        }




#endregion




#region Toolbar Methods


        private void tbrSave_Click(object sender, System.EventArgs e)
        {
            SaveToolStripMenuItem_Click(this, e);
        }

        
        private void tbrOpen_Click(object sender, System.EventArgs e)
        {
            OpenToolStripMenuItem_Click(this, e);
        }

        
        private void tbrNew_Click(object sender, System.EventArgs e)
        {
            NewToolStripMenuItem_Click(this, e);
        }

        
        private void tbrBold_Click(object sender, System.EventArgs e)
        {
            BoldToolStripMenuItem_Click(this, e);
        }

        
        private void tbrItalic_Click(object sender, System.EventArgs e)
        {
            ItalicToolStripMenuItem_Click(this, e);
        }

        
        private void tbrUnderline_Click(object sender, System.EventArgs e)
        {
            UnderlineToolStripMenuItem_Click(this, e);
        }

        
        private void tbrFont_Click(object sender, System.EventArgs e)
        {
            SelectFontToolStripMenuItem_Click(this, e);
        }

        
        private void tbrLeft_Click(object sender, System.EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }

        
        private void tbrCenter_Click(object sender, System.EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }

        
        private void tbrRight_Click(object sender, System.EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }


        private void tbrFind_Click(object sender, System.EventArgs e)
        {
            frmFind f = new frmFind(this);
            f.Show();
        }


        private void tspColor_Click(object sender, EventArgs e)
        {
            FontColorToolStripMenuItem_Click(this, new EventArgs());
        }




#endregion




#region Printing


        private void PrintDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            checkPrint = 0;

        }

        

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            rtbDoc1.Rtf = rtbDoc.Rtf;
           checkPrint = rtbDoc1.Print(checkPrint, rtbDoc.TextLength, e);

            if (checkPrint < rtbDoc.TextLength)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

        }



               

#endregion



        
#region Form Closing Handler


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (rtbDoc.Modified == true)
                {
                    System.Windows.Forms.DialogResult answer;
                    answer = MessageBox.Show("Save current document before exiting?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == System.Windows.Forms.DialogResult.No)
                    {
                        rtbDoc.Modified = false;
                        rtbDoc.Clear();
                        return;
                    }
                    else
                    {
                        SaveToolStripMenuItem_Click(this, new EventArgs());
                    }
                }
                else
                {
                    rtbDoc.Clear();
                }
                currentFile = "";
                this.Text = "Editor: New Document";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

#endregion

        private void richTextBoxPrintCtrl1_TextChanged(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl1;
        }

        private void richTextBoxPrintCtrl2_TextChanged(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl2;
        }

        private void richTextBoxPrintCtrl1_TabIndexChanged(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl1;
        }

        private void richTextBoxPrintCtrl3_TextChanged(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl3;
        }

        private void richTextBoxPrintCtrl4_TextChanged(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl4;
        }

        private void richTextBoxPrintCtrl5_TextChanged(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl5;
        }

        private void richTextBoxPrintCtrl6_TextChanged(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl6;
        }

        private void richTextBoxPrintCtrl7_TextChanged(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl7;
        }

        private void richTextBoxPrintCtrl2_Enter(object sender, EventArgs e)
        {
            rtbDoc = richTextBoxPrintCtrl2;
        }

        private bool validateFields()
        {
            bool isValid = true;
            if (richTextBoxPrintCtrl1.TextLength == 0)
            {
                MessageBox.Show("Please enter the Question Name");
                isValid = false;
            }

            if (isValid && comboBox2.Text.Length == 0)
            {
                MessageBox.Show("Please select Question Complexity");
                isValid = false;
            }

            if (isValid && comboBox1.Text.Length == 0)
            {
                MessageBox.Show("Please select Question Type");
                isValid = false;
            }

            if (isValid && ApplicableTo.Text.Length == 0)
            {
                MessageBox.Show("Please select Applicable To");
                isValid = false;
            }

            if (isValid && textBox6.TextLength == 0)
            {
                MessageBox.Show("Please enter the Correct answer");
                isValid = false;
            }
            else if( isValid &&  comboBox1.SelectedIndex == 0)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, "^[1-4]"))
                {
                    MessageBox.Show("Please enter the Correct answer");
                    isValid = false;
                }
            }
            else if (isValid && comboBox1.SelectedIndex == 1)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, @"^([1-4]+,)*[1-4]+$"))
                {
                    MessageBox.Show("Please enter the Correct answer");
                    isValid = false;
                }
            }
            if (isValid && richTextBoxPrintCtrl4.TextLength == 0)
            {
                MessageBox.Show("Please enter the Option 1");
                isValid = false;
            }
            if (isValid && richTextBoxPrintCtrl5.TextLength == 0)
            {
                MessageBox.Show("Please enter the Option 2");
                isValid = false;
            }
          

            return isValid;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (!validateFields())
                return;
            //  richTextBoxPrintCtrl5.Rtf = richTextBoxPrintCtrl4.Rtf;
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

          //  richTextBoxPrintCtrl5.AppendText(richTextBoxPrintCtrl4.Rtf);

            if (richTextBoxPrintCtrl1.Text.Length > 0)
                obj.Question = richTextBoxPrintCtrl1.Rtf.TrimEnd();
            // obj.Question = Utility.Base64Encode(richTextBoxPrintCtrl1. Rtf.TrimEnd());
            else
                obj.Question = ("");
            obj.Answer = textBox6.Text;


            //obj.QuestionOptions = textBox2.Text + ";" + textBox3.Text + ";" + textBox4.Text + ";" + textBox5.Text;
            //obj.QuestionOptions.Split(';')[1] = textBox3.Text;
            //obj.QuestionOptions.Split(';')[2]=textBox4.Text;
            //obj.QuestionOptions.Split(';')[3] = textBox5.Text;

            if (richTextBoxPrintCtrl2.Text.Length > 0)
                obj.CorrectAnswerDetails = richTextBoxPrintCtrl2.Rtf.TrimEnd();
               // obj.CorrectAnswerDetails = Utility.Base64Encode(richTextBoxPrintCtrl2.Rtf.TrimEnd());
            else
                obj.CorrectAnswerDetails = ("");

            if (richTextBoxPrintCtrl3.Text.Length > 0)
                obj.AnswerConcept = richTextBoxPrintCtrl3.Rtf.TrimEnd();
            else
                obj.AnswerConcept = ("");

            obj.ModuleName = ApplicableTo.Text;
            obj.AnswerType = comboBox1.Text;
            obj.Complexity = comboBox2.Text;
            //obj.Picture = (System.Drawing.Bitmap)this.pictureBox1.Image;
            //obj.Picture1 = (System.Drawing.Bitmap)this.pictureBox2.Image;
            //obj.Picture2 = (System.Drawing.Bitmap)this.pictureBox3.Image;
            //obj.Picture3 = (System.Drawing.Bitmap)this.pictureBox4.Image;
            if (richTextBoxPrintCtrl4.Text.Length > 0)
                obj.OptionOne = richTextBoxPrintCtrl4.Rtf.TrimEnd();
            else
                obj.OptionOne = ("");

            if (richTextBoxPrintCtrl5.Text.Length > 0)
                obj.OptionTwo =  richTextBoxPrintCtrl5.Rtf.TrimEnd();
            else
                obj.OptionTwo =( "");

            if (richTextBoxPrintCtrl6.Text.Length > 0)
                obj.OptionThree = richTextBoxPrintCtrl6.Rtf.TrimEnd();
            else
                obj.OptionThree = ("");

            if (richTextBoxPrintCtrl7.Text.Length > 0)
                obj.OptionFour = richTextBoxPrintCtrl7.Rtf.TrimEnd();
            else
                obj.OptionFour = ("");



            this.Close();
           // resultQuestions.objQuestionDetail = new List<QuestionDetail>();
            //resultQuestions.objQuestionDetail.Add(obj);
            //ObjectXMLSerializer<QuestionDetailData>.Save(resultQuestions, Application.StartupPath + @"\NewTest.xml");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
/*            resultQuestions = ObjectXMLSerializer<QuestionDetailData>.Load(Application.StartupPath + @"\NewTest.xml");

            obj = null;
            obj = resultQuestions.objQuestionDetail[0];

            richTextBoxPrintCtrl1.Rtf = obj.Question;
            textBox6.Text = obj.Answer;


            ////obj.QuestionOptions = textBox2.Text + ";" + textBox3.Text + ";" + textBox4.Text + ";" + textBox5.Text;
            ////obj.QuestionOptions.Split(';')[1] = textBox3.Text;
            ////obj.QuestionOptions.Split(';')[2]=textBox4.Text;
            ////obj.QuestionOptions.Split(';')[3] = textBox5.Text;

            richTextBoxPrintCtrl2.Rtf = obj.CorrectAnswerDetails;
            richTextBoxPrintCtrl3.Rtf=obj.AnswerConcept;
            comboBox1.Text = obj.AnswerType;
            comboBox2.Text = obj.Complexity;
            //obj.AnswerType = comboBox1.Text;
            //obj.Complexity = comboBox2.Text;
            ////obj.Picture = (System.Drawing.Bitmap)this.pictureBox1.Image;
            ////obj.Picture1 = (System.Drawing.Bitmap)this.pictureBox2.Image;
            ////obj.Picture2 = (System.Drawing.Bitmap)this.pictureBox3.Image;
            ////obj.Picture3 = (System.Drawing.Bitmap)this.pictureBox4.Image;
            richTextBoxPrintCtrl4.Rtf = obj.OptionOne;
             richTextBoxPrintCtrl5.Rtf =obj.OptionTwo;
            richTextBoxPrintCtrl6.Rtf=obj.OptionThree;
            richTextBoxPrintCtrl7.Rtf = obj.OptionFour;*/

        }

        private void setTextForRich(Khendys.Controls.ExRichTextBox obj, string question)
        {
            
            if (question != null &&  Utility.Base64Decode(question).Contains("\\rtf1\\ansi"))
                obj.Rtf = Utility.Base64Decode(question);
            else if (question != null && question.Length > 0)
                obj.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang16393{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}\viewkind4\uc1\pard\f0\fs20" + question +@"}";
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            
            if (obj.QuesNo > 0)
            {
                this.Text = "Question Detail - Edit - " + obj.QuesNo;
                setTextForRich( richTextBoxPrintCtrl1, obj.Question);

                textBox6.Text = obj.Answer;


                ////obj.QuestionOptions = textBox2.Text + ";" + textBox3.Text + ";" + textBox4.Text + ";" + textBox5.Text;
                ////obj.QuestionOptions.Split(';')[1] = textBox3.Text;
                ////obj.QuestionOptions.Split(';')[2]=textBox4.Text;
                ////obj.QuestionOptions.Split(';')[3] = textBox5.Text;

                setTextForRich(richTextBoxPrintCtrl2, obj.CorrectAnswerDetails);
                setTextForRich( richTextBoxPrintCtrl3, obj.AnswerConcept);
                comboBox1.Text = obj.AnswerType;
                comboBox2.Text = obj.Complexity;
                ApplicableTo.Text = obj.ModuleName;
                //obj.AnswerType = comboBox1.Text;
                //obj.Complexity = comboBox2.Text;
                ////obj.Picture = (System.Drawing.Bitmap)this.pictureBox1.Image;
                ////obj.Picture1 = (System.Drawing.Bitmap)this.pictureBox2.Image;
                ////obj.Picture2 = (System.Drawing.Bitmap)this.pictureBox3.Image;
                ////obj.Picture3 = (System.Drawing.Bitmap)this.pictureBox4.Image;
                setTextForRich( richTextBoxPrintCtrl4,obj.OptionOne);
                setTextForRich( richTextBoxPrintCtrl5,obj.OptionTwo);
                setTextForRich(richTextBoxPrintCtrl6,obj.OptionThree);
                setTextForRich( richTextBoxPrintCtrl7,obj.OptionFour);

               

            }
            else
            {
                this.Text = "Question Detail - Add";
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            richTextBoxPrintCtrl8.Text = strSubject;
            richTextBoxPrintCtrl9.Text = strQuestTopic;
            //setTextForRich(richTextBoxPrintCtrl8, strSubject);
            //setTextForRich(richTextBoxPrintCtrl9, strQuestTopic);
            LogEntry.WriteLog("<br/> Edit form Ends : ", DateTime.Now.ToLongTimeString());
        }

        
        


    }
}