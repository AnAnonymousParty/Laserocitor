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

using Laserocitor.Common;

namespace Laserocitor.UserControls.LissajousGenerator
{
    /// <summary>
    /// Interaction logic for LissajousGenerator.xaml.
    /// </summary>
    public partial class LissajousGenerator
    {
        /// <summary>
        /// Constructor (default).
        /// </summary>
        public LissajousGenerator()
        {
            InitializeComponent();

            UpdateUI();
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

        #region Dependency properties

        #region Control Enabled property

        public static readonly DependencyProperty ControlEnabledProperty = DependencyProperty.Register("ControlEnabled",
                                                                                                       typeof(bool),
                                                                                                       typeof(LissajousGenerator),
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
         ((LissajousGenerator)d).OnControlEnabledChanged(args);
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

        #region Section Title Text property.

        public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                         typeof(string),
                                                                                                         typeof(LissajousGenerator),
                                                                                                         new PropertyMetadata(""));
        public String SectionTitleText
        {
            get { return (String)GetValue(SectionTitleTextProperty); }
            set { SetValue(SectionTitleTextProperty, value); }
        }

        #endregion

        #region Selected X Modulation Oscillator waveform property

        public static readonly DependencyProperty XModWaveformProperty = DependencyProperty.Register("XModWaveform",
                                                                                                     typeof(eWAVEFORM),
                                                                                                     typeof(LissajousGenerator),
                                                                                                     new FrameworkPropertyMetadata(eWAVEFORM.eSin));
        public eWAVEFORM XModWaveform
        {
            get { return (eWAVEFORM)GetValue(XModWaveformProperty); }
            set
            {
                SetValue(XModWaveformProperty, value);
            }
        }

        #endregion

        #region Selected Y Modulation Oscillator waveform property

        public static readonly DependencyProperty YModWaveformProperty = DependencyProperty.Register("YModWaveform",
                                                                                                     typeof(eWAVEFORM),
                                                                                                     typeof(LissajousGenerator),
                                                                                                     new FrameworkPropertyMetadata(eWAVEFORM.eSin));
        public eWAVEFORM YModWaveform
        {
            get { return (eWAVEFORM)GetValue(YModWaveformProperty); }
            set
            {
                SetValue(YModWaveformProperty, value);
            }
        }

        #endregion

        #region Selected X Oscillator waveform property

        public static readonly DependencyProperty XWaveformProperty = DependencyProperty.Register("XWaveform",
                                                                                                  typeof(eWAVEFORM),
                                                                                                  typeof(LissajousGenerator),
                                                                                                  new FrameworkPropertyMetadata(eWAVEFORM.eSin));
        public eWAVEFORM XWaveform
        {
            get { return (eWAVEFORM)GetValue(XWaveformProperty); }
            set
            {
                SetValue(XWaveformProperty, value);
            }
        }

        #endregion

        #region Selected Y Oscillator waveform property

        public static readonly DependencyProperty YWaveformProperty = DependencyProperty.Register("YWaveform",
                                                                                                  typeof(eWAVEFORM),
                                                                                                  typeof(LissajousGenerator),
                                                                                                  new FrameworkPropertyMetadata(eWAVEFORM.eSin));
        public eWAVEFORM YWaveform
        {
            get { return (eWAVEFORM)GetValue(YWaveformProperty); }
            set
            {
                SetValue(YWaveformProperty, value);
            }
        }

        #endregion

        #region X Modulation Enabled property

        public static readonly DependencyProperty XModEnabledProperty = DependencyProperty.Register("XModEnabled",
                                                                                                    typeof(bool),
                                                                                                    typeof(LissajousGenerator),
                                                                                                    new FrameworkPropertyMetadata(false));
        public bool XModEnabled
        {
            get { return (bool)GetValue(XModEnabledProperty); }
            set
            {
                SetValue(XModEnabledProperty, value);
            }
        }

        #endregion

        #region X Modulation Gain property

        public static readonly DependencyProperty XModGainProperty = DependencyProperty.Register("XModGain",
                                                                                                 typeof(double),
                                                                                                 typeof(LissajousGenerator),
                                                                                                 new FrameworkPropertyMetadata(10.0));
        public double XModGain
        {
            get { return (double)GetValue(XModGainProperty); }
            set
            {
                SetValue(XModGainProperty, value);
            }
        }

        #endregion

        #region X Modulation Offset property

        public static readonly DependencyProperty XModOffsetProperty = DependencyProperty.Register("XModOffset",
                                                                                                   typeof(double),
                                                                                                   typeof(LissajousGenerator),
                                                                                                   new FrameworkPropertyMetadata(0.0));
        public double XModOffset
        {
            get { return (double)GetValue(XModOffsetProperty); }
            set
            {
                SetValue(XModOffsetProperty, value);
            }
        }

        #endregion

        #region X Modulation Oscillator Level property

        public static readonly DependencyProperty XModOscLevelProperty = DependencyProperty.Register("XModOscLevel",
                                                                                                     typeof(double),
                                                                                                     typeof(LissajousGenerator),
                                                                                                     new FrameworkPropertyMetadata(10.0));
        public double XModOscLevel
        {
            get { return (double)GetValue(XModOscLevelProperty); }
            set
            {
                SetValue(XModOscLevelProperty, value);
            }
        }

        #endregion

        #region X Modulation Oscillator Rate property

        public static readonly DependencyProperty XModOscRateProperty = DependencyProperty.Register("XModOscRate",
                                                                                                    typeof(double),
                                                                                                    typeof(LissajousGenerator),
                                                                                                    new FrameworkPropertyMetadata(10.0));
        public double XModOscRate
        {
            get { return (double)GetValue(XModOscRateProperty); }
            set
            {
                SetValue(XModOscRateProperty, value);
            }
        }

        #endregion

        #region X Oscillator Level property

        public static readonly DependencyProperty XOscLevelProperty = DependencyProperty.Register("XOscLevel",
                                                                                                  typeof(double),
                                                                                                  typeof(LissajousGenerator),
                                                                                                  new FrameworkPropertyMetadata(10.0));
        public double XOscLevel
        {
            get { return (double)GetValue(XOscLevelProperty); }
            set
            {
                SetValue(XOscLevelProperty, value);
            }
        }

        #endregion

        #region X Oscillator Rate property

        public static readonly DependencyProperty XOscRateProperty = DependencyProperty.Register("XOscRate",
                                                                                                 typeof(double),
                                                                                                 typeof(LissajousGenerator),
                                                                                                 new FrameworkPropertyMetadata(10.0));
        public double XOscRate
        {
            get { return (double)GetValue(XOscRateProperty); }
            set
            {
                SetValue(XOscRateProperty, value);
            }
        }

        #endregion

        #region Y Modulation Enabled property

        public static readonly DependencyProperty YModEnabledProperty = DependencyProperty.Register("YModEnabled",
                                                                                                    typeof(bool),
                                                                                                    typeof(LissajousGenerator),
                                                                                                    new FrameworkPropertyMetadata(false));
        public bool YModEnabled
        {
            get { return (bool)GetValue(YModEnabledProperty); }
            set
            {
                SetValue(YModEnabledProperty, value);
            }
        }

        #endregion

        #region Y Modulation Gain property

        public static readonly DependencyProperty YModGainProperty = DependencyProperty.Register("YModGain",
                                                                                                 typeof(double),
                                                                                                 typeof(LissajousGenerator),
                                                                                                 new FrameworkPropertyMetadata(10.0));
        public double YModGain
        {
            get { return (double)GetValue(YModGainProperty); }
            set
            {
                SetValue(YModGainProperty, value);
            }
        }

        #endregion

        #region Y Modulation Offset property

        public static readonly DependencyProperty YModOffsetProperty = DependencyProperty.Register("YModOffset",
                                                                                                   typeof(double),
                                                                                                   typeof(LissajousGenerator),
                                                                                                   new FrameworkPropertyMetadata(0.0));
        public double YModOffset
        {
            get { return (double)GetValue(XModOffsetProperty); }
            set
            {
                SetValue(YModOffsetProperty, value);
            }
        }

        #endregion

        #region Y Modulation Oscillator Level property

        public static readonly DependencyProperty YModOscLevelProperty = DependencyProperty.Register("YModOscLevel",
                                                                                                     typeof(double),
                                                                                                     typeof(LissajousGenerator),
                                                                                                     new FrameworkPropertyMetadata(10.0));
        public double YModOscLevel
        {
            get { return (double)GetValue(YModOscLevelProperty); }
            set
            {
                SetValue(YModOscLevelProperty, value);
            }
        }

        #endregion

        #region Y Modulation Oscillator Rate property

        public static readonly DependencyProperty YModOscRateProperty = DependencyProperty.Register("YModOscRate",
                                                                                                    typeof(double),
                                                                                                    typeof(LissajousGenerator),
                                                                                                    new FrameworkPropertyMetadata(10.0));
        public double YModOscRate
        {
            get { return (double)GetValue(YModOscRateProperty); }
            set
            {
                SetValue(YModOscRateProperty, value);
            }
        }

        #endregion

        #region Y Oscillator Level property

        public static readonly DependencyProperty YOscLevelProperty = DependencyProperty.Register("YOscLevel",
                                                                                                  typeof(double),
                                                                                                  typeof(LissajousGenerator),
                                                                                                  new FrameworkPropertyMetadata(10.0));
        public double YOscLevel
        {
            get { return (double)GetValue(YOscLevelProperty); }
            set
            {
                SetValue(YOscLevelProperty, value);
            }
        }

        #endregion

        #region Y Oscillator Rate property

        public static readonly DependencyProperty YOscRateProperty = DependencyProperty.Register("YOscRate",
                                                                                                 typeof(double),
                                                                                                 typeof(LissajousGenerator),
                                                                                                 new FrameworkPropertyMetadata(10.0));
        public double YOscRate
        {
            get { return (double)GetValue(YOscRateProperty); }
            set
            {
                SetValue(YOscRateProperty, value);
            }
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle BOTH Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void BothTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            XModEnabled = BothEnabledTS.SwitchOn;
            YModEnabled = BothEnabledTS.SwitchOn;

            XModEnabledTS.SwitchOn = BothEnabledTS.SwitchOn;
            YModEnabledTS.SwitchOn = BothEnabledTS.SwitchOn;

            UpdateUI();
        }

        /// <summary>
        /// Handle X Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void XTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            XModEnabled = XModEnabledTS.SwitchOn;

            UpdateUI();
        }

        /// <summary>
        /// Handle Y Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void YTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            YModEnabled = YModEnabledTS.SwitchOn;

            UpdateUI();
        }

        /// <summary>
        /// Handle X Modulator Oscillator Waveform Selected event.
        /// </summary>
        /// <param name="sender">Presumed WaveformSelector object containing the selected waveform.</param>
        /// <param name="eUnused"></param>
        private void OnXModWaveformSelected(object sender, RoutedEventArgs eUnused)
        {
         WaveformSelector.WaveformSelector ws = sender as WaveformSelector.WaveformSelector;

         if (null != ws)
         {
          XModWaveform = ws.Waveform;
         }
        }

        /// <summary>
        /// Handle X Oscillator Waveform Selected event.
        /// </summary>
        /// <param name="sender">Presumed WaveformSelector object containing the selected waveform.</param>
        /// <param name="eUnused"></param>
        private void OnXWaveformSelected(object sender, RoutedEventArgs eUnused)
        {
         WaveformSelector.WaveformSelector ws = sender as WaveformSelector.WaveformSelector;

         if (null != ws)
         {
          XWaveform = ws.Waveform;
         }
        }

        /// <summary>
        /// Handle Y Modulator Oscillator Waveform Selected event.
        /// </summary>
        /// <param name="sender">Presumed WaveformSelector object containing the selected waveform.</param>
        /// <param name="eUnused"></param>
        private void OnYModWaveformSelected(object sender, RoutedEventArgs eUnused)
        {
         WaveformSelector.WaveformSelector ws = sender as WaveformSelector.WaveformSelector;

         if (null != ws)
         {
          YModWaveform = ws.Waveform;
         }
        }

        /// <summary>
        /// Handle YX Oscillator Waveform Selected event.
        /// </summary>
        /// <param name="sender">Presumed WaveformSelector object containing the selected waveform.</param>
        /// <param name="eUnused"></param>
        private void OnYWaveformSelected(object sender, RoutedEventArgs eUnused)
        {
         WaveformSelector.WaveformSelector ws = sender as WaveformSelector.WaveformSelector;

         if (null != ws)
         {
          YWaveform = ws.Waveform;
         }
        }

        #endregion

        /// <summary>
        /// Ensure the UI reflects the current state of the system.
        /// </summary>
        private void UpdateUI()
        {
         BothEnabledTS.ControlEnabled = ControlEnabled;

         WS_XOsc.ControlEnabled = ControlEnabled;
         WS_YOsc.ControlEnabled = ControlEnabled;

         XModEnabledTS.ControlEnabled  = ControlEnabled;
         XModGainRC.ControlEnabled     = ControlEnabled;
         XModOffsetRC.ControlEnabled   = ControlEnabled;
         XModOscLevelRC.ControlEnabled = ControlEnabled;
         XModOscRateRC.ControlEnabled  = ControlEnabled;
         XOscRateRC.ControlEnabled     = ControlEnabled;
         XOscLevelRC.ControlEnabled    = ControlEnabled;
         XOscRateRC.ControlEnabled     = ControlEnabled;

         YModGainRC.ControlEnabled     = ControlEnabled;
         YModEnabledTS.ControlEnabled  = ControlEnabled;
         YModOffsetRC.ControlEnabled   = ControlEnabled;
         YModOscLevelRC.ControlEnabled = ControlEnabled;
         YModOscRateRC.ControlEnabled  = ControlEnabled;
         YOscLevelRC.ControlEnabled    = ControlEnabled;
         YOscRateRC.ControlEnabled     = ControlEnabled;

         if (true == XModEnabledTS.SwitchOn)
         {
          WS_XMod.ControlEnabled        = ControlEnabled;
          XModGainRC.ControlEnabled     = ControlEnabled;
          XModOffsetRC.ControlEnabled   = ControlEnabled;
          XModOscLevelRC.ControlEnabled = ControlEnabled;
          XModOscRateRC.ControlEnabled  = ControlEnabled;          
         }
         else
         {
          WS_XMod.ControlEnabled        = false;
          XModGainRC.ControlEnabled     = false;
          XModOffsetRC.ControlEnabled   = false;
          XModOscLevelRC.ControlEnabled = false;
          XModOscRateRC.ControlEnabled  = false;
         }

         if (true == YModEnabledTS.SwitchOn)
         {
          WS_YMod.ControlEnabled        = ControlEnabled;
          YModGainRC.ControlEnabled     = ControlEnabled;
          YModOffsetRC.ControlEnabled   = ControlEnabled;
          YModOscLevelRC.ControlEnabled = ControlEnabled;
          YModOscRateRC.ControlEnabled  = ControlEnabled;          
         }
         else
         {
          WS_YMod.ControlEnabled        = false;
          YModGainRC.ControlEnabled     = false;
          YModOffsetRC.ControlEnabled   = false;
          YModOscLevelRC.ControlEnabled = false;
          YModOscRateRC.ControlEnabled  = false;
         }
        }
    }
}
