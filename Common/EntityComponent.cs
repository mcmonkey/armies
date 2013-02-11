using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public abstract class EntityComponent<T> : IEntityComponent where T : IEntityComponent, new()
    {
        private static IEntityComponent template = new T();

        public static byte S_CompId { get { return template.CompId;  } }

        public abstract byte CompId { get; }

        public abstract void SetEntity(MatchEntity entity);
    }
}
