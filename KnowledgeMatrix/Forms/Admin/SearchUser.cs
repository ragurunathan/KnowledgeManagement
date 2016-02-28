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
    public partial class SearchUser : Form
    {
        private DataTable dbSearchUser;
        public string UserName;
        public string IP;
        public string ID;
        public string PublicKey;
        public SearchUser()
        {
            InitializeComponent();
         //   Grid.ColumnHeadersDefaultCellStyle.Alignment =
         //       DataGridViewContentAlignment.MiddleCenter;
            Grid.Update();// = buttonColumn3;
            //  dataGridView1.Sort(col0, ListSortDirection.Ascending);
           // Grid.Dock = DockStyle.Fill;
            Grid.BackgroundColor = Color.LightGray;
            Grid.BorderStyle = BorderStyle.Fixed3D;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
         //   Grid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
        }

        private void LoadPurchaseHistory()
        {
            AdminDatabaseMgmt obj = new AdminDatabaseMgmt();
            Grid.DataSource = obj.GetAllPurchaseUser(Convert.ToInt16(ID));

        }

        private void LoadData()
        {
                     AdminDatabaseMgmt obj = new AdminDatabaseMgmt();

                     obj.name = string.IsNullOrWhiteSpace(textBox1.Text) ? "%" : "%"+ textBox1.Text+ "%";
                     obj.email = string.IsNullOrWhiteSpace(textBox2.Text) ? "%" : "%" + textBox2.Text + "%";
                     obj.IP = string.IsNullOrWhiteSpace(textBox3.Text) ? "%" : "%" + textBox3.Text + "%";
                dbSearchUser = obj.GetAllRegisteredUsers();
            Grid.DataSource = dbSearchUser;
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserName = Convert.ToString(dbSearchUser.Rows[Grid.CurrentRowIndex]["Customer Name"]);
            IP = Convert.ToString(dbSearchUser.Rows[Grid.CurrentRowIndex]["Computer IP"]);
            ID = Convert.ToString(dbSearchUser.Rows[Grid.CurrentRowIndex]["Customer Id"]);
            PublicKey = Convert.ToString(dbSearchUser.Rows[Grid.CurrentRowIndex]["Product Key"]);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserName = null;
            IP = null;
            this.Close();
        }

        private void SearchUser_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ID))
            {
                
                LoadData();
                
            }
            else
            {
                this.Text = "Purchase History - " + UserName ;
                Grid.Location =new  Point(13,5);
                Grid.Size = new Size(Grid.Width, Grid.Height + 55);
                LoadPurchaseHistory();
                button1.Visible = false;
                panel1.Visible = false;
                button4.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchUser obj = new SearchUser();
            obj.ID = Convert.ToString(Convert.ToString(dbSearchUser.Rows[Grid.CurrentRowIndex]["Customer Id"]));
            obj.UserName = Convert.ToString(Convert.ToString(dbSearchUser.Rows[Grid.CurrentRowIndex]["Customer Name"]));
            obj.ShowDialog();
        }
    }
}
