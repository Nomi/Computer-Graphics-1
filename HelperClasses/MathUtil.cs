using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.HelperClasses
{
    public static class MathUtil
    {
		public static int Clamp(int value, int min, int max)
		{
			return (value < min) ? min : (value > max) ? max : value;
		}

		public static float FastSqRt(float number)
        {
			return (1 / FastInverseSqRt(number));
        }

		public static float FastInverseSqRt(float number) //Sourced from DOOM? lol.
		{
			unsafe
			{
				long i;
				float x2, y;
				const float threehalfs = 1.5F;

				x2 = number * 0.5F;
				y = number;
				i = *(long*)&y;                        // evil floating point bit level hacking
				i = 0x5f3759df - (i >> 1);             // what the ***k? 
				y = *(float*)&i;
				y = y * (threehalfs - (x2 * y * y));   // 1st iteration
				return y;
			}
		}
	}
}
