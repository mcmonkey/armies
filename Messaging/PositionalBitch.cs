using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Messaging
{
    [Serializable]
    public class PositionalBitch
    {
        public Vector2 direction = new Vector2();

        public int id = -1;

        /// <summary>
        /// RADIANS BITCH
        /// </summary>
        public float rotation;
    }
}
