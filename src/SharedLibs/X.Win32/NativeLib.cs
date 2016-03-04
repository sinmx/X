﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace X.Win32
{
    public static class NativeLib
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public int x { get; private set; }
            public int y { get; private set; }
            
            internal Win32Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            static public explicit operator Win32Point(Point pt)
            {
                return checked(new Win32Point((int)pt.X, (int)pt.Y));
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Win32Point pt);

    }
}
