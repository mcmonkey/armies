using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Messaging;
using Microsoft.Xna.Framework;
using Common;

namespace Server
{
    public class MatchSession
    {
        private Dictionary<int, PlayerSession> players = new Dictionary<int, PlayerSession>();

        private Match match = new Match();

        internal void RemovePlayer(PlayerSession player)
        {
            if(match.RemovePlayer(player.player))
            {
                SendMessage(new PlayerDisconnected() { id = player.player.id });
                players.Remove(player.player.id);
            }
        }

        internal void AddPlayer(PlayerSession playerSession)
        {
            if(match.AddPlayer(playerSession.player)) {
                playerSession.SendMessage(match);
                players.Add(playerSession.player.id, playerSession);
                SendMessage(new PlayerConnected() { info = playerSession.player });
                SendMessage(new ImHereBitch() { direction = new Vector2(20, 20), id = playerSession.player.id, rotation = 0 });
            }
        }

        private void SendMessage<T>(T message)
        {
            foreach(var player in players.Values) {
                player.SendMessage(message);
            }
        }

        internal void HandleMessage(object message, PlayerSession player)
        {
            if (message is PositionalBitch)
            {
                (message as PositionalBitch).id = player.player.id;
                SendMessage(message);
            }
        }
    }
}
