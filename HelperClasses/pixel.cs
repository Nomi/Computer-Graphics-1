using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.HelperClasses
{//maybe this ust won't work. I should wokr on setpixel, nextpixel, and getpixel functions first? or maybe instead implment operator[] for WritableBitmapExt which does the same code I normally do but automatically.
    public struct _pixel_bgr24_bgra32 //only for BGR(A) pixel formats
    {
        public byte blue;
        public byte green;
        public byte red;
    }
}
