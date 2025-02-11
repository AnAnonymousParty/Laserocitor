// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System.Windows;

namespace Laserocitor.Views.DisplayScreen
{
    /// <summary>
    /// Interaction logic for DisplayScreen.xaml.
    /// </summary>
    public partial class DisplayWindow : Window
    {
        private double xScreenSize;
        private double yScreenSize;

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
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            xScreenSize = Width;
            yScreenSize = Height;
        }
    }
}
