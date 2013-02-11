using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinClient.util.units
{
    public struct Degrees
    {
        public float value;

        public static implicit operator float(Degrees deg)
        {
            return deg.value;
        }

        public static implicit operator Degrees(float val)
        {
            return new Degrees() { value = val };
        }
    }
}
