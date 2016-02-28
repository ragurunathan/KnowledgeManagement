using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KnowledgeMatrix.Cryptography;
using KnowledgeMatrix.Framework;
using KnowledgeMatrix.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using ShanuNestedDataGridView;
//using RichTextEditor;
namespace KnowledgeMatrix
{
    static class Program
    {
        #region Exception Handling
        // Creates the error message and displays it. 
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
        
       
        private static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs args)
        {
            try
            {
                
                LogEntry.WriteLog(args.Exception,"Thread Exception");
                DialogResult result = DialogResult.Cancel;
                try
                {
                    result = ShowThreadExceptionDialog("Windows Forms Error", args.Exception);
                }
                catch
                {
                    try
                    {
                        MessageBox.Show("Fatal Windows Forms Error",
                            "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                    }
                    finally
                    {
                        Application.Exit();
                    }
                }

                // Exits the program when the user clicks Abort. 
                if (result == DialogResult.Abort)
                    Application.Exit();
                // Log error here or prompt user...
            }
            catch { }
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            
                
                
                
                // Log error here or prompt user...
           
            try
            {
                LogEntry.WriteLog(args.ExceptionObject.ToString(), "Unhandled Exception");
                Exception ex = (Exception)args.ExceptionObject;
                string errorMsg = "An application error occurred. Please contact the adminstrator " +
                    "with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log. 
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
            Application.Exit();

        }

        #endregion
        private static void validateAndCreateDirectory(string FolderName)
        {
            if (!Directory.Exists(Application.StartupPath + Utility.FolderType() + FolderName))
                Directory.CreateDirectory(Application.StartupPath + Utility.FolderType() + FolderName);
            if (!Utility.isStandard())
            {
                if (!Directory.Exists(KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\" + FolderName))
                    Directory.CreateDirectory(KnowledgeMatrix.Properties.Settings.Default.SharePath + @"\" + FolderName);
            }
        }
     

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           // string [] ab=Environment.GetCommandLineArgs();
            //MessageBox.Show(ab[0]);
         //   AuthorizationOperations obj = new AuthorizationOperations();
          // obj.isUserAccessible(OperationNames.TypeOfOperations.eTutor_Manage);

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new
          ThreadExceptionEventHandler(ThreadExceptionHandler);

            // Set the unhandled exception mode to force all Windows Forms errors
            // to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

          //  Utility.compress(@"Second Standard Science  - NSO.txt");

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new
            UnhandledExceptionEventHandler(UnhandledExceptionHandler);
           
            SplashScreen.SplashScreen.ShowSplashScreen();
            Application.DoEvents();
            
            
            SplashScreen.SplashScreen.SetStatus("Validating App Installation...");
            
            //Thread ms_oThread = null;
            //ms_oThread = new Thread(new ThreadStart(ShowForm));
            //ms_oThread.IsBackground = true;
            //ms_oThread.SetApartmentState(ApartmentState.MTA);
            //ms_oThread.Start();
            //Console.WriteLine("Before sleep");
            System.Threading.Thread.Sleep(2500);
            //Console.WriteLine("After sleep");
            validateAndCreateDirectory(@"QuestionPaper");
            validateAndCreateDirectory(@"QuestionBank");
            validateAndCreateDirectory(@"MockTest");
            validateAndCreateDirectory(@"eTutor");
            validateAndCreateDirectory(@"ProductsManagement");

            //if (!Directory.Exists(Application.StartupPath + @"\QuestionPaper\"))
            //    Directory.CreateDirectory(Application.StartupPath + @"\QuestionPaper\");
            //if (!Directory.Exists(Application.StartupPath + @"\QuestionBank\"))
            //    Directory.CreateDirectory(Application.StartupPath + @"\QuestionBank\");
            //if (!Directory.Exists(Application.StartupPath + @"\MockTest\"))
            //    Directory.CreateDirectory(Application.StartupPath + @"\MockTest\");
            //if (!Directory.Exists(Application.StartupPath + @"\eTutor\"))
            //    Directory.CreateDirectory(Application.StartupPath + @"\eTutor\");
            //if (!Directory.Exists(Application.StartupPath + @"\ProductsManagement\"))
            //    Directory.CreateDirectory(Application.StartupPath + @"\ProductsManagement\");
            

           // MessageBox.Show(EntropyGenerator.GetIPForMachine());
         //   MessageBox.Show(Properties.Settings.Default.IP);
          //  Properties.Settings.Default.Authenicated = "false";
           // Properties.Settings.Default.Save();
           // MessageBox.Show(Utility.isAppValidated().ToString());
            //MessageBox.Show(Application.StartupPath);
            //EncDec.Encrypt(@"C:\Users\224702\Desktop\Sample.txt", @"C:\Users\224702\Desktop\Sample1.txt", @"127.0.0.1");
            //EncDec.Decrypt(@"C:\Users\224702\Desktop\Sample1.txt", @"C:\Users\224702\Desktop\Sample2.txt", @"127.0.0.1");


        // Utility.ResetActivation();
            if (Utility.isAppValidated())
            {
                //check if the ip is same as in settings
                SplashScreen.SplashScreen.SetStatus("Validating User Info... ");
               
                if (KnowledgeMatrix.Properties.Settings.Default.IP != EntropyGenerator.GetIPForMachine())
                {

                    if (KnowledgeMatrix.Properties.Settings.Default.Setting == EntropyGenerator.GetSystemInfo(""))
                    {
                        if (KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate != null && 
                            KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate >= DateTime.Now)//Check the datetime
                        {
                            SplashScreen.SplashScreen.CloseForm();
                            MessageBox.Show("The system is tampered. Contact Administrator.");

                        }
                        else
                        {
                            SplashScreen.SplashScreen.CloseForm();
                            KnowledgeMatrix.Properties.Settings.Default.IP = EntropyGenerator.GetIPForMachine();
                            KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate = DateTime.Now;
                            KnowledgeMatrix.Properties.Settings.Default.Save();
                            MessageBox.Show("The app is used under different IP but in same machine.");
                            Application.EnableVisualStyles();
                         //   Application.SetCompatibleTextRenderingDefault(false);
                           // Application.Run(new frmMasterDetailGrid());
                            Application.Run(new Form1 ());
                        }
                    }
                    else
                        MessageBox.Show("The app is tampered. Contact Administrator.");
                }
                else
                {
                    if (KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate != null &&
                        KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate >= DateTime.Now)//Check the datetime
                    {
                        MessageBox.Show("The system is tampered. Contact Administrator.");
                        
                    }
                    else
                    {
                        SplashScreen.SplashScreen.CloseForm();
                        KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate = DateTime.Now;
                        KnowledgeMatrix.Properties.Settings.Default.Save();
                        Application.EnableVisualStyles();
                       // Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1());
                        //Application.Run(new frmMasterDetailGrid());
                    }

                }
            }
            else
            {
                SplashScreen.SplashScreen.CloseForm();
                //FileCryptography.DoEncryptDecrypt();
                KnowledgeMatrix.Properties.Settings.Default.LastAccessedDate = DateTime.Now;
                KnowledgeMatrix.Properties.Settings.Default.Save();
                Application.EnableVisualStyles();
               // Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
               // Application.Run(new frmMasterDetailGrid());
                if (System.Environment.UserInteractive)
                {
                    //string parameter = string.Concat(args);
                    //switch (parameter)
                    //{
                    //case "--install":
                    //DemoWinAppInstaller obj=new DemoWinAppInstaller();
                    //obj = null;//.Install();//(new string[] { Assembly.GetExecutingAssembly().Location });
                    //  break;
                    //case "--uninstall":
                    //    ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                    //    break;
                    //}
                }
                else
                {


                }
            }
        }
        
    }
}
