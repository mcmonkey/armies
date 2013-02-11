using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    public class Server
    {
        private MatchSession match = new MatchSession();

        public void Start()
        {
            TcpListener main = new TcpListener(IPAddress.Loopback, 765);
            main.Start();
            while(true) {
                TcpClient newSock = main.AcceptTcpClient();
                Console.WriteLine("Connected : " + newSock.Client.RemoteEndPoint);
                PlayerSession player = new PlayerSession(newSock);
                player.SetMatch(match);
            }
            
        }
    }
}
