using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Common
{
    [Serializable]
    public class MatchEntity
    {
        private Match match;

        private Dictionary<int, IEntityComponent> components = new Dictionary<int, IEntityComponent>();

        public MatchEntity(Match match)
        {
            id = match.getNextId();
            this.match = match;
        }

        public T getComponent<T>() where T: IEntityComponent, new()
        {
            return (T)(components[EntityComponent<T>.S_CompId]);
        }
        public MatchEntity()
        {
            id = -1;
        }

        public int id = -1;

        public Vector2 position = new Vector2();

        public Rectangle bounds = new Rectangle();

    }
}

