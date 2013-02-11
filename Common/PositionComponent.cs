using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class PositionComponent : EntityComponent<PositionComponent> {

        public override byte CompId
        {
            get { return 1; }
        }

        public override void SetEntity(MatchEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
