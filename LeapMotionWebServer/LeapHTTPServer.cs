
using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using Leap;

namespace WebServerLeapMotion
{
    public class WebServerLeapMotion
    {
        private Controller con = new Controller();
        private readonly HttpListener _listener = new HttpListener();
        //private readonly Func<HttpListenerRequest, string> _responderMethod;

        public WebServerLeapMotion(params string[] prefixes/*, Func<HttpListenerRequest, string> method*/)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes missing,, insert a valid URL");

            // A responder method is required
            /*if (method == null)
                throw new ArgumentException("method");*/

            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);

            //_responderMethod = method;
            _listener.Start();
        }

        /*public WebServerLeapMotion(Func<HttpListenerRequest, string> method,params string[] prefixes)
            : this(prefixes, method) { }*/

        public void Run()
        {

            Console.WriteLine("Webserver running...");
            try
            {
                while (_listener.IsListening)
                {
                    var ctx = _listener.GetContext();
                    try
                    {

                        //string rstr = _responderMethod(ctx.Request);
                        //string rstr = string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
                        Frame f = con.Frame();
                        string FS = f.ToString();
                        byte[] buf = Encoding.UTF8.GetBytes(FS);
                        ctx.Response.ContentLength64 = buf.Length;
                        ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                    }
                    catch { } // suppress any exceptions
                    finally
                    {
                        // always close the stream
                        ctx.Response.OutputStream.Close();
                    }
                }
            }
            catch { } // suppress any exceptions
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}