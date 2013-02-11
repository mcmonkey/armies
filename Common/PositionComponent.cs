using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Common
{
    public class PositionComponent : EntityComponent<PositionComponent> {

        public override byte CompId
        {
            get { return 1; }
        }

        private Vector2 position = new Vector2();

        public void GetPosition(ref Vector2 pos)
        {
            pos.X = position.X;
            pos.Y = position.Y;
        }
        
    }
}
