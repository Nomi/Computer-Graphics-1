using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.Lab5.Helpers
{
    public class TriangleCoordinates
    {
        public Point3D_AffC v1 { get; }
        public Point3D_AffC v2 { get; }
        public Point3D_AffC v3 { get; }

        public TriangleCoordinates(Point3D_AffC _a, Point3D_AffC _b, Point3D_AffC _c)
        {
            v1 = _a;
            v2 = _b;
            v3 = _c;
            //a = _a;
            //b = _b;
            //c = _c;
        }
    }
}
