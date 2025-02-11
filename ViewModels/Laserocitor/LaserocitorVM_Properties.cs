// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;
using System.Windows.Media;

using Laserocitor.Common;
using Laserocitor.Utils.Common;

namespace Laserocitor.ViewModels.Laserocitor
{
    /// <summary>
    /// This portion of the Laserocitor View Model contains all of 
    /// the properties bound to the UI elements on the Main View.
    /// </summary>
    partial class LaserocitorVM 
    {
        public eMODE ImageDrawingMode
        {
            get { return eMode; }
            set
            {
                eMode = value;

                OnPropertyChanged("ImageDrawingMode");
            }
        }

        public String ImageName
        {
            get
            {
                return strImageFileName;
            }
            set
            {
                strImageFileName = value;

                OnPropertyChanged("ImageName");
            }
        }

        #region Beam/Screen Properties

        public bool ChopperBEnabled
        {
            get
            {
                return bChopperBEnabled;
            }
            set
            {
                bChopperBEnabled = value;
                OnPropertyChanged("ChopperBEnabled");
            }
        }

        public bool ChopperEnabled
        {
            get
            {
                return bChopperEnabled;
            }
            set
            {
                bChopperEnabled = value;

                if (false == bChopperEnabled)
                {
                    bBlanked = false;
                }

                OnPropertyChanged("ChopperEnabled");
            }
        }

        public bool ChopperGEnabled
        {
            get
            {
                return bChopperGEnabled;
            }
            set
            {
                bChopperGEnabled = value;
                OnPropertyChanged("ChopperGEnabled");
            }
        }

        public bool ChopperREnabled
        {
            get
            {
                return bChopperREnabled;
            }
            set
            {
                bChopperREnabled = value;
                OnPropertyChanged("ChopperREnabled");
            }
        }

        public bool ColorModEnabled
        {
            get
            {
                return bColorModEnabled;
            }
            set
            {
                bColorModEnabled = value;
                OnPropertyChanged("ColorModEnabled");
            }
        }

        public byte BgColorB
        {
            get
            {
                return cBgColorB;
            }
            set
            {
                cBgColorB = value;

                oScreenColor = System.Windows.Media.Color.FromArgb(255, cBgColorR, cBgColorG, cBgColorB);

                Clear(oScreenColor);

                OnPropertyChanged("BgColorB");
            }
        }

        public byte BgColorG
        {
            get
            {
                return cBgColorG;
            }
            set
            {
                cBgColorG = value;
                oScreenColor = System.Windows.Media.Color.FromArgb(255, cBgColorR, cBgColorG, cBgColorB);
                Clear(oScreenColor);
                OnPropertyChanged("BgColorG");
            }
        }

        public byte BgColorR
        {
            get
            {
                return cBgColorR;
            }
            set
            {
                cBgColorR = value;
                oScreenColor = System.Windows.Media.Color.FromArgb(255, cBgColorR, cBgColorG, cBgColorB);
                Clear(oScreenColor);
                OnPropertyChanged("BgColorR");
            }
        }

        public byte FgColorB
        {
            get
            {
                return cFgColorB;
            }
            set
            {
                cFgColorB = value;
                oImageColor = System.Windows.Media.Color.FromArgb(255, cFgColorR, cFgColorG, cFgColorB);

                OnPropertyChanged("FgColorB");
            }
        }

        public byte FgColorG
        {
            get
            {
                return cFgColorG;
            }
            set
            {
                cFgColorG = value;
                oImageColor = System.Windows.Media.Color.FromArgb(255, cFgColorR, cFgColorG, cFgColorB);

                OnPropertyChanged("FgColorG");
            }
        }

        public byte FgColorR
        {
            get
            {
                return cFgColorR;
            }
            set
            {
                cFgColorR = value;
                oImageColor = System.Windows.Media.Color.FromArgb(255, cFgColorR, cFgColorG, cFgColorB);

                OnPropertyChanged("FgColorR");
            }
        }

        public byte MasterIntensity
        {
            get
            {
                return cMasterIntensity;
            }
            set
            {
                cMasterIntensity = value;

                OnPropertyChanged("MasterIntensity");
            }
        }

        public eCOLORMODOSC ColorModOscAssignBlue
        {
            get { return eColorModBlue; }
            set
            {
                eColorModBlue = value;

                OnPropertyChanged("ColorModOscAssignBlue");
            }
        }

        public eCOLORMODOSC ColorModOscAssignGreen
        {
            get { return eColorModGreen; }
            set
            {
                eColorModGreen = value;

                OnPropertyChanged("ColorModOscAssignGreen");
            }
        }

        public eCOLORMODOSC ColorModOscAssignRed
        {
            get { return eColorModRed; }
            set
            {
                eColorModRed = value;

                OnPropertyChanged("ColorModOscAssignRed");
            }
        }

        public eCOLORMODMODE ColorModMode
        {
            get { return eColorModMode; }
            set
            {
                eColorModMode = value;

                OnPropertyChanged("ColorModMode");
            }
        }

        public eCOLORMODSYNCSOURCE ColorModSyncSource

        {
            get
            {
                return eColorModSyncSource;
            }

            set
            {
                eColorModSyncSource = value;
                OnPropertyChanged("ColorModSyncSource");
            }
        }

        public int ChopperDutyCycle
        {
            get
            {
                return iChopOscDutyCycle;
            }
            set
            {
                iChopOscDutyCycle = value;
                waveforms.InitChopperSqPointsList(iChopOscDutyCycle);
                OnPropertyChanged("ChopperDutyCycle");
            }
        }

        public int ChopperFrequency
        {
            get
            {
                return iChopOscFrequency;
            }
            set
            {
                iChopOscFrequency = value;
                OnPropertyChanged("ChopperFrequency");
            }
        }

        public int ColorModFreq
        {
            get
            {
                return iColorModFreq;
            }
            set
            {
                iColorModFreq = value;

                OnPropertyChanged("ColorModFreq");
            }
        }

        public int ImageColor
        {
            get
            {
                return iBeamColorCtlVal;
            }
            set
            {
                iBeamColorCtlVal = value;

                Color oImageColor = CommonUtils.ColorInt2RGB(iBeamColorCtlVal, cMasterIntensity);

                FgColorB = oImageColor.B;
                FgColorG = oImageColor.G;
                FgColorR = oImageColor.R;

                OnPropertyChanged("ImageColor");
            }
        }

        public int OscAFreq
        {
            get
            {
                return iOscAFreq;
            }
            set
            {
                iOscAFreq = value;

                OnPropertyChanged("OscAFreq");
            }
        }

        public int OscBFreq
        {
            get
            {
                return iOscBFreq;
            }
            set
            {
                iOscBFreq = value;

                OnPropertyChanged("OscBFreq");
            }
        }

        public int OscCFreq
        {
            get
            {
                return iOscCFreq;
            }
            set
            {
                iOscCFreq = value;

                OnPropertyChanged("OscCFreq");
            }
        }

        public int ScreenColor
        {
            get
            {
                return iScreenColorCtlVal;
            }
            set
            {
                iScreenColorCtlVal = value;

                oScreenColor = System.Windows.Media.Color.FromArgb(255, cBgColorR, cBgColorG, cBgColorB);

                Clear(oScreenColor);

                OnPropertyChanged("ScreenColor");
            }
        }

        #endregion

        #region Cycloid Mode Section Properties
        public bool AutoCSLEnabled
        {
            get
            {
                return bAutoSweepLenEnabled;
            }
            set
            {
                bAutoSweepLenEnabled = value;
                OnPropertyChanged("AutoCSLEnabled");
            }
        }

        public eWAVEFORM ASLModOscWave
        {
            get { return eCSLOscWave; }
            set
            {

                eCSLOscWave = value;

                OnPropertyChanged("ASLModOscWave");
            }
        }

        public int CSLOscAmpl
        {
            get
            {
                return iCSLAmpl;
            }
            set
            {
                iCSLAmpl = value;
                OnPropertyChanged("CSLOscAmpl");
            }
        }

        public int CSLOscFreq
        {
            get
            {
                return iCSLRate;
            }
            set
            {
                iCSLRate = value;
                OnPropertyChanged("CSLOscFreq");
            }
        }

        public int CycloidL
        {
            get
            {
                return iCycloidL;
            }
            set
            {
                iCycloidL = value;
                OnPropertyChanged("CycloidL");
            }
        }

        public int CycloidRate
        {
            get
            {
                return iCycloidRate;
            }
            set
            {
                iCycloidRate = value;
                OnPropertyChanged("CycloidRate");
            }
        }

        public int CycloidRf
        {
            get
            {
                return iCycloidRf;
            }
            set
            {
                iCycloidRf = value;
                OnPropertyChanged("CycloidRf");
            }
        }

        public int CycloidRr
        {
            get
            {
                return iCycloidRr;
            }
            set
            {
                iCycloidRr = value;
                OnPropertyChanged("CycloidRr");
            }
        }

        #endregion

        #region Image Parameters Section Properties

        public bool CoupleOrbit
        {
            get
            {
                return bCoupleOrbit;
            }
            set
            {
                bCoupleOrbit = value;
                OnPropertyChanged("CoupleOrbit");
            }
        }

        public bool PosModEnabled
        {
            get
            {
                return bPosModEnabled;
            }
            set
            {
                bPosModEnabled = value;
                OnPropertyChanged("PosModEnabled");
            }
        }

        public Double TranslationJoystickXPos
        {
            get { return iImagePosX; }
            set
            {
                iImagePosX = (int)value;
                OnPropertyChanged("TranslationJoystickXPos");
            }
        }

        public Double TranslationJoystickYPos
        {
            get { return iImagePosY; }
            set
            {
                iImagePosY = -(int)value;
                OnPropertyChanged("TranslationJoystickYPos");
            }
        }

        public eDIRECTION AutoPosDir
        {
            get { return ePosDirection; }
            set
            {
                ePosDirection = value;

                OnPropertyChanged("AutoPosDir");
            }
        }

        public int AspectRatio
        {
            get
            {
                return iAspectCtlVal;
            }
            set
            {
                iAspectCtlVal = value;

                iAspectX = 50 - iAspectCtlVal;
                iAspectY = 50 + iAspectCtlVal;

                OnPropertyChanged("AspectRatio");
            }
        }

        public int AutoPosLevel
        {
            get
            {
                return iPosModLevel;
            }
            set
            {
                iPosModLevel = value;
                OnPropertyChanged("AutoPosLevel");
            }
        }

        public int AutoPosRate
        {
            get
            {
                return iPosModFreq;
            }
            set
            {
                iPosModFreq = value;
                OnPropertyChanged("AutoPosRate");
            }
        }

        public int AxisRotX
        {
            get
            {
                return iRotAxisX;
            }
            set
            {
                iRotAxisX = value;

                dXAxisSin = Math.Sin((double)(iRotAxisX * Common.Constants.PIDIV180));
                dXAxisCos = Math.Cos((double)(iRotAxisX * Common.Constants.PIDIV180));

                OnPropertyChanged("AxisRotX");
            }
        }

        public int AxisRotY
        {
            get
            {
                return iRotAxisY;
            }
            set
            {
                iRotAxisY = value;

                dYAxisSin = Math.Sin((double)(iRotAxisY * Common.Constants.PIDIV180));
                dYAxisCos = Math.Cos((double)(iRotAxisY * Common.Constants.PIDIV180));

                OnPropertyChanged("AxisRotY");
            }
        }

        public int AxisRotZ
        {
            get
            {
                return iRotAxisZ;
            }
            set
            {
                iRotAxisZ = value;

                dZAxisSin = Math.Sin((double)(iRotAxisZ * Common.Constants.PIDIV180));
                dZAxisCos = Math.Cos((double)(iRotAxisZ * Common.Constants.PIDIV180));

                OnPropertyChanged("AxisRotZ");
            }
        }

        public int ImagePosHoriz
        {
            get
            {
                return iImagePosX;
            }
            set
            {
                iImagePosX = value;
                OnPropertyChanged("ImagePosHoriz");
            }
        }

        public int ImagePosVert
        {
            get
            {
                return iImagePosY;
            }
            set
            {
                iImagePosY = value;
                OnPropertyChanged("ImagePosVert");
            }
        }

        public int ImageSize
        {
            get
            {
                return iImageSize;
            }
            set
            {
                iImageSize = value;
                OnPropertyChanged("ImageSize");
            }
        }

        public int Orbit
        {
            get
            {
                return iOrbitAngle;
            }
            set
            {
                iOrbitAngle = value;

                dOrbitSin = Math.Sin((double)(iOrbitAngle * Common.Constants.PIDIV180));
                dOrbitCos = Math.Cos((double)(iOrbitAngle * Common.Constants.PIDIV180));

                OnPropertyChanged("Orbit");
            }
        }

        public int Persistence
        {
            get { return iPersistence; }
            set
            {
                iPersistence = value;
                OnPropertyChanged("Persistence");
            }
        }

        public int Zoom
        {
            get
            {
                return iZoom;
            }
            set
            {
                iZoom = value;
                OnPropertyChanged("Zoom");
            }
        }

        #endregion

        #region Lissajous Mode Section Properties

        public bool LXModEnabled
        {
            get
            {
                return bLXModEnabled;
            }
            set
            {
                bLXModEnabled = value;
                OnPropertyChanged("LXModEnabled");
            }
        }

        public bool LYModEnabled
        {
            get
            {
                return bLYModEnabled;
            }
            set
            {
                bLYModEnabled = value;
                OnPropertyChanged("LYModEnabled");
            }
        }

        public eWAVEFORM LMXModOscWave
        {
            get { return eOSCXLMWave; }
            set
            {
                eOSCXLMWave = value;

                OnPropertyChanged("LMXOscWave");
            }
        }

        public eWAVEFORM LMYModOscWave
        {
            get { return eOSCYLMWave; }
            set
            {
                eOSCYLMWave = value;

                OnPropertyChanged("LMYOscWave");
            }
        }

        public eWAVEFORM LMXOscWave
        {
            get { return eOSCXLWave1; }
            set
            {
                eOSCXLWave1 = value;

                OnPropertyChanged("LMXOscWave");
            }
        }

        public eWAVEFORM LMYOscWave
        {
            get { return eOSCYLWave1; }
            set
            {
                eOSCYLWave1 = value;

                OnPropertyChanged("LMYOscWave");
            }
        }

        public int LXModGain
        {
            get
            {
                return iLXModGain;
            }
            set
            {
                iLXModGain = value;

                OnPropertyChanged("LXModGain");
            }
        }

        public int LXModOffset
        {
            get
            {
                return iLXModOffset;
            }
            set
            {
                iLXModOffset = value;

                OnPropertyChanged("LXModOffset");
            }
        }

        public int LXMOscAmplitude
        {
            get
            {
                return iLXMOscAmpl;
            }
            set
            {
                iLXMOscAmpl = value;

                OnPropertyChanged("LXMOscAmplitude");
            }
        }

        public int LXMOscDutyCycle
        {
            get
            {
                return iLXMOscDutyCycle;
            }
            set
            {
                iLXMOscDutyCycle = value;

                waveforms.InitLXOsc1SqPointsList(iLXMOscDutyCycle);
                OnPropertyChanged("LXMOscDutyCycle");
            }
        }

        public int LXMOscFreq
        {
            get
            {
                return iLXMOscFreq;
            }
            set
            {
                iLXMOscFreq = value;

                OnPropertyChanged("LXMOscFreq");
            }
        }

        public int LXOsc1Amplitude
        {
            get
            {
                return iLXOsc1Ampl;
            }
            set
            {
                iLXOsc1Ampl = value;

                OnPropertyChanged("LXOsc1Amplitude");
            }
        }

        public int LXOsc1DutyCycle
        {
            get
            {
                return iLXOsc1DutyCycle;
            }
            set
            {
                iLXOsc1DutyCycle = value;

                waveforms.InitLXOsc1SqPointsList(iLXOsc1DutyCycle);
                OnPropertyChanged("LXOsc1DutyCycle");
            }
        }

        public int LXOsc1Freq
        {
            get
            {
                return iLXOsc1Freq;
            }
            set
            {
                iLXOsc1Freq = value;

                OnPropertyChanged("LXOsc1Freq");
            }
        }

        public int LYModGain
        {
            get
            {
                return iLYModGain;
            }
            set
            {
                iLYModGain = value;

                OnPropertyChanged("LYModGain");
            }
        }

        public int LYModOffset
        {
            get
            {
                return iLYModOffset;
            }
            set
            {
                iLYModOffset = value;

                OnPropertyChanged("LYModOffset");
            }
        }

        public int LYMOscAmplitude
        {
            get
            {
                return iLYMOscAmpl;
            }
            set
            {
                iLYMOscAmpl = value;

                OnPropertyChanged("LYMOscAmplitude");
            }
        }

        public int LYMOscDutyCycle
        {
            get
            {
                return iLYMOscDutyCycle;
            }
            set
            {
                iLYMOscDutyCycle = value;

                waveforms.InitLYMOsc1SqPointsList(iLYMOscDutyCycle);
                OnPropertyChanged("LYMOscDutyCycle");
            }
        }

        public int LYMOscFreq
        {
            get
            {
                return iLYMOscFreq;
            }
            set
            {
                iLYMOscFreq = value;

                OnPropertyChanged("LYMOscFreq");
            }
        }

        public int LYOsc1Amplitude
        {
            get
            {
                return iLYOsc1Ampl;
            }
            set
            {
                iLYOsc1Ampl = value;

                OnPropertyChanged("LYOsc1Amplitude");
            }
        }

        public int LYOsc1DutyCycle
        {
            get
            {
                return iLYOsc1DutyCycle;
            }
            set
            {
                iLYOsc1DutyCycle = value;

                OnPropertyChanged("LYOsc1DutyCycle");
            }
        }

        public int LYOsc1Freq
        {
            get
            {
                return iLYOsc1Freq;
            }
            set
            {
                iLYOsc1Freq = value;

                OnPropertyChanged("LYOsc1Freq");
            }
        }

        public int LXOsc2Amplitude
        {
            get
            {
                return iLXOsc2Ampl;
            }
            set
            {
                iLXOsc2Ampl = value;

                OnPropertyChanged("LXOsc2Amplitude");
            }
        }

        public int LXOsc2DutyCycle
        {
            get
            {
                return iLXOsc2DutyCycle;
            }
            set
            {
                iLXOsc2DutyCycle = value;

                OnPropertyChanged("LXOsc2DutyCycle");
            }
        }

        public int LXOsc2Freq
        {
            get
            {
                return iLXOsc2Freq;
            }
            set
            {
                iLXOsc2Freq = value;

                OnPropertyChanged("LXOsc2Freq");
            }
        }

        public int LYOsc2Amplitude
        {
            get
            {
                return iLYOsc2Ampl;
            }
            set
            {
                iLYOsc2Ampl = value;

                OnPropertyChanged("LYOsc2Amplitude");
            }
        }

        public int LYOsc2DutyCycle
        {
            get
            {
                return iLYOsc2DutyCycle;
            }
            set
            {
                iLYOsc2DutyCycle = value;

                waveforms.InitLYOsc1SqPointsList(iLYOsc1DutyCycle);
                OnPropertyChanged("LYOsc2DutyCycle");
            }
        }

        public int LYOsc2Freq
        {
            get
            {
                return iLYOsc2Freq;
            }
            set
            {
                iLYOsc2Freq = value;

                OnPropertyChanged("LYOsc2Freq");
            }
        }

        #endregion

        #region Radial Scan Mode Section Properties

        public bool MultGainModEnabled
        {
            get
            {
                return bMultGainModEnabled;
            }
            set
            {
                bMultGainModEnabled = value;

                OnPropertyChanged("MultGainModEnabled");
            }
        }

        public bool Osc2InvertEnabled
        {
            get
            {
                return bOsc2InvertEnabled;
            }
            set
            {
                bOsc2InvertEnabled = value;

                OnPropertyChanged("Osc2InvertEnabled");
            }
        }

        public bool PerspectiveModEnabled
        {
            get
            {
                return bPerspectiveModEnabled;
            }
            set
            {
                bPerspectiveModEnabled = value;
                OnPropertyChanged("PerspectiveModEnabled");
            }
        }

        public double RadiatorAMAmpl
        {
            get { return iOsc2Level; }
            set
            {

                iOsc2Level = (int)value;

                OnPropertyChanged("RadiatorAMAmpl");
            }
        }

        public double RadiatorAMFreq
        {
            get { return dOsc2Val; }
            set
            {

                dOsc2Val = value;

                OnPropertyChanged("RadiatorAMFreq");
            }
        }

        public eBASEGEOMETRY RSBaseGeometry
        {
            get { return eBaseGeometry; }
            set
            {

                eBaseGeometry = value;

                OnPropertyChanged("RSBaseGeometry");
            }
        }

        public eDIRECTION BaseGDirection
        {
            get { return eBaseGDirection; }
            set
            {

                eBaseGDirection = value;

                OnPropertyChanged("BaseGDirection");
            }
        }

        public eDIRECTION PersModDir
        {
            get { return ePersDirection; }
            set
            {
                ePersDirection = value;

                OnPropertyChanged("PersModDir");
            }
        }

        public eWAVEFORM Osc1Waveform
        {
            get { return eOSC1Wave; }
            set
            {

                eOSC1Wave = value;

                OnPropertyChanged("Osc1Waveform");
            }
        }

        public eWAVEFORM Osc2Waveform
        {
            get { return eOSC2Wave; }
            set
            {

                eOSC2Wave = value;

                OnPropertyChanged("Osc2Waveform");
            }
        }

        public int AutoPersLevel
        {
            get
            {
                return iPerspectiveLevel;
            }
            set
            {
                iPerspectiveLevel = value;
                OnPropertyChanged("AutoPersLevel");
            }
        }

        public int AutoPersRate
        {
            get
            {
                return iPerspectiveRate;
            }
            set
            {
                iPerspectiveRate = value;
                OnPropertyChanged("AutoPersRate");
            }
        }

        public int GenPhase
        {
            get
            {
                return iGenPhase;
            }
            set
            {
                iGenPhase = value;

                OnPropertyChanged("GenPhase");
            }
        }

        public int ModOscDutyCycle
        {
            get
            {
                return iModOscDutyCycle;
            }
            set
            {
                iModOscDutyCycle = value;

                waveforms.InitModOsc1SqPointsList(iModOscDutyCycle);
                OnPropertyChanged("ModOscDutyCycle");
            }
        }

        public int ModOscAmp
        {
            get
            {
                return iModOscAmpl;
            }
            set
            {
                iModOscAmpl = value;

                OnPropertyChanged("ModOscAmp");
            }
        }

        public int ModOscFreq
        {
            get
            {
                return iModOscFreq;
            }
            set
            {
                iModOscFreq = value;

                OnPropertyChanged("ModOscFreq");
            }
        }

        public int ModOscGain
        {
            get
            {
                return iModMulGain;
            }
            set
            {
                iModMulGain = value;

                OnPropertyChanged("ModOscGain");
            }
        }

        public int ModOscOffset
        {
            get
            {
                return iModOscOffset;
            }
            set
            {
                iModOscOffset = value;

                OnPropertyChanged("ModOscOffset");
            }
        }

        public int Osc2Freq
        {
            get
            {
                return iOsc2Rate;
            }
            set
            {
                iOsc2Rate = value;
                OnPropertyChanged("Osc2Freq");
            }
        }

        public int Osc2Level
        {
            get
            {
                return iOsc2Level;
            }
            set
            {
                iOsc2Level = value;
                OnPropertyChanged("Osc2Level");
            }
        }

        public int PerspectiveCtlH
        {
            get
            {
                return iPerspectiveCtlH;
            }
            set
            {
                iPerspectiveCtlH = value;
                OnPropertyChanged("PerspectiveCtlH");
            }
        }

        public int PerspectiveCtlV
        {
            get
            {
                return iPerspectiveCtlV;
            }
            set
            {
                iPerspectiveCtlV = value;
                OnPropertyChanged("PerspectiveCtlV");
            }
        }

        public int QuadOsc1Level
        {
            get
            {
                return iGenBaseSize;
            }
            set
            {
                iGenBaseSize = value;
                OnPropertyChanged("QuadOsc1Level");
            }
        }

        public int QuadOsc1Freq
        {
            get
            {
                return iQOsc1Rate;
            }
            set
            {
                iQOsc1Rate = value;
                OnPropertyChanged("QuadOsc1Freq");
            }
        }

        #endregion

        #region Spiral Mode Section Properties

        public double SpiralRampGain
        {
            get { return dSpiralRampGain; }
            set 
            { 
                dSpiralRampGain = value;
                OnPropertyChanged("SpiralRampGain");
            }
        }


        public double SpiralRampOffset
        {
            get { return dSpiralRampOffset; }
            set
            {
                dSpiralRampOffset = value;
                OnPropertyChanged("SpiralRampOffset");
            }
        }

        public eDIRECTION SpiralPersDirection
        {
            get { return eSpiralPersDirection; }
            set
            {
                eSpiralPersDirection = value;
                OnPropertyChanged("SpiralPersDirection");
            }
        }

        public bool SpiralPersModEnable
        {
            get { return bSpiralPersModEnable; }
            set
            {
                bSpiralPersModEnable = value;
                OnPropertyChanged("SpiralPersModEnable");
            }
        }

        public double SpiralPersModFrequency
        {
            get { return dSpiralPersModFrequency; }
            set 
            {
                dSpiralPersModFrequency = value;
                OnPropertyChanged("SpiralPersModFrequency");
            }
        }

        public double SpiralPersModLevel
        {
            get { return dSpiralPersModLevel; }
            set
            {
                dSpiralPersModLevel = value;
                OnPropertyChanged("SpiralPersModLevel");
            }
        }

        public double SpiralPersJSXPos
        {
            get { return dSpiralPersJSXPos; }
            set
            {
                dSpiralPersJSXPos = value;
                OnPropertyChanged("SpiralPersJSXPos");
            }
        }

        public double SpiralPersJSYPos
        {
            get { return dSpiralPersJSYPos; }
            set
            {
                dSpiralPersJSYPos = value;
                OnPropertyChanged("SpiralPersJSYPos");
            }
        }

        public eDIRECTION SpiralDirection
        {
            get { return eSpiralDirection; }
            set
            {
                eSpiralDirection = value;
                OnPropertyChanged("SpiralDirection");
            }
        }

        public double SpiralSize
        {
            get { return dSpiralSize; }
            set
            {
                dSpiralSize = value;
                OnPropertyChanged("SpiralSize");
            }
        }

        public double SpiralRampFrequency
        {
            get { return dSpiralRampFrequency; }
            set
            {
                dSpiralRampFrequency = value;
                OnPropertyChanged("SpiralRampFrequency");
            }
        }

        #endregion
    }
}
