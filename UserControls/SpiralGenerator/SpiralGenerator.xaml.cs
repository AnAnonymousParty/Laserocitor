// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System.Windows;
using System;
using System.Windows.Controls;
using Laserocitor.Common;
using JoystickControl;


namespace Laserocitor.UserControls.SpiralGenerator
{
    /// <summary>
    /// Interaction logic for SpiralGenerator.xaml.
    /// </summary>
    public partial class SpiralGenerator : UserControl
    {
        /// <summary>
        /// Constructor (default).
        /// </summary>
        public SpiralGenerator()
        {
            InitializeComponent();

            var joystickControl = FindName("SGPerspectiveStick") as Joystick;

            if (null != joystickControl)
            {
                SGPerspectiveStick.Moved += PerspectiveStickMoved;
            }

            UpdateUI();
        }

        #region Dependency Properties

        #region Control Enabled property

        public static readonly DependencyProperty ControlEnabledProperty = DependencyProperty.Register("ControlEnabled",
                                                                                                       typeof(bool),
                                                                                                       typeof(SpiralGenerator),
                                                                                                       new FrameworkPropertyMetadata(false, OnControlEnabledChanged));
        public bool ControlEnabled
        {
            get { return (bool)GetValue(ControlEnabledProperty); }
            set
            {
                SetValue(ControlEnabledProperty, value);

                UpdateUI();
            }
        }
        /// <summary>
        /// Handle the event triggered when the control is disabled/enabled.
        /// </summary>
        /// <param name="d">DependencyObject object presumed to be of this type.</param>
        /// <param name="args">DependencyPropertyChangedEventArgs object containing the current state.</param>
        private static void OnControlEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((SpiralGenerator)d).OnControlEnabledChanged(args);
        }

        /// <summary>
        /// Do something when the control is disabled/enabled, namely update the UI to reflect the change.
        /// </summary>
        /// <param name="args">DependencyPropertyChangedEventArgs object from which the current state my be obtained.</param>
        protected virtual void OnControlEnabledChanged(DependencyPropertyChangedEventArgs args)
        {
            if (null != args.NewValue)
            {
                UpdateUI();
            }
        }

        #endregion

        #region Perspective Direction property.

        public static readonly DependencyProperty SGPersDirectionProperty = DependencyProperty.Register("SGPersDirection",
                                                                                                        typeof(eDIRECTION),
                                                                                                        typeof(SpiralGenerator),
                                                                                                        new PropertyMetadata(eDIRECTION.eCW));
        public eDIRECTION SGPersDirection
        {
            get { return (eDIRECTION)GetValue(SGPersDirectionProperty); }
            set { SetValue(SGPersDirectionProperty, value); }
        }

        #endregion

        #region Perspective Modulation Enable property.

        public static readonly DependencyProperty SGPersModEnableProperty = DependencyProperty.Register("SGPersModEnable",
                                                                                                        typeof(bool),
                                                                                                        typeof(SpiralGenerator),
                                                                                                        new PropertyMetadata(false));
        public bool SGPersModEnable
        {
            get { return (bool)GetValue(SGPersModEnableProperty); }
            set { SetValue(SGPersModEnableProperty, value); }
        }

        #endregion

        #region Perspective Modulation Frequency property.

        public static readonly DependencyProperty SGPersModFrequencyProperty = DependencyProperty.Register("SGPersModFrequency",
                                                                                                           typeof(double),
                                                                                                           typeof(SpiralGenerator),
                                                                                                           new PropertyMetadata(5.0));
        public double SGPersModFrequency
        {
            get { return (double)GetValue(SGPersModFrequencyProperty); }
            set { SetValue(SGPersModFrequencyProperty, value); }
        }

        #endregion

        #region Perspective Modulation Level property.

        public static readonly DependencyProperty SGPersModLevelProperty = DependencyProperty.Register("SGPersModLevel",
                                                                                                       typeof(double),
                                                                                                       typeof(SpiralGenerator),
                                                                                                       new PropertyMetadata(10.0));
        public double SGPersModLevel
        {
            get { return (double)GetValue(SGPersModLevelProperty); }
            set { SetValue(SGPersModLevelProperty, value); }
        }

        #endregion

        #region Perspective X property.

        public static readonly DependencyProperty SGPersJSXPosProperty = DependencyProperty.Register("SGPersJSXPos",
                                                                                                     typeof(double),
                                                                                                     typeof(SpiralGenerator),
                                                                                                     new PropertyMetadata(0.0));
        public double SGPersJSXPos
        {
            get { return (double)GetValue(SGPersJSXPosProperty); }
            set { SetValue(SGPersJSXPosProperty, value); }
        }

        #endregion

        #region Perspective Y property.

        public static readonly DependencyProperty SGPersJSYPosProperty = DependencyProperty.Register("SGPersJSYPos",
                                                                                                     typeof(double),
                                                                                                     typeof(SpiralGenerator),
                                                                                                     new PropertyMetadata(0.0));
        public double SGPersJSYPos
        {
            get { return (double)GetValue(SGPersJSYPosProperty); }
            set { SetValue(SGPersJSYPosProperty, value); }
        }

        #endregion

        #region Perspective Preset X property.

        public static readonly DependencyProperty SGPersPresetXProperty = DependencyProperty.Register("SGPersPresetX",
                                                                                                      typeof(double),
                                                                                                      typeof(SpiralGenerator),
                                                                                                      new PropertyMetadata(0.0, OnSGPersPresetXChanged));
        public double SGPersPresetX
        {
            get { return (double)GetValue(SGPersPresetXProperty); }
            set { SetValue(SGPersPresetXProperty, value); }
        }

        /// <summary>
        /// Handle the event triggered when the Perspective Preset X value is changed.
        /// </summary>
        /// <param name="d">DependencyObject object presumed to be of this type.</param>
        /// <param name="args">DependencyPropertyChangedEventArgs object containing the current state.</param>
        private static void OnSGPersPresetXChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((SpiralGenerator)d).OnSGPersPresetXChanged(args);
        }

        /// <summary>
        /// Do something when the Perspective Preset X value is changed, namely update the value.
        /// </summary>
        /// <param name="args">DependencyPropertyChangedEventArgs object from which the current state my be obtained.</param>
        protected virtual void OnSGPersPresetXChanged(DependencyPropertyChangedEventArgs args)
        {
            if (null != args.NewValue)
            {
                SGPersPresetX = (Double)args.NewValue;
            }
        }

        #endregion

        #region Perspective Preset Y property.

        public static readonly DependencyProperty SGPersPresetYProperty = DependencyProperty.Register("SGPersPresetY",
                                                                                                      typeof(double),
                                                                                                      typeof(SpiralGenerator),
                                                                                                      new PropertyMetadata(0.0, OnSGPersPresetYChanged));
        public double SGPersPresetY
        {
            get { return (double)GetValue(SGPersPresetYProperty); }
            set { SetValue(SGPersPresetYProperty, value); }
        }

        /// <summary>
        /// Handle the event triggered when the Perspective Preset X value is changed.
        /// </summary>
        /// <param name="d">DependencyObject object presumed to be of this type.</param>
        /// <param name="args">DependencyPropertyChangedEventArgs object containing the current state.</param>
        private static void OnSGPersPresetYChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((SpiralGenerator)d).OnSGPersPresetYChanged(args);
        }

        /// <summary>
        /// Do something when the Perspective Preset X value is changed, namely update the value.
        /// </summary>
        /// <param name="args">DependencyPropertyChangedEventArgs object from which the current state my be obtained.</param>
        protected virtual void OnSGPersPresetYChanged(DependencyPropertyChangedEventArgs args)
        {
            if (null != args.NewValue)
            {
                SGPersPresetY = (Double)args.NewValue;
            }
        }

        #endregion

        #region Ramp Gain property.

        public static readonly DependencyProperty SGRampGainProperty = DependencyProperty.Register("SGRampGain",
                                                                                                   typeof(double),
                                                                                                   typeof(SpiralGenerator),
                                                                                                   new PropertyMetadata(50.0));
        public double SGRampGain
        {
            get { return (double)GetValue(SGRampGainProperty); }
            set { SetValue(SGRampGainProperty, value); }
        }

        #endregion

        #region Ramp Offset property.

        public static readonly DependencyProperty SGRampOffsetProperty = DependencyProperty.Register("SGRampOffset",
                                                                                                     typeof(double),
                                                                                                     typeof(SpiralGenerator),
                                                                                                     new PropertyMetadata(50.0));
        public double SGRampOffset
        {
            get { return (double)GetValue(SGRampOffsetProperty); }
            set { SetValue(SGRampOffsetProperty, value); }
        }

        #endregion

        #region Spiral Direction property.

        public static readonly DependencyProperty SGDirectionProperty = DependencyProperty.Register("SGDirection",
                                                                                                    typeof(eDIRECTION),
                                                                                                    typeof(SpiralGenerator),
                                                                                                    new PropertyMetadata(eDIRECTION.eCW));
        public eDIRECTION SGDirection
        {
            get { return (eDIRECTION)GetValue(SGDirectionProperty); }
            set { SetValue(SGDirectionProperty, value); }
        }

        #endregion

        #region Spiral Size property.

        public static readonly DependencyProperty SGSizeProperty = DependencyProperty.Register("SGSize",
                                                                                               typeof(double),
                                                                                               typeof(SpiralGenerator),
                                                                                               new PropertyMetadata(50.0));
        public double SGSize
        {
            get { return (double)GetValue(SGSizeProperty); }
            set { SetValue(SGSizeProperty, value); }
        }

        #endregion

        #region Ramp Frequency property.

        public static readonly DependencyProperty SGRampFrequencyProperty = DependencyProperty.Register("SGRampFrequency",
                                                                                                        typeof(double),
                                                                                                        typeof(SpiralGenerator),
                                                                                                        new PropertyMetadata(0.0));
        public double SGRampFrequency
        {
            get { return (double)GetValue(SGRampFrequencyProperty); }
            set { SetValue(SGRampFrequencyProperty, value); }
        }

        #endregion

        #region Section Title Text property.

        public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                         typeof(string),
                                                                                                         typeof(SpiralGenerator),
                                                                                                         new PropertyMetadata(""));
        public String SectionTitleText
        {
            get { return (String)GetValue(SectionTitleTextProperty); }
            set { SetValue(SectionTitleTextProperty, value); }
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle Perspective joystick moved event.
        /// </summary>
        /// <param name="senderUnused">Presumed ToggleSwitch object indicating the desired state (unused).</param>  
        /// <param name="args">RoutedEventArgs object to provide arguments.</param>
        private void PerspectiveStickMoved(object sender, JoystickEventArgs args)
        {
            SGPersJSXPos = args.X * 5;
            SGPersJSYPos = -args.Y * 5;
        }

        /// <summary>
        /// Handle Spiral Direction switch clicked event.
        /// </summary>
        /// <param name="sender">Presumed ToggleSwitch object indicating the desired state.</param>
        /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
        private void SGDirTSClkd(object sender, RoutedEventArgs eUnused)
        {
            ToggleSwitchControl.ToggleSwitchControl tsc = sender as ToggleSwitchControl.ToggleSwitchControl;

            if (null != tsc)
            {
                SGDirection = (true == tsc.SwitchOn ? eDIRECTION.eCCW : eDIRECTION.eCW);

                UpdateUI();
            }
        }

        /// <summary>
        /// Handle Perspective Modulation Direction switch clicked event.
        /// </summary>
        /// <param name="sender">Presumed ToggleSwitch object indicating the desired state.</param>
        /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
        private void SGPersModDirTSClkd(object sender, RoutedEventArgs eUnused)
        {
            ToggleSwitchControl.ToggleSwitchControl tsc = sender as ToggleSwitchControl.ToggleSwitchControl;

            if (null != tsc)
            {
                SGPersDirection = (true == tsc.SwitchOn ? eDIRECTION.eCCW : eDIRECTION.eCW);

                UpdateUI();
            }
        }

        /// <summary>
        /// Handle Perspective Modulation Enable switch clicked event.
        /// </summary>
        /// <param name="sender">Presumed ToggleSwitch object indicating the desired state.</param>
        /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
        private void SGPersModEnableTSClkd(object sender, RoutedEventArgs eUnused)
        {
            ToggleSwitchControl.ToggleSwitchControl tsc = sender as ToggleSwitchControl.ToggleSwitchControl;

            if (null != tsc)
            {
                SGPersModEnable  = tsc.SwitchOn;

                UpdateUI();
            }
        }

        #endregion

        /// <summary>
        /// Ensure the UI reflects the current state of the system.
        /// </summary>
        private void UpdateUI()
        {
            SGDirTS.ControlEnabled            = ControlEnabled;
            SGPerspectiveStick.ControlEnabled = ControlEnabled;
            SGRampFrequencyRC.ControlEnabled  = ControlEnabled;
            SGRampGainRC.ControlEnabled       = ControlEnabled;
            SGRampOffsetRC.ControlEnabled     = ControlEnabled;
            SGSizeRC.ControlEnabled           = ControlEnabled;

            if (false == ControlEnabled)
            {
                SGPersModDirTS.ControlEnabled       = false;
                SGPersModFrequencyRC.ControlEnabled = false;
                SGPersModLevelRC.ControlEnabled     = false;
                SGPersModEnableTS.ControlEnabled    = false;
            }
            else
            {
                SGPersModDirTS.ControlEnabled       = SGPersModEnable;
                SGPersModFrequencyRC.ControlEnabled = SGPersModEnable;
                SGPersModLevelRC.ControlEnabled     = SGPersModEnable;
                SGPersModEnableTS.ControlEnabled    = true;
            }
        }
    }
}
