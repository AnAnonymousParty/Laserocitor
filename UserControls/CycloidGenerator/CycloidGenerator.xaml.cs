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

namespace Laserocitor.UserControls.CycloidGenerator
{
 /// <summary>
 /// Interaction logic for CycloidGenerator.xaml.
 /// </summary>
 public partial class CycloidGenerator
 {
  /// <summary>
  /// Constructor (default).
  /// </summary>
  public CycloidGenerator()
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

  #region Sweep Length Modulation Enabled property

  public static readonly DependencyProperty ASLModEnabledProperty = DependencyProperty.Register("ASLModEnabled",
                                                                                                typeof(bool),
                                                                                                typeof(CycloidGenerator),
                                                                                                new FrameworkPropertyMetadata(false));
  public bool ASLModEnabled
  {
   get { return (bool) GetValue(ASLModEnabledProperty); }
   set { SetValue(ASLModEnabledProperty, value); }
  }

  #endregion

  #region Sweep Length Modulation Level property.

  public static readonly DependencyProperty ASLLevelProperty = DependencyProperty.Register("ASLLevel",
                                                                                           typeof(double),
                                                                                           typeof(CycloidGenerator),
                                                                                           new PropertyMetadata(0.0));
  public double ASLLevel
  {
   get { return (double) GetValue(ASLLevelProperty); }
   set { SetValue(ASLLevelProperty, value); }
  }

  #endregion

  #region Sweep Length Modulation Rate property.

  public static readonly DependencyProperty ASLRateProperty = DependencyProperty.Register("ASLRate",
                                                                                          typeof(double),
                                                                                          typeof(CycloidGenerator),
                                                                                          new PropertyMetadata(0.0));
  public double ASLRate
  {
   get { return (double) GetValue(ASLRateProperty); }
   set { SetValue(ASLRateProperty, value); }
  }

  #endregion

  #region Selected ASL Modulation Oscillator waveform property

  public static readonly DependencyProperty ASLModWaveformProperty = DependencyProperty.Register("ASLModWaveform",
                                                                                                 typeof(eWAVEFORM),
                                                                                                 typeof(CycloidGenerator),
                                                                                                 new FrameworkPropertyMetadata(eWAVEFORM.eSin));
  public eWAVEFORM ASLModWaveform
  {
   get { return (eWAVEFORM) GetValue(ASLModWaveformProperty); }
   set { SetValue(ASLModWaveformProperty, value); }
  }

  #endregion

  #region Control Enabled property

  public static readonly DependencyProperty ControlEnabledProperty = DependencyProperty.Register("ControlEnabled",
                                                                                                 typeof(bool),
                                                                                                 typeof(CycloidGenerator),
                                                                                                 new FrameworkPropertyMetadata(false, OnControlEnabledChanged));
  public bool ControlEnabled
  {
   get { return (bool) GetValue(ControlEnabledProperty); }
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
   ((CycloidGenerator) d).OnControlEnabledChanged(args);
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

  #region Fixed Circle Radius property.

  public static readonly DependencyProperty FixedCircleRadiusProperty = DependencyProperty.Register("FixedCircleRadius",
                                                                                                    typeof(double),
                                                                                                    typeof(CycloidGenerator),
                                                                                                    new PropertyMetadata(0.0));
  public double FixedCircleRadius
  {
   get { return (double) GetValue(FixedCircleRadiusProperty); }
   set { SetValue(FixedCircleRadiusProperty, value); }
  }

  #endregion

  #region Rolling Circle RadiusScan Rate property.

  public static readonly DependencyProperty RollingCircleRadiusProperty = DependencyProperty.Register("RollingCircleRadius",
                                                                                                      typeof(double),
                                                                                                      typeof(CycloidGenerator),
                                                                                                      new PropertyMetadata(0.0));
  public double RollingCircleRadius
  {
   get { return (double) GetValue(RollingCircleRadiusProperty); }
   set { SetValue(RollingCircleRadiusProperty, value); }
  }

  #endregion

  #region Scan Rate property.

  public static readonly DependencyProperty ScanRateProperty = DependencyProperty.Register("ScanRate",
                                                                                           typeof(double),
                                                                                           typeof(CycloidGenerator),
                                                                                           new PropertyMetadata(0.0));
  public double ScanRate
  {
   get { return (double) GetValue(ScanRateProperty); }
   set { SetValue(ScanRateProperty, value); }
  }

  #endregion

  #region Section Title Text property.

  public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                   typeof(string),
                                                                                                   typeof(CycloidGenerator),
                                                                                                   new PropertyMetadata(""));
  public String SectionTitleText
  {
   get { return (String) GetValue(SectionTitleTextProperty); }
   set { SetValue(SectionTitleTextProperty, value); }
  }

  #endregion

  #region Sweep Length property.

  public static readonly DependencyProperty SweepLengthProperty = DependencyProperty.Register("SweepLength",
                                                                                              typeof(double),
                                                                                              typeof(CycloidGenerator),
                                                                                              new PropertyMetadata(0.0));
  public double SweepLength
  {
   get { return (double) GetValue(SweepLengthProperty); }
   set { SetValue(SweepLengthProperty, value); }
  }

  #endregion

  #endregion

  #region Event Handlers

  /// <summary>
  /// Handle BOTH Toggle Switch being clicked.
  /// </summary>
  /// <param name="senderUnused">UI element object triggering the event (unused).</param>
  /// <param name="argsUnused">RoutedEventArgs object to contain any arguments (unused).</param>
  private void AutoSLTSClkd(object senderUnused, RoutedEventArgs argsUnused)
  {
   ASLModEnabled = AutoSWTS.SwitchOn;

   UpdateUI();
  }

  #endregion

  /// <summary>
  /// Ensure the UI reflects the current state of the system.
  /// </summary>
  private void UpdateUI()
  {
   if (true == ControlEnabled)
   {
    ASLLevelRC.ControlEnabled    = ASLModEnabled;
    ASLRateRC.ControlEnabled     = ASLModEnabled;
    WS_AutoSLYMod.ControlEnabled = ASLModEnabled;
   }
   else
   {
    ASLLevelRC.ControlEnabled    = ControlEnabled;
    ASLRateRC.ControlEnabled     = ControlEnabled;
    WS_AutoSLYMod.ControlEnabled = ControlEnabled;
   }

   AutoSWTS.ControlEnabled              = ControlEnabled;
   FixedCircleRadiusRC.ControlEnabled   = ControlEnabled;
   RollingCircleRadiusRC.ControlEnabled = ControlEnabled;
   ScanRateRC.ControlEnabled            = ControlEnabled;
   SweepLengthRC.ControlEnabled         = ControlEnabled;

   Brush activeColor;
   Brush inactiveColor;

   if (true == ControlEnabled)
   {
    activeColor   = Brushes.LightGreen;
    inactiveColor = Brushes.LightPink;
   }
   else
   {
    activeColor   = Brushes.DarkGreen;
    inactiveColor = Brushes.DarkRed;
   }
  }
 }
}
