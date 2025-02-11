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

using JoystickControl;
using Laserocitor.Common;

namespace Laserocitor.UserControls.RadialScanGenerator
{
 /// <summary>
 /// Interaction logic for RadialScanGenerator.xaml.
 /// </summary>
 public partial class RadialScanGenerator
 {
  readonly Brush disabledOffBrush;
  readonly Brush disabledOnBrush;
  readonly Brush enabledOffBrush;
  readonly Brush enabledOnBrush;

  /// <summary>
  /// Constructor (default).
  /// </summary>
  public RadialScanGenerator()
  {
   disabledOffBrush = (Brush)FindResource("ButtonDisabledOff");
   disabledOnBrush  = (Brush)FindResource("ButtonDisabledOn");
   enabledOffBrush  = (Brush)FindResource("ButtonEnabledOff");
   enabledOnBrush   = (Brush)FindResource("ButtonEnabledOn");

   InitializeComponent();

   var joystickControl = FindName("PerspectiveStick") as Joystick;

   if (null != joystickControl)
   {
    PerspectiveStick.Moved += PerspectiveStickMoved;
   }

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

  #region Color Modulation Source property.

  public static readonly DependencyProperty RSColorModSourceProperty = DependencyProperty.Register("RSColorModSource",
                                                                                                   typeof(eCOLORMODSYNCSOURCE),
                                                                                                   typeof(RadialScanGenerator),
                                                                                                   new PropertyMetadata(eCOLORMODSYNCSOURCE.eColorMod));
  public eCOLORMODSYNCSOURCE RSColorModSource
  {
   get { return (eCOLORMODSYNCSOURCE)GetValue(RSColorModSourceProperty); }
   set { SetValue(RSColorModSourceProperty, value); }
  }

  #endregion

  #region Control Enabled property

  public static readonly DependencyProperty ControlEnabledProperty = DependencyProperty.Register("ControlEnabled",
                                                                                                 typeof(bool),
                                                                                                 typeof(RadialScanGenerator),
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
   ((RadialScanGenerator)d).OnControlEnabledChanged(args);
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

  #region Base Geometry Oscillator Amplitude property.

  public static readonly DependencyProperty RSBGAmplitudeProperty = DependencyProperty.Register("RSBGAmplitude",
                                                                                                typeof(double),
                                                                                                typeof(RadialScanGenerator),
                                                                                                new PropertyMetadata(50.0));
  public double RSBGAmplitude
  {
   get { return (double)GetValue(RSBGAmplitudeProperty); }
   set { SetValue(RSBGAmplitudeProperty, value); }
  }

  #endregion

  #region Base Geometry Oscillator Direction property.

  public static readonly DependencyProperty RSBGDirectionProperty = DependencyProperty.Register("RSBGDirection",
                                                                                                typeof(eDIRECTION),
                                                                                                typeof(RadialScanGenerator),
                                                                                                new PropertyMetadata(eDIRECTION.eCW));
  public eDIRECTION RSBGDirection
  {
   get { return (eDIRECTION)GetValue(RSBGDirectionProperty); }
   set { SetValue(RSBGDirectionProperty, value); }
  }

  #endregion

  #region Base Geometry Oscillator Frequency property.

  public static readonly DependencyProperty RSBGFrequencyProperty = DependencyProperty.Register("RSBGFrequency",
                                                                                                typeof(double),
                                                                                                typeof(RadialScanGenerator),
                                                                                                new PropertyMetadata(10.0));
  public double RSBGFrequency
  {
   get { return (double)GetValue(RSBGFrequencyProperty); }
   set { SetValue(RSBGFrequencyProperty, value); }
  }

  #endregion

  #region Base Geometry Oscillator Phase property.

  public static readonly DependencyProperty RSBGPhaseProperty = DependencyProperty.Register("RSBGPhase",
                                                                                            typeof(double),
                                                                                            typeof(RadialScanGenerator),
                                                                                            new PropertyMetadata(0.0));
  public double RSBGPhase
  {
   get { return (double)GetValue(RSBGPhaseProperty); }
   set { SetValue(RSBGPhaseProperty, value); }
  }

  #endregion

  #region Base Geometry Waveform property.

  public static readonly DependencyProperty RSBGWaveformProperty = DependencyProperty.Register("RSBGWaveform",
                                                                                               typeof(eBASEGEOMETRY),
                                                                                               typeof(RadialScanGenerator),
                                                                                               new PropertyMetadata(eBASEGEOMETRY.eCircle));
  public eBASEGEOMETRY RSBGWaveform
  {
   get { return (eBASEGEOMETRY)GetValue(RSBGWaveformProperty); }
   set { SetValue(RSBGWaveformProperty, value); }
  }

  #endregion

  #region Joystick X position property.

  public static readonly DependencyProperty JSXPosProperty = DependencyProperty.Register("JSXPos",
                                                                                         typeof(Double),
                                                                                         typeof(RadialScanGenerator),
                                                                                         new PropertyMetadata(0.0));
  public Double JSXPos
  {
   get { return (Double)GetValue(JSXPosProperty); }
   set { SetValue(JSXPosProperty, value); }
  }

  #endregion

  #region Joystick Y position property.
  public static readonly DependencyProperty JSYPosProperty = DependencyProperty.Register("JSYPos",
                                                                                         typeof(Double),
                                                                                         typeof(RadialScanGenerator),
                                                                                         new PropertyMetadata(0.0));
  public Double JSYPos
  {
   get { return (Double)GetValue(JSYPosProperty); }
   set { SetValue(JSYPosProperty, value); }
  }

  #endregion

  #region Modulation Gain property.

  public static readonly DependencyProperty RSModGainProperty = DependencyProperty.Register("RSModGain",
                                                                                            typeof(double),
                                                                                            typeof(RadialScanGenerator),
                                                                                            new PropertyMetadata(10.0));
  public double RSModGain
  {
   get { return (double)GetValue(RSModGainProperty); }
   set { SetValue(RSModGainProperty, value); }
  }

  #endregion

  #region Modulation Offset property.

  public static readonly DependencyProperty RSModOffsetProperty = DependencyProperty.Register("RSModOffset",
                                                                                              typeof(double),
                                                                                              typeof(RadialScanGenerator),
                                                                                              new PropertyMetadata(10.0));
  public double RSModOffset
  {
   get { return (double)GetValue(RSModOffsetProperty); }
   set { SetValue(RSModOffsetProperty, value); }
  }

  #endregion

  #region Perspective Modulation Oscillator Amplitude property.

  public static readonly DependencyProperty RSPersModLevelProperty = DependencyProperty.Register("RSPersModLevel",
                                                                                                 typeof(double),
                                                                                                 typeof(RadialScanGenerator),
                                                                                                 new PropertyMetadata(50.0));
  public double RSPersModLevel
  {
   get { return (double)GetValue(RSPersModLevelProperty); }
   set { SetValue(RSPersModLevelProperty, value); }
  }

  #endregion

  #region Pesrpecive Modulation Oscillator Direction property.

  public static readonly DependencyProperty RSPersModDirectionProperty = DependencyProperty.Register("RSPersModDirection",
                                                                                                     typeof(eDIRECTION),
                                                                                                     typeof(RadialScanGenerator),
                                                                                                     new PropertyMetadata(eDIRECTION.eCW));
  public eDIRECTION RSPersModDirection
  {
   get { return (eDIRECTION)GetValue(RSPersModDirectionProperty); }
   set { SetValue(RSPersModDirectionProperty, value); }
  }

  #endregion

  #region Pesrpective Modulation Oscillator Enable property.

  public static readonly DependencyProperty RSPersModEnableProperty = DependencyProperty.Register("RSPersModEnable",
                                                                                                  typeof(bool),
                                                                                                  typeof(RadialScanGenerator),
                                                                                                  new PropertyMetadata(false));
  public bool RSPersModEnable
  {
   get { return (bool)GetValue(RSPersModEnableProperty); }
   set { SetValue(RSPersModEnableProperty, value); }
  }

  #endregion

  #region Perspective Modulation Oscillator Frequency property.

  public static readonly DependencyProperty RSPersModFrequencyProperty = DependencyProperty.Register("RSPersModFrequency",
                                                                                                 typeof(double),
                                                                                                 typeof(RadialScanGenerator),
                                                                                                 new PropertyMetadata(2.0));
  public double RSPersModFrequency
  {
   get { return (double)GetValue(RSPersModFrequencyProperty); }
   set { SetValue(RSPersModFrequencyProperty, value); }
  }

        #endregion

        #region Perspective Preset X position property.

        public static readonly DependencyProperty PersPresetXProperty = DependencyProperty.Register("PersPresetX",
                                                                                                    typeof(Double),
                                                                                                    typeof(RadialScanGenerator),
                                                                                                    new PropertyMetadata(0.0, OnPersPresetXChanged));
        public Double PersPresetX
        {
            get { return (Double)GetValue(PersPresetXProperty); }
            set { SetValue(PersPresetXProperty, value); }
        }

        /// <summary>
        /// Handle the event triggered when the Perspective Preset X value is changed.
        /// </summary>
        /// <param name="d">DependencyObject object presumed to be of this type.</param>
        /// <param name="args">DependencyPropertyChangedEventArgs object containing the current state.</param>
        private static void OnPersPresetXChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((RadialScanGenerator)d).OnPersPresetXChanged(args);
        }

        /// <summary>
        /// Do something when the Perspective Preset X value is changed, namely update the value.
        /// </summary>
        /// <param name="args">DependencyPropertyChangedEventArgs object from which the current state my be obtained.</param>
        protected virtual void OnPersPresetXChanged(DependencyPropertyChangedEventArgs args)
        {
            if (null != args.NewValue)
            {
                PersPresetX = (Double)args.NewValue;
            }
        }

        #endregion

        #region Perspective Preset Y position property.

        public static readonly DependencyProperty PersPresetYProperty = DependencyProperty.Register("PersPresetY",
                                                                                                    typeof(Double),
                                                                                                    typeof(RadialScanGenerator),
                                                                                                    new PropertyMetadata(0.0, OnPersPresetYChanged));
        public Double PersPresetY
        {
            get { return (Double)GetValue(PersPresetYProperty); }
            set { SetValue(PersPresetYProperty, value); }
        }

        /// <summary>
        /// Handle the event triggered when the Perspective Preset Y value is changed.
        /// </summary>
        /// <param name="d">DependencyObject object presumed to be of this type.</param>
        /// <param name="args">DependencyPropertyChangedEventArgs object containing the current state.</param>
        private static void OnPersPresetYChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((RadialScanGenerator)d).OnPersPresetYChanged(args);
        }

        /// <summary>
        /// Do something when the Perspective Preset Y value is changed, namely update the value.
        /// </summary>
        /// <param name="args">DependencyPropertyChangedEventArgs object from which the current state my be obtained.</param>
        protected virtual void OnPersPresetYChanged(DependencyPropertyChangedEventArgs args)
        {
            if (null != args.NewValue)
            {
                PersPresetY = (Double)args.NewValue;
            }
        }

        #endregion


        #region Radiator AM Oscillator Frequency property.

        public static readonly DependencyProperty RSAMFrequencyProperty = DependencyProperty.Register("RSAMFrequency",
                                                                                                typeof(double),
                                                                                                typeof(RadialScanGenerator),
                                                                                                new PropertyMetadata(10.0));
  public double RSAMFrequency
  {
   get { return (double)GetValue(RSAMFrequencyProperty); }
   set { SetValue(RSAMFrequencyProperty, value); }
  }

  #endregion

  #region Radiator AM Oscillator Amplitude property.

  public static readonly DependencyProperty RSAMLevelProperty = DependencyProperty.Register("RSAMLevel",
                                                                                            typeof(double),
                                                                                            typeof(RadialScanGenerator),
                                                                                            new PropertyMetadata(10.0));
  public double RSAMLevel
  {
   get { return (double)GetValue(RSAMLevelProperty); }
   set { SetValue(RSAMLevelProperty, value); }
  }

  #endregion

  #region Radiator AM Oscillator Phase property.

  public static readonly DependencyProperty RSAMPhaseProperty = DependencyProperty.Register("RSAMPhase",
                                                                                            typeof(bool),
                                                                                            typeof(RadialScanGenerator),
                                                                                            new PropertyMetadata(false));
  public bool RSAMPhase
  {
   get { return (bool)GetValue(RSAMPhaseProperty); }
   set { SetValue(RSAMPhaseProperty, value); }
  }

  #endregion

  #region Radiator Oscillator Waveform property.

  public static readonly DependencyProperty RSAMWaveformProperty = DependencyProperty.Register("RSAMWaveform",
                                                                                               typeof(eWAVEFORM),
                                                                                               typeof(RadialScanGenerator),
                                                                                               new PropertyMetadata(eWAVEFORM.eSin));
  public eWAVEFORM RSAMWaveform
  {
   get { return (eWAVEFORM)GetValue(RSAMWaveformProperty); }
   set { SetValue(RSAMWaveformProperty, value); }
  }

  #endregion

  #region Radiator Modulation Oscillator Enable property.

  public static readonly DependencyProperty RSRadiatorModEnableProperty = DependencyProperty.Register("RSRadiatorModEnable",
                                                                                                      typeof(bool),
                                                                                                      typeof(RadialScanGenerator),
                                                                                                      new PropertyMetadata(false));
  public bool RSRadiatorModEnable
  {
   get { return (bool)GetValue(RSRadiatorModEnableProperty); }
   set { SetValue(RSRadiatorModEnableProperty, value); }
  }

  #endregion

  #region Radiator Oscillator Frequency property.

  public static readonly DependencyProperty RSRadiatorFrequencyProperty = DependencyProperty.Register("RSRadiatorFrequency",
                                                                                                      typeof(double),
                                                                                                      typeof(RadialScanGenerator),
                                                                                                      new PropertyMetadata(10.0));
  public double RSRadiatorFrequency
  {
   get { return (double)GetValue(RSRadiatorFrequencyProperty); }
   set { SetValue(RSRadiatorFrequencyProperty, value); }
  }

  #endregion

  #region Radiator Oscillator Amplitude property.

  public static readonly DependencyProperty RSRadiatorLevelProperty = DependencyProperty.Register("RSRadiatorLevel",
                                                                                                  typeof(double),
                                                                                                  typeof(RadialScanGenerator),
                                                                                                  new PropertyMetadata(10.0));
  public double RSRadiatorLevel
  {
   get { return (double)GetValue(RSRadiatorLevelProperty); }
   set { SetValue(RSRadiatorLevelProperty, value); }
  }

  #endregion

  #region Radiator Oscillator Duty Cycle Phase property.

  public static readonly DependencyProperty RSRDutyCycleProperty = DependencyProperty.Register("RSRDutyCycle",
                                                                                               typeof(double),
                                                                                               typeof(RadialScanGenerator),
                                                                                               new PropertyMetadata(50.0));
  public double RSRDutyCycle
  {
   get { return (double)GetValue(RSRDutyCycleProperty); }
   set { SetValue(RSRDutyCycleProperty, value); }
  }

  #endregion

  #region Radiator Oscillator Waveform property.

  public static readonly DependencyProperty RSRadiatorWaveformProperty = DependencyProperty.Register("RSRadiatorWaveform",
                                                                                            typeof(eWAVEFORM),
                                                                                            typeof(RadialScanGenerator),
                                                                                            new PropertyMetadata(eWAVEFORM.eSin));
  public eWAVEFORM RSRadiatorWaveform
  {
   get { return (eWAVEFORM)GetValue(RSRadiatorWaveformProperty); }
   set { SetValue(RSRadiatorWaveformProperty, value); }
  }

  #endregion

  #endregion

  #region Section Title Text property.

  public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                   typeof(string),
                                                                                                   typeof(RadialScanGenerator),
                                                                                                   new PropertyMetadata(""));
  public String SectionTitleText
  {
   get { return (String) GetValue(SectionTitleTextProperty); }
   set { SetValue(SectionTitleTextProperty, value); }
  }

  #endregion

  #region Event Handlers

  /// <summary>
  /// Handle Radiator AM Enable switch clicked event.
  /// </summary>
  /// <param name="sender">Presumed ToggleSwitch object indicating the desired state.</param>
  /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
  private void AMEnableTSClkd(object sender, RoutedEventArgs eUnused)
  {
   ToggleSwitchControl.ToggleSwitchControl tsc = sender as ToggleSwitchControl.ToggleSwitchControl;

   if (null != tsc)
   {
    RSRadiatorModEnable = tsc.SwitchOn;

    UpdateUI();
   }
  }

  /// <summary>
  /// Handle AM Phase switch clicked event.
  /// </summary>
  /// <param name="sender">Presumed ToggleSwitch object indicating the desired state.</param>
  /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
  private void AMPhaseTSClkd(object sender, RoutedEventArgs eUnused)
  {
   ToggleSwitchControl.ToggleSwitchControl tsc = sender as ToggleSwitchControl.ToggleSwitchControl;

   if (null != tsc)
   {
    RSAMPhase = tsc.SwitchOn;
   }
  }

  /// <summary>
  /// Handle Circle button being clicked.
  /// </summary>
  /// <param name="senderUnused">UI element object triggering the event (unused).</param>
  /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
  private void CircleBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
  {
   RSBGWaveform = eBASEGEOMETRY.eCircle;

   UpdateUI();
  }

  /// <summary>
  /// Handle Diamond wave button being clicked.
  /// </summary>
  /// <param name="senderUnused">UI element object triggering the event (unused).</param>
  /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
  private void DiamondBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
  {
   RSBGWaveform = eBASEGEOMETRY.eDiamond;

   UpdateUI();
  }

  /// <summary>
  /// Handle Waveform Selected event.
  /// </summary>
  /// <param name="sender">Presumed WaveformSelector object containing the selected waveform.</param>
  /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
  private void OnAMWaveformSelected(object sender, RoutedEventArgs eUnused)
  {
   WaveformSelector.WaveformSelector ws = sender as WaveformSelector.WaveformSelector;

  }

  /// <summary>
  /// Handle Waveform Selected event.
  /// </summary>
  /// <param name="senderUnused">Presumed WaveformSelector object containing the selected waveform (unused).</param>
  /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
  private void OnRadiatorWaveformSelected(object sender, RoutedEventArgs eUnused)
  {
   UpdateUI();
  }

  /// <summary>
  /// Handle Perspective Modulation Direction switch clicked event.
  /// </summary>
  /// <param name="sender">Presumed ToggleSwitch indicating the desired direction.</param>  
  /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
  private void PersModDirTSClkd(object sender, RoutedEventArgs eUnused)
  {
   ToggleSwitchControl.ToggleSwitchControl tsc = sender as ToggleSwitchControl.ToggleSwitchControl;

   if (null != tsc)
   {
    RSPersModDirection = (true == tsc.SwitchOn ? eDIRECTION.eCCW : eDIRECTION.eCW);
   }
  }

  /// <summary>
  /// Handle Perspective Modulation Enable switch clicked event.
  /// </summary>
  /// <param name="sender">Presumed ToggleSwitch object indicating the desired state.</param>  
  /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
  private void PersModEnableTSClkd(object sender, RoutedEventArgs eUnused)
  {
   ToggleSwitchControl.ToggleSwitchControl tsc = sender as ToggleSwitchControl.ToggleSwitchControl;

   if (null != tsc)
   {
    RSPersModEnable = tsc.SwitchOn;
   }
  }

/// <summary>
/// Handle Perspective joystick moved event.
/// </summary>
/// <param name="senderUnused">Presumed ToggleSwitch object indicating the desired state (unused).</param>  
/// <param name="args">RoutedEventArgs object to provide arguments.</param>
private void PerspectiveStickMoved(object sender, JoystickEventArgs args)
  {
   JSXPos =  args.X * 5;
   JSYPos = -args.Y * 5;
  }

  /// <summary>
  /// Handle Base Generator Oscillator Direction switch clicked event.
  /// </summary>
  /// <param name="sender">Presumed ToggleSwitchControl object indicating the selected direction.</param>
  /// <param name="eUnused">RoutedEventArgs object to provide arguments (unused).</param>
  private void RSBGDirTSClkd(object sender, RoutedEventArgs eUnused)
  {
   ToggleSwitchControl.ToggleSwitchControl tsc = sender as ToggleSwitchControl.ToggleSwitchControl;

   if (null != tsc)
   {
    RSBGDirection = (true == tsc.SwitchOn ? eDIRECTION.eCCW : eDIRECTION.eCW);
   }
  }

  /// <summary>
  /// Handle Triangle wave button being clicked.
  /// </summary>
  /// <param name="senderUnused">UI element object triggering the event (unused).</param>
  /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
  private void TriangleBtnClkd(object senderUnused, RoutedEventArgs argsUnused)
  {
   RSBGWaveform = eBASEGEOMETRY.eTriangle;

   UpdateUI();
  }

  #endregion

  /// <summary>
  /// Ensure the UI reflects the current state of the system.
  /// </summary>
  private void UpdateUI()
  {
   AMEnableTS.ControlEnabled              = ControlEnabled;
   AutoPerspectiveEnableTS.ControlEnabled = ControlEnabled;
   PerspectiveStick.ControlEnabled        = ControlEnabled;
   RSBGAmplitudeRC.ControlEnabled         = ControlEnabled;
   RSBGDirTS.ControlEnabled               = ControlEnabled;
   RSBGFrequencyRC.ControlEnabled         = ControlEnabled;
   RSBGPhaseRC.ControlEnabled             = ControlEnabled;
   RSRDutyCycleRC.ControlEnabled          = ControlEnabled;
   RSModGainRC.ControlEnabled             = ControlEnabled;
   RSModOffsetRC.ControlEnabled           = ControlEnabled;
   RSRadiatorFrequencyRC.ControlEnabled   = ControlEnabled;
   RSRadiatorLevelRC.ControlEnabled       = ControlEnabled;
   RS_RadiatorWaveformWS.ControlEnabled   = ControlEnabled;

   if (true == AutoPerspectiveEnableTS.ControlEnabled)
   {
    AutpPerspectiveDirTS.ControlEnabled = ControlEnabled;
    RSPersModFrequencyRC.ControlEnabled = ControlEnabled;
    RSPersModLevelRC.ControlEnabled     = ControlEnabled;
   }
   else
   {
    AutpPerspectiveDirTS.ControlEnabled = false;
    RSPersModFrequencyRC.ControlEnabled = false;
    RSPersModLevelRC.ControlEnabled     = false;
   }

   if (true == AMEnableTS.ControlEnabled)
   {
    AMPhaseTS.ControlEnabled       = ControlEnabled;
    RSAMFrequencyRC.ControlEnabled = ControlEnabled;
    RSAMLevelRC.ControlEnabled     = ControlEnabled;
    RSAMWaveformWS.ControlEnabled  = ControlEnabled;
   }
   else
   {
    AMPhaseTS.ControlEnabled       = false;
    RSAMFrequencyRC.ControlEnabled = false;
    RSAMLevelRC.ControlEnabled     = false;
    RSAMWaveformWS.ControlEnabled  = false;
   }

   if (true == RSRadiatorModEnable)
   {
    AMPhaseTS.ControlEnabled       = ControlEnabled;
    RSAMFrequencyRC.ControlEnabled = ControlEnabled;
    RSAMLevelRC.ControlEnabled     = ControlEnabled;
    RSAMWaveformWS.ControlEnabled  = ControlEnabled;
   }
   else
   {
    AMPhaseTS.ControlEnabled       = false;
    RSAMFrequencyRC.ControlEnabled = false;
    RSAMLevelRC.ControlEnabled     = false;
    RSAMWaveformWS.ControlEnabled  = false;
   }

   if (eWAVEFORM.eSquare == RSRadiatorWaveform)
   {
    RSRDutyCycleRC.ControlEnabled = ControlEnabled;
   }
   else
   {
    RSRDutyCycleRC.ControlEnabled = false;
   }

   switch (RSBGWaveform)
   {
    case eBASEGEOMETRY.eCircle:
    {
     CircleBtn.Background   = (true == ControlEnabled ? enabledOnBrush  : disabledOnBrush);
     DiamondBtn.Background  = (true == ControlEnabled ? enabledOffBrush : disabledOffBrush);
     TriangleBtn.Background = (true == ControlEnabled ? enabledOffBrush : disabledOffBrush);
    }
    break;

    case eBASEGEOMETRY.eDiamond:
    {
     CircleBtn.Background   = (true == ControlEnabled ? enabledOffBrush : disabledOffBrush);
     DiamondBtn.Background  = (true == ControlEnabled ? enabledOnBrush  : disabledOnBrush);
     TriangleBtn.Background = (true == ControlEnabled ? enabledOffBrush : disabledOffBrush);
    }
    break;

    case eBASEGEOMETRY.eTriangle:
    {
     CircleBtn.Background   = (true == ControlEnabled ? enabledOffBrush : disabledOffBrush);
     DiamondBtn.Background  = (true == ControlEnabled ? enabledOffBrush : disabledOffBrush);
     TriangleBtn.Background = (true == ControlEnabled ? enabledOnBrush  : disabledOnBrush);
    }
    break;
   }
  }
 }
}
