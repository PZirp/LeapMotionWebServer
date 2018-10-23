using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace WebServerLeapMotion
{
    class ServerMain
    {
        static void Main(string[] args)
        {
            WebServerLeapMotion ws = new WebServerLeapMotion(/*SendResponse, */"http://localhost:8080/test/");
            ws.Run();
            Console.WriteLine("Spegni WebServer.");
            Console.ReadKey();
            ws.Stop();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
        }
    }
}