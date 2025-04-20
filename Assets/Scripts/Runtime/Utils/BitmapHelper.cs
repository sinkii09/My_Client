using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace Nara.Utils
{
    public static partial class BitmapHelper
    {
        public static void FillBitmap(Bitmap bmp, Func<int, int, Color> computePixel)
        {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            // stride is number of bytes per row ( it include padding so not always = bmp.Width * bytesPerPixel )
            int stride = bmpData.Stride;
            int height = bmp.Height;
            int width = bmp.Width;

            // RGBA format
            int bytesPerPixel = 4;

            // Create a managed byte array to hold the pixel data
            byte[] pixelBuffer = new byte[stride * height];

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = computePixel(x, y);
                    int index = y * stride + x * bytesPerPixel;

                    pixelBuffer[index + 0] = color.B; // Blue
                    pixelBuffer[index + 1] = color.G; // Green
                    pixelBuffer[index + 2] = color.R; // Red
                    pixelBuffer[index + 3] = color.A; // Alpha
                }
            });
            // Copy the managed byte array back to the bitmap
            Marshal.Copy(pixelBuffer, 0, bmpData.Scan0, pixelBuffer.Length);

            bmp.UnlockBits(bmpData);
        }
    }
}