using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface IEntityComponent
    {
        byte CompId { get; }
        void SetEntity(MatchEntity entity);
    }
}
