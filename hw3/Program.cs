#define DEBUG
#define TRACE

using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace HTTPServer
{
    class Client
    {
        private void SendError(TcpClient Client, int Code)
        {
            string CodeStr = Code.ToString() + " " + ((HttpStatusCode)Code).ToString();
            string Html = "<html><body><h1>" + CodeStr + "</h1></body></html>";
            string Str = "HTTP/1.1 " + CodeStr + "\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
            byte[] Buffer = Encoding.ASCII.GetBytes(Str);
            Client.GetStream().Write(Buffer, 0, Buffer.Length);
            Client.Close();
        }

        private void SendText(TcpClient Client, int Code, string Text) {
            string CodeStr = Code.ToString() + " " + ((HttpStatusCode)Code).ToString();
            string Str = "HTTP/1.1 " + CodeStr + "\nContent-type: text/plain\nContent-Length:" + Text.Length.ToString() + "\n\n" + Text;
            byte[] Buffer = Encoding.Default.GetBytes(Str);

            Client.GetStream().Write(Buffer, 0, Buffer.Length);
            Client.Close();
        }

        private static string DecodeUrlString(string url) {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }

        public Client(TcpClient Client)
        {
            Debug.WriteLine("NEW INCOMING REQUEST");
            string Request = "";
            byte[] Buffer = new byte[1024];
            int Count;
            while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
            {
                Request += Encoding.ASCII.GetString(Buffer, 0, Count);
                if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096)
                {
                    break;
                }
            }

            Debug.WriteLine("Full request:");
            Debug.WriteLine("********************");
            Debug.WriteLine(string.Format("{0}", Request));
            Debug.WriteLine("********************");

            Match ReqMatch = Regex.Match(Request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");

            if (ReqMatch == Match.Empty)
            {
                SendError(Client, 400);
                return;
            }

            string RequestUri = ReqMatch.Groups[1].Value;

            RequestUri = Uri.UnescapeDataString(RequestUri);
            Debug.WriteLine(String.Format("Request URI: '{0}'", RequestUri));


            if (RequestUri == "/submit") {
                string paramString = Request.Split(" ")[1].Split("?")[1];
                string[] paramList = paramString.Split("&");
                string mathExpr = DecodeUrlString(paramList[0].Split("=")[1]);
                string result;
                try {
                    var polish = HW1.MakeReversePolish(mathExpr);
                    result = HW1.InterpretReversePolish(polish).ToString();
                } catch (Exception) {
                    SendText(Client, 200, "invalid expression");
                    return;
                }
                SendText(Client, 200, result);
                return;
            }

            string ContentType = "text/html";

            FileStream FS;
            try
            {
                FS = new FileStream("index.html", FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                SendError(Client, 500);
                return;
            }

            string Headers = "HTTP/1.1 200 OK\nContent-Type: " + ContentType + "\nContent-Length: " + FS.Length + "\n\n";
            byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
            Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);

            while (FS.Position < FS.Length)
            {
                Count = FS.Read(Buffer, 0, Buffer.Length);
                Client.GetStream().Write(Buffer, 0, Count);
            }

            FS.Close();
            Client.Close();
        }
    }

    class Server
    {
        TcpListener Listener;

        public Server(int Port)
        {
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start();
            Debug.WriteLine("Запущен сервер по адресу {0}", Listener.LocalEndpoint);

            while (true)
            {
                #pragma warning disable CS8622
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), Listener.AcceptTcpClient());
            }
        }

        static void ClientThread(Object StateInfo)
        {
            new Client((TcpClient)StateInfo);
        }

        ~Server()
        {
            if (Listener != null)
            {
                Debug.WriteLine("Остановлен сервер по адресу '{0}'", Listener.LocalEndpoint);
                Listener.Stop();
            }
        }

        static void Main(string[] args)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;

            int MaxThreadsCount = Environment.ProcessorCount * 4;
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            ThreadPool.SetMinThreads(2, 2);
            new Server(80);
        }
    }
}
