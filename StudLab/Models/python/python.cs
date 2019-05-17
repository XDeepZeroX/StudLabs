using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StudLab.Models.python
{
    public static class python
    {
        public static int PORT = 6666;
        public static string Socket(Dictionary<string,string> argsDict)
        {
            return Socket(GetArgsStr(argsDict));
        }
        public static string Socket(string message = null)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);


            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);
            data = new byte[9999999];
            StringBuilder stringBuilder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = socket.Receive(data, data.Length, SocketFlags.None);
                stringBuilder.Append(Encoding.UTF8.GetString(data, 0, bytes));

            } while (socket.Available > 0);
            // закрываем сокет
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return stringBuilder.ToString();
        }

        public static string GetArgsStr(Dictionary<string,string> argsDict)
        {
            List<string> args = new List<string>();
            foreach (var arg in argsDict)
            {
                args.Append($"--{arg.Key} {arg.Value}");
            }
            return String.Join(" ", args);
        }
    }
}
