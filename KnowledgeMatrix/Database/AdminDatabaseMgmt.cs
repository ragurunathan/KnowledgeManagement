using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using KnowledgeMatrix.Framework;
using KnowledgeMatrix.Cryptography;

namespace KnowledgeMatrix.Database
{
    public class AdminDatabaseMgmt
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();

        public string name;
        public string email;
        public string Licensekey;
        public string SystemInfo;
        public string ProductId;
        public string IP;
        public string phoneNumber;

        private void SetConnection()
        {

            sql_con = new SQLiteConnection("Data Source=DemoT.s3db;Version=3;New=True;Compress=True;");

        }
        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();

            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;

            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
        public void CreateTable()
        {
            try
            {
                SetConnection();
                sql_con.Open();

                sql_cmd = sql_con.CreateCommand();
                //string CommandText = "select id, desc from  mains";
                string CommandText = "CREATE TABLE LicenseMaster ([id] INTEGER  NOT NULL PRIMARY KEY,[uname] TEXT  NULL,[email] TEXT  NULL,[Licensekey] TEXT  NULL,[SystemInfo] TEXT  NULL,[ProductId] TEXT  NULL,[IP] TEXT  NULL,[phonenumber] TEXT  NULL,CreateDateTime TEXT NULL)";
                sql_cmd.CommandText = CommandText;     // Set CommandText to our query that will create the table
                int i = sql_cmd.ExecuteNonQuery();                  // Execute the query


               CommandText= "CREATE TABLE [ProductPurchase] ([Id] INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL,[LicenseMasterID] INTEGER  NULL,[IP] TEXT  NULL,[ProductName] TEXT  NULL,[ProductType] TEXT  NULL,[DateOfPurchase] TEXT  NULL,[DateOfExpiry] TEXT  NULL,[CreateDateTime] TEXT  NULL,[IPAdmin] TEXT  NULL)";
               sql_cmd.CommandText = CommandText;     // Set CommandText to our query that will create the table
                i = sql_cmd.ExecuteNonQuery();                  // Execute the query
                sql_con.Close();
            }
            catch (Exception ex)
            {
                LogEntry.WriteLog(ex, "Database DemoT.s3db creation Exception");
                //MessageBox.Show(ex.InnerException.ToString());
            }
        }
        #region LiecnseMaster
        public void Add()
        {
            try
            {
                string txtSQLQuery = "insert into  LicenseMaster (uname,email,Licensekey,SystemInfo,ProductId,IP,phonenumber,CreateDateTime) values ('" + name + "','" + email + "','" + Licensekey
                    + "','" + SystemInfo + "','" + ProductId + "','" + IP + "','" + phoneNumber + "','" + System.DateTime.Now.ToString() + "')";
                ExecuteQuery(txtSQLQuery);
            }
            catch (Exception ex)
            {
                LogEntry.WriteLog(ex, "Database DemoT.s3db Record Insertion Exception");
                //MessageBox.Show(ex.InnerException.ToString());
            }
        }

        public DataTable GetAllRegisteredUsers()
        {
            DataTable DT = new DataTable();
            SetConnection();
            sql_con.Open();
            string CommandText1 = "select id [Customer Id],uname [Customer Name],email [Email Id],IP [Computer IP],phonenumber [Phone Number], CreateDateTime [License Date],ProductId [Product Key],LicenseKey [Activation Key] from  LicenseMaster where uname like '" + name + "' and  email like '" + email + "' and IP like '" + IP + "'";
            DB = new SQLiteDataAdapter(CommandText1, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            sql_con.Close();
            return DT;
        }
        public bool ValidateRecord()
        {
            DataTable dss = GetAllRegisteredUsers();
            if (dss.Rows.Count > 0)
            {
            
                return true;
            }
            else
            {
                //sql_con.Close();
                return false;
            }

        }
        #endregion 

        #region ProductPurchase
        public void AddProductPurchase(LicenseDetailInfo obj)
        {
            try
            {
                string txtSQLQuery = "insert into  ProductPurchase (LicenseMasterID,IP,ProductName,ProductType,DateOfPurchase,DateOfExpiry,CreateDateTime,IPAdmin) values (" + obj.LicenseMasterID + ",'" + obj.IP + "','" + obj.ProductName
                    + "','" + obj.ProductType + "','" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.AddDays(365).ToString() + "','" + System.DateTime.Now.ToString() + "','" + EntropyGenerator.GetIPForMachine() + "')";
                ExecuteQuery(txtSQLQuery);
            }
            catch (Exception ex)
            {
                LogEntry.WriteLog(ex, "Database DemoT.s3db Record AddProductPurchase Insertion Exception");
                //MessageBox.Show(ex.InnerException.ToString());
            }
        }
        public DataTable GetAllPurchaseUser(int LicenseMasterID)
        {
            DataTable DT = new DataTable();
            SetConnection();
            sql_con.Open();
            string CommandText1 = "select ProductName [Standard And Subject],ProductType [Product Name],DateOfPurchase [Date Of Purchase],DateOfExpiry [Date Of Expiry] from  ProductPurchase where LicenseMasterID =" + LicenseMasterID;
            DB = new SQLiteDataAdapter(CommandText1, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            sql_con.Close();
            return DT;
        }
        #endregion
    }
}
