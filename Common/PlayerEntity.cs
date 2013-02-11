using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUtil;

namespace Common
{
    [Serializable]
    public class PlayerEntity : MatchEntity
    {
        public PlayerEntity(Match match, PlayerInfo owner) :
            base(match) {
                this.owner = owner.id;
        }

        public PlayerEntity(){
        }

        public int owner;
    }
}
