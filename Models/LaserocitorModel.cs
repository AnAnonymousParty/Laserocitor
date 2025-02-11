// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;
using Laserocitor.Common;

using Laserocitor.ViewModels.Laserocitor;

namespace Laserocitor.Models
{
    /// <summary>
    /// This is the Model for the Laserocitor, just a big container of
    /// properties to be exchanged between a view model and persistence layer.
    /// </summary>
    public class LaserocitorModel 
    {
        /// <summary>
        /// Constructor (default).
        /// </summary>
        /// <remarks>
        /// Sets the default values for everything.
        /// </remarks>
        public LaserocitorModel()
        {
            InitToDefaults();
        }

        /// <summary>
        /// Constructor, given Laserocitor View Model.
        /// </summary>
        /// <remarks>
        /// Use this constructor to create a Model from a View Model.
        /// </remarks>
        /// <param name="laserocitorVM">LaserocitorVM object from which to construct the Model.</param>
        public LaserocitorModel(LaserocitorVM laserocitorVM)
        {
            if (null == laserocitorVM)
            {
                InitToDefaults();

                return;
            }

            AutoCSLEnabled        = laserocitorVM.AutoCSLEnabled;
            ChopperBEnabled       = laserocitorVM.ChopperBEnabled;
            ChopperEnabled        = laserocitorVM.ChopperEnabled;
            ChopperGEnabled       = laserocitorVM.ChopperGEnabled;
            ChopperREnabled       = laserocitorVM.ChopperREnabled;
            ColorModEnabled       = laserocitorVM.ColorModEnabled;
            CoupleOrbit           = laserocitorVM.CoupleOrbit;
            LXModEnabled          = laserocitorVM.LXModEnabled;
            LYModEnabled          = laserocitorVM.LYModEnabled;
            MultGainModEnabled    = laserocitorVM.MultGainModEnabled;
            Osc2InvertEnabled     = laserocitorVM.Osc2InvertEnabled;
            PerspectiveModEnabled = laserocitorVM.PerspectiveModEnabled;
            PosModEnabled         = laserocitorVM.PosModEnabled;
            SpiralPersModEnable   = laserocitorVM.SpiralPersModEnable;

            BgColorB        = laserocitorVM.BgColorB;
            BgColorG        = laserocitorVM.BgColorG;       
            BgColorR        = laserocitorVM.BgColorR;       
            FgColorB        = laserocitorVM.FgColorB;       
            FgColorG        = laserocitorVM.FgColorG;
            FgColorR        = laserocitorVM.FgColorR;
            MasterIntensity = laserocitorVM.MasterIntensity;

            RadiatorAMAmpl          = laserocitorVM.RadiatorAMAmpl;
            RadiatorAMFreq          = laserocitorVM.RadiatorAMFreq;
            TranslationJoystickXPos = laserocitorVM.TranslationJoystickXPos;
            TranslationJoystickYPos = laserocitorVM.TranslationJoystickYPos; 

            RSBaseGeometry         = laserocitorVM.RSBaseGeometry;
            ColorModMode           = laserocitorVM.ColorModMode;
            ColorModOscAssignBlue  = laserocitorVM.ColorModOscAssignBlue;
            ColorModOscAssignGreen = laserocitorVM.ColorModOscAssignGreen;
            ColorModOscAssignRed   = laserocitorVM.ColorModOscAssignRed;  
            ColorModSyncSource     = laserocitorVM.ColorModSyncSource;    
            AutoPosDir             = laserocitorVM.AutoPosDir;           
            BaseGDirection         = laserocitorVM.BaseGDirection;        
            PersModDir             = laserocitorVM.PersModDir;           
            ImageDrawingMode       = laserocitorVM.ImageDrawingMode;      
            ASLModOscWave          = laserocitorVM.ASLModOscWave;         
            LMXModOscWave          = laserocitorVM.LMXModOscWave;        
            LMYModOscWave          = laserocitorVM.LMYModOscWave;         
            LMXOscWave             = laserocitorVM.LMXOscWave;           
            LMYOscWave             = laserocitorVM.LMYOscWave;           
            Osc1Waveform           = laserocitorVM.Osc1Waveform;          
            Osc2Waveform           = laserocitorVM.Osc2Waveform;   
            SpiralDirection        = laserocitorVM.SpiralDirection;
            SpiralPersDirection    = laserocitorVM.SpiralPersDirection;

            AspectRatio            = laserocitorVM.AspectRatio;
            AutoPersLevel          = laserocitorVM.AutoPersLevel;    
            AutoPersRate           = laserocitorVM.AutoPersRate;   
            AutoPosLevel           = laserocitorVM.AutoPosLevel;     
            AutoPosRate            = laserocitorVM.AutoPosRate;      
            AxisRotX               = laserocitorVM.AxisRotX;         
            AxisRotY               = laserocitorVM.AxisRotY;         
            AxisRotZ               = laserocitorVM.AxisRotZ;         
            ChopperDutyCycle       = laserocitorVM.ChopperDutyCycle;
            ChopperFrequency       = laserocitorVM.ChopperFrequency; 
            ColorModFreq           = laserocitorVM.ColorModFreq;     
            CSLOscAmpl             = laserocitorVM.CSLOscAmpl;       
            CSLOscFreq             = laserocitorVM.CSLOscFreq;       
            CycloidL               = laserocitorVM.CycloidL;         
            CycloidRate            = laserocitorVM.CycloidRate;      
            CycloidRf              = laserocitorVM.CycloidRf;        
            CycloidRr              = laserocitorVM.CycloidRr;        
            GenPhase               = laserocitorVM.GenPhase;         
            ImageColor             = laserocitorVM.ImageColor;       
            ImagePosHoriz          = laserocitorVM.ImagePosHoriz;    
            ImagePosVert           = laserocitorVM.ImagePosVert;     
            ImageSize              = laserocitorVM.ImageSize;        
            LXModGain              = laserocitorVM.LXModGain;        
            LXModOffset            = laserocitorVM.LXModOffset;      
            LXMOscAmplitude        = laserocitorVM.LXMOscAmplitude;  
            LXMOscDutyCycle        = laserocitorVM.LXMOscDutyCycle;  
            LXMOscFreq             = laserocitorVM.LXMOscFreq;       
            LXOsc1Amplitude        = laserocitorVM.LXOsc1Amplitude;  
            LXOsc1DutyCycle        = laserocitorVM.LXOsc1DutyCycle;  
            LXOsc1Freq             = laserocitorVM.LXOsc1Freq;       
            LYModGain              = laserocitorVM.LYModGain;        
            LYModOffset            = laserocitorVM.LYModOffset;      
            LYMOscAmplitude        = laserocitorVM.LYMOscAmplitude;  
            LYMOscDutyCycle        = laserocitorVM.LYMOscDutyCycle;  
            LYMOscFreq             = laserocitorVM.LYMOscFreq;       
            LYOsc1Amplitude        = laserocitorVM.LYOsc1Amplitude;  
            LYOsc1DutyCycle        = laserocitorVM.LYOsc1DutyCycle;  
            LYOsc1Freq             = laserocitorVM.LYOsc1Freq;       
            LXOsc2Amplitude        = laserocitorVM.LXOsc2Amplitude;  
            LXOsc2DutyCycle        = laserocitorVM.LXOsc2DutyCycle;  
            LXOsc2Freq             = laserocitorVM.LXOsc2Freq;       
            LYOsc2Amplitude        = laserocitorVM.LYOsc2Amplitude;  
            LYOsc2DutyCycle        = laserocitorVM.LYOsc2DutyCycle;  
            LYOsc2Freq             = laserocitorVM.LYOsc2Freq;       
            ModOscDutyCycle        = laserocitorVM.ModOscDutyCycle;  
            ModOscAmp              = laserocitorVM.ModOscAmp;        
            ModOscFreq             = laserocitorVM.ModOscFreq;       
            ModOscGain             = laserocitorVM.ModOscGain;       
            ModOscOffset           = laserocitorVM.ModOscOffset;     
            Orbit                  = laserocitorVM.Orbit;            
            Osc2Freq               = laserocitorVM.Osc2Freq;         
            Osc2Level              = laserocitorVM.Osc2Level;        
            OscAFreq               = laserocitorVM.OscAFreq;         
            OscBFreq               = laserocitorVM.OscBFreq;         
            OscCFreq               = laserocitorVM.OscCFreq;              
            PerspectiveCtlH        = laserocitorVM.PerspectiveCtlH;  
            PerspectiveCtlV        = laserocitorVM.PerspectiveCtlV;
            Persistence            = laserocitorVM.Persistence;
            QuadOsc1Level          = laserocitorVM.QuadOsc1Level;    
            QuadOsc1Freq           = laserocitorVM.QuadOsc1Freq;     
            ScreenColor            = laserocitorVM.ScreenColor;
            SpiralPersJSXPos       = laserocitorVM.SpiralPersJSXPos;
            SpiralPersJSYPos       = laserocitorVM.SpiralPersJSYPos;
            SpiralPersModFrequency = laserocitorVM.SpiralPersModFrequency;
            SpiralPersModLevel     = laserocitorVM.SpiralPersModLevel;
            SpiralRampGain         = laserocitorVM.SpiralRampGain;
            SpiralRampOffset       = laserocitorVM.SpiralRampOffset;
            SpiralSize             = laserocitorVM.SpiralSize;
            Zoom                   = laserocitorVM.Zoom;

            ImageName = laserocitorVM.ImageName;
        }

        #region Model Properties

        public bool AutoCSLEnabled
        {
            get;
            set;
        }

        public bool ChopperBEnabled
        {
            get;
            set;
        }

        public bool ChopperEnabled
        {
            get;
            set;
        }

        public bool ChopperGEnabled
        {
            get;
            set;
        }

        public bool ChopperREnabled
        {
            get;
            set;
        }

        public bool ColorModEnabled
        {
            get;
            set;
        }

        public bool CoupleOrbit
        {
            get;
            set;
        }

        public bool LXModEnabled
        {
            get;
            set;
        }

        public bool LYModEnabled
        {
            get;
            set;
        }

        public bool MultGainModEnabled
        {
            get;
            set;
        }

        public bool Osc2InvertEnabled
        {
            get;
            set;
        }

        public bool PerspectiveModEnabled
        {
            get;
            set;
        }

        public bool PosModEnabled
        {
            get;
            set;
        }

        public bool SpiralPersModEnable
        {
            get;
            set;
        }

        public byte BgColorB
        {
            get;
            set;
        }

        public byte BgColorG
        {
            get;
            set;
        }

        public byte BgColorR
        {
            get;
            set;
        }

        public byte FgColorB
        {
            get;
            set;
        }

        public byte FgColorG
        {
            get;
            set;
        }

        public byte FgColorR
        {
            get;
            set;
        }

        public byte MasterIntensity
        {
            get;
            set;
        }

        public double RadiatorAMAmpl
        {
            get;
            set;
        }

        public double RadiatorAMFreq
        {
            get;
            set;
        }

        public double SpiralPersJSXPos
        {
            get;
            set;
        }

        public double SpiralPersJSYPos
        {
            get;
            set;
        }

        public double SpiralPersModFrequency
        {
            get;
            set;
        }

        public double SpiralPersModLevel
        {
            get;
            set;
        }

        public double SpiralRampFrequency
        {
            get;
            set;
        }

        public double SpiralRampGain
        {
            get;
            set;
        }

        public double SpiralRampOffset
        {
            get;
            set;
        }

        public double SpiralSize
        {
            get;
            set;
        }

        public eBASEGEOMETRY RSBaseGeometry
        {
            get;
            set;
        }

        public eCOLORMODMODE ColorModMode
        {
            get;
            set;
        }

        public eCOLORMODOSC ColorModOscAssignBlue
        {
            get;
            set;
        }

        public eCOLORMODOSC ColorModOscAssignGreen
        {
            get;
            set;
        }

        public eCOLORMODOSC ColorModOscAssignRed
        {
            get;
            set;
        }

        public eCOLORMODSYNCSOURCE ColorModSyncSource
        {
            get;
            set;
        }

        public eDIRECTION AutoPosDir
        {
            get;
            set;
        }

        public eDIRECTION BaseGDirection
        {
            get;
            set;
        }

        public eDIRECTION PersModDir
        {
            get;
            set;
        }

        public eDIRECTION SpiralDirection
        {                 
            get;
            set;
        }

        public eDIRECTION SpiralPersDirection
        {
            get;
            set;
        }

        public eMODE ImageDrawingMode
        {
            get;
            set;
        }

        public eWAVEFORM ASLModOscWave
        {
            get;
            set;
        }

        public eWAVEFORM LMXModOscWave
        {
            get;
            set;
        }

        public eWAVEFORM LMYModOscWave
        {
            get;
            set;
        }

        public eWAVEFORM LMXOscWave
        {
            get;
            set;
        }

        public eWAVEFORM LMYOscWave
        {
            get;
            set;
        }

        public eWAVEFORM Osc1Waveform
        {
            get;
            set;
        }

        public eWAVEFORM Osc2Waveform
        {
            get;
            set;
        }

        public Double TranslationJoystickXPos
        {
            get;
            set;
        }

        public Double TranslationJoystickYPos
        {
            get;
            set;
        }

        public int AspectRatio
        {
            get;
            set;
        }

        public int AutoPersLevel
        {
            get;
            set;
        }

        public int AutoPersRate
        {
            get;
            set;
        }

        public int AutoPosLevel
        {
            get;
            set;
        }

        public int AutoPosRate
        {
            get;
            set;
        }

        public int AxisRotX
        {
            get;
            set;
        }

        public int AxisRotY
        {
            get;
            set;
        }

        public int AxisRotZ
        {
            get;
            set;
        }

        public int ChopperDutyCycle
        {
            get;
            set;
        }

        public int ChopperFrequency
        {
            get;
            set;
        }

        public int ColorModFreq
        {
            get;
            set;
        }

        public int CSLOscAmpl
        {
            get;
            set;
        }

        public int CSLOscFreq
        {
            get;
            set;
        }

        public int CycloidL
        {
            get;
            set;
        }

        public int CycloidRate
        {
            get;
            set;
        }

        public int CycloidRf
        {
            get;
            set;
        }

        public int CycloidRr
        {
            get;
            set;
        }

        public int GenPhase
        {
            get;
            set;
        }

        public int ImageColor
        {
            get;
            set;
        }

        public int ImagePosHoriz
        {
            get;
            set;
        }

        public int ImagePosVert
        {
            get;
            set;
        }

        public int ImageSize
        {
            get;
            set;
        }

        public int LXModGain
        {
            get;
            set;
        }

        public int LXModOffset
        {
            get;
            set;
        }

        public int LXMOscAmplitude
        {
            get;
            set;
        }

        public int LXMOscDutyCycle
        {
            get;
            set;
        }

        public int LXMOscFreq
        {
            get;
            set;
        }

        public int LXOsc1Amplitude
        {
            get;
            set;
        }

        public int LXOsc1DutyCycle
        {
            get;
            set;
        }

        public int LXOsc1Freq
        {
            get;
            set;
        }

        public int LYModGain
        {
            get;
            set;
        }

        public int LYModOffset
        {
            get;
            set;
        }

        public int LYMOscAmplitude
        {
            get;
            set;
        }

        public int LYMOscDutyCycle
        {
            get;
            set;
        }

        public int LYMOscFreq
        {
            get;
            set;
        }

        public int LYOsc1Amplitude
        {
            get;
            set;
        }

        public int LYOsc1DutyCycle
        {
            get;
            set;
        }

        public int LYOsc1Freq
        {
            get;
            set;
        }

        public int LXOsc2Amplitude
        {
            get;
            set;
        }

        public int LXOsc2DutyCycle
        {
            get;
            set;
        }

        public int LXOsc2Freq
        {
            get;
            set;
        }

        public int LYOsc2Amplitude
        {
            get;
            set;
        }

        public int LYOsc2DutyCycle
        {
            get;
            set;
        }

        public int LYOsc2Freq
        {
            get;
            set;
        }

        public int ModOscDutyCycle
        {
            get;
            set;
        }

        public int ModOscAmp
        {
            get;
            set;
        }

        public int ModOscFreq
        {
            get;
            set;
        }

        public int ModOscGain
        {
            get;
            set;
        }

        public int ModOscOffset
        {
            get;
            set;
        }

        public int Orbit
        {
            get;
            set;
        }

        public int Osc2Freq
        {
            get;
            set;
        }

        public int Osc2Level
        {
            get;
            set;
        }

        public int OscAFreq
        {
            get;
            set;
        }

        public int OscBFreq
        {
            get;
            set;
        }

        public int OscCFreq
        {
            get;
            set;
        }

        public int PerspectiveCtlH
        {
            get;
            set;
        }

        public int PerspectiveCtlV
        {
            get;
            set;
        }

        public int Persistence
        {
            get;
            set;
        }

        public int QuadOsc1Level
        {
            get;
            set;
        }

        public int QuadOsc1Freq
        {
            get;
            set;
        }

        public int ScreenColor
        {
            get;
            set;
        }

        public int Zoom
        {
            get;
            set;
        }

        public String ImageName
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Initialize View Model to default values.
        /// </summary>
        private void InitToDefaults()
        {
            AutoCSLEnabled        = false;
            ChopperBEnabled       = true;
            ChopperEnabled        = false;
            ChopperGEnabled       = true;
            ChopperREnabled       = true;
            ColorModEnabled       = false;
            CoupleOrbit           = false;
            LXModEnabled          = false;
            LYModEnabled          = false;
            MultGainModEnabled    = false;
            Osc2InvertEnabled     = false;
            PerspectiveModEnabled = false;
            PosModEnabled         = false;
            SpiralPersModEnable   = false;

            BgColorB        = 0;
            BgColorG        = 0;
            BgColorR        = 0;
            FgColorB        = 0;
            FgColorG        = 0;
            FgColorR        = 255;
            MasterIntensity = 100;
             
            RadiatorAMAmpl          = 50;
            RadiatorAMFreq          = 100;
            TranslationJoystickXPos = 0;
            TranslationJoystickYPos = 0;

            RSBaseGeometry         = eBASEGEOMETRY.eCircle;
            ColorModMode           = eCOLORMODMODE.eSynchronized;
            ColorModOscAssignBlue  = eCOLORMODOSC.eOSCCPOS;
            ColorModOscAssignGreen = eCOLORMODOSC.eOSCBPOS;
            ColorModOscAssignRed   = eCOLORMODOSC.eOSCAPOS;
            ColorModSyncSource     = eCOLORMODSYNCSOURCE.eColorMod;
            AutoPosDir             = eDIRECTION.eCW;
            BaseGDirection         = eDIRECTION.eCW;
            PersModDir             = eDIRECTION.eCW;
            ImageDrawingMode       = eMODE.eCycloid;
            ASLModOscWave          = eWAVEFORM.eSin;
            LMXModOscWave          = eWAVEFORM.eSin;
            LMYModOscWave          = eWAVEFORM.eSin;
            LMXOscWave             = eWAVEFORM.eSin;
            LMYOscWave             = eWAVEFORM.eSin;
            Osc1Waveform           = eWAVEFORM.eSin;
            Osc2Waveform           = eWAVEFORM.eSin;
            SpiralDirection        = eDIRECTION.eCW;
            SpiralPersDirection    = eDIRECTION.eCW;

            AspectRatio            = 0;
            AutoPersLevel          = 50;
            AutoPersRate           = 10;
            AutoPosLevel           = 25;
            AutoPosRate            = 10;
            AxisRotX               = 0;
            AxisRotY               = 0;
            AxisRotZ               = 0;
            ChopperDutyCycle       = 50;
            ChopperFrequency       = 10;
            ColorModFreq           = 100;
            CSLOscAmpl             = 50;
            CSLOscFreq             = 10;
            CycloidL               = 100;
            CycloidRate            = 50;
            CycloidRf              = 120;
            CycloidRr              = 100;
            GenPhase               = 0;
            ImageColor             = 0;
            ImagePosHoriz          = 0;
            ImagePosVert           = 0;
            ImageSize              = 100;
            LXModGain              = 100;
            LXModOffset            = 25;
            LXMOscAmplitude        = 50;
            LXMOscDutyCycle        = 50;
            LXMOscFreq             = 50;
            LXOsc1Amplitude        = 50;
            LXOsc1DutyCycle        = 50;
            LXOsc1Freq             = 4;
            LYModGain              = 100;
            LYModOffset            = 25;
            LYMOscAmplitude        = 50;
            LYMOscDutyCycle        = 50;
            LYMOscFreq             = 5;
            LYOsc1Amplitude        = 50;
            LYOsc1DutyCycle        = 50;
            LYOsc1Freq             = 5;
            LXOsc2Amplitude        = 50;
            LXOsc2DutyCycle        = 50;
            LXOsc2Freq             = 5;
            LYOsc2Amplitude        = 50;
            LYOsc2DutyCycle        = 50;
            LYOsc2Freq             = 5;
            ModOscDutyCycle        = 50;
            ModOscAmp              = 50;
            ModOscFreq             = 5;
            ModOscGain             = 50;
            ModOscOffset           = 50;
            Orbit                  = 0;
            Osc2Freq               = 5;
            Osc2Level              = 50;
            OscAFreq               = 5;  
            OscBFreq               = 50;
            OscCFreq               = 500;
            PerspectiveCtlH        = 0;
            PerspectiveCtlV        = 0;
            QuadOsc1Level          = 50;
            QuadOsc1Freq           = 100;
            Persistence            = 40;
            ScreenColor            = 0;
            SpiralPersJSXPos       = 0;
            SpiralPersJSYPos       = 0;
            SpiralPersModFrequency = 10;
            SpiralPersModLevel     = 10;
            SpiralRampGain         = 50;
            SpiralRampOffset       = 50;
            SpiralSize             = 50;
            Zoom                   = 100;

            ImageName = "DEFAULT";
        }
    }
}
