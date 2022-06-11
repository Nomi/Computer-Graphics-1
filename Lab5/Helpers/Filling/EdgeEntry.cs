using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.Lab5.Helpers.Filling
{
    public class EdgeEntry
    {
        public int yMax, yMin;
        public float xMin, xMax;
        public float inverseSlope;
        public Vertex3D v1, v2;
        public float textureX, textureY;
        public float length;

        public EdgeEntry() { }

        public EdgeEntry(int y, float x, float inverseSlope)
        {
            yMax = y;
            xMin = x;
            this.inverseSlope = inverseSlope;
        }
    }
}
