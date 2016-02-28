using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using System.Security.Cryptography;

using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using KnowledgeMatrix.Framework;

namespace KnowledgeMatrix.Cryptography
{
    class FileCryptography
    {
        public static FileStream fStream;
        public static string decryptedData;
        public static string encryptedData=null;
        public static string FileName;
        public static byte[] entropy;
        
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string DoEncrypt(string dataToEncrypt,string FileNames,string _entropy)
        {

            if (string.IsNullOrWhiteSpace(_entropy))
                    _entropy = EntropyGenerator.GetIPForMachine();

            if(string.IsNullOrWhiteSpace(FileNames))
                FileName = Application.StartupPath + @"\Data.txt";
            else
                FileName=FileNames;

            //Write to file
            if(File.Exists(Application.StartupPath + @"\dataToEncrypt.txt"))
                System.IO.File.Delete(Application.StartupPath + @"\dataToEncrypt.txt");
            System.IO.File.WriteAllText(Application.StartupPath + @"\dataToEncrypt.txt", dataToEncrypt);                     
            EncDec.Encrypt(Application.StartupPath + @"\dataToEncrypt.txt", FileName, _entropy);
            System.IO.File.Delete(Application.StartupPath + @"\dataToEncrypt.txt");

            //EncDec.Decrypt(@"C:\Users\224702\Desktop\Sample1.txt", @"C:\Users\224702\Desktop\Sample2.txt", @"127.0.0.1");
            /*
            entropy = UnicodeEncoding.ASCII.GetBytes(_entropy.ToCharArray(), 0, 16);
            IV = UnicodeEncoding.ASCII.GetBytes(Reverse(_entropy).ToCharArray(), 0, 16);

            // Create a new instance of the Aes 
            // class.  This generates a new key and initialization  
            // vector (IV). 
            encryptedData = dataToEncrypt;
            FileName = FileNames;

            FileStream stream = new FileStream(FileName, 
         FileMode.OpenOrCreate,FileAccess.Write);

Aes cryptic = Aes.Create();
//cryptic.Padding = PaddingMode.None;
cryptic.Key = entropy;
cryptic.IV = IV;

            // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = cryptic.CreateEncryptor(entropy, IV);

CryptoStream crStream = new CryptoStream(stream,
   decryptor, CryptoStreamMode.Write);


byte[] data = ASCIIEncoding.ASCII.GetBytes(dataToEncrypt);

crStream.Write(data,0,data.Length);

crStream.Close();
stream.Close();


                // Encrypt the string to an array of bytes. 
        //        byte[] encrypted = EncryptStringToBytes_Aes(dataToEncrypt,
//entropy,IV);

  //              string roundtrip = DecryptStringFromBytes_Aes(encrypted,
 //entropy, IV);


               
            



            

            //if(_entropy != null)
            //    entropy = UnicodeEncoding.ASCII.GetBytes(_entropy);
            //DoEncrypt();*/
            return FileName;
        }
        public static void DoDecrypt(string FileNames, string _entropy)
        {
            
            if (string.IsNullOrWhiteSpace(_entropy))
                _entropy = EntropyGenerator.GetIPForMachine();

            /*if (_entropy != null)
            {
                _entropy = _entropy + "test this app for exam app";
            }
            else
                _entropy = EntropyGenerator.GetIPForMachine() + "test this app for exam app";

            entropy = UnicodeEncoding.ASCII.GetBytes(_entropy.ToCharArray(), 0, 16);
            IV = UnicodeEncoding.ASCII.GetBytes(Reverse(_entropy).ToCharArray(), 0, 16);
            */

            decryptedData = null;
            // Open the file.
            if (string.IsNullOrWhiteSpace(FileNames))
                FileName = Application.StartupPath + @"\Data.txt";
            else
                FileName = FileNames;

            //if (FileName.EndsWith(".xml"))
            //{
            //    EncDec.Decrypt(FileName, Application.StartupPath + @"\dataToDecrypt.xml", _entropy);
            //    decryptedData = System.IO.File.ReadAllText(Application.StartupPath + @"\dataToDecrypt.xml");
            //}
            //else
            //{
            if (File.Exists(Application.StartupPath + @"\dataToDecrypt.txt"))
                System.IO.File.Delete(Application.StartupPath + @"\dataToDecrypt.txt");

            try
            {
                EncDec.Decrypt(FileName, Application.StartupPath + @"\dataToDecrypt.txt", _entropy);

                decryptedData = System.IO.File.ReadAllText(Application.StartupPath + @"\dataToDecrypt.txt");
                System.IO.File.Delete(Application.StartupPath + @"\dataToDecrypt.txt");
            }
            catch (Exception ex)
            {
                LogEntry.WriteLog(ex, "Thread Exception");
                
            }
            //}
          /*
fStream = new FileStream("Data.dat", FileMode.Open);
            }





            Aes aesAlg = Aes.Create();

            aesAlg.Key = entropy;
            aesAlg.IV = IV;
           // aesAlg.Padding = PaddingMode.None;
            // Create a decrytor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateDecryptor(entropy, IV);
            CryptoStream crStream = new CryptoStream(fStream,
                encryptor, CryptoStreamMode.Read);

            StreamReader reader = new StreamReader(crStream);

            decryptedData = reader.ReadToEnd();

            reader.Close();
            fStream.Close();


            /*CODE 
            int bytesWritten = Convert.ToInt32(f.Length);

            byte[] inBuffer = new byte[bytesWritten];


            // Read the encrypted data from a stream. 
            if (fStream.CanRead)
            {
                fStream.Read(inBuffer, 0, bytesWritten);
            }

            /* byte[] buffer = new byte[bytesWritten];
             using (MemoryStream ms = new MemoryStream())
             {
                 int read;
                 while ((read = fStream.Read(buffer, 0, buffer.Length)) > 0)
                 {
                     ms.Write(buffer, 0, read);
                 }
                
            

            // Decrypt the bytes to a string. 
            string roundtrip = DecryptStringFromBytes_Aes(inBuffer,
entropy, IV); */
            //}
            /*if (_entropy != null)
                entropy = UnicodeEncoding.ASCII.GetBytes(_entropy);
            DoDecrypt();*/

        }

        /// <summary>
        /// Obsolete
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key,byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an Aes object 
            // with the specified key and IV. 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(Key
, IV);
                // Open the file.
                if (FileName != null && FileName.Length >= 0)
                    fStream = new FileStream(FileName, FileMode.OpenOrCreate);

                else
                {
                    FileName = "Data.dat";
                    fStream = new FileStream("Data.dat", FileMode.Open);
                }


                /*using (FileStream fsOutput = new FileStream(FileName, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(FileName, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
                */
                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            FileInfo f = new FileInfo(FileName);
            fStream.Write(encrypted,0,encrypted.Count());
            fStream.Close();
            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        /// <summary>
        /// Obsolete
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an Aes object 
            // with the specified key and IV. 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(Key
, IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt
, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(
csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting 
                            //stream
                                // and place them in a string.
                                                        plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        public static void DoEncrypt()
        {
            /////////////////////////////// 
            // 
            // Data Encryption - ProtectedData 
            // 
            /////////////////////////////// 

            // Create the original data to be encrypted
            //   byte[] toEncrypt = UnicodeEncoding.ASCII.GetBytes("This is some data of any length.");
            byte[] toEncrypt = UnicodeEncoding.ASCII.GetBytes(encryptedData);
            // Create a file.
            if (FileName != null && FileName.Length == 0)
                FileName = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\DataLicense.dat";

            fStream = new FileStream(FileName, FileMode.OpenOrCreate);

            // Create some random entropy. 
            if (entropy == null)
                entropy = EntropyGenerator.GetIP();


            // Encrypt a copy of the data to the stream. 
            int bytesWritten = EncryptDataToStream(toEncrypt, entropy, DataProtectionScope.LocalMachine, fStream);

            fStream.Close();

        }

        
        public static void DoDecrypt()
        {

            // Open the file.
            if (FileName != null && FileName.Length >= 0)
                fStream = new FileStream(FileName, FileMode.OpenOrCreate);

            else
            {
                FileName = "Data.dat";
                fStream = new FileStream("Data.dat", FileMode.Open);
            }
            FileInfo f = new FileInfo(FileName);
            int bytesWritten = Convert.ToInt32(f.Length);


            // Create some random entropy. 
            if (entropy == null)
                entropy = EntropyGenerator.GetIP(); ;
            // Read from the stream and decrypt the data. 
            byte[] decryptData = DecryptDataFromStream(entropy, DataProtectionScope.LocalMachine, fStream, bytesWritten);
            if (decryptData != null)
                decryptedData = UnicodeEncoding.ASCII.GetString(decryptData);
            else
                decryptedData = null;
            fStream.Close();



        }


        public static void DoEncryptDecrypt()
        {

            DoEncrypt();
            DoDecrypt();


        }
        public static int EncryptDataToStream(byte[] Buffer, byte[] Entropy, DataProtectionScope Scope, Stream S)
        {
            if (Buffer.Length <= 0)
                throw new ArgumentException("Buffer");
            if (Buffer == null)
                throw new ArgumentNullException("Buffer");
            if (Entropy.Length <= 0)
                throw new ArgumentException("Entropy");
            if (Entropy == null)
                throw new ArgumentNullException("Entropy");
            if (S == null)
                throw new ArgumentNullException("S");

            int length = 0;

            // Encrypt the data in memory. The result is stored in the same same array as the original data. 
            byte[] encrptedData = ProtectedData.Protect(Buffer, Entropy, Scope);

            // Write the encrypted data to a stream. 
            if (S.CanWrite && encrptedData != null)
            {
                S.Write(encrptedData, 0, encrptedData.Length);

                length = encrptedData.Length;
            }

            // Return the length that was written to the stream.  
            return length;

        }

        public static byte[] DecryptDataFromStream(byte[] Entropy, DataProtectionScope Scope, Stream S, int Length)
        {
            if (S == null)
                throw new ArgumentNullException("S");
            if (Length <= 0)
                throw new ArgumentException("Length");
            if (Entropy == null)
                throw new ArgumentNullException("Entropy");
            if (Entropy.Length <= 0)
                throw new ArgumentException("Entropy");



            byte[] inBuffer = new byte[Length];
            byte[] outBuffer;

            // Read the encrypted data from a stream. 
            if (S.CanRead)
            {
                S.Read(inBuffer, 0, Length);
                try
                {
                    outBuffer = ProtectedData.Unprotect(inBuffer, Entropy, Scope);
                }
                catch (Exception ex)
                {
                    LogEntry.WriteLog(ex, "Thread Exception");
                    outBuffer = null;
                }
            }
            else
            {
                throw new IOException("Could not read the stream.");
            }

            // Return the length that was written to the stream.  
            return outBuffer;

        }
    }
}
