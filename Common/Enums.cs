// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express  prior written consent of Laserocitors afs@laserocitors.com.


using System.ComponentModel;

namespace Laserocitor.Common
{
    public enum eCOLORMODMODE  // Enumerations for Color Modulation Mode selections.
    {
        [Description("Synchronized")]    eSynchronized,
        [Description("Unsynchronized")]  eUnsynchronized
    };

    public enum eCOLORMODOSC  // Enumerations for Color Modulation Oscillator selections.
    {
        [Description("Oscillator A+")]  eOSCAPOS,
        [Description("Oscillator A-")]  eOSCANEG,
        [Description("Oscillator B+")]  eOSCBPOS,
        [Description("Oscillator B-")]  eOSCBNEG,
        [Description("Oscillator C+")]  eOSCCPOS,
        [Description("Oscillator C-")]  eOSCCNEG
    };

    public enum eCOLORMODSYNCSOURCE  // Enumerations for Color Modulation Synchronization source selections.
    {
        [Description("Color Modulator")]            eColorMod,
        [Description("Cycloid sweep modulator")]    eCycloidMod,
        [Description("Cycloid Scan")]               eCycloidScan,
        [Description("Radial Scan Base Geometry")]  eRSBaseGeo,
        [Description("Radial Scan Modulator")]      eRSBaseMod,
        [Description("Radial Scan Radiator")]       eRSBaseRad
    };

    public enum eDIRECTION  // Enumerations for defining directions or rotations, etc.
    {
        [Description("Counter Clockwise")]  eCCW,
        [Description("Clockwise")]          eCW,
        [Description("Forward")]            eFwd,
        [Description("Reverse")]            eRev
    };

    public enum eBASEGEOMETRY  // Base Geometry mode:
    {
        [Description("Circle")]    eCircle,
        [Description("Diamond")]   eDiamond,
        [Description("Triangle")]  eTriangle
    };

    public enum eMODE  // Drawing mode:
    {
        [Description("Cycloid")]      eCycloid,       // Cycloid mode, includes Epicycloid, Epitrochoid, Hypocycloid and Hypotrochoid variants.
        [Description("Lissajous")]    eLissajous,     // Lissajous figure mode.
        [Description("Radial Scan")]  eRadialScan,    // Radial scanning mode.
        [Description("Spiral")]       eSpiral         // Spiral generator mode.
    };

    public enum eWAVEFORM  // Enumeration of waveform types:
    {
        [Description("Ramp")]                  eRamp,          // Ramp:                       /|/|/|/|
        [Description("Rectified sine")]        eRectSin,       // Rectified Sine:             UUUUUUUU
        [Description("Sawtooth")]              eSawtooth,      // Sawtooth:                   |\|\|\|\
        [Description("Sine")]                  eSin,           // Sine:                       ~~~~~~~~
        [Description("Square")]                eSquare,        // Square:                     ][][][][
        [Description("Triangle")]              eTriangle,      // Triangle:                   /\/\/\/\ 
        [Description("Variable Duty Square")]  eVarDutySquare  // Variable Duty Cycle Square: _|_| |__
    };
}
