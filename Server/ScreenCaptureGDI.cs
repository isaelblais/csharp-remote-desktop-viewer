using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    static class ScreenCaptureGDI
    {
        [System.Runtime.InteropServices.DllImport("GDI32")]
        private static extern int CreateDC(string lpDriverName, string lpDeviceName, string lpOutput, string lpInitData);

        [System.Runtime.InteropServices.DllImport("GDI32")]
        private static extern int CreateCompatibleDC(int hDC);

        [System.Runtime.InteropServices.DllImport("GDI32")]
        private static extern int CreateCompatibleBitmap(int hDC, int nWidth, int nHeight);

        [System.Runtime.InteropServices.DllImport("GDI32")]
        private static extern int GetDeviceCaps(int hdc, int nIndex);

        [System.Runtime.InteropServices.DllImport("GDI32")]
        private static extern int SelectObject(int hDC, int hObject);

        [System.Runtime.InteropServices.DllImport("GDI32")]
        private static extern int BitBlt(int srchDC, int srcX, int srcY, int srcW, int srcH, int desthDC, int destX, int destY, int op);

        [System.Runtime.InteropServices.DllImport("GDI32")]
        private static extern int DeleteDC(int hDC);

        [System.Runtime.InteropServices.DllImport("GDI32")]
        private static extern int DeleteObject(int hObj);

        public static Bitmap capture = null;
        private static int FW, FH;

        public static void Capture()
        {
            int hSDC, hMDC;
            int hBMP, hBMPOld;
            int r;

            hSDC = CreateDC("DISPLAY", "", "", "");
            hMDC = CreateCompatibleDC(hSDC);

            FW = GetDeviceCaps(hSDC, 8);
            FH = GetDeviceCaps(hSDC, 10);
            hBMP = CreateCompatibleBitmap(hSDC, FW, FH);

            hBMPOld = SelectObject(hMDC, hBMP);
            r = BitBlt(hMDC, 0, 0, FW, FH, hSDC, 0, 0, 13369376);
            hBMP = SelectObject(hMDC, hBMPOld);

            r = DeleteDC(hSDC);
            r = DeleteDC(hMDC);

            capture = Image.FromHbitmap(new IntPtr(hBMP));
            DeleteObject(hBMP);
        }
    }

}
