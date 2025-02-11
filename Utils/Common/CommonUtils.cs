// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.
 

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace Laserocitor.Utils.Common
{
    /// <summary>
    /// Mostly static functions to perform various actions.
    /// </summary>
    class CommonUtils
    {
        private static Random rand = new Random();  // For all your random needs.

        /// <summary>
        /// Move window to the foreground.
        /// </summary>
        /// <param name="window">Window object for window to be featured.</param>
        public static void BringWindowForward(Window window)
        {
            try
            {
                // Prevent the window from grabbing focus away from other windows the first time is created. 

                window.ShowActivated = false;

                if (Visibility.Visible != window.Visibility)
                {
                    window.Visibility = Visibility.Visible;
                }

                if (WindowState.Maximized == window.WindowState)
                {
                    window.WindowState = WindowState.Normal;
                }

                window.Topmost = true;
            }
            catch (Exception)
            {
                // Ignore "Cannot set visibility while window is closing".
            }
        }

        /// <summary>
        /// Convert integer value to RGB values.
        /// </summary>
        /// <remarks>
        /// This function converts an integer color index in the range 
        /// 0 - 1535 (covering six ranges of 256 RGB combinations each,   
        /// Red -> Yellow -> Green -> Cyan -> Blue -> Violet -> Red), 
        /// to a set of RGB values, each in the range 0 - 255.                 
        /// </remarks>
        /// <param name="iColorNdx">Integer color index (0 - 1535),</param>
        /// <param name="cMasterIntensity">The master intensity value.</param>
        /// <returns>RGB color value.</returns>
        public static Color ColorInt2RGB(int iColorNdx, int cMasterIntensity)
        {
            byte blue       = 0,  // Blue component of calculated RGB value.
                 colorRange = 0,  // Color range.
                 green      = 0,  // Green component of calculated RGB value.
                 red        = 0;  // Red component of calculated RGB value.

            // Determine which range we are in:

            while (iColorNdx > 255)
            {
                // Subtract maximum RGB value (256) from the caller supplied integer color index,
                // incrementing the color range each time, until the color index < 256.  The
                // residual color index can then be used directly to compute the variable component
                // of whatever range we find ourselves in.

                iColorNdx -= 256;

                ++colorRange;
            }

            // Convert color index to RGB value, according to the calculated range, using the color
            // index as modified by the previous range determination:

            switch (colorRange)
            {
                case 0:  // Red -> Yellow range (includes Orange [duh]).
                    {
                        red   = 255;
                        green = (byte)(iColorNdx);
                        blue  = 0;
                    }
                    break;

                case 1:  // Yellow -> Green range.
                    {
                        red   = (byte)(255 - iColorNdx);
                        green = 255;
                        blue  = 0;
                    }
                    break;

                case 2:  // Green -> Cyan range.
                    {
                        red   = 0;
                        green = 255;
                        blue  = (byte)iColorNdx;
                    }
                    break;

                case 3:  // Cyan -> Blue range.
                    {
                        red   = 0;
                        green = (byte)(255 - iColorNdx);
                        blue  = 255;
                    }
                    break;

                case 4:  // Blue -> Violet range.
                    {
                        red   = (byte)iColorNdx;
                        green = 0;
                        blue  = 255;
                    }
                    break;

                case 5:  // Violet -> Red range.
                    {
                        red   = 255;
                        green = 0;
                        blue  = (byte)(255 - iColorNdx);
                    }
                    break;
            }

            // Use the calculated RGB values to set the drawing color:

            return (Color.FromArgb(255, (byte)(red   * cMasterIntensity / 100.0),
                                        (byte)(green * cMasterIntensity / 100.0),
                                        (byte)(blue  * cMasterIntensity / 100.0)));
        }

        /// <summary>
        /// Convert color RGB values to an integer color index.
        /// </summary>
        /// <remarks>
        /// This function converts an RGB color value to a color index in the range 0 - 1535
        /// (covering six ranges of 256 RGB combinations each,                              
        /// Red -> Yellow -> Green -> Cyan -> Blue -> Magenta -> Red). 
        ///  0       255      511      767    1023     1279     1535
        /// </remarks>
        /// <param name="oColor">RGB Color value.</param>
        /// <returns>Color index (integer).</returns>
        public static int ColorRGB2Int(Color oColor)
        {
            byte alpha = oColor.A,  // Alpha component of RGB value.
                 blue  = oColor.B,  // Blue  component of RGB value.
                 green = oColor.G,  // Green component of RGB value.
                 red   = oColor.R;  // Red   component of RGB value.

            // +==========+==========+==========+==========+===========+
            // |   COLOR  |   RED    |  GREEN   |   BLUE   |   VALUE   |
            // +==========+==========+==========+==========+===========+
            // | R        | 255      |        0 |        0 |         0 | <----+
            // | R/O -> Y | 255      | 255 <- 0 |        0 |     1-255 |      |
            // | Y        | 255      | 255      |        0 |       255 |      |
            // | Y -> G   | 255 -> 0 | 255      |        0 |   256-511 |      |
            // | G        |        0 | 255      |        0 |       511 |      |
            // | G -> C   |        0 | 255      | 255 <- 0 |   512-767 |      |
            // | C        |        0 | 255      | 255      |       767 |      |
            // | C -> B   |        0 | 255 -> 0 | 255      |  768-1023 |      |
            // | B        |        0 |        0 | 255      |      1023 |      |
            // | B -> M   | 255 <- 0 |        0 | 255      | 1024-1279 |      |
            // | M        | 255      |        0 | 255      |      1279 |      |
            // | M -> R   | 255      |        0 | 255 -> 0 | 1280-1535 | >>---+
            // +==========+==========+==========+==========+===========+

            if (0 == blue)
            {
                if (255 == red)
                {
                    if (255 == green)
                    {
                        return 255;  // Solid Yellow.
                    }

                    return green;  // Red/Orange -> Yellow (0-255).
                }

                if (255 == green)
                {
                    if (0 == red)
                    {
                        return 511;  // Solid Green;
                    }

                    return 511 - red;  // Yellow -> Green (256-511).
                }                
            }
            
            if (255 == blue)
            {
                if (0 == blue && 0 == green)
                {
                    return 1023;  // Solid Blue;
                }

                if (255 == green)
                {
                    return 767;  // Solid Cyan.
                }

                if (255 == red)
                {
                    return 1279;  // Solid Magenta.
                }

                if (0 == green)
                {
                    return 1279 - red;
                }

                return 1023 - green;  // Cyan -> Blue (768-1023).                
            }
            
            if (0 == green)
            {
                if (255 == red)
                {
                    return 1535 - blue;  // Magenta -> Red (1280-1535).
                }
            }    

            if (255 == green)
            {
                return 767 - blue;  // Green -> Cyan (512-767).
            }

            return 0;  // Solid Red.
        }

        /// <summary>
        /// Given an enumerated value, return its associated descriptive string.
        /// </summary>
        /// <param name="value">The enumerated value.</param>
        /// <returns>The enum;s description.</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (null != attributes && true == attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        /// <summary>
        /// Get a random color.
        /// </summary>
        /// <returns>Random color value.</returns>
        public static int GetRandomColor()
        {
            return (int)(0xFF000000 | (uint)rand.Next(0xFFFFFF));
        }

        /// <summary>
        /// Now you see it, now you don't.
        /// </summary>
        /// <param name="window">Window object of window to be yeeted.</param>
        public static void SendWindowToBackground(Window window)
        {
            try
            {
                // Prevent the window from grabbing focus away from other windows the first time is created. 

                window.ShowActivated = false;

                if (Visibility.Collapsed != window.Visibility)
                {
                    window.Visibility = Visibility.Collapsed;
                }

                if (WindowState.Maximized == window.WindowState)
                {
                    window.WindowState = WindowState.Normal;
                }

                window.Topmost = false;
            }
            catch (Exception)
            {
                // Ignore "Cannot set visibility while window is closing".
            }
        }

        /// <summary>
        /// Display a message box.
        /// </summary>
        /// <param name="msg">String containing message to be displayed.</param>
        /// <param name="title">String containing title to be displayed.</param>
        /// <param name="icon">Icon to be displayed (optional, Question by default).</param>
        public static void ShowMessage(String title,
                                       String msg,
                                       MessageBoxImage icon = MessageBoxImage.Information)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, icon);
        }

        /// <summary>
        /// Prompt user for a response.
        /// </summary>
        /// <param name="msg">String containing message to be displayed.</param>
        /// <param name="title">String containing title to be displayed.</param>
        /// <param name="promptType">Type of prompt to be displayed (optional, OK/Cancel by default).</param>
        /// <param name="icon">Icon to be displayed (optional, Question by default).</param>
        /// <returns>MessageBoxResult object containing the user's response.</returns>
        public static MessageBoxResult ShowPrompt(String title, 
                                                  String msg, 
                                                  MessageBoxButton promptType = MessageBoxButton.OKCancel, 
                                                  MessageBoxImage icon = MessageBoxImage.Question)
        {
            return MessageBox.Show(msg, title, promptType, icon);
        }
    }
}

