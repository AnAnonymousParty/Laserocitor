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
using System.Windows.Controls;
using System.Windows.Media;

using Laserocitor.Common;
using Laserocitor.Utils.Diagnostics;

namespace Laserocitor.UserControls.ColorModulation
{
    /// <summary>
    /// Interaction logic for ColorModulation.xaml.
    /// </summary>
    public partial class ColorModulation : UserControl
    {
        /// <summary>
        /// Constructor (default).
        /// </summary>
        public ColorModulation()
        {
            InitializeComponent();

            CMEnable = CMAutoTS.SwitchOn;
            CMMode   = (false == ColorModModeTS.SwitchOn ? eCOLORMODMODE.eSynchronized : eCOLORMODMODE.eUnsynchronized);

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

        #region 3-Phase Oscillator Rate property.

        public static readonly DependencyProperty CM3PhaseOscRateProperty = DependencyProperty.Register("CM3PhaseOscRate",
                                                                                                        typeof(int),
                                                                                                        typeof(ColorModulation),
                                                                                                        new PropertyMetadata(1));
        public int CM3PhaseOscRate
        {
            get { return (int)GetValue(CM3PhaseOscRateProperty); }
            set { SetValue(CM3PhaseOscRateProperty, value); }
        }

        #endregion

        #region Image Drawing Mode property.

        public static readonly DependencyProperty CMDrawingModeProperty = DependencyProperty.Register("CMDrawingMode",
                                                                                                      typeof(eMODE),
                                                                                                      typeof(ColorModulation),
                                                                                                      new PropertyMetadata(eMODE.eCycloid,
                                                                                                                           OnDrawingModeChanged));
        public eMODE CMDrawingMode
        {
            get { return (eMODE)GetValue(CMDrawingModeProperty); }
            set { SetValue(CMDrawingModeProperty, value); }
        }

        /// <summary>
        /// Handle the event triggered when the control is disabled/enabled.
        /// </summary>
        /// <param name="d">DependencyObject object presumed to be of this type.</param>
        /// <param name="args">DependencyPropertyChangedEventArgs object containing the current state.</param>
        private static void OnDrawingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((ColorModulation)d).OnDrawingModeChanged(args);
        }

        /// <summary>
        /// Do something when the control is disabled/enabled, namely update the UI to reflect the change.
        /// </summary>
        /// <param name="args">DependencyPropertyChangedEventArgs object from which the current state my be obtained.</param>
        protected virtual void OnDrawingModeChanged(DependencyPropertyChangedEventArgs args)
        {
            if (null != args.NewValue)
            {
                CMDrawingMode = (eMODE)args.NewValue;

                UpdateUI();
            }
        }

        #endregion

        #region Color Modulation Enable property.

        public static readonly DependencyProperty CMEnableProperty = DependencyProperty.Register("CMEnable",
                                                                                                 typeof(bool),
                                                                                                 typeof(ColorModulation),
                                                                                                 new PropertyMetadata(false));
        public bool CMEnable
        {
            get { return (bool)GetValue(CMEnableProperty); }
            set { SetValue(CMEnableProperty, value); }
        }

        #endregion

        #region Color Modulation Blue Source property.

        public static readonly DependencyProperty CMBlueSourceProperty = DependencyProperty.Register("CMBlueSource",
                                                                                                     typeof(eCOLORMODOSC),
                                                                                                     typeof(ColorModulation),
                                                                                                     new PropertyMetadata(eCOLORMODOSC.eOSCCPOS));
        public eCOLORMODOSC CMBlueSource
        {
            get { return (eCOLORMODOSC)GetValue(CMBlueSourceProperty); }
            set { SetValue(CMBlueSourceProperty, value); }
        }

        #endregion

        #region Color Modulation Red Source property.

        public static readonly DependencyProperty CMRedSourceProperty = DependencyProperty.Register("CMRedSource",
                                                                                                    typeof(eCOLORMODOSC),
                                                                                                    typeof(ColorModulation),
                                                                                                    new PropertyMetadata(eCOLORMODOSC.eOSCAPOS));
        public eCOLORMODOSC CMRedSource
        {
            get { return (eCOLORMODOSC)GetValue(CMRedSourceProperty); }
            set { SetValue(CMRedSourceProperty, value); }
        }

        #endregion

        #region Color Modulation RGB property.

        public static readonly DependencyProperty CMRGBProperty = DependencyProperty.Register("CMRGB",
                                                                                              typeof(int),
                                                                                              typeof(ColorModulation),
                                                                                              new PropertyMetadata(0));
        public int CMRGB
        {
            get { return (int)GetValue(CMRGBProperty); }
            set { SetValue(CMRGBProperty, value); }
        }

        #endregion

        #region Color Modulation Green Source property.

        public static readonly DependencyProperty CMGreenSourceProperty = DependencyProperty.Register("CMGreenSource",
                                                                                                      typeof(eCOLORMODOSC),
                                                                                                      typeof(ColorModulation),
                                                                                                      new PropertyMetadata(eCOLORMODOSC.eOSCBPOS));
        public eCOLORMODOSC CMGreenSource
        {
            get { return (eCOLORMODOSC)GetValue(CMGreenSourceProperty); }
            set { SetValue(CMGreenSourceProperty, value); }
        }

        #endregion

        #region Intensity property.

        public static readonly DependencyProperty CMIntensityProperty = DependencyProperty.Register("CMIntensity",
                                                                                              typeof(int),
                                                                                              typeof(ColorModulation),
                                                                                              new PropertyMetadata(100));
        public int CMIntensity
        {
            get { return (int)GetValue(CMIntensityProperty); }
            set { SetValue(CMIntensityProperty, value); }
        }

        #endregion

        #region Color Modulation Mode property.

        public static readonly DependencyProperty CMModeProperty = DependencyProperty.Register("CMMode",
                                                                                               typeof(eCOLORMODMODE),
                                                                                               typeof(ColorModulation),
                                                                                               new PropertyMetadata(eCOLORMODMODE.eSynchronized));
        public eCOLORMODMODE CMMode
        {
            get { return (eCOLORMODMODE)GetValue(CMModeProperty); }
            set { SetValue(CMModeProperty, value); }
        }

        #endregion

        #region Color Modulation Source property.

        public static readonly DependencyProperty CMColorModSourceProperty = DependencyProperty.Register("CMColorModSource",
                                                                                                         typeof(eCOLORMODSYNCSOURCE),
                                                                                                         typeof(ColorModulation),
                                                                                                         new PropertyMetadata(eCOLORMODSYNCSOURCE.eColorMod));

        public eCOLORMODSYNCSOURCE CMColorModSource
        {
            get { return (eCOLORMODSYNCSOURCE)GetValue(CMColorModSourceProperty); }
            set { SetValue(CMColorModSourceProperty, value); }
        }

        #endregion

        #region Oscillator A Rate property.

        public static readonly DependencyProperty CMOscARateProperty = DependencyProperty.Register("CMOscARate",
                                                                                                   typeof(int),
                                                                                                   typeof(ColorModulation),
                                                                                                   new PropertyMetadata(1));
        public int CMOscARate
        {
            get { return (int)GetValue(CMOscARateProperty); }
            set { SetValue(CMOscARateProperty, value); }
        }

        #endregion

        #region Oscillator B Rate property.

        public static readonly DependencyProperty CMOscBRateProperty = DependencyProperty.Register("CMOscBRate",
                                                                                                   typeof(int),
                                                                                                   typeof(ColorModulation),
                                                                                                   new PropertyMetadata(1));
        public int CMOscBRate
        {
            get { return (int)GetValue(CMOscBRateProperty); }
            set { SetValue(CMOscBRateProperty, value); }
        }

        #endregion

        #region Oscillator C Rate property.

        public static readonly DependencyProperty CMOscCRateProperty = DependencyProperty.Register("CMOscCRate",
                                                                                                   typeof(int),
                                                                                                   typeof(ColorModulation),
                                                                                                   new PropertyMetadata(1));
        public int CMOscCRate
        {
            get { return (int)GetValue(CMOscCRateProperty); }
            set { SetValue(CMOscCRateProperty, value); }
        }

        #endregion

        #region Blue property.

        public static readonly DependencyProperty CMBlueProperty = DependencyProperty.Register("CMBlue",
                                                                                              typeof(int),
                                                                                              typeof(ColorModulation),
                                                                                              new PropertyMetadata(1));
        public int CMBlue
        {
            get { return (int)GetValue(CMBlueProperty); }
            set { SetValue(CMBlueProperty, value); }
        }

        #endregion

        #region Green property.

        public static readonly DependencyProperty CMGreenProperty = DependencyProperty.Register("CMGreen",
                                                                                                typeof(int),
                                                                                                typeof(ColorModulation),
                                                                                                new PropertyMetadata(1));
        public int CMGreen
        {
            get { return (int)GetValue(CMGreenProperty); }
            set { SetValue(CMGreenProperty, value); }
        }

        #endregion

        #region Red property.

        public static readonly DependencyProperty CMRedProperty = DependencyProperty.Register("CMRed",
                                                                                              typeof(int),
                                                                                              typeof(ColorModulation),
                                                                                              new PropertyMetadata(1));
        public int CMRed
        {
            get { return (int)GetValue(CMRedProperty); }
            set { SetValue(CMRedProperty, value); }
        }

        #endregion

        #region Section Title Text property.

        public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                         typeof(string),
                                                                                                         typeof(ColorModulation),
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
        /// Handle Blue Oscillator A (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void BANegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMBlueSource = eCOLORMODOSC.eOSCANEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Blue Oscillator A (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void BAPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMBlueSource = eCOLORMODOSC.eOSCAPOS;

            UpdateUI();
        }

        /// <summary>
        /// Handle Blue Oscillator B (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void BBNegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMBlueSource = eCOLORMODOSC.eOSCBNEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Blue Oscillator B (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void BBPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMBlueSource = eCOLORMODOSC.eOSCBPOS;

            UpdateUI();
        }

        /// <summary>
        /// Handle Blue Oscillator C (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void BCNegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMBlueSource = eCOLORMODOSC.eOSCCNEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Blue Oscillator C (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void BCPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMBlueSource = eCOLORMODOSC.eOSCCPOS;

            UpdateUI();
        }

        /// <summary>
        /// Handle Color Modulation source button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void CMBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMColorModSource = eCOLORMODSYNCSOURCE.eColorMod;

            UpdateUI();
        }

        /// <summary>
        /// Handle Base Generator Oscillator button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void CMGOscBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMColorModSource = eCOLORMODSYNCSOURCE.eRSBaseGeo;

            UpdateUI();
        }

        /// <summary>
        /// HandleRadiator Oscillator button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void CMROscBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMColorModSource = eCOLORMODSYNCSOURCE.eRSBaseRad;

            UpdateUI();
        }

        /// <summary>
        /// Handle AM Phase switch clicked event.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void CMRSAMBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMColorModSource = eCOLORMODSYNCSOURCE.eRSBaseMod;

            UpdateUI();
        }

        /// <summary>
        /// Handle Scan Oscillator source button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void CMSROBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMColorModSource = eCOLORMODSYNCSOURCE.eCycloidScan;

            UpdateUI();
        }

        /// <summary>
        /// Handle Sweep Oscillator source button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void CMSWOBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMColorModSource = eCOLORMODSYNCSOURCE.eCycloidMod;

            UpdateUI();
        }

        /// <summary>
        /// Handle Color Modulation Auto Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void ColorModAutoTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMEnable = CMAutoTS.SwitchOn;

            UpdateUI();
        }

        /// <summary>
        /// Handle Color Modulation Mode Toggle Switch being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void ColorModModeTSClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMMode = (false == ColorModModeTS.SwitchOn ? eCOLORMODMODE.eSynchronized : eCOLORMODMODE.eUnsynchronized);

            UpdateUI();
        }

        /// <summary>
        /// Handle Green Oscillator A (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void GANegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMGreenSource = eCOLORMODOSC.eOSCANEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Green Oscillator A (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void GAPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMGreenSource = eCOLORMODOSC.eOSCAPOS;

            UpdateUI();
        }

        /// <summary>
        /// Handle Green Oscillator B (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void GBNegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMBlueSource = eCOLORMODOSC.eOSCBNEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Green Oscillator B (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void GBPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMGreenSource = eCOLORMODOSC.eOSCBPOS;

            UpdateUI();
        }

        /// <summary>
        /// Handle Green Oscillator C (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void GCNegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMGreenSource = eCOLORMODOSC.eOSCCNEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Green Oscillator C (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void GCPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMGreenSource = eCOLORMODOSC.eOSCCPOS;

            UpdateUI();
        }

        /// <summary>
        /// Handle Red Oscillator A (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void RANegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMRedSource = eCOLORMODOSC.eOSCANEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Red Oscillator A (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void RAPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMRedSource = eCOLORMODOSC.eOSCAPOS;

            UpdateUI();
        }

        /// <summary>
        /// Handle Red Oscillator B (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void RBNegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMRedSource = eCOLORMODOSC.eOSCBNEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Red Oscillator B (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void RBPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMRedSource = eCOLORMODOSC.eOSCBPOS;

            UpdateUI();
        }

        /// <summary>
        /// Handle Red Oscillator C (inverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void RCNegBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMRedSource = eCOLORMODOSC.eOSCCNEG;

            UpdateUI();
        }

        /// <summary>
        /// Handle Red Oscillator C (noninverted) button being clicked.
        /// </summary>
        /// <param name="senderUnused">UI element object triggering the event (unused).</param>
        /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
        private void RCPosBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
        {
            CMRedSource = eCOLORMODOSC.eOSCCPOS;

            UpdateUI();
        }

        #endregion

        /// <summary>
        /// Ensure the UI reflects the current state of the system.
        /// </summary>
        private void UpdateUI()
        {
            Brush activeColor;
            Brush inactiveColor;

            if (false == CMEnable)
            {
                CM3PhaseOscRateRC.ControlEnabled = false;
                CMBlueRC.ControlEnabled          = true;
                CMGreenRC.ControlEnabled         = true;
                CMRedRC.ControlEnabled           = true;
                CMRGBRC.ControlEnabled           = true;
                OscARateRC.ControlEnabled        = false;
                OscBRateRC.ControlEnabled        = false;
                OscCRateRC.ControlEnabled        = false;

                ColorModModeTS.ControlEnabled = false;

                activeColor   = Brushes.LightPink;
                inactiveColor = Brushes.DarkRed;
            }
            else
            {
                CMBlueRC.ControlEnabled  = false;
                CMGreenRC.ControlEnabled = false;
                CMRedRC.ControlEnabled   = false;
                CMRGBRC.ControlEnabled   = false;

                ColorModModeTS.ControlEnabled = true;

                switch (CMMode)
                {
                    case eCOLORMODMODE.eSynchronized:
                    {
                        activeColor   = Brushes.LightPink;
                        inactiveColor = Brushes.DarkRed;
                    }
                    break;

                    case eCOLORMODMODE.eUnsynchronized:
                    {
                        activeColor   = Brushes.LightGreen;
                        inactiveColor = Brushes.DarkGreen; 
                    }
                    break;

                    default:
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eWarning,
                            GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name,
                            "", "eColorMode", (uint) CMMode));

                        activeColor   = Brushes.LightPink;
                        inactiveColor = Brushes.DarkRed;
                    }
                    break;
                }         
            }

            switch (CMBlueSource)
            {
                case eCOLORMODOSC.eOSCANEG:
                {
                    BANegBtn.Background = activeColor;
                    BAPosBtn.Background = inactiveColor;
                    BBNegBtn.Background = inactiveColor;
                    BBPosBtn.Background = inactiveColor;
                    BCNegBtn.Background = inactiveColor;
                    BCPosBtn.Background = inactiveColor;
                }
                break;

                case eCOLORMODOSC.eOSCAPOS:
                {
                    BANegBtn.Background = inactiveColor;
                    BAPosBtn.Background = activeColor;
                    BBNegBtn.Background = inactiveColor;
                    BBPosBtn.Background = inactiveColor;
                    BCNegBtn.Background = inactiveColor;
                    BCPosBtn.Background = inactiveColor;
                }
                break;

                case eCOLORMODOSC.eOSCBNEG:
                {
                    BANegBtn.Background = inactiveColor;
                    BAPosBtn.Background = inactiveColor;
                    BBNegBtn.Background = activeColor;
                    BBPosBtn.Background = inactiveColor;
                    BCNegBtn.Background = inactiveColor;
                    BCPosBtn.Background = inactiveColor;
                }
                break;

                case eCOLORMODOSC.eOSCBPOS:
                {
                    BANegBtn.Background = inactiveColor;
                    BAPosBtn.Background = inactiveColor;
                    BBNegBtn.Background = inactiveColor;
                    BBPosBtn.Background = activeColor;
                    BCNegBtn.Background = inactiveColor;
                    BCPosBtn.Background = inactiveColor;
                }
                break;

                case eCOLORMODOSC.eOSCCNEG:
                {
                    BANegBtn.Background = inactiveColor;
                    BAPosBtn.Background = inactiveColor;
                    BBNegBtn.Background = inactiveColor;
                    BBPosBtn.Background = inactiveColor;
                    BCNegBtn.Background = activeColor;
                    BCPosBtn.Background = inactiveColor;
                }
                break;

                case eCOLORMODOSC.eOSCCPOS:
                {
                    BANegBtn.Background = inactiveColor;
                    BAPosBtn.Background = inactiveColor;
                    BBNegBtn.Background = inactiveColor;
                    BBPosBtn.Background = inactiveColor;
                    BCNegBtn.Background = inactiveColor;
                    BCPosBtn.Background = activeColor;
                }
                break;

                default:
                {
                    // ReSharper disable once PossibleNullReferenceException
                    Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eWarning,
                        GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name,
                        "", "eColorMode", (uint)CMBlueSource));

                    BANegBtn.Background = inactiveColor;
                    BAPosBtn.Background = inactiveColor;
                    BBNegBtn.Background = inactiveColor;
                    BBPosBtn.Background = inactiveColor;
                    BCNegBtn.Background = inactiveColor;
                    BCPosBtn.Background = inactiveColor;
                }
                break;
            }

            switch (CMGreenSource)
            {
                case eCOLORMODOSC.eOSCANEG:
                    {
                        GANegBtn.Background = activeColor;
                        GAPosBtn.Background = inactiveColor;
                        GBNegBtn.Background = inactiveColor;
                        GBPosBtn.Background = inactiveColor;
                        GCNegBtn.Background = inactiveColor;
                        GCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCAPOS:
                    {
                        GANegBtn.Background = inactiveColor;
                        GAPosBtn.Background = activeColor;
                        GBNegBtn.Background = inactiveColor;
                        GBPosBtn.Background = inactiveColor;
                        GCNegBtn.Background = inactiveColor;
                        GCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCBNEG:
                    {
                        GANegBtn.Background = inactiveColor;
                        GAPosBtn.Background = inactiveColor;
                        GBNegBtn.Background = activeColor;
                        GBPosBtn.Background = inactiveColor;
                        GCNegBtn.Background = inactiveColor;
                        GCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCBPOS:
                    {
                        GANegBtn.Background = inactiveColor;
                        GAPosBtn.Background = inactiveColor;
                        GBNegBtn.Background = inactiveColor;
                        GBPosBtn.Background = activeColor;
                        GCNegBtn.Background = inactiveColor;
                        GCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCCNEG:
                    {
                        GANegBtn.Background = inactiveColor;
                        GAPosBtn.Background = inactiveColor;
                        GBNegBtn.Background = inactiveColor;
                        GBPosBtn.Background = inactiveColor;
                        GCNegBtn.Background = activeColor;
                        GCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCCPOS:
                    {
                        GANegBtn.Background = inactiveColor;
                        GAPosBtn.Background = inactiveColor;
                        GBNegBtn.Background = inactiveColor;
                        GBPosBtn.Background = inactiveColor;
                        GCNegBtn.Background = inactiveColor;
                        GCPosBtn.Background = activeColor;
                    }
                    break;

                default:
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eWarning,
                            GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name,
                            "", "eColorMode", (uint)CMGreenSource));

                        GANegBtn.Background = inactiveColor;
                        GAPosBtn.Background = inactiveColor;
                        GBNegBtn.Background = inactiveColor;
                        GBPosBtn.Background = inactiveColor;
                        GCNegBtn.Background = inactiveColor;
                        GCPosBtn.Background = inactiveColor;
                    }
                    break;
            }

            switch (CMRedSource)
            {
                case eCOLORMODOSC.eOSCANEG:
                    {
                        RANegBtn.Background = activeColor;
                        RAPosBtn.Background = inactiveColor;
                        RBNegBtn.Background = inactiveColor;
                        RBPosBtn.Background = inactiveColor;
                        RCNegBtn.Background = inactiveColor;
                        RCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCAPOS:
                    {
                        RANegBtn.Background = inactiveColor;
                        RAPosBtn.Background = activeColor;
                        RBNegBtn.Background = inactiveColor;
                        RBPosBtn.Background = inactiveColor;
                        RCNegBtn.Background = inactiveColor;
                        RCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCBNEG:
                    {
                        RANegBtn.Background = inactiveColor;
                        RAPosBtn.Background = inactiveColor;
                        RBNegBtn.Background = activeColor;
                        RBPosBtn.Background = inactiveColor;
                        RCNegBtn.Background = inactiveColor;
                        RCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCBPOS:
                    {
                        RANegBtn.Background = inactiveColor;
                        RAPosBtn.Background = inactiveColor;
                        RBNegBtn.Background = inactiveColor;
                        RBPosBtn.Background = activeColor;
                        RCNegBtn.Background = inactiveColor;
                        RCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCCNEG:
                    {
                        RANegBtn.Background = inactiveColor;
                        RAPosBtn.Background = inactiveColor;
                        RBNegBtn.Background = inactiveColor;
                        RBPosBtn.Background = inactiveColor;
                        RCNegBtn.Background = activeColor;
                        RCPosBtn.Background = inactiveColor;
                    }
                    break;

                case eCOLORMODOSC.eOSCCPOS:
                    {
                        RANegBtn.Background = inactiveColor;
                        RAPosBtn.Background = inactiveColor;
                        RBNegBtn.Background = inactiveColor;
                        RBPosBtn.Background = inactiveColor;
                        RCNegBtn.Background = inactiveColor;
                        RCPosBtn.Background = activeColor;
                    }
                    break;

                default:
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eWarning,
                            GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name,
                            "", "eColorMode", (uint)CMRedSource));

                        RANegBtn.Background = inactiveColor;
                        RAPosBtn.Background = inactiveColor;
                        RBNegBtn.Background = inactiveColor;
                        RBPosBtn.Background = inactiveColor;
                        RCNegBtn.Background = inactiveColor;
                        RCPosBtn.Background = inactiveColor;
                    }
                    break;
            }

            if (false == CMEnable)
            {
                activeColor   = Brushes.LightPink;
                inactiveColor = Brushes.DarkRed;
            }
            else
            {
                activeColor   = Brushes.LightGreen;
                inactiveColor = Brushes.DarkGreen;
            }

            if (eCOLORMODMODE.eSynchronized == CMMode)
            {
                CM3PhaseOscRateRC.ControlEnabled = CMEnable;

                OscARateRC.ControlEnabled = false;
                OscBRateRC.ControlEnabled = false;
                OscCRateRC.ControlEnabled = false;
            }
            else
            {
                CM3PhaseOscRateRC.ControlEnabled = false;

                OscARateRC.ControlEnabled = CMEnable;
                OscBRateRC.ControlEnabled = CMEnable;
                OscCRateRC.ControlEnabled = CMEnable;
            }

            switch (CMDrawingMode)
            {
                case eMODE.eCycloid:
                    {
                        CMCSROBtn.Background = inactiveColor;
                        CMCSWOBtn.Background = inactiveColor;
                        CMRSAMBtn.Background = Brushes.DarkRed;
                        CMRSGOBtn.Background = Brushes.DarkRed;
                        CMRSROBtn.Background = Brushes.DarkRed;
                    }
                    break;

                case eMODE.eLissajous:
                    {
                        CMCSROBtn.Background = Brushes.DarkRed;
                        CMCSWOBtn.Background = Brushes.DarkRed;
                        CMRSAMBtn.Background = Brushes.DarkRed;
                        CMRSAMBtn.Background = Brushes.DarkRed;
                        CMRSGOBtn.Background = Brushes.DarkRed;
                        CMRSROBtn.Background = Brushes.DarkRed;
                    }
                    break;

                case eMODE.eRadialScan:
                    {
                        CMCSWOBtn.Background = Brushes.DarkRed;
                        CMRSAMBtn.Background = Brushes.DarkRed;
                        CMRSAMBtn.Background = inactiveColor;
                        CMRSGOBtn.Background = inactiveColor;
                        CMRSROBtn.Background = inactiveColor;
                    }
                    break;

                case eMODE.eSpiral:
                    {
                        CMCSWOBtn.Background = Brushes.DarkRed;
                        CMRSAMBtn.Background = Brushes.DarkRed;
                        CMRSAMBtn.Background = Brushes.DarkRed;
                        CMRSGOBtn.Background = Brushes.DarkRed;
                        CMRSROBtn.Background = Brushes.DarkRed;
                    }
                    break;

            }

            CMBtn.Background = inactiveColor;

            switch (CMColorModSource)
            {
                case eCOLORMODSYNCSOURCE.eColorMod:
                    {
                        CMBtn.Background = activeColor;
                    }
                    break;

                case eCOLORMODSYNCSOURCE.eCycloidMod:
                    {
                        CMCSWOBtn.Background = (eMODE.eCycloid == CMDrawingMode ? activeColor : Brushes.DarkRed);
                    }
                    break;

                case eCOLORMODSYNCSOURCE.eCycloidScan:
                    {
                        CMCSROBtn.Background = (eMODE.eCycloid == CMDrawingMode ? activeColor : Brushes.DarkRed);
                    }
                    break;

                case eCOLORMODSYNCSOURCE.eRSBaseGeo:
                    {
                        CMRSGOBtn.Background = (eMODE.eRadialScan == CMDrawingMode ? activeColor : Brushes.DarkRed);
                    }
                    break;

                case eCOLORMODSYNCSOURCE.eRSBaseMod:
                    {
                        CMRSAMBtn.Background = (eMODE.eRadialScan == CMDrawingMode ? activeColor : Brushes.DarkRed);
                    }
                    break;

                case eCOLORMODSYNCSOURCE.eRSBaseRad:
                    {
                        CMRSROBtn.Background = (eMODE.eRadialScan == CMDrawingMode ? activeColor : Brushes.DarkRed);
                    }
                    break;

            }
        }
    }
}
