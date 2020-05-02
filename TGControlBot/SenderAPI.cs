/////////////////////////////////////////////////////////
///////////////////By Telegram: @madcod//////////////////
/////////////////////////////////////////////////////////

using System;
using System.Net;
using System.Threading;

namespace TGControlBot
{
    class SenderAPI
    {
        public static void GET(string url)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; //Без этого не работает SSL Соединение.                                                                                                                     // Выполняем запрос по адресу и получаем ответ в виде строки
                    var response = webClient.DownloadString(url);
                }

            }
            catch
            {
                using (var webClient = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var proxy = new WebProxy(Program.ip_proxy, Program.port_proxy) // IP Proxy
                    {
                        Credentials = new NetworkCredential(Program.login_proxy, Program.password_proxy) // Логин и пароль Proxy
                    };
                    webClient.Proxy = proxy;
                    var response = webClient.DownloadString(url);
                }

            }
            return;

        }


        public static void POST(byte[] file, string filename, string contentType, string url)
        {

            Thread.Sleep(new Random(Environment.TickCount).Next(500, 2000));
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                WebClient webClient = new WebClient
                {
                    Proxy = null //Не используем
                };

                string text = "------------------------" + DateTime.Now.Ticks.ToString("x");
                webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + text);
                string @string = webClient.Encoding.GetString(file);
                string s = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"document\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", new object[]
                {

                    text,
                    filename,
                    contentType,
                    @string
                });
                byte[] bytes = webClient.Encoding.GetBytes(s);
                webClient.UploadData(url, "POST", bytes);
            }
            catch
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                using (var webClient = new WebClient())
                {
                    string text = "------------------------" + DateTime.Now.Ticks.ToString("x");
                    webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + text);
                    string @string = webClient.Encoding.GetString(file);
                    string s = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"document\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", new object[]
                    {

                    text,
                    filename,
                    contentType,
                    @string
                    });
                    byte[] bytes = webClient.Encoding.GetBytes(s);
                    var proxy = new WebProxy(Program.ip_proxy, Program.port_proxy) // IP Proxy
                    {
                        Credentials = new NetworkCredential(Program.login_proxy, Program.password_proxy) // Логин и пароль Proxy
                    };
                    webClient.Proxy = proxy;
                    webClient.UploadData(url, "POST", bytes);
                }
            }
            return;

        }
    }
}
