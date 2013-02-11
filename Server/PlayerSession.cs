using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Messaging;
using System.Threading;
using Common;
using CommonUtil;

namespace Server
{
	public class PlayerSession
	{
        public static int nextId = 1;

        private static IFormatter formatter = new BinaryFormatter();

        private TcpClient client;

        public PlayerInfo player;

        public MatchSession match { get; private set; }

        public PlayerSession(TcpClient client)
        {
            this.client = client;
            var thread = new Thread(Start);
            thread.Start();

            var ping = new Thread(Ping);
            ping.Start();
        }

        private void Ping()
        {
            Console.WriteLine("Bytes: " + client.Available);
            Thread.Sleep(4000);
        }

        private void Start()
        {
            HandShake shake = formatter.Deserialize(client.GetStream()) as HandShake;
            if (shake != null)
            {
                player = new PlayerInfo();
                player.id = new Random().Next();
                player.color = (uint)new Random().Next(int.MinValue, int.MaxValue);

                formatter.Serialize(client.GetStream(), new HandShakeAck() { info = player });
                if (match != null)
                {
                    match.AddPlayer(this);
                }
                while (true)
                {
                    var message = formatter.Deserialize(client.GetStream());
                    if (message is MoveBitch)
                    {
                        match.HandleMessage(message, this);
                    }
                }
            }
            else
            {
                throw new Exception("SDfjkLSDJF");
            }
          
        }

        public void SetMatch(MatchSession match)
        {
            if (match != this.match)
            {
                if (this.match != null)
                {
                    this.match.RemovePlayer(this);
                }
                this.match = match;
                if (match != null && player != null)
                {
                    match.AddPlayer(this);
                }
            }
        }

        internal void SendMessage(object message)
        {
            formatter.Serialize((client.GetStream()), message);
        }
    }
}
