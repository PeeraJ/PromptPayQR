using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PromptPayQR
{
    public static class DrawQR
    {
        public static void Draw(Bitmap qrBitmap)
        {
            var handler = GetConsoleHandle();

            using (var graphics = Graphics.FromHwnd(handler))
            using (var image = qrBitmap)
            {
                graphics.DrawImage(image, 100, 100, 350, 350);
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        private static extern IntPtr GetConsoleHandle();
    }
}