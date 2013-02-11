using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CommonUtil;
using System.Runtime.Serialization;

namespace Common
{
    [Serializable]
    public class Match
    {

        public Dictionary<int, MatchEntity> entities = new Dictionary<int, MatchEntity>();

        public Dictionary<int, PlayerEntity> playerPieces = new Dictionary<int, PlayerEntity>();

        public Dictionary<int, PlayerInfo> players = new Dictionary<int, PlayerInfo>();

        private int nextId = 1;

        public int getNextId()
        {
            return nextId++;
        }
        public bool AddPlayer(PlayerInfo player)
        {
            if (!playerPieces.ContainsKey(player.id))
            {
                var entity = new PlayerEntity(this, player);
                AddEntity(entity);
                playerPieces.Add(player.id, entity);
                players.Add(player.id, player);
                return true;
            }
            return false;
        }

        private void AddEntity(MatchEntity entity)
        {
            entities.Add(entity.id, entity);
        }

        private MatchEntity MakeEntity()
        {
            return new MatchEntity() { id = nextId++, bounds = new Rectangle(0, 0, 10, 10), position = new Vector2(30, 30) };
        }

        public bool RemovePlayer(PlayerInfo player)
        {
            PlayerEntity entity;
            if (playerPieces.TryGetValue(player.id, out entity))
            {
                playerPieces.Remove(player.id);
                RemoveEntity(entity);
                players.Remove(player.id);
                return true;
            }
            return false;
        }

        private bool RemoveEntity(MatchEntity entity)
        {
            if (entities.Remove(entity.id))
            {
                return true;
            }
            return false;
        }

    }
}
