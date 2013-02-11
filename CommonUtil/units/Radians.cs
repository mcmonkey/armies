using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinClient.util.units
{
    public struct Radians
    {
        public const float PI = (float)Math.PI / 180f;

        public const float OVER_PI = 180f / (float)Math.PI;

        public float value;

        public static implicit operator Radians(Degrees deg)
        {
            return new Radians() { value = deg * PI };
        }

        public static implicit operator Degrees(Radians rad)
        {
            return new Degrees() { value = rad * OVER_PI };
        }

        public static implicit operator float(Radians rad)
        {
            return rad.value;
        }

        public static implicit operator Radians(float val)
        {
            return new Radians() { value = val };
        }
    }
}
