// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;
using System.Windows;
using System.Windows.Media;

using Laserocitor.Common;

namespace Laserocitor.UserControls.ImageAssignment
{
    /// <summary>
    /// Interaction logic for ImageAssignment.xaml.
    /// </summary>
    public partial class ImageAssignment 
    {
     private Brush activeButtonBkgnd;
     private Brush inActiveButtonBkgnd;

        /// <summary>
        /// Constructor (default).
        /// </summary>
        public ImageAssignment()
        {
         var buttonStyles = new ResourceDictionary
         {
          Source = new Uri("/Laserocitor;component/Resources/UIElementStyles.xaml", UriKind.RelativeOrAbsolute)
         };

         activeButtonBkgnd   = buttonStyles["ButtonEnabledOn"]  as SolidColorBrush;
         inActiveButtonBkgnd = buttonStyles["ButtonEnabledOff"] as SolidColorBrush;
         
         InitializeComponent();

         UpdateUI();
        }

        #region Dependency Properties

        #region Image Drawing Mode property.

        public static readonly DependencyProperty BCDrawingModeProperty = DependencyProperty.Register("BCDrawingMode",
                                                                                                      typeof(eMODE),
                                                                                                      typeof(ImageAssignment),
                                                                                                      new PropertyMetadata(eMODE.eCycloid, OnBCDrawingModeChanged));
        public eMODE BCDrawingMode
        {
            get { return (eMODE)GetValue(BCDrawingModeProperty); }
            set 
            { 
                SetValue(BCDrawingModeProperty, value);
            }
        }

        /// <summary>
        /// Handle the event triggered when the drawing mode is changed.
        /// </summary>
        /// <param name="d">DependencyObject object presumed to be of this type.</param>
        /// <param name="args">DependencyPropertyChangedEventArgs object containing the current state.</param>
        private static void OnBCDrawingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((ImageAssignment)d).OnBCDrawingModeChanged(args);
        }

        /// <summary>
        /// Do something when the drawing mode is changed, namely update the UI to reflect the change.
        /// </summary>
        /// <param name="args">DependencyPropertyChangedEventArgs object from which the current state my be obtained.</param>
        protected virtual void OnBCDrawingModeChanged(DependencyPropertyChangedEventArgs args)
        {
            if (null != args.NewValue)
            {
                UpdateUI();
            }
        }

        #endregion

        #region Section Title Text property.

        public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                         typeof(string),
                                                                                                         typeof(ImageAssignment),
                                                                                                         new PropertyMetadata(""));
        public String SectionTitleText
        {
            get { return (String)GetValue(SectionTitleTextProperty); }
            set { SetValue(SectionTitleTextProperty, value); }
        }

        #endregion

        #endregion

        #region Event Handlers

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handle Cycloid Mode Button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void CycloidBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            BCDrawingMode = eMODE.eCycloid;

            UpdateUI();
        }

        /// <summary>
        /// Handle Lissajous Mode Button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void LissajousBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            BCDrawingMode = eMODE.eLissajous;

            UpdateUI();
        }

        /// <summary>
        /// Handle Radial Scan Button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void RadialScanBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            BCDrawingMode = eMODE.eRadialScan;

            UpdateUI();
        }

        /// <summary>
        /// Handle Spiral Button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void SpiralBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            BCDrawingMode = eMODE.eSpiral;

            UpdateUI();
        }

        #endregion

        /// <summary>
        /// Ensure the UI reflects the current state of the system.
        /// </summary>
        private void UpdateUI()
        {
            switch (BCDrawingMode)
            {
                case eMODE.eCycloid:
                {
                    IACycloidBtn.Background    = activeButtonBkgnd;
                    IALissajousBtn.Background  = inActiveButtonBkgnd;
                    IARadialScanBtn.Background = inActiveButtonBkgnd;
                    IASpiralBtn.Background     = inActiveButtonBkgnd;
                    }
                break;

                case eMODE.eLissajous:
                {
                    IACycloidBtn.Background    = inActiveButtonBkgnd;
                    IALissajousBtn.Background  = activeButtonBkgnd;
                    IARadialScanBtn.Background = inActiveButtonBkgnd;
                    IASpiralBtn.Background     = inActiveButtonBkgnd;
                    }
                break;

                case eMODE.eRadialScan:
                {
                    IACycloidBtn.Background    = inActiveButtonBkgnd;
                    IALissajousBtn.Background  = inActiveButtonBkgnd;
                    IARadialScanBtn.Background = activeButtonBkgnd;
                    IASpiralBtn.Background     = inActiveButtonBkgnd;
                    }
                break;

                case eMODE.eSpiral:
                {
                    IACycloidBtn.Background    = inActiveButtonBkgnd;
                    IALissajousBtn.Background  = inActiveButtonBkgnd;
                    IARadialScanBtn.Background = inActiveButtonBkgnd;
                    IASpiralBtn.Background     = activeButtonBkgnd;
                }
                break;
            }
        }
    }
}
