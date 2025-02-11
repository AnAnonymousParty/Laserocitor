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

namespace Laserocitor.UserControls.BeamChopper
{
    /// <summary>
    /// Interaction logic for BeamChopper.xaml.
    /// </summary>
    public partial class BeamChopper
    {
        /// <summary>
        /// Constructor (default).
        /// </summary>
        public BeamChopper()
        {
            InitializeComponent();

            UpdateUI();
        }

        #region Dependency Properties

        #region Chopper Blue Enable property.

        public static readonly DependencyProperty BCBlueEnableProperty = DependencyProperty.Register("BCBlueEnable",
                                                                                                     typeof(bool),
                                                                                                     typeof(BeamChopper),
                                                                                                     new PropertyMetadata(false));
        public bool BCBlueEnable
        {
            get { return (bool)GetValue(BCBlueEnableProperty); }
            set { SetValue(BCBlueEnableProperty, value); }
        }

        #endregion

        #region Chopper Duty Cycle property.

        public static readonly DependencyProperty BCDutyCycleProperty = DependencyProperty.Register("BCDutyCycle",
                                                                                                    typeof(int),
                                                                                                    typeof(BeamChopper),
                                                                                                    new PropertyMetadata(1));
        public int BCDutyCycle
        {
            get { return (int)GetValue(BCDutyCycleProperty); }
            set { SetValue(BCDutyCycleProperty, value); }
        }

        #endregion

        #region Chopper Green Enable property.

        public static readonly DependencyProperty BCGreenEnableProperty = DependencyProperty.Register("BCGreenEnable",
                                                                                                      typeof(bool),
                                                                                                      typeof(BeamChopper),
                                                                                                      new PropertyMetadata(false));
        public bool BCGreenEnable
        {
            get { return (bool)GetValue(BCGreenEnableProperty); }
            set { SetValue(BCGreenEnableProperty, value); }
        }

        #endregion

        #region Chopper Master Enable property.

        public static readonly DependencyProperty BCMasterEnableProperty = DependencyProperty.Register("BCMasterEnable",
                                                                                                       typeof(bool),
                                                                                                       typeof(BeamChopper),
                                                                                                       new PropertyMetadata(false));
        public bool BCMasterEnable
        {
            get { return (bool)GetValue(BCMasterEnableProperty); }
            set
            {
                SetValue(BCMasterEnableProperty, value);
            }
        }

        #endregion

        #region Chopper Rate property.

        public static readonly DependencyProperty BCRateProperty = DependencyProperty.Register("BCRate",
                                                                                               typeof(int),
                                                                                               typeof(BeamChopper),
                                                                                               new PropertyMetadata(1));
        public int BCRate
        {
            get { return (int)GetValue(BCRateProperty); }
            set { SetValue(BCRateProperty, value); }
        }

        #endregion

        #region Chopper Red Enable property.

        public static readonly DependencyProperty BCRedEnableProperty = DependencyProperty.Register("BCRedEnable",
                                                                                                    typeof(bool),
                                                                                                    typeof(BeamChopper),
                                                                                                    new PropertyMetadata(false));
        public bool BCRedEnable
        {
            get { return (bool)GetValue(BCRedEnableProperty); }
            set { SetValue(BCRedEnableProperty, value); }
        }

        #endregion

        #region Section Title Text property.

        public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                         typeof(string),
                                                                                                         typeof(BeamChopper),
                                                                                                         new PropertyMetadata(""));
        public String SectionTitleText
        {
            get { return (String)GetValue(SectionTitleTextProperty); }
            set { SetValue(SectionTitleTextProperty, value); }
        }

        #endregion

        #endregion

        /// <summary>
        /// Handle Color Modulation Mode Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void ChopperEnableTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handle Color Modulation Mode Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void ChopperBlueTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            BCBlueEnable = BCBlueEnableTS.SwitchOn;
        }

        /// <summary>
        /// Handle Color Modulation Mode Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void ChopperGreenTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            BCGreenEnable = BCGreenEnableTS.SwitchOn;
        }

        /// <summary>
        /// Handle Color Modulation Mode Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void ChopperRedTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            BCRedEnable = BCRedEnableTS.SwitchOn;
        }

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ensure the UI reflects the current state of the system.
        /// </summary>
        private void UpdateUI()
        {
            BCMasterEnable = BCMasterEnableTS.SwitchOn;

            if (true == BCMasterEnable)
            {
                BCBlueEnableTS.ControlEnabled  = true;
                BCDutyCycleRC.ControlEnabled   = true;
                BCGreenEnableTS.ControlEnabled = true;
                BCRateRC.ControlEnabled        = true;
                BCRedEnableTS.ControlEnabled   = true;

                return;
            }

            BCBlueEnableTS.ControlEnabled  = false;
            BCDutyCycleRC.ControlEnabled   = false;
            BCGreenEnableTS.ControlEnabled = false;
            BCRateRC.ControlEnabled        = false;
            BCRedEnableTS.ControlEnabled   = false;
        }
    }
}
