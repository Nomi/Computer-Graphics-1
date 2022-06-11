using Computer_Graphics_1.Lab3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Computer_Graphics_1.Lab5.Helpers.Filling;

namespace Computer_Graphics_1.Lab5.Helpers
{
    public class TriMeshFragment
    {
        public Vertex3D v1 { get; }
        public Vertex3D v2 { get; }
        public Vertex3D v3 { get; }

        public Bitmap texture=null; //if null, don't fill.

        public TriMeshFragment(Vertex3D _a, Vertex3D _b, Vertex3D _c)
        {
            v1 = _a;
            v2 = _b;
            v3 = _c;
        }

        public Vertex3D this[int index]
        {
            get
            {
                if (index == 0)
                    return v1;
                if (index == 1)
                    return v2;
                if (index == 2)
                    return v3;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public void DrawFill()//draw fill here! :D
        {
            if (texture == null)
                throw new NullReferenceException();


        }
    }
}
