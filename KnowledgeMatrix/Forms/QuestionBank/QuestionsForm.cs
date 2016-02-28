using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KnowledgeMatrix.Database;
using ExampApp.Database;

namespace KnowledgeMatrix
{
    
    public partial class QuestionsForm : Form
    {
        
        public QuestionDetail obj;
        public QuestionsForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obj.Question = textBox1.Text;
            obj.Answer = textBox6.Text;
            

            obj.QuestionOptions=textBox2.Text + ";" + textBox3.Text +";"+textBox4.Text+";" +textBox5.Text;
            //obj.QuestionOptions.Split(';')[1] = textBox3.Text;
            //obj.QuestionOptions.Split(';')[2]=textBox4.Text;
            //obj.QuestionOptions.Split(';')[3] = textBox5.Text;

            obj.CorrectAnswerDetails = textBox7.Text;
            obj.AnswerConcept = textBox8.Text;
            obj.AnswerType = comboBox1.Text;
             obj.Complexity = comboBox2.Text;
             obj.Picture= (System.Drawing.Bitmap)this.pictureBox1.Image;
             obj.Picture1 = (System.Drawing.Bitmap)this.pictureBox2.Image;
             obj.Picture2 = (System.Drawing.Bitmap)this.pictureBox3.Image;
             obj.Picture3 = (System.Drawing.Bitmap)this.pictureBox4.Image;
            this.Close();
        }

        private void QuestionsForm_Load(object sender, EventArgs e)
        {
            if (obj.QuesNo > 0)
            {
                textBox1.Text = obj.Question;
                textBox6.Text = obj.Answer;
                textBox2.Text = obj.QuestionOptions.Split(';')[0];
                textBox3.Text = obj.QuestionOptions.Split(';')[1];
                textBox4.Text = obj.QuestionOptions.Split(';')[2];
                textBox5.Text = obj.QuestionOptions.Split(';')[3];
                textBox7.Text = obj.CorrectAnswerDetails;
                textBox8.Text = obj.AnswerConcept;
                comboBox1.Text = obj.AnswerType;
                comboBox2.Text = obj.Complexity;
                this.pictureBox1.Image = (Image)obj.Picture;
                this.pictureBox2.Image = (Image)obj.Picture1;
                this.pictureBox3.Image = (Image)obj.Picture2;
                this.pictureBox4.Image = (Image)obj.Picture3;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FileName = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;

            openFileDialog.Filter = "All picture files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                this.pictureBox1.Image = Image.FromFile(FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string FileName = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;

            openFileDialog.Filter = "All picture files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                this.pictureBox2.Image = Image.FromFile(FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string FileName = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;

            openFileDialog.Filter = "All picture files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                this.pictureBox3.Image = Image.FromFile(FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string FileName = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;

            openFileDialog.Filter = "All picture files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                this.pictureBox4.Image = Image.FromFile(FileName);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            VisibleInvisible();

        }
        private void VisibleInvisible()
        {
            if (comboBox1.Text.Contains("Text"))
            {
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;

            }
            else
            {
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
            }
           button3.Visible=button4.Visible=button5.Visible=button6.Visible= pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = pictureBox1.Visible = !textBox2.Visible;
        }
    }
}
