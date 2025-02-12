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
using System.Windows.Media.Imaging;
using Laserocitor.AudioInterface;
using Laserocitor.Common;
using Laserocitor.Models;
using Laserocitor.Utils.Common;
using Laserocitor.Utils.Diagnostics;
using Laserocitor.Utils.MVVM;
using Laserocitor.ViewModels.Base;
using Laserocitor.Views.DisplayScreen;
using Laserocitor.Views.Monitor;

namespace Laserocitor.ViewModels.Laserocitor
{
    /// <summary>
    /// This class is the View Model for the Laserocitor, responsible for initializing the UI
    /// either from default values or via a Model object obtained from stored values, starting
    /// the laser display rendering and applying changes from UI bound variables to the current
    /// drawing algorithm.
    /// </summary>
    public partial class LaserocitorVM : BaseViewModel
    {
        #region Private internal variables

        private bool bAudioEnabled,        // true = Enable Audio output.
                     bBlanked,             // true = 'display blanked' (current drawing color = current background color).
                     bOK2CloseDispWindow;  // true = Display window can be closed (need to figure out how to remove close buttons).                    

        private DataMonitor monitorWindow;  // The raw data display window.

        private DisplayWindow displayWindow;  // The detached display window.

        private CircularQueue<AudioSample> sampleQueue;  // Circular queue of audio samples derived from the image points.

        private double dColorModOscVal,    // Current color modulation oscillator value (0 - 1535).
                       dColorModOscAVal,   // Current color modulation oscillator A value.
                       dColorModOscBVal,   // Current color modulation oscillator B value.
                       dColorModOscCVal,   // Current color modulation oscillator C value.
                       dChopOscVal,        // Chopper Oscillator value.
                       dCSLOscVal,         // Current value of CSL Oscillator index.
                       dLXMOscVal,         // Current value of the Lissajous X Modulator Oscillator.
                       dLXOsc1Val,         // Current value of the Lissajous X Oscillator 1.
                       dLXOsc2Val,         // Current value of the Lissajous X Oscillator 2.
                       dLYMOscVal,         // Current value of the Lissajous Y Modulator Oscillator.
                       dLYOsc1Val,         // Current value of the Lissajous Y Oscillator 1.
                       dLYOsc2Val,         // Current value of the Lissajous Y Oscillator 1.
                       dModOscCnt,         // Modulator Oscillator Count.
                       dOrbitCos,          // Cosine of current orbit rotation angle.
                       dOrbitSin,          // Sine   of current orbit rotation angle.
                       dOsc2Val,           // Cycloid modulation (Oscillator 2) value.
                       dPosModOscVal,      // Current position modulation oscillator value.
                       dRSPerspectiveOsc,  // Current value of Radial Scan mode Perspective Modulation oscillator.                       
                       dQOsc1Val,          // Current value of Oscillator 1.
                       dSPPerspectiveOsc,  // Current value of Spiral Scan mode Perspective Modulation oscillator.
                       dSPQOscVal,         // Current value of Spiral quadrature (base circle) oscillator.
                       dSPRampOscCnt,      // Current Spiral Ramp Oscillator value.
                       dXAxisCos,          // Cosine of current X-Axis rotation angle.
                       dXAxisSin,          // Sine   of current X-Axis rotation angle.
                       dYAxisCos,          // Cosine of current Y-Axis rotation angle.
                       dYAxisSin,          // Sine   of current Y-Axis rotation angle.
                       dZAxisCos,          // Cosine of current Z-Axis rotation angle.
                       dZAxisSin;          // Sine   of current Z-Axis rotation angle.

        private int iAspectX,        // Image aspect ratio (X-Axis)
                    iAspectY,        // Image aspect ratio (Y-Axis).
                    iBitmapCenterX,  // Center of 'screen' (X).
                    iBitmapCenterY,  // Center of 'screen' (Y).
                    iBitmapHeight,   // Bitmap Height.
                    iBitmapWidth,    // Bitmap Width.
                    iCurX,           // Current computed X display point.
                    iCurY,           // Current computed Y display point.
                    iQOsc1Rate,      // Quadrature Oscillator (1) Rate (frequency) value.
                    iMaxX,           // Maximum X value of image.
                    iMaxY,           // Maximum Y value of image.
                    iMinX,           // Minimum X value of image.
                    iMinY,           // Minimum Y value of image.
                    iSPQOscRate;     // Spiral base circle quadrature oscillator rate (frequency) value.

        private Waveforms waveforms;  // Handle to Waveform Generators.

        private WriteableBitmap oDisplayImage;  // The drawing 'screen'.

        #endregion

        #region Private internal variables backing public properties

        private eBASEGEOMETRY eBaseGeometry;  // Current base geometry for radial scan mode.

        private bool bAutoSweepLenEnabled,    // true = Auto Cycloid Sweep Length modulation enabled.
                     bChopperEnabled,         // true = Beam chopper is enabled.
                     bChopperREnabled,        // true = Red beam chopper is enabled.
                     bChopperGEnabled,        // true = Green beam chopper is enabled.
                     bChopperBEnabled,        // true = Blue beam chopper is enabled.
                     bColorModEnabled,        // true = Color modulation is enabled.
                     bCoupleOrbit,            // true = Orbit angle is coupled to auto position modulation angle.
                     bLXModEnabled,           // true = Lissajous X modulation enabled.
                     bLYModEnabled,           // true = Lissajous Y modulation enabled.
                     bMultGainModEnabled,     // true = Multiplier Gain modulation enabled.
                     bOsc2InvertEnabled,      // true = Invert Oscillator 2 waveform.
                     bPaused,                 // true = Scan generator paused.
                     bPerspectiveModEnabled,  // true = Perspective auto-rotation enabled.
                     bPosModEnabled,          // true = Position modulation enabled.
                     bSpiralPersModEnable;    // true = Spiral perspective modulation enabled.

        private byte cBgColorB,         // Background color (Blue).
                     cBgColorG,         // Background color (Green).
                     cBgColorR,         // Background color (Red).
                     cFgColorB,         // Foreground color (Blue).
                     cFgColorG,         // Foreground color (Green).
                     cFgColorR,         // Foreground color (Red).
                     cMasterIntensity;  // Master intensity.

        private Color oImageColor,   // Image color.
                      oScreenColor;  // Display screen color.

        private eCOLORMODMODE eColorModMode;  // Current Color modulation mode.

        private eCOLORMODOSC eColorModBlue,   // Current color modulator oscillator assignment for the blue laser.
                             eColorModGreen,  // Current color modulator oscillator assignment for the green laser.
                             eColorModRed;    // Current color modulator oscillator assignment for the red laser.

        private eCOLORMODSYNCSOURCE eColorModSyncSource;  // Current Color modulation synchronization source.

        private eDIRECTION eBaseGDirection,       // Base Geometry generator direction.
                           ePersDirection,        // Perspective modulation apparent rotation direction.
                           ePosDirection,         // Position modulation apparent rotation direction.
                           eSpiralDirection,      // Selected Spiral baseline direction.
                           eSpiralPersDirection;  // Selected Spiral Auto Perspective Rotation direction.

        private double dSpiralPersJSXPos,        // Current Spiral Perspective joystick X position value.
                       dSpiralPersJSYPos,        // Current Spiral Perspective joystick Y position value.
                       dSpiralPersModFrequency,  // Spiral Auto Perspective rotation rate.
                       dSpiralPersModLevel,      // Spiral Auto Perspective rotation intensity.
                       dSpiralRampFrequency,     // Spiral ramp rate.
                       dSpiralRampGain,          // Spiral ramp gain.
                       dSpiralRampOffset,        // Spiral ramp offset.
                       dSpiralSize;              // Size of Spiral base circle.

        private int iBeamColorCtlVal,    // Current setting of beam color control.
                    iCSLRate,            // Current setting of CSL Oscillator rate control.
                    iCSLAmpl,            // Current setting of CSL Oscillator amplitude control.
                    iChopOscDutyCycle,   // Chopper oscillator duty cycle setting (1 - 99%).
                    iChopOscFrequency,   // Chopper oscillator frequency.
                    iCycloidL,           // Cycloid generator parameter l.
                    iCycloidRate,        // Cycloid repeat rate (oscillator frequency).
                    iCycloidRf,          // Cycloid generator parameter R(f).
                    iCycloidRr,          // Cycloid generator parameter R(r).
                    iModOscDutyCycle,    // Modulator Oscillator Square Wave Duty Cycle.
                    iAspectCtlVal,       // Aspect Ratio control setting.
                    iColorModFreq,       // Color modulation frequency.
                    iImagePosX,          // Image position (X-Axis).
                    iImagePosY,          // Image position (Y-Axis).
                    iImageSize,          // Absolute image size.
                    iLXModGain,          // Lissajous X Modulation Gain.
                    iLXModOffset,        // Lissajous X Modulation Offset.
                    iLXMOscAmpl,         // Lissajous X Modulator Oscillator Amplitude.  
                    iLXMOscDutyCycle,    // Lissajous X Modulator Oscillator Duty Cycle.
                    iLXMOscFreq,         // Lissajous X Modulator Oscillator Frequency.
                    iLXOsc1Ampl,         // Lissajous X Oscillator 1 Amplitude.  
                    iLXOsc1DutyCycle,    // Lissajous X Oscillator 1 Duty Cycle.
                    iLXOsc1Freq,         // Lissajous X Oscillator 1 Frequency.
                    iLXOsc2Ampl,         // Lissajous X Oscillator 2 Amplitude.  
                    iLXOsc2DutyCycle,    // Lissajous X Oscillator 2 Duty Cycle.
                    iLXOsc2Freq,         // Lissajous X Oscillator 2 Frequency.
                    iLYModGain,          // Lissajous Y Modulation Gain.
                    iLYModOffset,        // Lissajous Y Modulation Offset.
                    iLYMOscAmpl,         // Lissajous Y Modulator Oscillator Amplitude. 
                    iLYMOscDutyCycle,    // Lissajous Y Modulator Oscillator Duty Cycle. 
                    iLYMOscFreq,         // Lissajous Y Modulator Oscillator Frequency.
                    iLYOsc1Ampl,         // Lissajous Y Oscillator 1 Amplitude.  
                    iLYOsc1DutyCycle,    // Lissajous Y Oscillator 1 Duty Cycle.
                    iLYOsc1Freq,         // Lissajous Y Oscillator 1 Frequency.
                    iLYOsc2Ampl,         // Lissajous Y Oscillator 2 Amplitude.  
                    iLYOsc2DutyCycle,    // Lissajous Y Oscillator 2 Duty Cycle.
                    iLYOsc2Freq,         // Lissajous Y Oscillator 2 Frequency.
                    iModMulGain,         // Modulator Multiplier Gain.
                    iModOscAmpl,         // Amplitude of Modulator Oscillator.
                    iModOscOffset,       // Modulator Oscillator baseline offset angle.
                    iModOscFreq,         // Modulator Oscillator Frequency.
                    iOrbitAngle,         // Orbit angle.
                    iOscAFreq,           // Oscillator A frequency value.  
                    iOscBFreq,           // Oscillator B frequency value.
                    iOscCFreq,           // Oscillator C frequency value.
                    iOsc2Level,          // Oscillator 2 Level (amplitude) value.    
                    iOsc2Rate,           // Oscillator 2 Rate (frequency) value.
                    iPersistence,        // Persistence of vision time.
                    iPerspectiveRate,    // Perspective Modulation Rate.
                    iPerspectiveLevel,   // Perspective Modulation Level.
                    iPosModFreq,         // Position Modulation frequency.
                    iPosModLevel,        // Position Modulation Level (radius, effectively).
                    iGenBaseSize,        // Amplitude of Quadrature Oscillator 1 (Baseline circle generator).
                    iGenPhase,           // Quadrature Oscillator 1 Phase shift angle.
                    iPerspectiveCtlH,    // Perspective control horizontal setting.
                    iPerspectiveCtlV,    // Perspective control vertical setting.
                    iPerspectiveX,       // X-Axis Perspective shift.
                    iPerspectiveY,       // Y-Axis Perspective shift.
                    iRotAxisX,           // Image X-Axis rotation angle.
                    iRotAxisY,           // Image Y-Axis rotation angle.
                    iRotAxisZ,           // Image Z-Axis rotation angle.
                    iScreenColorCtlVal,  // Current setting of combined screen color control.
                    iZoom;               // Zoom.

        private eMODE eMode;  // Current image generation mode.

        private eWAVEFORM eCSLOscWave,  // Current waveform selection for Cycloid Sweep Length Oscillator.
                          eOSC1Wave,    // Current waveform selection for Oscillator 1.
                          eOSC2Wave,    // Current waveform selection for Oscillator 2.
                          eOSCXLMWave,  // Current waveform selection for Lissajous X Modulator Oscillator.
                          eOSCXLWave1,  // Current waveform selection for Lissajous X Oscillator 1.
                          eOSCYLMWave,  // Current waveform selection for Lissajous Y Modulator Oscillator.
                          eOSCYLWave1;  // Current waveform selection for Lissajous Y Oscillator 1.

        private string strImageFileName;  // Image save file name.
        #endregion

        #region Button/Command Relay Commands

        public RelayCommand ClearCommand       { get; set; }
        public RelayCommand LoadCommand        { get; set; }
        public RelayCommand PauseCommand       { get; set; }
        public RelayCommand ResetCommand       { get; set; }
        public RelayCommand SaveCommand        { get; set; }
        public RelayCommand ShowDetailsCommand { get; set; }
        public RelayCommand ToggleAudioCommand { get; set; }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle event triggered when the Monitor window is being closed.
        /// </summary>
        /// <param name="senderUnused">Object invoking the event (unused).</param>
        /// <param name="args">Arguments associated with the event (unused).</param>
        public void DisplayWindowClosing(object senderUnused, CancelEventArgs args)
        {
            if (true == bOK2CloseDispWindow)
            {
                return;
            }

            Utils.Common.CommonUtils.ShowMessage("Laserocitor", "To exit the program, close the Main Window.");

            args.Cancel = true;
        }

        /// <summary>
        /// Draw the simulated laser image using the current drawing mode and
        /// associated parameters.
        /// </summary>
        /// <remarks>
        /// Called at approximately the video vertical refresh rate.
        /// </remarks>
        /// <param name="senderUnused">Object invoking the event (unused).</param>
        /// <param name="argsUnused">Arguments associated with the event (unused).</param>
        private void DrawImage(object senderUnused, EventArgs argsUnused)
        {
            Draw();
        }

        /// <summary>
        /// Handle event triggered when the Main window is being closed
        /// (app shutting down).
        /// </summary>
        /// <param name="senderUnused">Object invoking the event (unused).</param>
        /// <param name="argsUnused">Arguments associated with the event (unused).</param>
        public void MainWindowClosing(object senderUnused, CancelEventArgs argsUnused)
        {
            if (null != displayWindow)
            {
                bOK2CloseDispWindow = true;

                displayWindow.Close();
            }

            if (null != monitorWindow)
            {
                monitorWindow.Close();
            }
        }

        /// <summary>
        /// Handle event triggered when the Monitor window is being closed.
        /// </summary>
        /// <param name="senderUnused">Object invoking the event (unused).</param>
        /// <param name="argsUnused">Arguments associated with the event (unused).</param>
        public void MonitorWindowClosing(object senderUnused, CancelEventArgs argsUnused)
        {
            monitorWindow = null;
        }

        #endregion

        #region Constructors/Initializers

        /// <summary>
        /// Constructor, given a LaserocitorModel object.
        /// </summary>
        /// <param name="objLaserocitorModel">LaserocitorModel object containing all the values needed.</param>
        public LaserocitorVM(LaserocitorModel objLaserocitorModel)
        { 
            Init(objLaserocitorModel);

            AudioSampleWriter.GetInstance().Start(sampleQueue);

            // Start rendering loop by adding an event handler which will update the display
            // screen at approximately the video refresh rate via the Draw() event handler:

            CompositionTarget.Rendering += DrawImage;
        }

        /// <summary>
        /// Populate the Laserocitor View Model from the Laserocitor
        /// Model.
        /// </summary>
        /// <param name="model">LaserocitorModel object to be incorporated into the View Model.</param>
        private void IncorporateModel(LaserocitorModel model)
        {
            if (null == model)
            {
                return;
            }

            AutoCSLEnabled        = model.AutoCSLEnabled;
            ChopperBEnabled       = model.ChopperBEnabled;
            ChopperEnabled        = model.ChopperEnabled;
            ChopperGEnabled       = model.ChopperGEnabled;
            ChopperREnabled       = model.ChopperREnabled;
            ColorModEnabled       = model.ColorModEnabled;
            CoupleOrbit           = model.CoupleOrbit;
            LXModEnabled          = model.LXModEnabled;
            LYModEnabled          = model.LYModEnabled;
            MultGainModEnabled    = model.MultGainModEnabled;
            Osc2InvertEnabled     = model.Osc2InvertEnabled;
            PerspectiveModEnabled = model.PerspectiveModEnabled;
            PosModEnabled         = model.PosModEnabled;
            bSpiralPersModEnable  = model.SpiralPersModEnable;

            BgColorB        = model.BgColorB;
            BgColorG        = model.BgColorG;       
            BgColorR        = model.BgColorR;       
            FgColorB        = model.FgColorB;       
            FgColorG        = model.FgColorG;
            FgColorR        = model.FgColorR;
            MasterIntensity = model.MasterIntensity;

            RadiatorAMAmpl          = model.RadiatorAMAmpl;
            RadiatorAMFreq          = model.RadiatorAMFreq;
            TranslationJoystickXPos = model.TranslationJoystickXPos;
            TranslationJoystickYPos = model.TranslationJoystickYPos; 

            RSBaseGeometry         = model.RSBaseGeometry;
            ColorModMode           = model.ColorModMode;
            ColorModOscAssignBlue  = model.ColorModOscAssignBlue;
            ColorModOscAssignGreen = model.ColorModOscAssignGreen;
            ColorModOscAssignRed   = model.ColorModOscAssignRed;  
            ColorModSyncSource     = model.ColorModSyncSource;    
            AutoPosDir             = model.AutoPosDir;           
            BaseGDirection         = model.BaseGDirection;        
            PersModDir             = model.PersModDir;           
            ImageDrawingMode       = model.ImageDrawingMode;      
            ASLModOscWave          = model.ASLModOscWave;         
            LMXModOscWave          = model.LMXModOscWave;        
            LMYModOscWave          = model.LMYModOscWave;         
            LMXOscWave             = model.LMXOscWave;           
            LMYOscWave             = model.LMYOscWave;           
            Osc1Waveform           = model.Osc1Waveform;          
            Osc2Waveform           = model.Osc2Waveform;
            SpiralDirection        = model.SpiralDirection;
            SpiralPersDirection    = model.SpiralPersDirection;
                 
            AspectRatio            = model.AspectRatio;
            AutoPersLevel          = model.AutoPersLevel;    
            AutoPersRate           = model.AutoPersRate;   
            AutoPosLevel           = model.AutoPosLevel;     
            AutoPosRate            = model.AutoPosRate;      
            AxisRotX               = model.AxisRotX;         
            AxisRotY               = model.AxisRotY;         
            AxisRotZ               = model.AxisRotZ;         
            ChopperDutyCycle       = model.ChopperDutyCycle;
            ChopperFrequency       = model.ChopperFrequency; 
            ColorModFreq           = model.ColorModFreq;     
            CSLOscAmpl             = model.CSLOscAmpl;       
            CSLOscFreq             = model.CSLOscFreq;       
            CycloidL               = model.CycloidL;         
            CycloidRate            = model.CycloidRate;      
            CycloidRf              = model.CycloidRf;        
            CycloidRr              = model.CycloidRr;        
            GenPhase               = model.GenPhase;         
            ImageColor             = model.ImageColor;       
            ImagePosHoriz          = model.ImagePosHoriz;    
            ImagePosVert           = model.ImagePosVert;     
            ImageSize              = model.ImageSize;        
            LXModGain              = model.LXModGain;        
            LXModOffset            = model.LXModOffset;      
            LXMOscAmplitude        = model.LXMOscAmplitude;  
            LXMOscDutyCycle        = model.LXMOscDutyCycle;  
            LXMOscFreq             = model.LXMOscFreq;       
            LXOsc1Amplitude        = model.LXOsc1Amplitude;  
            LXOsc1DutyCycle        = model.LXOsc1DutyCycle;  
            LXOsc1Freq             = model.LXOsc1Freq;       
            LYModGain              = model.LYModGain;        
            LYModOffset            = model.LYModOffset;      
            LYMOscAmplitude        = model.LYMOscAmplitude;  
            LYMOscDutyCycle        = model.LYMOscDutyCycle;  
            LYMOscFreq             = model.LYMOscFreq;       
            LYOsc1Amplitude        = model.LYOsc1Amplitude;  
            LYOsc1DutyCycle        = model.LYOsc1DutyCycle;  
            LYOsc1Freq             = model.LYOsc1Freq;       
            LXOsc2Amplitude        = model.LXOsc2Amplitude;  
            LXOsc2DutyCycle        = model.LXOsc2DutyCycle;  
            LXOsc2Freq             = model.LXOsc2Freq;       
            LYOsc2Amplitude        = model.LYOsc2Amplitude;  
            LYOsc2DutyCycle        = model.LYOsc2DutyCycle;  
            LYOsc2Freq             = model.LYOsc2Freq;       
            ModOscDutyCycle        = model.ModOscDutyCycle;  
            ModOscAmp              = model.ModOscAmp;        
            ModOscFreq             = model.ModOscFreq;       
            ModOscGain             = model.ModOscGain;       
            ModOscOffset           = model.ModOscOffset;     
            Orbit                  = model.Orbit;            
            Osc2Freq               = model.Osc2Freq;         
            Osc2Level              = model.Osc2Level;        
            OscAFreq               = model.OscAFreq;         
            OscBFreq               = model.OscBFreq;         
            OscCFreq               = model.OscCFreq;               
            PerspectiveCtlH        = model.PerspectiveCtlH;  
            PerspectiveCtlV        = model.PerspectiveCtlV;
            Persistence            = model.Persistence;
            QuadOsc1Level          = model.QuadOsc1Level;    
            QuadOsc1Freq           = model.QuadOsc1Freq;     
            ScreenColor            = model.ScreenColor;
            SpiralPersJSXPos       = model.SpiralPersJSXPos;
            SpiralPersJSYPos       = model.SpiralPersJSYPos;
            SpiralPersModFrequency = model.SpiralPersModFrequency;
            SpiralPersModLevel     = model.SpiralPersModLevel;
            SpiralRampFrequency    = model.SpiralRampFrequency;
            SpiralRampGain         = model.SpiralRampGain;
            SpiralRampOffset       = model.SpiralRampOffset;
            SpiralSize             = model.SpiralSize;
            Zoom                   = model.Zoom;

            ImageName = model.ImageName;
        }

        /// <summary>
        /// Apply values from a Model to the View Model.
        /// </summary>
        /// <param name="objLaserocitorModel">LaserocitorModel containing the properties to be applied.</param>
        private void Init(LaserocitorModel objLaserocitorModel)
        {
            bAudioEnabled       = false;
            bOK2CloseDispWindow = false;

            sampleQueue = new CircularQueue<AudioSample>(2048, false);

            Application.Current.MainWindow.Closing += MainWindowClosing;

            waveforms = new Waveforms();  // Create and initialize Waveform arrays.
            
            // Hook up Relay Commands to their associated handler functions:  

            ClearCommand       = new RelayCommand(param => OnClearCommand());
            LoadCommand        = new RelayCommand(param => OnLoadCommand());
            PauseCommand       = new RelayCommand(param => OnPauseCommand());
            ResetCommand       = new RelayCommand(param => OnResetCommand());
            SaveCommand        = new RelayCommand(param => OnSaveCommand());
            ShowDetailsCommand = new RelayCommand(param => OnShowDetailsCommand());
            ToggleAudioCommand = new RelayCommand(param => OnToggleAudioCommand());

            // Initialize the WriteableBitmap and open the
            // simulated laser display window: 

            oDisplayImage = BitmapFactory.New(500, 500);

            displayWindow = new DisplayWindow();

            displayWindow.DataContext = this;
            displayWindow.Closing += DisplayWindowClosing;
            displayWindow.Show();  

            iBitmapHeight = oDisplayImage.PixelHeight;
            iBitmapWidth  = oDisplayImage.PixelWidth;

            iBitmapCenterX = iBitmapWidth  / 2;  // Center X Coordinate of bitmap.
            iBitmapCenterY = iBitmapHeight / 2;  // Center Y Coordinate of bitmap.

            iCurX = iBitmapCenterX;  // Current computed X display point.
            iCurY = iBitmapCenterY;  // Current computed Y display point.

            iMaxX = iBitmapWidth;
            iMaxY = iBitmapHeight;
            iMinX = 0;
            iMinY = 0;

            oImageColor  = Color.FromArgb(255, 255, 0, 0);  // Initial drawing color = red.
            oScreenColor = Color.FromArgb(255, 0, 0, 0);    // Initial screen background color = black.

            IncorporateModel(objLaserocitorModel);
        }

        #endregion

        #region Button/Command Handlers

        /// <summary>
        /// User selected the Clear Screen menu option, so clear the screen.
        /// </summary>
        public void OnClearCommand()
        {
            Clear(oScreenColor);
        }

        /// <summary>
        /// User selected the Load menu option, so attempt to load and use a Model.
        /// </summary>
        private void OnLoadCommand()
        {
            LaserocitorModel model = Utils.Persistence.DataRepository.GetInstance().LoadModel();

            IncorporateModel(model);
        }

        /// <summary>
        /// User selected the Pause menu option, so resume/suspend drawing operations.
        /// </summary>
        private void OnPauseCommand()
        {
            bPaused = !bPaused;
        }

        /// <summary>
        /// User selected the Reset Screen menu option, so reset the View Model
        /// using a default model.
        /// </summary>
        private void OnResetCommand()
        {
            IncorporateModel(new LaserocitorModel());
        }

        /// <summary>
        /// User selected the Save menu option, so generate a Model object from
        /// the current View Model and save it.
        /// </summary>
        private void OnSaveCommand()
        {
            Utils.Persistence.DataRepository.GetInstance().SaveModel(new LaserocitorModel(this), displayWindow);
        }

        /// <summary>
        /// User selected the Toggle Audio Output option, so turn audio off/on.
        /// </summary>
        private void OnToggleAudioCommand()
        {
            if (false == bAudioEnabled)
            {
                if (MessageBoxResult.OK != CommonUtils.ShowPrompt("Enable Audio Output",
                                                                  "Check volume level before enabling audio."))
                {
                    return;
                }
            }

            bAudioEnabled = !bAudioEnabled;
        }

        /// <summary>
        /// User selected the Show Details option, so open the Monitor window.
        /// </summary>
        private void OnShowDetailsCommand()
        {
            if (null == monitorWindow) 
            {
                monitorWindow = new DataMonitor();
            }

            if (false == monitorWindow.IsLoaded)
            {
                monitorWindow.Closing += MonitorWindowClosing;

                monitorWindow.DataContext = this;

                monitorWindow.Show();
            }
        }

        #endregion

        #region Public Properties

        public WriteableBitmap DisplayImage
        {
            get
            {
                return oDisplayImage;
            }
            set
            {
                oDisplayImage = value;
                OnPropertyChanged("DisplayImage");
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Clear screen to defined color.
        /// </summary>
        /// <remarks>
        /// This function clears the 'screen' (the writeable bitmap) to a the  
        /// current screen color selected by the user, provided by the caller. 
        /// </remarks>
        /// <param name="oColor">Color object containing desired screen color.</param>
        private unsafe void Clear(Color oColor)
        {
            if (null == oDisplayImage)
            {
                return;  // The function can get called before the writable bitmap is created, so don't
                         // try to use it if it isn't there yet.
            }

            using (BitmapContext bitmapContext = oDisplayImage.GetBitmapContext())
            {
                int iColor = (oColor.A << 24) | (oColor.R << 16) | (oColor.G << 8) | oColor.B;

                var aPixelArray = bitmapContext.Pixels;

                int iPixelArraySize = bitmapContext.Length;

                for (int iPixelNdx = 0; iPixelNdx < iPixelArraySize; ++iPixelNdx)
                {
                    aPixelArray[iPixelNdx] = iColor;
                }
            }            
        }

        /// <summary>
        /// Based on the current imaging mode, draw one cycle of an image.
        /// </summary>
        private void Draw()
        {
            switch (eMode)
            {
                case eMODE.eCycloid:
                    {
                        DrawCycloid();
                    }
                    break;

                case eMODE.eLissajous:
                    {
                        DrawLissajous();
                    }
                    break;

                case eMODE.eRadialScan:
                    {
                        DrawRadialScan();
                    }
                    break;

                case eMODE.eSpiral:
                    {
                        DrawSpiralScan();
                    }
                    break;
            }

            if (false == bPaused)
            {
                Fade();  // Simulate persistence of vision.
            }
        }

        /// <summary>
        /// Draws a Cycloid based on the current controller parameters.
        ///  
        /// x[a] = (Rf - Rr) * cos(a) + l * cos((Rf - Rr) / Rr) * a)
        /// y[a] = (Rf - Rr) * sin(a) - l * sin((Rf - Rr) / Rr) * a) 
        ///
        /// Where:
        ///
        ///  a = Angle.
        /// Rf = Radius of Fixed circle.
        /// Rr = Radius of Rolling circle.
        ///  l = Length of Draw bar. 
        ///     
        ///  4. Apply Aspect Ratio and Size multipliers:
        /// 
        ///     x =  (AspectX / 100) * (Size / 100)
        ///     y =  (AspectY / 100) * (Size / 100)
        ///     
        ///  5. Apply x, y and z axis rotations
        ///  
        ///  6. Apply translation and orbit.
        ///  
        ///  7. Apply chop.
        ///  
        ///  8. Apply color modulation.
        ///  
        ///  9. Erase previously scanned portions of the image.
        ///  
        /// </summary>
        private void DrawCycloid()
        {
            // Wrap updates in a GetContext call, to prevent invalidation 
            // and nested locking/unlocking during this block:

            using (oDisplayImage.GetBitmapContext())
            {
                double newX,  // New computed X display position.
                       newY;  // New computed Y display position.

                int iMaxIterations = 720;

                while (false == bPaused && 0 != iMaxIterations--)
                {
                    double dCycloidL = iCycloidL;

                    if (true == bAutoSweepLenEnabled)
                    {
                        switch (eCSLOscWave)
                        {
                            case eWAVEFORM.eRamp:
                                {
                                    dCycloidL = iCSLAmpl * waveforms.GetRampOscSqVal((uint)dCSLOscVal);
                                }
                                break;

                            case eWAVEFORM.eRectSin:
                                {
                                    dCycloidL = iCSLAmpl * waveforms.GetRectSinOscSqVal((uint)dCSLOscVal);
                                }
                                break;

                            case eWAVEFORM.eSawtooth:
                                {
                                    dCycloidL = iCSLAmpl * waveforms.GetSawOscVal((uint)dCSLOscVal);
                                }
                                break;

                            case eWAVEFORM.eSin:
                                {
                                    dCycloidL = iCSLAmpl * Math.Sin(((uint)(dCSLOscVal) * Constants.PIDIV180));
                                }
                                break;

                            case eWAVEFORM.eSquare:
                                {
                                    dCycloidL = iCSLAmpl * waveforms.GetModOscSqVal((uint)dCSLOscVal);
                                }
                                break;

                            case eWAVEFORM.eTriangle:
                                {
                                    dCycloidL = iCSLAmpl * waveforms.GetTriOscVal((uint)dCSLOscVal);
                                }
                                break;
                                
                            default:
                                {
                                    DebugHelpers.AssertBadEnum((int)eCSLOscWave);
                                }
                                break;
                        }
                    }

                    dCSLOscVal += iCSLRate / 10000.0;

                    dModOscCnt += iCycloidRate / 1000.0;
                
                    double Rdiff = iCycloidRf - iCycloidRr;

                    double iAngleRad = (dModOscCnt * Constants.PIDIV180);

                    double term2 = (Rdiff / iCycloidRr * dModOscCnt) * Constants.PIDIV180;

                    newX = Rdiff * Math.Cos(iAngleRad) + dCycloidL * Math.Cos(term2);
                    newY = Rdiff * Math.Sin(iAngleRad) - dCycloidL * Math.Sin(term2);

                    // Apply aspect ratio and size controls:

                    newX = newX * iAspectX / 100.0;
                    newY = newY * iAspectY / 100.0;

                    newX = newX * iImageSize / 100.0;
                    newY = newY * iImageSize / 100.0;


                    // Apply rotations:

                    int zRotX = (int)newX,  // Computed X display position, based on Z-Axis rotation.
                        zRotY = (int)newY;  // Computed Y display position, based on Z-Axis rotation.

                    RotateAndTranslate(ref zRotX, ref zRotY);

 
                    // Draw image:

                    DrawImageSegment(zRotX, zRotY);
                }

                // Invalidates on exit of using block.

                dOsc2Val += iOsc2Rate;
            }
        }

        /// <summary>
        /// Draws a line segment from the end of the last line segment drawn
        /// up to the location of the provided endpoint, applying chopper and
        /// color modulation modifications as currently enabled and computed.
        /// </summary>
        private void DrawImageSegment(int zRotX, int zRotY)
        {
            if ((eMODE.eRadialScan == eMode && dQOsc1Val > 0)    || 
                (eMODE.eLissajous  == eMode && dLXOsc1Val > 0)   || 
                (eMODE.eCycloid    == eMode && dModOscCnt > 0.0) ||
                (eMODE.eSpiral     == eMode && dModOscCnt > 0.0))
            {
                // Only draw iterations from n+1 (Don't draw first line segment from origin)

                // Handle image blanking:

                if (true == bChopperEnabled)
                {
                    uint iChopArrayNdx = (uint)(dChopOscVal) % 359;

                    if (-1 == waveforms.GetChopperOscSqVal(iChopArrayNdx))
                    {
                        bBlanked = true;
                    }
                    else
                    {
                        bBlanked = false;
                    }
                }

                if (true == bColorModEnabled)
                {
                    // Handle color modulation:

                    if (eColorModMode == eCOLORMODMODE.eSynchronized)
                    {
                        switch (eColorModSyncSource)
                        {
                            case eCOLORMODSYNCSOURCE.eColorMod:
                                {
                                    dColorModOscVal += iColorModFreq / 1000.0;
                                }
                                break;

                            case eCOLORMODSYNCSOURCE.eCycloidMod:
                                {
                                    dColorModOscVal = dCSLOscVal;
                                }
                                break;

                            case eCOLORMODSYNCSOURCE.eCycloidScan:
                                {
                                    dColorModOscVal = dModOscCnt / 10.0;
                                }
                                break;

                            case eCOLORMODSYNCSOURCE.eRSBaseGeo:
                                {
                                    dColorModOscVal = dQOsc1Val / 100.0;
                                }
                                break;

                            case eCOLORMODSYNCSOURCE.eRSBaseRad:
                                {
                                    dColorModOscVal = dModOscCnt / 100.0;
                                }
                                break;

                            case eCOLORMODSYNCSOURCE.eRSBaseMod:
                                {
                                    dColorModOscVal = dOsc2Val;
                                }
                                break;

                            default:
                                {
                                    DebugHelpers.AssertBadEnum((int)eColorModSyncSource);
                                }
                                break;
                        }

                        dColorModOscVal %= 1535;

                        oImageColor = CommonUtils.ColorInt2RGB((int)dColorModOscVal, cMasterIntensity);
                    } 
                    else 
                    {
                        dColorModOscAVal += iOscAFreq / 1000.0;
                        dColorModOscBVal += iOscBFreq / 1000.0;
                        dColorModOscCVal += iOscCFreq / 1000.0;

                        double dColorModOscAValSin = Math.Abs(Math.Sin(dColorModOscAVal * Constants.PIDIV180)),
                               dColorModOscBValSin = Math.Abs(Math.Sin(dColorModOscBVal * Constants.PIDIV180)),
                               dColorModOscCValSin = Math.Abs(Math.Sin(dColorModOscCVal * Constants.PIDIV180));

                        int blueVal  = 0,
                            greenVal = 0,
                            redVal   = 0;

                        switch (eColorModBlue)
                        {
                            case eCOLORMODOSC.eOSCANEG:
                                {
                                    blueVal = (int)(dColorModOscAValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCAPOS:
                                {
                                    blueVal = 255 - (int)(dColorModOscAValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCBNEG:
                                {
                                    blueVal = (int)(dColorModOscBValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCBPOS:
                                {
                                    blueVal = 255 - (int)(dColorModOscBValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCCNEG:
                                {
                                    blueVal = (int)(dColorModOscCValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCCPOS:
                                {
                                    blueVal = 255 - (int)(dColorModOscCValSin * 255);
                                }
                                break;
                        }

                        switch (eColorModGreen)
                        {
                            case eCOLORMODOSC.eOSCANEG:
                                {
                                    greenVal = (int)(dColorModOscAValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCAPOS:
                                {
                                    greenVal = 255 - (int)(dColorModOscAValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCBNEG:
                                {
                                    greenVal = (int)(dColorModOscBValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCBPOS:
                                {
                                    greenVal = 255 - (int)(dColorModOscBValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCCNEG:
                                {
                                    greenVal = (int)(dColorModOscCValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCCPOS:
                                {
                                    greenVal = 255 - (int)(dColorModOscCValSin * 255);
                                }
                                break;
                        }

                        switch (eColorModRed)
                        {
                            case eCOLORMODOSC.eOSCANEG:
                                {
                                    redVal = (int)(dColorModOscAValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCAPOS:
                                {
                                    redVal = 255 - (int)(dColorModOscAValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCBNEG:
                                {
                                    redVal = (int)(dColorModOscBValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCBPOS:
                                {
                                    redVal = 255 - (int)(dColorModOscBValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCCNEG:
                                {
                                    redVal = (int)(dColorModOscCValSin * 255);
                                }
                                break;

                            case eCOLORMODOSC.eOSCCPOS:
                                {
                                    redVal = 255 - (int)(dColorModOscCValSin * 255);
                                }
                                break;
                        }

                        oImageColor = Color.FromArgb(255, 
                            (byte)Math.Abs(redVal   * cMasterIntensity / 100.0),
                            (byte)Math.Abs(greenVal * cMasterIntensity / 100.0),
                            (byte)Math.Abs(blueVal  * cMasterIntensity / 100.0));
                    }
                }
                else
                {
                    oImageColor = Color.FromArgb(255, (byte)(cFgColorR * cMasterIntensity / 100.0),
                                                      (byte)(cFgColorG * cMasterIntensity / 100.0),
                                                      (byte)(cFgColorB * cMasterIntensity / 100.0));
                }


                // Decide whether or not to 'turn on the laser beam':

                if (true == bBlanked)
                {
                    byte  iRed,
                          iGreen,
                          iBlue;

                    if (true == bChopperREnabled)
                    {
                        iRed = 0;
                    }
                    else
                    {
                        iRed = oImageColor.R;
                    }

                    if (true == bChopperGEnabled)
                    {
                        iGreen = 0;
                    }
                    else
                    {
                        iGreen = oImageColor.G;
                    }

                    if (true == bChopperBEnabled)
                    {
                        iBlue = 0;
                    }
                    else
                    {
                        iBlue = oImageColor.B;
                    }
                   
                    oImageColor = Color.FromArgb(255, iRed, iGreen, iBlue);
                }
                else
                {
                    if (true == bColorModEnabled)
                    {
                        //oImageColor = ColorInt2RGB((int)dColorModOscVal);
                    }
                    else
                    {
                        oImageColor = Color.FromArgb(255, (byte)(cFgColorR * cMasterIntensity / 100.0),
                                                          (byte)(cFgColorG * cMasterIntensity / 100.0),
                                                          (byte)(cFgColorB * cMasterIntensity / 100.0));
                    }
                }

                // Draw a line segment:

                oDisplayImage.DrawLine(iCurX, iCurY, zRotX, zRotY, oImageColor);
            }

            iCurX = zRotX;
            iCurY = zRotY;

            if (iCurX < iMinX)
            {
                iCurX = iMinX - 1;
            }

            if (iCurY < iMinY)
            {
                iCurY = iMinY - 1;
            }

            if (iCurX > iMaxX)
            {
                iCurX = iMaxX + 1;
            }

            if (iCurY > iMaxY)
            {
                iCurY = iMaxY + 1;
            }

            dChopOscVal += iChopOscFrequency / 1000.0;

            if (false == bAudioEnabled)
            {
                return;  // Shhh...
            }

            int baseOffsetX = UInt16.MaxValue / 2 - iBitmapCenterX;
            int baseOffsetY = UInt16.MaxValue / 2 - iBitmapCenterY;

            try
            {
                if (false == sampleQueue.IsFull())
                {
                    sampleQueue.EnQueue(new AudioSample((UInt16)(iCurX + baseOffsetX), (UInt16)(iCurY + baseOffsetY)));
                }
                else
                {
                    sampleQueue.ClearQueue();
                }
            }
            catch (InvalidOperationException ex)
            {
                // ReSharper disable once PossibleNullReferenceException
                Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eWarning,
                                                        LogMessage.LogMessageType.eException,
                                                        GetType().FullName,
                                                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                        ex.ToString()));
            }
        }

        /// <summary>
        /// Draws a lissajous figure based on the current controller parameters.
        /// </summary>
        private void DrawLissajous()
        {
            // Wrap updates in a GetContext call, to prevent invalidation 
            // and nested locking/unlocking during this block:

            using (oDisplayImage.GetBitmapContext())
            {
                double  newX = iCurX,  // New computed X display position.
                        newY = iCurY;  // New computed Y display position.

                int iMaxIterations = 1000;

                while (false == bPaused && 0 != iMaxIterations--)
                {
                    // Increment all the oscillator values based on their corresponding frequency control settings:

                    dLXMOscVal += iLXMOscFreq / 1000.0;
                    dLXOsc1Val += iLXOsc1Freq / 1000.0;
                    dLXOsc2Val += iLXOsc2Freq / 1000.0;
                    dLYMOscVal += iLYMOscFreq / 1000.0;
                    dLYOsc1Val += iLYOsc1Freq / 1000.0;
                    dLYOsc2Val += iLYOsc2Freq / 1000.0;
                    
                    switch (eOSCXLWave1)
                    {
                        case eWAVEFORM.eRamp:
                            {
                                newX = waveforms.GetRampOscSqVal((uint)dLXOsc1Val);
                            }
                            break;

                        case eWAVEFORM.eRectSin:
                            {
                                newX = waveforms.GetRectSinOscSqVal((uint)dLXOsc1Val);
                            }
                            break;

                        case eWAVEFORM.eSin:
                            {
                                newX = Math.Sin(((uint)(dLXOsc1Val) * Constants.PIDIV180));
                            }
                            break;

                        case eWAVEFORM.eSawtooth:
                            {
                                newX = waveforms.GetSawOscVal((uint)dLXOsc1Val);
                            }
                            break;

                        case eWAVEFORM.eTriangle:
                            {
                                newX = waveforms.GetTriOscVal((uint)dLXOsc1Val);
                            }
                            break;

                        case eWAVEFORM.eVarDutySquare:
                            {
                                newX = waveforms.GetLXMOsc1SqVal((uint)dLXOsc1Val);
                            }
                            break;
                    }


                    double dXOscMultVal = 0.0;

                    switch (eOSCXLMWave)
                    {
                        case eWAVEFORM.eRamp:
                            {
                                dXOscMultVal = waveforms.GetRampOscSqVal((uint)dLXMOscVal);
                            }
                            break;

                        case eWAVEFORM.eRectSin:
                            {
                                dXOscMultVal = waveforms.GetRectSinOscSqVal((uint)dLXMOscVal);
                            }
                            break;

                        case eWAVEFORM.eSin:
                            {
                                dXOscMultVal = Math.Sin(((uint)(dLXMOscVal) * Constants.PIDIV180));
                            }
                            break;

                        case eWAVEFORM.eSawtooth:
                            {
                                dXOscMultVal = waveforms.GetSawOscVal((uint)dLXMOscVal);
                            }
                            break;

                        case eWAVEFORM.eTriangle:
                            {
                                dXOscMultVal = waveforms.GetTriOscVal((uint)dLXMOscVal);
                            }
                            break;

                        case eWAVEFORM.eVarDutySquare:
                            {
                                dXOscMultVal = waveforms.GetLXMOscSqVal((uint)dLXMOscVal);
                            }
                            break;
                    }

                    if (true == bLXModEnabled)
                    {
                        double dXModVal = iLXOsc1Ampl * dXOscMultVal * (iLXMOscAmpl / 100.0);

                        dXModVal += iLXModOffset;

                        newX *= dXModVal * iLXModGain / 100.0;
                    }
                    else
                    {
                        newX *= iLXOsc1Ampl;
                    }

                    switch (eOSCYLWave1)
                    {
                        case eWAVEFORM.eRamp:
                            {
                                newY = waveforms.GetRampOscSqVal((uint)dLYOsc1Val);
                            }
                            break;

                        case eWAVEFORM.eRectSin:
                            {
                                newY = waveforms.GetRectSinOscSqVal((uint)dLYOsc1Val);
                            }
                            break;

                        case eWAVEFORM.eSin:
                            {
                                newY = Math.Sin(((uint)(dLYOsc1Val) * Constants.PIDIV180));
                            }
                            break;

                        case eWAVEFORM.eSawtooth:
                            {
                                newY = waveforms.GetSawOscVal((uint)dLYOsc1Val);
                            }
                            break;

                        case eWAVEFORM.eTriangle:
                            {
                                newY = waveforms.GetTriOscVal((uint)dLYOsc1Val);
                            }
                            break;

                        case eWAVEFORM.eVarDutySquare:
                            {
                                newY = waveforms.GetLXMOsc1SqVal((uint)dLYOsc1Val);
                            }
                            break;
                    }

                    double dYOscMultVal = 0.0;

                    switch (eOSCYLMWave)
                    {
                        case eWAVEFORM.eRamp:
                            {
                                dYOscMultVal = waveforms.GetRampOscSqVal((uint)dLYMOscVal);
                            }
                            break;

                        case eWAVEFORM.eRectSin:
                            {
                                dYOscMultVal = waveforms.GetRectSinOscSqVal((uint)dLYMOscVal);
                            }
                            break;

                        case eWAVEFORM.eSin:
                            {
                                dYOscMultVal = Math.Sin(((uint)(dLYMOscVal) * Constants.PIDIV180));
                            }
                            break;

                        case eWAVEFORM.eSawtooth:
                            {
                                dYOscMultVal = waveforms.GetSawOscVal((uint)dLYMOscVal);
                            }
                            break;

                        case eWAVEFORM.eTriangle:
                            {
                                dYOscMultVal = waveforms.GetTriOscVal((uint)dLYMOscVal);
                            }
                            break;

                        case eWAVEFORM.eVarDutySquare:
                            {
                                dYOscMultVal = waveforms.GetLYMOscSqVal((uint)dLYMOscVal);
                            }
                            break;
                    }

                    if (true == bLYModEnabled)
                    {
                        double dYModVal = iLYOsc1Ampl * dYOscMultVal * (iLYMOscAmpl / 100.0);

                        dYModVal += iLYModOffset * (double)iLYModGain / 100.0;

                        newY *= dYModVal * iLYModGain / 100.0;
                    }
                    else
                    {
                        newY *= iLYOsc1Ampl;
                    }


                    // Apply aspect ratio and size controls:

                    newX = newX * iAspectX / 100.0;
                    newY = newY * iAspectY / 100.0;

                    newX = newX * iImageSize / 100.0;
                    newY = newY * iImageSize / 100.0;


                    // Apply rotations:

                    int zRotX = (int)newX,  // Computed X display position, based on Z-Axis rotation.
                        zRotY = (int)newY;  // Computed Y display position, based on Z-Axis rotation.

                    RotateAndTranslate(ref zRotX, ref zRotY);

                    // Draw image:

                    DrawImageSegment(zRotX, zRotY);

                    iCurX = zRotX;
                    iCurY = zRotY;
                }

                // Invalidates on exit of using block.
            }
        }

        /// <summary>
        /// Draws a Radial Scan image based on the current controller parameters.
        /// 
        /// 1. Compute the base circle for angle t and radius r, where t is a
        ///    continuously incrementing counter:
        /// 
        ///    x = sin(t + PhaseShift) * r
        ///    y = cos(t - PhaseShift) * r
        ///  
        ///  2. Add the perspective shift amount x and y components. If perspective 
        ///     modulation is in effect, compute the perspective x and y components
        ///     from a quadrature (sin/cos) derived from the free running perspective
        ///     counter, multiplied by the perspective amplitude:
        ///  
        ///        PerspectiveX = sin(Perspective angle) * Perspective level
        ///        PerspectiveY = cos(Perspective angle) * Perspective level
        /// 
        ///     x = sin(t + PhaseShift) * r + PerspectiveX
        ///     y = cos(t - PhaseShift) * r + PerspectiveY
        ///     
        ///  3. Multiply the base circle by the modulation waveform. The waveform
        ///     is either a sine, ramp, rectified sine, sawtooth, square or triangle
        ///     wave. The modulation waveform has a quadrature (sin/cos) offset, to
        ///     which is added to the instantaneous waveform value. If multiplier
        ///     modulation is enabled, this value is multiplied by a modulation oscillator
        ///     value times its amplitude:
        ///     
        ///       modX = sin(offsetX) + multOscValue * multModOscVal * multModOscAmpl
        ///       modX = sin(offsetY) + multOscValue * multModOscVal * multModOscAmpl
        /// 
        ///     x = sin(t + PhaseShift) * r + PerspectiveX * modX
        ///     y = cos(t - PhaseShift) * r + PerspectiveY * modY
        ///     
        ///  4. Apply Aspect Ratio and Size multipliers:
        /// 
        ///     x = (sin(t + PhaseShift) * r + PerspectiveX * modX) * (AspectX / 100) * (Size / 100)
        ///     y = (cos(t - PhaseShift) * r + PerspectiveY * modY) * (AspectY / 100) * (Size / 100)
        ///     
        ///  5. Apply x, y and z axis rotations
        ///  
        ///  6. Apply translation and orbit.
        ///  
        ///  7. Apply chop.
        ///  
        ///  8. Apply color modulation.
        ///  
        ///  9. Erase previously scanned portions of the image.
        ///  
        /// </summary>
        private void DrawRadialScan()
        {
            // Wrap updates in a GetContext call, to prevent invalidation 
            // and nested locking/unlocking during this block:

            using (oDisplayImage.GetBitmapContext())
            {
                double newX = 0,  // New computed X display position.
                       newY = 0;  // New computed Y display position.

                int iMaxIterations = 360;

                while (false == bPaused && 0 != iMaxIterations--)
                {
                    // Compute baseline ("generator" circle):

                    if (true == bPerspectiveModEnabled)
                    {
                        if (eDIRECTION.eCCW == ePersDirection)
                        {
                            iPerspectiveX = (int)(Math.Sin(dRSPerspectiveOsc * Constants.PIDIV180) * iPerspectiveLevel);
                            iPerspectiveY = (int)(Math.Cos(dRSPerspectiveOsc * Constants.PIDIV180) * iPerspectiveLevel);
                        }
                        else
                        {
                            iPerspectiveX = (int)(Math.Cos(dRSPerspectiveOsc * Constants.PIDIV180) * iPerspectiveLevel);
                            iPerspectiveY = (int)(Math.Sin(dRSPerspectiveOsc * Constants.PIDIV180) * iPerspectiveLevel);
                        }

                        dRSPerspectiveOsc += (iPerspectiveRate / 1000000.0);
                    }
                    else
                    {
                        iPerspectiveX = iPerspectiveCtlH;
                        iPerspectiveY = iPerspectiveCtlV;
                    }

                    switch (eBaseGeometry)
                    {
                        case eBASEGEOMETRY.eCircle:
                            {
                                if (eDIRECTION.eCW == eBaseGDirection)
                                {
                                    newX = (Math.Sin(((dQOsc1Val + iGenPhase) * Constants.PIDIV180)) * iGenBaseSize + iPerspectiveX);
                                    newY = (Math.Cos(((dQOsc1Val - iGenPhase) * Constants.PIDIV180)) * iGenBaseSize + iPerspectiveY);
                                }
                                else
                                {
                                    newY = (Math.Sin(((dQOsc1Val + iGenPhase) * Constants.PIDIV180)) * iGenBaseSize + iPerspectiveX);
                                    newX = (Math.Cos(((dQOsc1Val - iGenPhase) * Constants.PIDIV180)) * iGenBaseSize + iPerspectiveY);
                                }
                            }
                            break;

                        case eBASEGEOMETRY.eDiamond:
                            {
                                int iX,
                                    iY;

                                if (eDIRECTION.eCW == eBaseGDirection)
                                {
                                    iX = (int)((dQOsc1Val + iGenPhase) % Constants.DEGREESINCIRCLE);
                                    iY = (int)((dQOsc1Val - iGenPhase + 90) % Constants.DEGREESINCIRCLE);
                                }
                                else 
                                {
                                    iY = (int)((dQOsc1Val + iGenPhase) % Constants.DEGREESINCIRCLE);
                                    iX = (int)((dQOsc1Val - iGenPhase + 90) % Constants.DEGREESINCIRCLE);                                
                                }

                                newX = waveforms.GetTriOscVal((uint)iX) * iGenBaseSize + iPerspectiveX;
                                newY = waveforms.GetTriOscVal((uint)iY) * iGenBaseSize + iPerspectiveY;
                            }
                            break;

                        case eBASEGEOMETRY.eTriangle:
                            {
                                int iX,
                                    iY;

                                if (eDIRECTION.eCW == eBaseGDirection)
                                {
                                    iX = (int)((dQOsc1Val + iGenPhase) % Constants.DEGREESINCIRCLE);
                                    iY = (int)((dQOsc1Val - iGenPhase) % Constants.DEGREESINCIRCLE);
                                }
                                else
                                {
                                    iY = (int)((dQOsc1Val + iGenPhase) % Constants.DEGREESINCIRCLE);
                                    iX = (int)((dQOsc1Val - iGenPhase) % Constants.DEGREESINCIRCLE);
                                }

                                newX = waveforms.GetTriGeoPoint((uint)iX).XPoint * iGenBaseSize + iPerspectiveX;
                                newY = waveforms.GetTriGeoPoint((uint)iY).YPoint * iGenBaseSize + iPerspectiveY;
                            }
                            break;
                    }
                    

                    // Modulate baseline by modulator:

                    double dModVal = 0.0;

                    uint iArrayNdx = (uint)(dModOscCnt % 359);

                    dModOscCnt += iModOscFreq / 1000.0;

                    switch (eOSC1Wave)
                    {
                        case eWAVEFORM.eRamp:
                            {
                                dModVal = waveforms.GetRampOscSqVal(iArrayNdx) * (iModOscAmpl / 50.0) + iModMulGain / 10.0;
                            }
                            break;

                        case eWAVEFORM.eRectSin:
                            {
                                dModVal = waveforms.GetRectSinOscSqVal(iArrayNdx) * (iModOscAmpl / 50.0) + iModMulGain / 10.0;
                            }
                            break;

                        case eWAVEFORM.eSawtooth:
                            {
                                dModVal = waveforms.GetSawOscVal(iArrayNdx) * (iModOscAmpl / 50.0) + iModMulGain / 10.0;
                            }
                            break;

                        case eWAVEFORM.eSin:
                            {
                                dModVal = Math.Sin((iArrayNdx * Constants.PIDIV180)) * (iModOscAmpl / 50.0) + iModMulGain / 10.0;
                            }
                            break;

                        case eWAVEFORM.eSquare:
                        case eWAVEFORM.eVarDutySquare:
                            {
                                dModVal = waveforms.GetModOscSqVal(iArrayNdx) * (iModOscAmpl / 50.0) + iModMulGain / 10.0;
                            }
                            break;

                        case eWAVEFORM.eTriangle:
                            {
                                dModVal = waveforms.GetTriOscVal((uint)iArrayNdx) * (iModOscAmpl / 50.0) + iModMulGain / 10.0;
                            }
                            break;

                        default:
                            {
                                DebugHelpers.AssertBadEnum((int)eOSC1Wave);
                            }
                            break;
                    }

                    double dMultModVal = 1.0;

                    if (true == MultGainModEnabled)
                    {
                        uint iWaveArrayNdx = (uint)(dOsc2Val) % 359;

                        switch (eOSC2Wave)
                        {
                            case eWAVEFORM.eRamp:
                                {
                                    dMultModVal = waveforms.GetRampOscSqVal(iWaveArrayNdx);
                                }
                                break;

                            case eWAVEFORM.eRectSin:
                                {
                                    dMultModVal = waveforms.GetRectSinOscSqVal(iWaveArrayNdx);
                                }
                                break;

                            case eWAVEFORM.eSawtooth:
                                {
                                    dMultModVal = waveforms.GetSawOscVal(iWaveArrayNdx);
                                }
                                break;

                            case eWAVEFORM.eSin:
                                {
                                    dMultModVal = Math.Sin(((dOsc2Val) * Constants.PIDIV180));
                                }
                                break;

                            case eWAVEFORM.eSquare:
                                {
                                    dMultModVal = waveforms.GetModOscSqVal(iWaveArrayNdx);
                                }
                                break;

                            case eWAVEFORM.eTriangle:
                                {
                                    dMultModVal = waveforms.GetTriOscVal((uint)iWaveArrayNdx);
                                }
                                break;

                            default:
                                {
                                    DebugHelpers.AssertBadEnum((int)eOSC1Wave);
                                }
                                break;
                        }

                        if (true == Osc2InvertEnabled)
                        {
                            dMultModVal *= -1;
                        }

                        dMultModVal *= iOsc2Level / 10.0;
                    }

                    newX = newX * (Math.Sin(((iModOscOffset * Constants.PIDIV180))) + (dModVal * dMultModVal));
                    newY = newY * (Math.Cos(((iModOscOffset * Constants.PIDIV180))) + (dModVal * dMultModVal));


                    // Apply aspect ratio and size controls:

                    newX = newX * iAspectX / 100.0;
                    newY = newY * iAspectY / 100.0;

                    newX = newX * iImageSize / 100.0;
                    newY = newY * iImageSize / 100.0;


                    // Apply rotations and translations:

                    int zRotX = (int)newX,  // Computed X display position, based on Z-Axis rotation.
                        zRotY = (int)newY;  // Computed Y display position, based on Z-Axis rotation.

                    RotateAndTranslate(ref zRotX, ref zRotY);

                    // Draw image:

                    DrawImageSegment(zRotX, zRotY);

                    dQOsc1Val += iQOsc1Rate / 1000.0;
                }

                // Invalidates on exit of using block.

                dOsc2Val += iOsc2Rate;
            }
        }

        /// <summary>
        /// Draws a Spiral Scan image based on the current controller parameters.
        /// 
        /// 1. Compute the base circle for angle t and radius r, where t is a
        ///    continuously incrementing counter:
        /// 
        ///    x = sin(t + PhaseShift) * r
        ///    y = cos(t - PhaseShift) * r
        ///  
        ///  2. Add the perspective shift amount x and y components. If perspective 
        ///     modulation is in effect, compute the perspective x and y components
        ///     from a quadrature (sin/cos) derived from the free running perspective
        ///     counter, multiplied by the perspective amplitude:
        ///  
        ///        PerspectiveX = sin(Perspective angle) * Perspective level
        ///        PerspectiveY = cos(Perspective angle) * Perspective level
        /// 
        ///     x = sin(t + PhaseShift) * r + PerspectiveX
        ///     y = cos(t - PhaseShift) * r + PerspectiveY
        ///     
        ///  3. Multiply the base circle by the modulation waveform. The waveform
        ///     is either a sine, ramp, rectified sine, sawtooth, square or triangle
        ///     wave. The modulation waveform has a quadrature (sin/cos) offset, to
        ///     which is added to the instantaneous waveform value. If multiplier
        ///     modulation is enabled, this value is multiplied by a modulation oscillator
        ///     value times its amplitude:
        ///     
        ///       modX = sin(offsetX) + multOscValue * multModOscVal * multModOscAmpl
        ///       modX = sin(offsetY) + multOscValue * multModOscVal * multModOscAmpl
        /// 
        ///     x = sin(t + PhaseShift) * r + PerspectiveX * modX
        ///     y = cos(t - PhaseShift) * r + PerspectiveY * modY
        ///     
        ///  4. Apply Aspect Ratio and Size multipliers:
        /// 
        ///     x = (sin(t + PhaseShift) * r + PerspectiveX * modX) * (AspectX / 100) * (Size / 100)
        ///     y = (cos(t - PhaseShift) * r + PerspectiveY * modY) * (AspectY / 100) * (Size / 100)
        ///     
        ///  5. Apply x, y and z axis rotations
        ///  
        ///  6. Apply translation and orbit.
        ///  
        ///  7. Apply chop.
        ///  
        ///  8. Apply color modulation.
        ///  
        ///  9. Erase previously scanned portions of the image.
        ///  
        /// </summary>
        private void DrawSpiralScan()
        {
            iSPQOscRate = 25000;

            // Wrap updates in a GetContext call, to prevent invalidation 
            // and nested locking/unlocking during this block:

            using (oDisplayImage.GetBitmapContext())
            {
                double newX = 0,  // New computed X display position.
                       newY = 0;  // New computed Y display position.

                int iMaxIterations = 360;

                while (false == bPaused && 0 != iMaxIterations--)
                {
                    // Compute baseline ("generator" circle):

                    if (true == bSpiralPersModEnable)
                    {
                        if (eDIRECTION.eCCW == eSpiralPersDirection)
                        {
                            iPerspectiveX = (int)(Math.Sin(dSPPerspectiveOsc * Constants.PIDIV180) * iPerspectiveLevel);
                            iPerspectiveY = (int)(Math.Cos(dSPPerspectiveOsc * Constants.PIDIV180) * iPerspectiveLevel);
                        }
                        else
                        {
                            iPerspectiveX = (int)(Math.Cos(dSPPerspectiveOsc * Constants.PIDIV180) * iPerspectiveLevel);
                            iPerspectiveY = (int)(Math.Sin(dSPPerspectiveOsc * Constants.PIDIV180) * iPerspectiveLevel);
                        }

                        dSPPerspectiveOsc += (dSpiralRampFrequency / 1000000.0);
                    }
                    else
                    {
                        iPerspectiveX = (int)dSpiralPersJSXPos;
                        iPerspectiveY = (int)dSpiralPersJSYPos;
                    }


                    if (eDIRECTION.eCW == eSpiralDirection)
                    {
                        newX = (Math.Sin((dSPQOscVal * Constants.PIDIV180)) * dSpiralSize / 75 + iPerspectiveX);
                        newY = (Math.Cos((dSPQOscVal * Constants.PIDIV180)) * dSpiralSize / 75 + iPerspectiveY);
                    }
                    else
                    {
                        newY = (Math.Sin((dSPQOscVal * Constants.PIDIV180)) * dSpiralSize / 75 + iPerspectiveX);
                        newX = (Math.Cos((dSPQOscVal * Constants.PIDIV180)) * dSpiralSize / 75 + iPerspectiveY);
                    }


                    // Apply ramp to base circle:

                    double dModVal = 0.0;

                    uint iArrayNdx = (uint)(dSPRampOscCnt % 359);

                    dSPRampOscCnt += dSpiralRampFrequency / 1000.0;

                    dModVal = waveforms.GetRampOscSqVal(iArrayNdx) * 2 + dSpiralRampGain / 10.0;

                    //dModVal = waveforms.GetSawOscVal(iArrayNdx) * (iModOscAmpl / 50.0) + iModMulGain / 10.0;

                    newX = newX * (Math.Sin(((dSpiralRampOffset * Constants.PIDIV180))) + dModVal);
                    newY = newY * (Math.Cos(((dSpiralRampOffset * Constants.PIDIV180))) + dModVal);


                    // Apply aspect ratio and size controls:

                    newX = newX * iAspectX / 100.0;
                    newY = newY * iAspectY / 100.0;

                    newX = newX * iImageSize / 100.0;
                    newY = newY * iImageSize / 100.0;


                    // Apply rotations and translations:

                    int zRotX = (int)newX,  // Computed X display position, based on Z-Axis rotation.
                        zRotY = (int)newY;  // Computed Y display position, based on Z-Axis rotation.

                    RotateAndTranslate(ref zRotX, ref zRotY);

                    // Draw image:

                    DrawImageSegment(zRotX, zRotY);

                    dSPQOscVal += iSPQOscRate / 1000.0;
                }

                // Invalidates on exit of using block.
            }
        }

        /// <summary>
        /// Fade the entire image by one step to simulate the effect of
        /// persistence of vision.
        /// </summary>
        private unsafe void Fade()
        {
            using (BitmapContext bitmapContext = oDisplayImage.GetBitmapContext())
            {
                var aPixelArray = bitmapContext.Pixels;

                int iPixelArraySize = bitmapContext.Length;

                for (int index = 0; index < iPixelArraySize; index++)
                {
                    int pixel = aPixelArray[index];

                    if (pixel != 0)
                    {
                        int r = pixel >> 16 & 0xFF;
                        int g = pixel >> 8 & 0xFF;
                        int b = pixel & 0xFF;

                        r = Math.Max(0, r - iPersistence);

                        if (r < oScreenColor.R)
                        {
                            r = oScreenColor.R;
                        }

                        g = Math.Max(0, g - iPersistence);

                        if (g < oScreenColor.G)
                        {
                            g = oScreenColor.G;
                        }

                        b = Math.Max(0, b - iPersistence);

                        if (b < oScreenColor.B)
                        {
                            b = oScreenColor.B;
                        }

                        aPixelArray[index] = r << 16 | g << 8 | b;
                    }
                }
            }
        }

        /// <summary>
        /// Rotate point about X, Y and Z axes per current control settings.
        /// </summary>
        private void RotateAndTranslate(ref int iX, ref int iY)
        {
            // Y - Axis rotation:

            iY = (int)(dYAxisCos * iY);

            // X - Axis rotation:

            iX = (int)(dXAxisCos * iX);

            // Z-Axis rotation:
            //
            // x = cos(a) * x - sin(a) * y;
            // y = sin(a) * x + cos(a) * y;

            int iXr = (int)(dZAxisCos * iX - dZAxisSin * iY);
            int iYr = (int)(dZAxisSin * iX + dZAxisCos * iY);

            iX = iXr;
            iY = iYr;

            // Translate image position:

            iX += iImagePosX;
            iY += iImagePosY;

            if (true == bPosModEnabled)
            {
                dPosModOscVal += iPosModFreq / 400000.0;
            }

            double dPosCos = Math.Cos(dPosModOscVal * Constants.PIDIV180),     
                   dPosSin = Math.Sin(dPosModOscVal * Constants.PIDIV180);
                   
            // Apply orbit rotation if orbit is not coupled to direction:

            if (true == bCoupleOrbit)
            {
                iXr = (int)(dPosCos * iX - dPosSin * iY);
                iYr = (int)(dPosSin * iX + dPosCos * iY);
            }
            else
            {
                iXr = (int)(dOrbitCos * iX - dOrbitSin * iY);
                iYr = (int)(dOrbitSin * iX + dOrbitCos * iY);
            }


            iX = iXr;
            iY = iYr;


            // Apply Position modulation, if enabled:

            if (true == bPosModEnabled)
            {
                if (eDIRECTION.eCW == ePosDirection)
                {
                    iX = iX + (int)(dPosCos * iPosModLevel);
                    iY = iY + (int)(dPosSin * iPosModLevel);
                }
                else
                {
                    iX = iX - (int)(dPosSin * iPosModLevel);
                    iY = iY - (int)(dPosCos * iPosModLevel);
                }
            }


            // Apply Zoom:

            iX = iX * iZoom / 100 + (iBitmapCenterX);
            iY = iY * iZoom / 100 + (iBitmapCenterY);
        }

        #endregion
    }
}
