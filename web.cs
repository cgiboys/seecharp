using System;
using System.IO;
using System.Net;
using System.Threading;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"www/"; // Change this to the path of the folder containing the files you want to host
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/"); // Change this to the desired port number
            listener.Start();
            Console.WriteLine("Web server running...");
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    HttpListenerResponse response = context.Response;
                    string filePath = context.Request.Url.AbsolutePath;
                    filePath = filePath.Substring(1);
                    filePath = Path.Combine(folderPath, filePath);
                    if (File.Exists(filePath))
                    {
                        byte[] buffer = File.ReadAllBytes(filePath);
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                        response.OutputStream.Close();
                    }
                    else
                    {
                        string error = "File not found";
                        byte[] errorBuffer = System.Text.Encoding.UTF8.GetBytes(error);
                        response.ContentLength64 = errorBuffer.Length;
                        response.OutputStream.Write(errorBuffer, 0, errorBuffer.Length);
                        response.OutputStream.Close();
                    }
                }, null);
            }
        }
    }
}