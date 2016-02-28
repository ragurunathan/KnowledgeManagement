using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace KnowledgeMatrix.Framework
{
    public static class LogEntry
    {
        
        private static string sLog;
        private static string sEvent;
        private static string sSource;

        public  static void LogEntryInitialise()
        {
            try
            {
                sSource = "dotNET Sample App";
                sLog = "Application";
                sEvent = "Home Event";
                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);
                EventLog.WriteEntry(sSource, sEvent);
            }catch(Exception ex)
            {
                WriteLog(ex, "Event Log Initialisation");
            }
            
        }

        public static void InsertEntry(string sEvent)
        {
            try
            {       
            EventLog.WriteEntry(sSource, sEvent,EventLogEntryType.Information);
             }catch(Exception ex)
            {
                WriteLog(ex, "Event Log Insertion");
            }
        }

        public static void WriteLog(Exception ex,string sEvent)
        {
            //TODO
            //The file should be encrypted

            string filePath = Application.StartupPath + @"\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Event :" + sEvent + "<br/>" + Environment.NewLine + "Message :" + ex.Message + "<br/>" + Environment.NewLine + "Exception Details :" + ex.InnerException + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
          //  System.Windows.Forms.MessageBox.Show("An application error happened. Contact System Administrator");
        }

        public static void WriteLog(string ex, string sEvent)
        {
            //TODO
            //The file should be encrypted

            string filePath = Application.StartupPath + @"\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Event :" + sEvent + "<br/>" + Environment.NewLine + "Exception :" + ex);
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
           // System.Windows.Forms.MessageBox.Show("An application error happened. Contact System Administrator");
        }
    }
}
