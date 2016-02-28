using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Management;

namespace KnowledgeMatrix.Cryptography
{
    class EntropyGenerator
    {
        public static byte[] CreateRandomEntropy()
        {
            // Create a byte array to hold the random value. 
            byte[] entropy = new byte[16];


            System.Management.ManagementClass objMgmtCls = new
    System.Management.ManagementClass("Win32_NetworkAdapter");

            foreach (System.Management.ManagementObject objMgmt in objMgmtCls.GetInstances())
            {
                if (objMgmt["MACAddress"] != null && objMgmt["MACAddress"].ToString().Length > 0)
                {
                    entropy = UnicodeEncoding.ASCII.GetBytes(objMgmt["MACAddress"].ToString());
                    break;
                }

            }



            // Return the array. 
            return entropy;


        }

        public static byte[] GetIP()
        {
            IPHostEntry SystemAC = Dns.GetHostEntry(Dns.GetHostName());
            string IPAddress = string.Empty;
            foreach (var address in SystemAC.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPAddress = address.ToString();

                }
            }
            return UnicodeEncoding.ASCII.GetBytes(IPAddress);
        }
        public static string GetIPForMachine()
        {
            IPHostEntry SystemAC = Dns.GetHostEntry(Dns.GetHostName());
            string IPAddress = string.Empty;
            foreach (var address in SystemAC.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPAddress = address.ToString();

                }
            }
            return IPAddress;
        }

        public static string GetKeyForMachine()
        {
            return KnowledgeMatrix.Properties.Settings.Default.ProductKey;
        }

        public static byte[] GetKeyBytesForMachine()
        {
            return UnicodeEncoding.ASCII.GetBytes(KnowledgeMatrix.Properties.Settings.Default.ProductKey);
        }
        #region -> Private Variables

        public static bool UseProcessorID=true;
        public static bool UseBaseBoardProduct=true;
        public static bool UseBaseBoardManufacturer=true;
        public static bool UseDiskDriveSignature=false;
        public static bool UseVideoControllerCaption=false;
        public static bool UsePhysicalMediaSerialNumber=true;
        public static bool UseBiosVersion=true;
        public static bool UseBiosManufacturer=false;
        public static bool UseWindowsSerialNumber=false;

        #endregion

        public static string GetSystemInfo(string SoftwareName)
        {
            if (UseProcessorID == true)
                SoftwareName += RunQuery("Processor", "ProcessorId");

            if (UseBaseBoardProduct == true)
                SoftwareName += RunQuery("BaseBoard", "Product");

            if (UseBaseBoardManufacturer == true)
                SoftwareName += RunQuery("BaseBoard", "Manufacturer");

            if (UseDiskDriveSignature == true)
                SoftwareName += RunQuery("DiskDrive", "Signature");

            if (UseVideoControllerCaption == true)
                SoftwareName += RunQuery("VideoController", "Caption");

            if (UsePhysicalMediaSerialNumber == true)
                SoftwareName += RunQuery("PhysicalMedia", "SerialNumber");

            if (UseBiosVersion == true)
                SoftwareName += RunQuery("BIOS", "Version");

            if (UseWindowsSerialNumber == true)
                SoftwareName += RunQuery("OperatingSystem", "SerialNumber");

            SoftwareName = RemoveUseLess(SoftwareName);

            if (SoftwareName.Length < 25)
                return GetSystemInfo(SoftwareName);

            return SoftwareName.Substring(0, 25).ToUpper();
        }

        private static string RemoveUseLess(string st)
        {
            char ch;
            for (int i = st.Length - 1; i >= 0; i--)
            {
                ch = char.ToUpper(st[i]);

                if ((ch < 'A' || ch > 'Z') &&
                    (ch < '0' || ch > '9'))
                {
                    st = st.Remove(i, 1);
                }
            }
            return st;
        }

        private static string RunQuery(string TableName, string MethodName)
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * from Win32_" + TableName);
            foreach (ManagementObject MO in MOS.Get())
            {
                try
                {
                    return MO[MethodName].ToString();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            }
            return "";
        }
    }
}
