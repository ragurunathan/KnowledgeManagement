using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeMatrix.Cryptography;
using KnowledgeMatrix.Database;
using System.Windows.Forms;
using KnowledgeMatrix.Framework;

namespace KnowledgeMatrix.BusinessObjects
{
    class License
    {
        public string name;
        public string email;
        public string filename;
        public string LicenseKey;// = @"1234567890ABCDEFGHIJKLMNO";//This needs to be replaced by GUID 
        public string licensekey;
        public string systeminfo;
        public string productId;
        public string IP;
        public string phoneNumber;
        public string organisationName;

        public void GenerateLicense()
        {
            StringBuilder strLic = new StringBuilder();
            strLic.Append(name);
            strLic.Append("|");
            strLic.Append(email);
            strLic.Append("|");
            strLic.Append(LicenseKey);
            strLic.Append("|");
            systeminfo=EntropyGenerator.GetSystemInfo("");
            strLic.Append(systeminfo);
            strLic.Append("|");
            productId=Guid.NewGuid().ToString().Replace("-", "").Remove(25);
            strLic.Append(productId);

            

            if(string.IsNullOrWhiteSpace(IP))
            {
                IP=EntropyGenerator.GetIPForMachine();                
            }

            //Include the Product Catalog along with the 
            //License info
            string prodCatalog = System.IO.File.ReadAllText(Application.StartupPath + Utility.FolderType() + @"ProductsManagement\QuestionMaster.txt");
            strLic.Append("%");
            strLic.Append(prodCatalog);

                filename =FileCryptography.DoEncrypt(strLic.ToString(), filename,IP);
                
            
            //For Admin Generate Database
            AdminDatabaseMgmt obj = new AdminDatabaseMgmt();
            obj.name = name;
            obj.email = email;
            obj.Licensekey = LicenseKey;
            obj.SystemInfo = systeminfo;
            obj.ProductId = productId;
            obj.IP = IP;
            obj.phoneNumber = phoneNumber;
            obj.Add();

        }

        public bool ValidateLicense()
        {
            FileCryptography.DoDecrypt(filename,null);
            string decryptedData = FileCryptography.decryptedData;


            if (!string.IsNullOrEmpty(decryptedData))
            {
                string[] prodCatalog = decryptedData.Split('%');
                
                string[] strLic = prodCatalog[0].ToString().Split('|'); ;// decryptedData.Split('|');
               // if ((name == strLic[0]) && (email == strLic[1]) && (licensekey == strLic[2]) && (EntropyGenerator.GetSystemInfo("") == strLic[3]))
                if ((name == strLic[0]) && (email == strLic[1]) && (licensekey == strLic[2]))
                {
                
                    KnowledgeMatrix.Properties.Settings.Default.IP = EntropyGenerator.GetIPForMachine();
                    KnowledgeMatrix.Properties.Settings.Default.Authenicated = "true";
                    KnowledgeMatrix.Properties.Settings.Default.Setting = EntropyGenerator.GetSystemInfo("");
                    KnowledgeMatrix.Properties.Settings.Default.ProductKey = strLic[4];
                    KnowledgeMatrix.Properties.Settings.Default.DateOfActivation = DateTime.Now;
                    KnowledgeMatrix.Properties.Settings.Default.Save();

                    FileCryptography.encryptedData = prodCatalog[1];
                    FileCryptography.entropy = EntropyGenerator.GetKeyBytesForMachine();
                    FileCryptography.FileName = KnowledgeMatrix.Framework.Utility.XML_QUESTION_NAME;
                    FileCryptography.DoEncrypt();

           //         FileCryptography.DoEncrypt(prodCatalog[1], KnowledgeMatrix.Framework.Utility.XML_QUESTION_NAME, null);

                    return true;
                }
                else
                {
                    KnowledgeMatrix.Properties.Settings.Default.IP = "0.0.0.0";
                    KnowledgeMatrix.Properties.Settings.Default.Authenicated = "false";
                    KnowledgeMatrix.Properties.Settings.Default.Setting = "";
                    KnowledgeMatrix.Properties.Settings.Default.ProductKey = "";
                    KnowledgeMatrix.Properties.Settings.Default.DateOfActivation = DateTime.MaxValue;
                    KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate = DateTime.MaxValue;
                    KnowledgeMatrix.Properties.Settings.Default.Save();

                    return false;
                }
            }
            else
            {
                KnowledgeMatrix.Properties.Settings.Default.IP = "0.0.0.0";
                KnowledgeMatrix.Properties.Settings.Default.Authenicated = "false";
                KnowledgeMatrix.Properties.Settings.Default.Setting = "";
                KnowledgeMatrix.Properties.Settings.Default.ProductKey = "";
                KnowledgeMatrix.Properties.Settings.Default.DateOfActivation = DateTime.MaxValue;
                KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate = DateTime.MaxValue;
                KnowledgeMatrix.Properties.Settings.Default.Save();

                return false;
            }
        }

        public void RegisterUser()
        {
            StringBuilder strLic = new StringBuilder();
            strLic.Append(name);
            strLic.Append("|");
            strLic.Append(email);
            strLic.Append("|");
            strLic.Append(organisationName);
            strLic.Append("|");      
            strLic.Append(phoneNumber);
            strLic.Append("|");            
            systeminfo = EntropyGenerator.GetSystemInfo("");
            strLic.Append(systeminfo);
            strLic.Append("|");
            strLic.Append(EntropyGenerator.GetIPForMachine());
            filename = FileCryptography.DoEncrypt(strLic.ToString(), filename, KnowledgeMatrix.Properties.Settings.Default.RegistrationKey);
            //FileCryptography.DoDecrypt(filename, Properties.Settings.Default.RegistrationKey);
        }
    }


}
