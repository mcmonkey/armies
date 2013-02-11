using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public abstract class EntityComponent<T> : IEntityComponent where T : IEntityComponent, new()
    {
        private static IEntityComponent template = new T();

        public static byte S_CompId { get { return template.CompId;  } }

        protected MatchEntity m_entity;

        public abstract byte CompId { get; }

        public virtual void SetEntity(MatchEntity entity)
        {
            m_entity = entity;
        }
    }
}
