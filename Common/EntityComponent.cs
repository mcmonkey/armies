using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public abstract class EntityComponent<T> where T : IEntityComponent, new()
    {
        private static IEntityComponent template = new T();

        public static int CompId { get { return template.CompId } }
    }
}
