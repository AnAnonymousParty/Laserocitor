// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;

namespace Laserocitor.Common
{
    /// <summary>
    /// Provides arrays of values of various waveforms at any given angle.
    /// </summary>
    internal class Waveforms
    {  
        // Arrays of points to define waveforms:

        private double[] dRampPointsList    = new double[Constants.DEGREESINCIRCLE];  // Ramp wave.
        private double[] dRectSinPointsList = new double[Constants.DEGREESINCIRCLE];  // Rectified sine wave.
        private double[] dSawPointsList     = new double[Constants.DEGREESINCIRCLE];  // Sawtooth wave.
        private double[] dTriPointsList     = new double[Constants.DEGREESINCIRCLE];  // Triangle wave.

        private Geometry.Point[] objTriGeometryPointsList = new Geometry.Point[Constants.DEGREESINCIRCLE];  // Triangle shape points list.

        // There are multiple arrays of square wave values as they may be generated
        // with different duty cycles.

        private int[] iChopperSqPointsList = new int[Constants.DEGREESINCIRCLE];  // Chopper Oscillator variable duty square wave.
        private int[] iLXMOscSqPointsList  = new int[Constants.DEGREESINCIRCLE];  // Lissajous X Modulation Oscillator variable duty square wave.
        private int[] iLXOsc1SqPointsList  = new int[Constants.DEGREESINCIRCLE];  // Lissajous X Oscillator 1 variable duty square wave.
        private int[] iLXOsc2SqPointsList  = new int[Constants.DEGREESINCIRCLE];  // Lissajous X Oscillator 2 variable duty square wave.
        private int[] iLYMOscSqPointsList  = new int[Constants.DEGREESINCIRCLE];  // Lissajous Y Modulation Oscillator variable duty square wave.
        private int[] iLYOsc1SqPointsList  = new int[Constants.DEGREESINCIRCLE];  // Lissajous Y Oscillator 1 variable duty square wave.
        private int[] iLYOsc2SqPointsList  = new int[Constants.DEGREESINCIRCLE];  // Lissajous Y Oscillator 2 variable duty square wave.
        private int[] iModOscSqPointsList  = new int[Constants.DEGREESINCIRCLE];  // Modulation Oscillator 1 variable duty square wave.

        /// <summary>
        /// Constructor (default).
        /// </summary>
        public Waveforms()
        {
            InitRampPointsList();
            InitRectSinPointsList();
            InitSawPointsList();
            InitTriGeometryPointsList();
            InitTriPointsList();
            InitVarDutySquarePointsList(iChopperSqPointsList, 50);
            InitVarDutySquarePointsList(iModOscSqPointsList,  50);
            InitVarDutySquarePointsList(iLXMOscSqPointsList,  50);
            InitVarDutySquarePointsList(iLXOsc1SqPointsList,  50);
            InitVarDutySquarePointsList(iLYMOscSqPointsList,  50);
            InitVarDutySquarePointsList(iLYOsc1SqPointsList,  50);
        }

        /// <summary>
        /// Get the value for the Chopper Square Wave oscillator, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Chopper Square Wave oscillator at the given angle.</returns>
        public int GetChopperOscSqVal(uint angle)
        {
            return iChopperSqPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for Lissajous X Square Wave oscillator, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Lissajous X Wave oscillator at the given angle.</returns>
        public int GetLXMOscSqVal(uint angle)
        {
            return iLXMOscSqPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for Lissajous X Square Wave oscillator, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Lissajous X Wave oscillator at the given angle.</returns>
        public int GetLXMOsc1SqVal(uint angle)
        {
            return iLXOsc1SqPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for Lissajous Y Square Wave oscillator, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Lissajous Y Wave oscillator at the given angle.</returns>
        public int GetLYMOscSqVal(uint angle)
        {
            return iLYMOscSqPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for Modulation oscillator, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Chopper Square Wave oscillator at the given angle.</returns>
        public int GetModOscSqVal(uint angle)
        {
            return iModOscSqPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for a Ramp wave, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Ramp Wave oscillator at the given angle.</returns>
        public double GetRampOscSqVal(uint angle)
        {
            return dRampPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for a Rectified Sine wave, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Rectified Sine Wave oscillator at the given angle.</returns>
        public double GetRectSinOscSqVal(uint angle)
        {
            return dRectSinPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for a Sawtooth wave, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Sawtooth Wave oscillator at the given angle.</returns>
        public double GetSawOscVal(uint angle)
        {
            return dSawPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for a Triangle wave, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Triangle oscillator at the given angle.</returns>
        public Geometry.Point GetTriGeoPoint(uint angle)
        {
            return objTriGeometryPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Get the value for a Triangle wave, given an angle.
        /// </summary>
        /// <param name="angle"The angle></param>
        /// <returns>Value for the Triangle oscillator at the given angle.</returns>
        public double GetTriOscVal(uint angle)
        {
            return dTriPointsList[angle % Constants.DEGREESINCIRCLE];
        }

        /// <summary>
        /// Initialize the Chopper Square wave values array given a duty cycle value.
        /// </summary>
        /// <param name="dutyCycle">The duty cycle value.</param>
        public void InitChopperSqPointsList(int dutyCycle)
        {
            InitVarDutySquarePointsList(iChopperSqPointsList, dutyCycle);
        }

        /// <summary>
        /// Initialize the Lissajous X Square wave values array given a duty cycle value.
        /// </summary>
        /// <param name="dutyCycle">The duty cycle value.</param>
        public void InitLXOsc1SqPointsList(int dutyCycle)
        {
            InitVarDutySquarePointsList(iLXOsc1SqPointsList, dutyCycle);
        }

        /// <summary>
        /// Initialize the Lissajous Y Square wave values array given a duty cycle value.
        /// </summary>
        /// <param name="dutyCycle">The duty cycle value.</param>
        public void InitLYOsc1SqPointsList(int dutyCycle)
        {
            InitVarDutySquarePointsList(iLYOsc1SqPointsList, dutyCycle);
        }

        /// <summary>
        /// Initialize the Lissajous Y Square wave values array given a duty cycle value.
        /// </summary>
        /// <param name="dutyCycle">The duty cycle value.</param>
        public void InitLYMOsc1SqPointsList(int dutyCycle)
        {
            InitVarDutySquarePointsList(iLYMOscSqPointsList, dutyCycle);
        }

        /// <summary>
        /// Initialize the Modulation Square wave values array given a duty cycle value.
        /// </summary>
        /// <param name="dutyCycle">The duty cycle value.</param>
        public void InitModOsc1SqPointsList(int dutyCycle)
        {
            InitVarDutySquarePointsList(iModOscSqPointsList, dutyCycle);
        }

        /// <summary>
        /// Initialize the array of Ramp wave values.
        /// </summary>
        private void InitRampPointsList()
        {
            double dRampPoint = -1.0,
                   dRampPointInc = 2.0 / 360.0;

            for (int iArrayNdx = 0; iArrayNdx < Constants.DEGREESINCIRCLE; ++iArrayNdx)
            {
                dRampPoint += dRampPointInc;

                dRampPointsList[iArrayNdx] = dRampPoint;
            }
        }

        /// <summary>
        /// Initialize the array of Sawtooth wave values.
        /// </summary>
        private void InitSawPointsList()
        {
            double dSawPoint = 1.0,
                   dSawPointInc = 2.0 / 360.0;

            for (int iArrayNdx = 0; iArrayNdx < Constants.DEGREESINCIRCLE; ++iArrayNdx)
            {
                dSawPoint -= dSawPointInc;

                dSawPointsList[iArrayNdx] = dSawPoint;
            }
        }

        /// <summary>
        /// Initialize the array of Rectified Sine wave values.
        /// </summary>
        private void InitRectSinPointsList()
        {
            for (int iArrayNdx = 0; iArrayNdx < Constants.DEGREESINCIRCLE / 2; ++iArrayNdx)
            {
                dRectSinPointsList[iArrayNdx] = Math.Sin(iArrayNdx * Constants.PIDIV180);
                dRectSinPointsList[iArrayNdx + 180] = dRectSinPointsList[iArrayNdx];
            }
        }

        /// <summary>
        /// Initialize the array of Triangle wave values.
        /// </summary>
        private void InitTriGeometryPointsList()
        {
            double dX =  0.0,
                   dY = -1.0;

            double dDistInc = 2.0 / (Constants.DEGREESINCIRCLE / 3);

            double dDistXInc = Math.Sin(((180 - 30) * Constants.PIDIV180)) * dDistInc;
            double dDistYInc = Math.Cos(((180 - 30) * Constants.PIDIV180)) * dDistInc;

            for (int iArrayNdx = 0; iArrayNdx < Constants.DEGREESINCIRCLE / 3; ++iArrayNdx)
            {
                dX += dDistXInc;
                dY -= dDistYInc;

                objTriGeometryPointsList[iArrayNdx] = new Geometry.Point(dX, dY);
            }

            for (int iArrayNdx = 0; iArrayNdx < Constants.DEGREESINCIRCLE / 3; ++iArrayNdx)
            {

                dDistXInc = Math.Sin((270 * Constants.PIDIV180)) * dDistInc;
                dDistYInc = Math.Cos((270 * Constants.PIDIV180)) * dDistInc;

                dX += dDistXInc;
                dY += dDistYInc;

                objTriGeometryPointsList[iArrayNdx + Constants.DEGREESINCIRCLE / 3] = new Geometry.Point(dX, dY);
            }

            for (int iArrayNdx = 0; iArrayNdx < Constants.DEGREESINCIRCLE / 3; ++iArrayNdx)
            {
                dDistXInc = Math.Sin((150 * Constants.PIDIV180)) * dDistInc;
                dDistYInc = Math.Cos((150 * Constants.PIDIV180)) * dDistInc;

                dX += dDistXInc;
                dY += dDistYInc;

                objTriGeometryPointsList[iArrayNdx + (Constants.DEGREESINCIRCLE / 3) * 2] = new Geometry.Point(dX, dY);
            }
        }

        /// <summary>
        /// Initialize the array of Triangle wave values.
        /// </summary>
        private void InitTriPointsList()
        {
            double dTriPoint = -1.0,
                   dTriPointInc = 2.0 / 180.0;

            for (int iArrayNdx = 0; iArrayNdx < Constants.DEGREESINCIRCLE / 2; ++iArrayNdx)
            {
                dTriPoint += dTriPointInc;

                dTriPointsList[iArrayNdx] = dTriPoint;
            }

            for (int iArrayNdx = 180; iArrayNdx < Constants.DEGREESINCIRCLE; ++iArrayNdx)
            {
                dTriPoint -= dTriPointInc;

                dTriPointsList[iArrayNdx] = dTriPoint;
            }
        }

        /// <summary>
        /// Initialize an array of Variable Duty Cycle Square wave values.
        /// </summary>
        /// <param name="list">The array to be initialized.</param>
        /// <param name="iPercentOn">The duty cycle value.</param>
        private void InitVarDutySquarePointsList(int[] list, int iPercentOn)
        {
            int iThreshold = (int)(list.Length * (iPercentOn / 100.0));

            for (int iArrayNdx = 0; iArrayNdx < Constants.DEGREESINCIRCLE; ++iArrayNdx)
            {
                if (iArrayNdx > iThreshold)
                {
                    list[iArrayNdx] = 1;
                }
                else
                {
                    list[iArrayNdx] = -1;
                }
            }
        }
    }
}
