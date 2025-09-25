// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Laserocitor.Views.DisplayScreen
{
    /// <summary>
    /// Interaction logic for DisplayScreen.xaml.
    /// </summary>
    public partial class DisplayWindow : Window
    {
        private double xScreenSize;
        private double yScreenSize;

        private const int GWL_STYLE  = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Constructor (default).
        /// </summary>
        public DisplayWindow()
        {
            InitializeComponent();

            xScreenSize = Width;
            yScreenSize = Height;

            SizeChanged += Window_SizeChanged;
        }
        
        public double GetScreenHeight()
        {
            return xScreenSize;
        }

        public double GetScreenWidth()
        {
            return yScreenSize;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;

            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            xScreenSize = Width;
            yScreenSize = Height;
        }
    }
}
