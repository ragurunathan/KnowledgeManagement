using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;

namespace KnowledgeMatrix.Framework
{
  static  class Utility
    {

      public static string MOD_ALL = "All";
      public static string MOD_QUEST_BANK = "Question Bank";
      public static string MOD_MOCK_TST = "Mock Test";

      public enum TypeOfLicense
      {
          STANDARD,
          PROFESSIONAL
      };
      public static TypeOfLicense License = TypeOfLicense.STANDARD;

      public static string XML_FILE_NAME = Application.StartupPath + @"\Physics.txt";
      public static string XML_QUESTION_NAME = Application.StartupPath+ FolderType() + @"\ProductsManagement\QuestionMaster.txt";
     // public static string XML_QUESTION_NAME_QP = Application.StartupPath + @"\QuestionPaper\QuestionMaster.txt";
      public  enum QuestionTypes
      {
          SingleText,
          MultiText,
          SingleImage,
          MultiImage
      }

      public static bool isNumeric(char KeyPressed)
      {
          if ((KeyPressed > 47 && KeyPressed < 59) || KeyPressed == 8)
             return  false;
          else
             return  true;
      }
      public static bool isStandard()
      {
          if (License == TypeOfLicense.STANDARD)
              return true;
          else
              return false;
      }
      public static void compress(string FileName)
  {
     
    /*  string[] files = new string[1];
      files[0] = @"E:\abc.txt";
      int i = 0;
      //foreach (var fList_item in files)
      //{
      //    files[i] = "\"" + fList_item.Value;
      //    i++;
      //}
      string fileList = string.Join("\" ", files);
      fileList += "\"";
      System.Diagnostics.ProcessStartInfo sdp = new System.Diagnostics.ProcessStartInfo();
      string cmdArgs = string.Format("A {0} {1} -ep -r",
          String.Format("\"{0}\"", @"E:\help"),
          fileList);
      sdp.ErrorDialog = true;
      sdp.UseShellExecute = true;
      sdp.Arguments = cmdArgs;
      sdp.FileName = @"C:\Program Files (x86)\WinRAR\Winrar.exe";//rarPath;//Winrar.exe path
      sdp.CreateNoWindow = false;
      sdp.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
      System.Diagnostics.Process process = System.Diagnostics.Process.Start(sdp);
      process.WaitForExit();

          // DirectoryInfo directorySelected = new DirectoryInfo(@"E:\Personal\Rangarajan\exam app\Exam App .NET\Code\ExamApp\ExamApp\bin\Debug");

          // foreach (FileInfo file in directorySelected.GetFiles("Second Standard Science  - NSO*.txt"))
          //using (FileStream originalFileStream = file.OpenRead())
          //{
          //    if ((File.GetAttributes(file.FullName) & FileAttributes.Hidden)
          //        != FileAttributes.Hidden & file.Extension != ".cmp")
          //    {
          //        using (FileStream compressedFileStream = File.Create(file.FullName + ".cmp"))
          //        {
          //            using (DeflateStream compressionStream = new DeflateStream(compressedFileStream, CompressionMode.Compress))
          //            {
          //                originalFileStream.CopyTo(compressionStream);
          //            }
          //        }

          //        FileInfo info = new FileInfo(@"E:\Personal\Rangarajan\exam app\Exam App .NET\Code\ExamApp\ExamApp\bin\Debug" + "\\" + file.Name + ".cmp");
          //        Console.WriteLine("Compressed {0} from {1} to {2} bytes.", file.Name, file.Length, info.Length);
          //    }
          //}

      //http://stackoverflow.com/questions/15988856/how-to-make-rar-file-of-full-folder-using-c-sharp
      FileStream sourceFileStream = File.OpenRead(FileName);
FileStream destFileStream = File.Create("sitemap.rar");
 
GZipStream compressingStream = new GZipStream(destFileStream, 
    CompressionMode.Compress,true);

byte[] buffer = new byte[sourceFileStream.Length];

sourceFileStream.Read(buffer, 0, buffer.Length);

using (GZipStream output = new GZipStream(destFileStream,
    CompressionMode.Compress))
{
    Console.WriteLine("Compressing {0} to {1}.", sourceFileStream.Name,
        destFileStream.Name, false);

    output.Write(buffer, 0, buffer.Length);
}


//int bytesRead;
//while ((bytesRead = sourceFileStream.Read(bytes, 0, bytes.Length)) != 0)
//{
//    compressingStream.Write(bytes, 0, bytesRead);
//}
 
sourceFileStream.Close();   
compressingStream.Close();
destFileStream.Close();*/
  }
      public static string Base64Encode(string plainText)
      {
//          System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
//string StoreMe =
//System.Convert.ToBase64String(enc.GetBytes(plainText));

          //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
          //return System.Convert.ToBase64String(plainTextBytes);
return plainText;
      }
      public static string Base64Decode(string base64EncodedData)
      {
          //var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
          ////return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
          //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
          //byte[] result = System.Convert.FromBase64String(base64EncodedData);
          //string RTFString = enc.GetString(result);
          return base64EncodedData;
      }
        public  static bool isAppValidated()
        {
            if (KnowledgeMatrix.Properties.Settings.Default.Authenicated == "false")
                return false;
            else
            {

                return true;
            }
                  

        }
        public static void validateAndCreateDirectory(string FolderName)
        {
            if (!Directory.Exists(Application.StartupPath + Utility.FolderType() + FolderName))
                Directory.CreateDirectory(Application.StartupPath + Utility.FolderType() + FolderName);
        }
        public static void ResetActivation()
        {
            KnowledgeMatrix.Properties.Settings.Default.IP = "0.0.0.0";
            KnowledgeMatrix.Properties.Settings.Default.Authenicated = "false";
            KnowledgeMatrix.Properties.Settings.Default.Setting = "";
            KnowledgeMatrix.Properties.Settings.Default.ProductKey = "";
           // Properties.Settings.Default.DateOfActivation = ;
            KnowledgeMatrix.Properties.Settings.Default.Save();
        }
      
        public static bool IsAdmin()
        {
           bool isAdmin=false;
           isAdmin = Convert.ToBoolean(KnowledgeMatrix.Properties.Settings.Default.Administrator);
           return isAdmin;
        }

        public static string FolderType()
        {
             if(IsAdmin())
                return @"\Admin\";
                    else
                return @"\Client\";
        }
    }
}
