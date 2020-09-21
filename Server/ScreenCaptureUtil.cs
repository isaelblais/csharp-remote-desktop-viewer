using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    static class ScreenCaptureUtil
    {
        public static Bitmap Capture()
        {
            Bitmap screenshot = null;
            Rectangle bounds;
            Graphics graph = null;
            bounds = Screen.PrimaryScreen.Bounds;
            screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            graph = Graphics.FromImage(screenshot);
            graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenshot;
        }
    }

}
