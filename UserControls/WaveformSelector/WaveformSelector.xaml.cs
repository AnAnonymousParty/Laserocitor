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
using System.Windows.Media;

using Laserocitor.Common;

namespace Laserocitor.UserControls.WaveformSelector
{
    /// <summary>
    /// Interaction logic for WaveformSelector.xaml.
    /// </summary>
    public partial class WaveformSelector
    {
        public event RoutedEventHandler WaveformSelected;

        public static readonly RoutedEvent WaveformSelectedEvent = EventManager.RegisterRoutedEvent("WaveformSelected",
                                                                                                    RoutingStrategy.Bubble,
                                                                                                    typeof(RoutedEventHandler),
                                                                                                    typeof(WaveformSelector));  
        /// <summary>
        /// Constructor (default).
        /// </summary>
        public WaveformSelector()
        {
            WaveformSelected = null;

            InitializeComponent();

            Waveform = eWAVEFORM.eSin;

            UpdateUI();
        }

        /// <summary>
        /// Unused required override to prevent warnings.
        /// </summary>
        /// <remarks>
        /// Attaches events and names to XAML-initiated content.
        /// </remarks>
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

        #region Control Enabled property

        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static readonly DependencyProperty ControlEnabledProperty = DependencyProperty.Register("ControlEnabled",
                                                                                                       typeof(bool),
                                                                                                       typeof(WaveformSelector),
                                                                                                       new FrameworkPropertyMetadata(false, OnControlEnabledChanged));
        public bool ControlEnabled
        {
            get { return (bool)GetValue(ControlEnabledProperty); }
            set
            {
                SetValue(ControlEnabledProperty, value);
            }
        }
        
        /// <summary>
        /// Handle the event triggered when the control is disabled/enabled.
        /// </summary>
        /// <param name="d">DependencyObject object presumed to be of this type.</param>
        /// <param name="args">DependencyPropertyChangedEventArgs object containing the current state.</param>
        private static void OnControlEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
         ((WaveformSelector)d).OnControlEnabledChanged(args);
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

        #region Selected waveform property

        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static readonly DependencyProperty WaveformProperty = DependencyProperty.Register("Waveform",
                                                                                                 typeof(eWAVEFORM),
                                                                                                 typeof(WaveformSelector),
                                                                                                 new FrameworkPropertyMetadata(eWAVEFORM.eSin));
        public eWAVEFORM Waveform
        {
            get { return (eWAVEFORM)GetValue(WaveformProperty); }
            set
            {
                SetValue(WaveformProperty, value);
            }
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle Ramp Sine wave button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void OscRampBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            Waveform = eWAVEFORM.eRamp;

            UpdateUI();

            TriggerWaveformSelectedEvent();
        }

        /// <summary>
        /// Handle Rectified Sine wave button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void OscRectSinBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            Waveform = eWAVEFORM.eRectSin;

            UpdateUI();

            TriggerWaveformSelectedEvent();
        }

        /// <summary>
        /// Handle Rectified Sawtooth wave button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void OscSawBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            Waveform = eWAVEFORM.eSawtooth;

            UpdateUI();

            TriggerWaveformSelectedEvent();
        }

        /// <summary>
        /// Handle Sine wave button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void OscSinBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            Waveform = eWAVEFORM.eSin;

            UpdateUI();

            TriggerWaveformSelectedEvent();
        }

        /// <summary>
        /// Handle Square wave button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void OscSqBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            Waveform = eWAVEFORM.eSquare;

            UpdateUI();

            TriggerWaveformSelectedEvent();
        }

        /// <summary>
        /// Handle Triangle wave button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void OscTriBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            Waveform = eWAVEFORM.eTriangle;

            UpdateUI();

            TriggerWaveformSelectedEvent();
        }

        #endregion

        /// <summary>
        /// Trigger the WaveformSelected event.
        /// </summary>
        private void TriggerWaveformSelectedEvent()
        {
            if (null == WaveformSelected)
            {
                // Keep compiler from whining.
            }

            RoutedEventArgs routedEventArgs = new RoutedEventArgs(WaveformSelectedEvent);

            RaiseEvent(routedEventArgs);
        }

        /// <summary>
        /// Ensure the UI reflects the current state of the system.
        /// </summary>
        private void UpdateUI()
        {
            Brush activeColor;
            Brush inactiveColor;

            if (true == ControlEnabled)
            {
                activeColor   = Brushes.LightGreen;
                inactiveColor = Brushes.DarkGreen;
            }
            else
            {
                activeColor   = Brushes.LightPink;
                inactiveColor = Brushes.DarkRed;               
            }

            switch (Waveform)
            {
                case eWAVEFORM.eRamp:
                {
                    OscRampBtn.Background    = activeColor;
                    OscRectSinBtn.Background = inactiveColor;
                    OscSawBtn.Background     = inactiveColor;
                    OscSinBtn.Background     = inactiveColor;
                    OscSqBtn.Background      = inactiveColor;
                    OscTriBtn.Background     = inactiveColor;
                }
                break;

                case eWAVEFORM.eRectSin:
                {
                    OscRampBtn.Background    = inactiveColor;
                    OscRectSinBtn.Background = activeColor;
                    OscSawBtn.Background     = inactiveColor;
                    OscSinBtn.Background     = inactiveColor;
                    OscSqBtn.Background      = inactiveColor;
                    OscTriBtn.Background     = inactiveColor;
                }
                break;

                case eWAVEFORM.eSawtooth:
                {
                    OscRampBtn.Background    = inactiveColor;
                    OscRectSinBtn.Background = inactiveColor;
                    OscSawBtn.Background     = activeColor;
                    OscSinBtn.Background     = inactiveColor;
                    OscSqBtn.Background      = inactiveColor;
                    OscTriBtn.Background     = inactiveColor;
                }
                break;

                case eWAVEFORM.eSin:
                {
                    OscRampBtn.Background    = inactiveColor;
                    OscRectSinBtn.Background = inactiveColor;
                    OscSawBtn.Background     = inactiveColor;
                    OscSinBtn.Background     = activeColor;
                    OscSqBtn.Background      = inactiveColor;
                    OscTriBtn.Background     = inactiveColor;
                }
                break;

                case eWAVEFORM.eSquare:
                {
                    OscRampBtn.Background    = inactiveColor;
                    OscRectSinBtn.Background = inactiveColor;
                    OscSawBtn.Background     = inactiveColor;
                    OscSinBtn.Background     = inactiveColor;
                    OscSqBtn.Background      = activeColor;
                    OscTriBtn.Background     = inactiveColor;
                }
                break;

                case eWAVEFORM.eTriangle:
                {
                    OscRampBtn.Background    = inactiveColor;
                    OscRectSinBtn.Background = inactiveColor;
                    OscSawBtn.Background     = inactiveColor;
                    OscSinBtn.Background     = inactiveColor;
                    OscSqBtn.Background      = inactiveColor;
                    OscTriBtn.Background     = activeColor;
                }
                break;

                case eWAVEFORM.eVarDutySquare:
                {
                    OscRampBtn.Background    = inactiveColor;
                    OscRectSinBtn.Background = inactiveColor;
                    OscSawBtn.Background     = inactiveColor;
                    OscSinBtn.Background     = inactiveColor;
                    OscSqBtn.Background      = inactiveColor;
                    OscTriBtn.Background     = inactiveColor;
                }
                break;

                default:
                {
                    OscRampBtn.Background    = inactiveColor;
                    OscRectSinBtn.Background = inactiveColor;
                    OscSawBtn.Background     = inactiveColor;
                    OscSinBtn.Background     = inactiveColor;
                    OscSqBtn.Background      = inactiveColor;
                    OscTriBtn.Background     = inactiveColor;
                }
                break;
            }
        }
    }
}
