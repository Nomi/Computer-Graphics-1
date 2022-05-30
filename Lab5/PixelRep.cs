using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.Lab5
{
    public class PixelRep
    {
        public int X;
        public int Y;
        public Color color;

        public PixelRep(int _x, int _y, Color c)
        {
            X = _x;
            Y = _y;
            color = c;
        }
    }
}
