using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Computer_Graphics_1;

namespace Computer_Graphics_1.HelperClasses.Extensions
{
    public static class WriteableBitmapExJOJO
    {
        public static IntPtr GetPixelIntPtrAt(this WriteableBitmap wbmp, int row, int col) //can make it faster if I do the pixelNumChannel calculation only once and outside of this.
        {
            int pixelNumChannels = wbmp.GetPixelSizeBytes() / 8; //Because of this, program supports only images with 8-bit color and BGR/BGRA channel layout Pixel formats for now. This command exists in order to maintain compatibility wiht both BGR and BGRA config layout.
            return (IntPtr)((long)wbmp.BackBuffer + row * wbmp.BackBufferStride + col * pixelNumChannels);
        }

        //public static _pixel_bgr24_bgra32 operator [
        public static int GetPixelSizeBytes(this WriteableBitmap writtenImg)
        {
            int pixelNumChannels = writtenImg.Format.BitsPerPixel;
            return pixelNumChannels;
        }
    }
}
