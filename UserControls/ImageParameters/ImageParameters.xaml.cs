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
using System.Windows;

using JoystickControl;
using Laserocitor.Common;
using Laserocitor.Utils.Diagnostics;

namespace Laserocitor.UserControls.ImageParameters
{
    /// <summary>
    /// Interaction logic for ImageParameters.xaml.
    /// </summary>
    public partial class ImageParameters
    {
        /// <summary>
        /// Constructor (default).
        /// </summary>
        public ImageParameters()
        {
            if (false == DesignMode)
            {
                // ReSharper disable once PossibleNullReferenceException
                Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                    LogMessage.LogMessageType.eSubCall,
                    GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name));
            }

            InitializeComponent();

            var joystickControl = FindName("TranslationStick") as Joystick;

            if (null != joystickControl)
            {
                TranslationStick.Moved += TranslationStickMoved;
            }

            UpdateUI();

            if (false == DesignMode)
            {
                // ReSharper disable once PossibleNullReferenceException
                Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                    LogMessage.LogMessageType.eSubExit,
                    GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Attaches events and names to compiled content.
        /// </summary>
        /// <param name="connectionIdUnused">An identifier token to distinguish calls.</param>
        /// <param name="targetUnused">The target to connect events and names to.</param>
        public void Connect(int connectionIdUnused, object targetUnused)
        {
            throw new NotImplementedException();
        }

        #region Public Properties

        /// <summary>
        /// Indicate whether the program is running in design mode.
        /// Usage of this property is mainly to avoid warnings in the XAML designer.
        /// </summary>
        public bool DesignMode
        {
            get
            {
                return (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            }
        }

        #endregion

        #region Dependency Properties

        #region Aspect Ratio property.

        public static readonly DependencyProperty IPAspectRatioProperty = DependencyProperty.Register("IPAspectRatio",
                                                                                                      typeof(int),
                                                                                                      typeof(ImageParameters),
                                                                                                      new PropertyMetadata(0));
        public int IPAspectRatio
        {
            get { return (int)GetValue(IPAspectRatioProperty); }
            set { SetValue(IPAspectRatioProperty, value); }
        }

        #endregion

        #region Auto Position Enable/Disable property.

        public static readonly DependencyProperty IPAutoPositionProperty = DependencyProperty.Register("IPAutoPosition",
                                                                                                       typeof(bool),
                                                                                                       typeof(ImageParameters), 
                                                                                                       new PropertyMetadata(false));
        public bool IPAutoPosition
        {
            get { return (bool)GetValue(IPAutoPositionProperty); }
            set { SetValue(IPAutoPositionProperty, value); }
        }

        #endregion

        #region Auto Position Direction property.

        public static readonly DependencyProperty IPAutoPositionDirProperty = DependencyProperty.Register("IPAutoPositionDir",
                                                                                                          typeof(eDIRECTION),
                                                                                                          typeof(ImageParameters),
                                                                                                          new PropertyMetadata(eDIRECTION.eCW));
        public eDIRECTION IPAutoPositionDir
        {
            get { return (eDIRECTION)GetValue(IPAutoPositionDirProperty); }
            set { SetValue(IPAutoPositionDirProperty, value); }
        }

        #endregion

        #region Auto Position Level property.

        public static readonly DependencyProperty IPAutoPosLevelProperty = DependencyProperty.Register("IPAutoPosLevel",
                                                                                                       typeof(int),
                                                                                                       typeof(ImageParameters),
                                                                                                       new PropertyMetadata(0));
        public int IPAutoPosLevel
        {
            get { return (int)GetValue(IPAutoPosLevelProperty); }
            set { SetValue(IPAutoPosLevelProperty, value); }
        }

        #endregion

        #region Auto Position Rate property.

        public static readonly DependencyProperty IPAutoPosRateProperty = DependencyProperty.Register("IPAutoPosRate",
                                                                                                      typeof(int),
                                                                                                      typeof(ImageParameters),
                                                                                                      new PropertyMetadata(0));
        public int IPAutoPosRate
        {
            get { return (int)GetValue(IPAutoPosRateProperty); }
            set { SetValue(IPAutoPosRateProperty, value); }
        }

        #endregion

        #region Auto Orbit Coupling property.

        public static readonly DependencyProperty IPAutoPosOrbitProperty = DependencyProperty.Register("IPAutoPosOrbit",
                                                                                                       typeof(bool),
                                                                                                       typeof(ImageParameters),
                                                                                                       new PropertyMetadata(false));
        public bool IPAutoPosOrbit
        {
            get { return (bool)GetValue(IPAutoPosOrbitProperty); }
            set { SetValue(IPAutoPosOrbitProperty, value); }
        }

        #endregion

        #region Decay property.

        public static readonly DependencyProperty IPDecayProperty = DependencyProperty.Register("IPDecay",
                                                                                                typeof(int),
                                                                                                typeof(ImageParameters),
                                                                                                new PropertyMetadata(0));
        public int IPDecay
        {
            get { return (int)GetValue(IPDecayProperty); }
            set { SetValue(IPDecayProperty, value); }
        }

        #endregion

        #region Image Size property.

        public static readonly DependencyProperty IPImageSizeProperty = DependencyProperty.Register("IPImageSize",
                                                                                                    typeof(int),
                                                                                                    typeof(ImageParameters),
                                                                                                    new PropertyMetadata(0));
        public int IPImageSize
        {
            get { return (int)GetValue(IPImageSizeProperty); }
            set { SetValue(IPImageSizeProperty, value); }
        }

        #endregion

        #region Position Joystick Auto-center property.

        public static readonly DependencyProperty JSAutoCenterProperty = DependencyProperty.Register("JSAutoCenter", 
                                                                                                     typeof(bool),
                                                                                                     typeof(ImageParameters), 
                                                                                                     new PropertyMetadata(false));
        public bool JSAutoCenter
        {
            get { return (bool)GetValue(JSAutoCenterProperty); }
            set { SetValue(JSAutoCenterProperty, value); }
        }

        #endregion

        #region Joystick X position property.

        public static readonly DependencyProperty JSXPosProperty = DependencyProperty.Register("JSXPos", 
                                                                                               typeof(Double),
                                                                                               typeof(ImageParameters), 
                                                                                               new PropertyMetadata(0.0));
        public Double JSXPos
        {
            get { return (Double)GetValue(JSXPosProperty); }
            set { SetValue(JSXPosProperty, value); }
        }

        #endregion

        #region Joystick X position display property.

        public static readonly DependencyProperty JSXPosDispProperty = DependencyProperty.Register("JSXPosDisp",
                                                                                                   typeof(String),
                                                                                                   typeof(ImageParameters),
                                                                                                   new PropertyMetadata());
        public String JSXPosDisp
        {
            get { return (String)GetValue(JSXPosDispProperty); }
            set { SetValue(JSXPosDispProperty, value); }
        }

        #endregion

        #region Joystick Y position property.

        public static readonly DependencyProperty JSYPosProperty = DependencyProperty.Register("JSYPos", 
                                                                                               typeof(Double),
                                                                                               typeof(ImageParameters), 
                                                                                               new PropertyMetadata(0.0));
        public Double JSYPos
        {
            get { return (Double)GetValue(JSYPosProperty); }
            set { SetValue(JSYPosProperty, value); }
        }

        #endregion

        #region Joystick Y position display property.

        public static readonly DependencyProperty JSYPosDispProperty = DependencyProperty.Register("JSYPosDisp", 
                                                                                                   typeof(string),
                                                                                                   typeof(ImageParameters), 
                                                                                                   new PropertyMetadata(""));
        public String JSYPosDisp
        {
            get { return (String)GetValue(JSYPosDispProperty); }
            set { SetValue(JSYPosDispProperty, value); }
        }

        #endregion

        #region Orbit property.

        public static readonly DependencyProperty IPOrbitProperty = DependencyProperty.Register("IPOrbit",
                                                                                                typeof(int),
                                                                                                typeof(ImageParameters),
                                                                                                new PropertyMetadata(0));
        public int IPOrbit
        {
            get { return (int)GetValue(IPOrbitProperty); }
            set { SetValue(IPOrbitProperty, value); }
        }

        #endregion

        #region Position Preset X position property.

        public static readonly DependencyProperty PosPresetXProperty = DependencyProperty.Register("PosPresetX",
                                                                                                    typeof(Double),
                                                                                                    typeof(ImageParameters),
                                                                                                    new PropertyMetadata(0.0));
        public Double PosPresetX
        {
            get { return (Double)GetValue(PosPresetXProperty); }
            set { SetValue(PosPresetXProperty, value); }
        }

        #endregion

        #region Perspective Preset Y position property.

        public static readonly DependencyProperty PosPresetYProperty = DependencyProperty.Register("PosPresetY",
                                                                                                    typeof(Double),
                                                                                                    typeof(ImageParameters),
                                                                                                    new PropertyMetadata(0.0));
        public Double PosPresetY
        {
            get { return (Double)GetValue(PosPresetYProperty); }
            set { SetValue(PosPresetYProperty, value); }
        }

        #endregion

        #region Section Title Text property.

        public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                         typeof(string),
                                                                                                         typeof(ImageParameters),
                                                                                                         new PropertyMetadata(""));
        public String SectionTitleText
        {
            get { return (String)GetValue(SectionTitleTextProperty); }
            set { SetValue(SectionTitleTextProperty, value); }
        }

        #endregion

        #region X-Axis Rotation property.

        public static readonly DependencyProperty XRotationProperty = DependencyProperty.Register("XRotation",
                                                                                                  typeof(int),
                                                                                                  typeof(ImageParameters),
                                                                                                  new PropertyMetadata(0));
        public int XRotation
        {
            get { return (int)GetValue(XRotationProperty); }
            set { SetValue(XRotationProperty, value); }
        }

        #endregion

        #region Y-Axis Rotation property.

        public static readonly DependencyProperty YRotationProperty = DependencyProperty.Register("YRotation",
                                                                                                  typeof(int),
                                                                                                  typeof(ImageParameters),
                                                                                                  new PropertyMetadata(0));
        public int YRotation
        {
            get { return (int)GetValue(YRotationProperty); }
            set { SetValue(YRotationProperty, value); }
        }

        #endregion

        #region Z-Axis Rotation property.

        public static readonly DependencyProperty ZRotationProperty = DependencyProperty.Register("ZRotation",
                                                                                                  typeof(int),
                                                                                                  typeof(ImageParameters),
                                                                                                  new PropertyMetadata(0));
        public int ZRotation
        {
            get { return (int)GetValue(ZRotationProperty); }
            set { SetValue(ZRotationProperty, value); }
        }

        #endregion

        #region Image Size property.

        public static readonly DependencyProperty IPZoomProperty = DependencyProperty.Register("IPZoom",
                                                                                               typeof(int),
                                                                                               typeof(ImageParameters),
                                                                                               new PropertyMetadata(0));
        public int IPZoom
        {
            get { return (int)GetValue(IPZoomProperty); }
            set { SetValue(IPZoomProperty, value); }
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle joystick being moved.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="args">RoutedEventArgs object to contain any arguments.</param>
        private void TranslationStickMoved(object senderUnused, JoystickEventArgs args)
        {
            JSXPos = args.X;
            JSYPos = args.Y;
        }

        /// <summary>
        /// Handle Auto Position Direction Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void AutoPosDirTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            IPAutoPositionDir = (true == AutoPosDirTS.SwitchOn ? eDIRECTION.eCW : eDIRECTION.eCCW);
        }

        /// <summary>
        /// Handle Auto Position Orbit Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void AutoPosOrbitTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            IPAutoPosOrbit = AutoPosOrbitTS.SwitchOn;
        }

        /// <summary>
        /// Handle Auto Position Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void AutoPosTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            IPAutoPosition = AutoPosTS.SwitchOn;

            UpdateUI();
        }

        #endregion

        /// <summary>
        /// Ensure the UI reflects the current state of the system.
        /// </summary>
        private void UpdateUI()
        {
            if (true == IPAutoPosition)
            {
                AutoPosDirTS.ControlEnabled   = true;
                AutoPosLevelRC.ControlEnabled = true;
                AutoPosOrbitTS.ControlEnabled = true;
                AutoPosRateRC.ControlEnabled  = true;

                return;
            }

            AutoPosDirTS.ControlEnabled   = false;
            AutoPosLevelRC.ControlEnabled = false;
            AutoPosOrbitTS.ControlEnabled = false;
            AutoPosRateRC.ControlEnabled  = false;            
        }
    }
}
