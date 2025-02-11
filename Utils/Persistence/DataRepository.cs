// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.
 

using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;

using Laserocitor.Models;
using Laserocitor.Utils.Diagnostics;
using Laserocitor.Views.DisplayScreen;


namespace Laserocitor.Utils.Persistence
{
    /// <summary>
    /// This singleton class is responsible for serializing model data
    /// to/from xml and saving/loading files.
    /// </summary>
    public class DataRepository
    {
        private static readonly object _lock = new object();

        private static DataRepository _instance = null;

        /// <summary>
        /// Obtain a reference to the singleton instance of this class.
        /// </summary>
        /// <returns>The sole instance of this class.</returns>
        public static DataRepository GetInstance()
        {
            if (null != _instance)
            {
                return _instance;
            }

            lock (_lock)
            {
                _instance = new DataRepository();
            }
            
            return _instance;
        }

        /// <summary>
        /// Load Model from file.
        /// </summary>
        /// <returns>LaserocitorModel object.</returns>
        public LaserocitorModel LoadModel()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".xml";
            dlg.Filter     = "XML documents (.xml)|*.xml";
            dlg.Title      = "Load saved Image Parameters";

            Nullable<bool> result = dlg.ShowDialog();

            if (false == result)
            {
                return new LaserocitorModel();
            }

            XDocument imageParms = XDocument.Load(dlg.FileName);

            return GenerateModelFromXml(imageParms);
        }

        /// <summary>
        /// Save Model to file.
        /// </summary>
        /// <remarks>
        /// Write all of the Model's properties to an xml file and capture a screen
        /// shot of the display window as a jpeg file.
        /// </remarks>
        /// <param name="model">LaserocitorModel model to be saved.</param>
        /// <param name="displayWindow">DisplayWindow object from which to obtain a screen shot.</param>
        public void SaveModel(LaserocitorModel model, DisplayWindow displayWindow)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog.CheckFileExists  = false;
            saveFileDialog.CheckPathExists  = true;
            saveFileDialog.DefaultExt       = "xml";
            saveFileDialog.Filter           = "XML files (*.xml)|*.xml";
            saveFileDialog.FilterIndex      = 2;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title            = "Save Image";
            saveFileDialog.ValidateNames    = true;

            Nullable<bool> result = saveFileDialog.ShowDialog();

            if (false == result)
            {
                return;
            }

            model.ImageName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

            string strFileName = Path.GetDirectoryName(saveFileDialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
            
            XDocument imageParms = null;

            try
            {
                imageParms = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Laser Projector Image Parameters"),
                    new XElement("LaserImageParms", new XAttribute("ImageName",  strFileName),
                                                    new XAttribute("ScreenShot", strFileName + ".jpg"),
                                                    new XAttribute("Version",    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version),
                        new XElement("BaseParms",
                            new XElement("ScreenColor",
                                new XElement("Red",   model.BgColorR),
                                new XElement("Green", model.BgColorG), 
                                new XElement("Blue",  model.BgColorB)
                            ),
                            new XElement("ImageColor",
                                new XElement("Red",   model.FgColorR),
                                new XElement("Green", model.FgColorG),
                                new XElement("Blue",  model.FgColorB)
                            ),
                            new XElement("ImageSize",   model.ImageSize),
                            new XElement("ImageZoom",   model.Zoom),
                            new XElement("Persistence", model.Persistence),
                            new XElement("AspectRatio", model.AspectRatio)
                        ),                    
                        new XElement("ImageParms",
                            new XElement("Mode", model.ImageDrawingMode.ToString()),
                            new XElement("CycloidParms",
                                new XElement("CycloidRate",         model.CycloidRate),
                                new XElement("FixedCircleRadius",   model.CycloidRf),
                                new XElement("RollingCircleRadius", model.CycloidRr),
                                new XElement("SweepLength",         model.CycloidL),
                                new XElement("AutoCSL",             model.AutoCSLEnabled),
                                new XElement("CSLWaveForm",         model.ASLModOscWave.ToString()),
                                new XElement("CSLFreq",             model.CSLOscFreq),
                                new XElement("CSLAmpl",             model.CSLOscAmpl)
                            ),
                            new XElement("LissajousParms",
                                new XElement("XOscWaveform",     model.LMXOscWave.ToString()),
                                new XElement("XOscFrequency",    model.LXOsc1Freq),
                                new XElement("XOscDutyCycle",    model.LXOsc1DutyCycle),
                                new XElement("XOscAmplitude",    model.LXOsc1Amplitude),
                                new XElement("YOscWaveform",     model.LMYOscWave.ToString()),
                                new XElement("YOscFrequency",    model.LYOsc1Freq),
                                new XElement("YOscDutyCycle",    model.LYOsc1DutyCycle),
                                new XElement("YOscAmplitude",    model.LYOsc1Amplitude),
                                new XElement("XModOscWaveform",  model.LMXModOscWave.ToString()),
                                new XElement("XModOscFrequency", model.LXMOscFreq),
                                new XElement("XModOscDutyCycle", model.LXMOscDutyCycle),
                                new XElement("XModOscAmplitude", model.LXMOscAmplitude),
                                new XElement("YModOscWaveform",  model.LMYModOscWave.ToString()),
                                new XElement("YModOscFrequency", model.LYMOscFreq),
                                new XElement("YModOscDutyCycle", model.LYMOscDutyCycle),
                                new XElement("YModOscAmplitude", model.LYMOscAmplitude),
                                new XElement("XModEnabled",      model.LXModEnabled),
                                new XElement("YModEnabled",      model.LYModEnabled),
                                new XElement("XModGain",         model.LXModGain),
                                new XElement("XModOffset",       model.LXModOffset),
                                new XElement("YModGain",         model.LYModGain),
                                new XElement("YModOffset",       model.LYModOffset)
                            ),
                            new XElement("RadialScanParms",
                                new XElement("BaseGeometry",     model.RSBaseGeometry.ToString()),
                                new XElement("BaseGeometryDir",  model.BaseGDirection.ToString()),
                                new XElement("BaseGeometryFreq", model.QuadOsc1Freq),
                                new XElement("QuadOsc1Phase",    model.GenPhase),
                                new XElement("QuadOsc1Ampl",     model.QuadOsc1Level),
                                new XElement("ModOsc1Waveform",  model.Osc1Waveform.ToString()),
                                new XElement("ModOsc1Freq",      model.ModOscFreq),
                                new XElement("ModOsc1Ampl",      model.ModOscAmp),
                                new XElement("MultGainMod",      model.MultGainModEnabled),
                                new XElement("MultGain",         model.ModOscGain),
                                new XElement("MultOffset",       model.ModOscOffset),
                                new XElement("Osc2",
                                    new XElement("Invert",       model.Osc2InvertEnabled),
                                    new XElement("Osc2Waveform", model.Osc2Waveform.ToString()),
                                    new XElement("Osc2Level",    model.RadiatorAMAmpl),
                                    new XElement("Osc2Rate",     model.RadiatorAMFreq)
                                )
                            ),
                            new XElement("SpiralParms",
                                new XElement("SpiralDirection",        model.SpiralDirection.ToString()),
                                new XElement("SpiralPersDirection",    model.SpiralPersDirection.ToString()),
                                new XElement("SpiralPersModEnable",    model.SpiralPersModEnable.ToString()),
                                new XElement("SpiralPersJSXPos",       model.SpiralPersJSXPos),
                                new XElement("SpiralPersJSYPos",       model.SpiralPersJSYPos),
                                new XElement("SpiralPersModFrequency", model.SpiralPersModFrequency),
                                new XElement("SpiralPersModLevel",     model.SpiralPersModLevel),
                                new XElement("SpiralRampFrequency",    model.SpiralRampFrequency),
                                new XElement("SpiralRampGain",         model.SpiralRampGain),
                                new XElement("SpiralRampOffset",       model.SpiralRampOffset),
                                new XElement("SpiralSize",             model.SpiralSize)                                
                            )
                        ),
                        new XElement("ModifierParms",
                            new XElement("RotationMatrix",
                                new XElement("XRotation",
                                    new XElement("XAngle", model.AxisRotX)
                                ),
                                new XElement("YRotation",
                                    new XElement("YAngle", model.AxisRotY)
                                ),
                                new XElement("ZRotation",
                                    new XElement("ZAngle", model.AxisRotZ)
                                )
                            ),
                            new XElement("Translation",
                                new XElement("XPos",            model.TranslationJoystickXPos),
                                new XElement("YPos",            model.TranslationJoystickYPos),
                                new XElement("OrbitAngle",      model.Orbit),
                                new XElement("Auto",            model.PosModEnabled),
                                new XElement("PosModFreq",      model.AutoPosRate),
                                new XElement("PosModRadius",    model.AutoPosLevel),
                                new XElement("PosModDirection", model.AutoPosDir.ToString()),
                                new XElement("CoupleOrbit",     model.CoupleOrbit)
                            ),
                            new XElement("ColorModulation",
                                new XElement("Enabled",       model.ColorModEnabled),
                                new XElement("ColorModMode",  model.ColorModMode.ToString()),
                                new XElement("Frequency",     model.ColorModFreq),
                                new XElement("FrequencyA",    model.OscAFreq),
                                new XElement("FrequencyB",    model.OscBFreq),
                                new XElement("FrequencyC",    model.OscCFreq),
                                new XElement("ColorModBlue",  model.ColorModOscAssignBlue.ToString()),
                                new XElement("ColorModGreen", model.ColorModOscAssignGreen.ToString()),
                                new XElement("ColorModRed",   model.ColorModOscAssignRed.ToString()),
                                new XElement("SyncSource",    model.ColorModSyncSource.ToString())
                            ),
                            new XElement("Perspective",
                                new XElement("Auto",      model.PerspectiveModEnabled),
                                new XElement("X",         model.PerspectiveCtlV),
                                new XElement("Y",         model.PerspectiveCtlH),
                                new XElement("Frequency", model.AutoPersRate),
                                new XElement("Amplitude", model.AutoPersLevel),
                                new XElement("Direction", model.PersModDir.ToString())
                            ),
                            new XElement("Chopper",
                                new XElement("Enabled",   model.ChopperEnabled),
                                new XElement("Frequency", model.ChopperFrequency),
                                new XElement("DutyCycle", model.ChopperDutyCycle),
                                new XElement("EnabledR",  model.ChopperREnabled),
                                new XElement("EnabledG",  model.ChopperGEnabled),
                                new XElement("EnabledB",  model.ChopperBEnabled)
                            )
                        )
                    )
                );
            }
            catch (NullReferenceException ex)
            {
                Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eInfo,
                                                        LogMessage.LogMessageType.eException,
                                                        GetType().FullName,
                                                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                        ex.Message));
            }

            imageParms.Save(strFileName + ".xml");

            Common.CommonUtils.BringWindowForward(displayWindow);

            int screenHeightOffset = 32;
            int screenWidthOffset  = 8;

            Rectangle rect = new Rectangle((int)displayWindow.Left + screenWidthOffset,
                                           (int)displayWindow.Top  + screenHeightOffset,
                                           (int)displayWindow.GetScreenWidth()  - (screenWidthOffset * 2),
                                           (int)displayWindow.GetScreenHeight() - screenHeightOffset);

            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bmp);

            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            bmp.Save(strFileName + ".jpg", ImageFormat.Jpeg);
        }

        /// <summary>
        /// Start the sub-system.
        /// </summary>
        public void Start()
        {

        }

        /// <summary>
        /// Constructor (default).
        /// </summary>
        private DataRepository()
        {
        }

        /// <summary>
        /// Given an XML document full of image parameters, use it to
        /// populate a Model.
        /// </summary>
        /// <param name="imageParms">XDocument object containing the paramters.</param>
        /// <returns>LaserocitorModel object.</returns>
        private LaserocitorModel GenerateModelFromXml(XDocument imageParms)
        {
            LaserocitorModel model = new LaserocitorModel();
            
            for (;;)
            {
                XElement rootElem = imageParms.Element("LaserImageParms");

                if (null == rootElem)
                {
                    MessageBox.Show("The selected image file cannot be read");

                    break;
                }

                model.ImageName = Path.GetFileNameWithoutExtension((string)rootElem.Attribute("ImageName"));

                string strVersion;

                if (null != rootElem.Attribute("Version"))
                {
                    strVersion = (string)rootElem.Attribute("Version");

                    string[] fileVerParts = strVersion.Split('.');

                    string appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                    string[] appVerParts = appVersion.Split('.');

                    if (
                        int.Parse(appVerParts[0]) >= int.Parse(fileVerParts[0]) &&
                        int.Parse(appVerParts[1]) >= int.Parse(fileVerParts[1]) &&
                        int.Parse(appVerParts[2]) >= int.Parse(fileVerParts[2])
                       )
                    {
                        // MessageBox.Show("File good.");
                    }
                    else
                    {
                        Common.CommonUtils.ShowMessage("Unable to load file", "File too old.");
                    }
                }
                else
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "No version #");
                }

                #region Base Parameters.

                // Load all the Base Parameters:

                {
                    var redVal   = imageParms.XPathSelectElement("/LaserImageParms/BaseParms/ImageColor/Red").Value;
                    var greenVal = imageParms.XPathSelectElement("/LaserImageParms/BaseParms/ImageColor/Green").Value;
                    var blueVal  = imageParms.XPathSelectElement("/LaserImageParms/BaseParms/ImageColor/Blue").Value;

                    if (null == redVal || null == greenVal || null == blueVal)
                    {
                        Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");

                        break;
                    }

                    byte cRed   = Convert.ToByte(redVal);
                    byte cGreen = Convert.ToByte(greenVal);
                    byte cBlue  = Convert.ToByte(blueVal);

                    model.FgColorR = cRed;
                    model.FgColorG = cGreen;
                    model.FgColorB = cBlue;
                }

                {
                    var redVal   = imageParms.XPathSelectElement("/LaserImageParms/BaseParms/ScreenColor/Red").Value;
                    var greenVal = imageParms.XPathSelectElement("/LaserImageParms/BaseParms/ScreenColor/Green").Value;
                    var blueVal  = imageParms.XPathSelectElement("/LaserImageParms/BaseParms/ScreenColor/Blue").Value;

                    if (null == redVal || null == greenVal || null == blueVal)
                    {
                        Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");

                        break;
                    }

                    byte cRed   = Convert.ToByte(redVal);
                    byte cGreen = Convert.ToByte(greenVal);
                    byte cBlue  = Convert.ToByte(blueVal);

                    model.BgColorR = cRed;
                    model.BgColorG = cGreen;
                    model.BgColorB = cBlue;
                }


                var element = imageParms.Descendants("ImageSize");

                var imgSiz = element.FirstOrDefault();

                if (null != imgSiz)
                {
                    model.ImageSize = Convert.ToInt32(imgSiz.Value);
                }
                else
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");

                    break;
                }


                element = imageParms.Descendants("ImageZoom");

                var zoom = element.FirstOrDefault();

                if (null != zoom)
                {
                    model.Zoom = Convert.ToInt32(zoom.Value);
                }
                else
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");

                    break;
                }

                model.AspectRatio = Convert.ToInt32(imageParms.XPathSelectElement("/LaserImageParms/BaseParms/AspectRatio").Value);

                break;
            }

            #endregion

            // Load all the image generator parameters:

            {
                var element = imageParms.Descendants("Mode");
                var mode = element.FirstOrDefault();

                if (null != mode)
                {
                    string strMode = mode.Value;

                    model.ImageDrawingMode = (Laserocitor.Common.eMODE)Enum.Parse(typeof(Laserocitor.Common.eMODE), strMode);
                }

                #region Cycloid Mode parameters.

                // Cycloid mode parms:

                element = imageParms.Descendants("CycloidRate");

                var cycloidRate = element.FirstOrDefault();

                if (null != cycloidRate)
                {
                    model.CycloidRate = Convert.ToInt32(cycloidRate.Value);
                }

                element = imageParms.Descendants("FixedCircleRadius");

                var fixedCircleRadius = element.FirstOrDefault();

                if (null != fixedCircleRadius)
                {
                    model.CycloidRf = Convert.ToInt32(fixedCircleRadius.Value);
                }


                element = imageParms.Descendants("RollingCircleRadius");

                var rollingCircleRadius = element.FirstOrDefault();

                if (null != rollingCircleRadius)
                {
                    model.CycloidRr = Convert.ToInt32(rollingCircleRadius.Value);
                }


                element = imageParms.Descendants("SweepLength");

                var sweepLength = element.FirstOrDefault();

                if (null != sweepLength)
                {
                    model.CycloidL = Convert.ToInt32(sweepLength.Value);
                }


                var xAutoCSLEnable = imageParms.XPathSelectElement("/LaserImageParms/ImageParms/CycloidParms/AutoCSL").Value;

                switch (xAutoCSLEnable)
                {
                    case "true":
                        {
                            model.AutoCSLEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.AutoCSLEnabled = false;
                        }
                        break;
                }


                element = imageParms.Descendants("CSLWaveForm");

                var oscCSLWaveForm = element.FirstOrDefault();

                if (null != oscCSLWaveForm)
                {
                    string strOscWaveform = oscCSLWaveForm.Value;

                    model.ASLModOscWave = (Laserocitor.Common.eWAVEFORM)Enum.Parse(typeof(Laserocitor.Common.eWAVEFORM), strOscWaveform);
                }

                element = imageParms.Descendants("CSLFreq");

                var cslRate = element.FirstOrDefault();

                if (null != cslRate)
                {
                    model.CSLOscFreq = Convert.ToInt32(cslRate.Value);
                }


                element = imageParms.Descendants("CSLAmpl");

                var cslAmpl = element.FirstOrDefault();

                if (null != cslAmpl)
                {
                    model.CSLOscAmpl = Convert.ToInt32(cslAmpl.Value);
                }

                #endregion

                #region Lissajous mode parameters.


                // Lissajous mode parameters:

                element = imageParms.Descendants("XOscWaveform");

                var oscXWaveForm = element.FirstOrDefault();

                if (null != oscXWaveForm)
                {
                    string strOscWaveform = oscXWaveForm.Value;

                    model.LMXOscWave = (Laserocitor.Common.eWAVEFORM)Enum.Parse(typeof(Laserocitor.Common.eWAVEFORM), strOscWaveform);
                }

                element = imageParms.Descendants("YOscWaveform");

                var oscYWaveForm = element.FirstOrDefault();

                if (null != oscYWaveForm)
                {
                    string strOscWaveform = oscYWaveForm.Value;

                    model.LMYOscWave = (Laserocitor.Common.eWAVEFORM)Enum.Parse(typeof(Laserocitor.Common.eWAVEFORM), strOscWaveform);
                }


                element = imageParms.Descendants("XModOscWaveform");

                var oscXMWaveForm = element.FirstOrDefault();

                if (null != oscYWaveForm)
                {
                    string strOscWaveform = oscXMWaveForm.Value;

                    model.LMXModOscWave = (Laserocitor.Common.eWAVEFORM)Enum.Parse(typeof(Laserocitor.Common.eWAVEFORM), strOscWaveform);
                }

                element = imageParms.Descendants("YModOscWaveform");

                var oscYMWaveForm = element.FirstOrDefault();

                if (null != oscYWaveForm)
                {
                    string strOscWaveform = oscYMWaveForm.Value;

                    model.LMYModOscWave = (Laserocitor.Common.eWAVEFORM)Enum.Parse(typeof(Laserocitor.Common.eWAVEFORM), strOscWaveform);
                }


                element = imageParms.Descendants("XOscFrequency");

                var xOscFreq = element.FirstOrDefault();

                if (null != xOscFreq)
                {
                    model.LXOsc1Freq = Convert.ToInt32(xOscFreq.Value);
                }


                element = imageParms.Descendants("YOscFrequency");

                var yOscFreq = element.FirstOrDefault();

                if (null != yOscFreq)
                {
                    model.LYOsc1Freq = Convert.ToInt32(yOscFreq.Value);
                }


                element = imageParms.Descendants("XModOscFrequency");

                var xmOscFreq = element.FirstOrDefault();

                if (null != xOscFreq)
                {
                    model.LXMOscFreq = Convert.ToInt32(xmOscFreq.Value);
                }


                element = imageParms.Descendants("YModOscFrequency");

                var ymOscFreq = element.FirstOrDefault();

                if (null != ymOscFreq)
                {
                    model.LYMOscFreq = Convert.ToInt32(ymOscFreq.Value);
                }


                element = imageParms.Descendants("XOscAmplitude");

                var xOscAmpl = element.FirstOrDefault();

                if (null != xOscAmpl)
                {
                    model.LXOsc1Amplitude = Convert.ToInt32(xOscAmpl.Value);
                }


                element = imageParms.Descendants("YOscAmplitude");

                var yOscAmpl = element.FirstOrDefault();

                if (null != yOscAmpl)
                {
                    model.LYOsc1Amplitude = Convert.ToInt32(yOscAmpl.Value);
                }


                element = imageParms.Descendants("XModOscAmplitude");

                var xmOscAmpl = element.FirstOrDefault();

                if (null != xOscAmpl)
                {
                    model.LXMOscAmplitude = Convert.ToInt32(xmOscAmpl.Value);
                }


                element = imageParms.Descendants("YModOscAmplitude");

                var ymOscAmpl = element.FirstOrDefault();

                if (null != ymOscAmpl)
                {
                    model.LYMOscAmplitude = Convert.ToInt32(ymOscAmpl.Value);
                }


                element = imageParms.Descendants("XOscDutyCycle");

                var xOscDutyCycle = element.FirstOrDefault();

                if (null != xOscDutyCycle)
                {
                    model.LXOsc1DutyCycle = Convert.ToInt32(xOscDutyCycle.Value);
                }


                element = imageParms.Descendants("YOscDutyCycle");

                var yOscDutyCycle = element.FirstOrDefault();

                if (null != yOscDutyCycle)
                {
                    model.LYOsc1DutyCycle = Convert.ToInt32(yOscDutyCycle.Value);
                }


                element = imageParms.Descendants("XModOscDutyCycle");

                var xmOscDutyCycle = element.FirstOrDefault();

                if (null != xOscDutyCycle)
                {
                    model.LXMOscDutyCycle = Convert.ToInt32(xmOscDutyCycle.Value);
                }


                element = imageParms.Descendants("YModOscDutyCycle");

                var ymOscDutyCycle = element.FirstOrDefault();

                if (null != ymOscDutyCycle)
                {
                    model.LYMOscDutyCycle = Convert.ToInt32(ymOscDutyCycle.Value);
                }


                element = imageParms.Descendants("XModGain");

                var xModGain = element.FirstOrDefault();

                if (null != xModGain)
                {
                    model.LXModGain = Convert.ToInt32(xModGain.Value);
                }


                element = imageParms.Descendants("YModGain");

                var yModGain = element.FirstOrDefault();

                if (null != yModGain)
                {
                    model.LYModGain = Convert.ToInt32(yModGain.Value);
                }


                element = imageParms.Descendants("XModOffset");

                var xModOffset = element.FirstOrDefault();

                if (null != xModOffset)
                {
                    model.LXModOffset = Convert.ToInt32(xModOffset.Value);
                }


                element = imageParms.Descendants("YModOffset");

                var yModOffset = element.FirstOrDefault();

                if (null != yModOffset)
                {
                    model.LYModOffset = Convert.ToInt32(yModOffset.Value);
                }


                var xModEnable = imageParms.XPathSelectElement("/LaserImageParms/ImageParms/LissajousParms/XModEnabled").Value;

                switch (xModEnable)
                {
                    case "true":
                        {
                            model.LXModEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.LXModEnabled = false;
                        }
                        break;
                }


                var yModEnable = imageParms.XPathSelectElement("/LaserImageParms/ImageParms/LissajousParms/YModEnabled").Value;

                switch (yModEnable)
                {
                    case "true":
                        {
                            model.LYModEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.LYModEnabled = false;
                        }
                        break;
                }

                #endregion

                #region Radial Scan mode parameters.

                // Radial scan mode parameters:

                element = imageParms.Descendants("ModOsc1Waveform");

                var modOsc1WaveForm = element.FirstOrDefault();

                if (null != modOsc1WaveForm)
                {
                    string strModOsc1Waveform = modOsc1WaveForm.Value;

                    model.Osc1Waveform = (Laserocitor.Common.eWAVEFORM)Enum.Parse(typeof(Laserocitor.Common.eWAVEFORM), strModOsc1Waveform);
                }


                element = imageParms.Descendants("ModOsc1Freq");

                var modOsc1Freq = element.FirstOrDefault();

                if (null != modOsc1Freq)
                {
                    model.ModOscFreq = Convert.ToInt32(modOsc1Freq.Value);
                }


                element = imageParms.Descendants("ModOsc1Ampl");

                var modOsc1Ampl = element.FirstOrDefault();

                if (null != modOsc1Ampl)
                {
                    model.ModOscAmp = Convert.ToInt32(modOsc1Ampl.Value);
                }

                element = imageParms.Descendants("BaseGeometry");

                var baseGMode = element.FirstOrDefault();

                if (null != baseGMode)
                {
                    string strBaseGMode = baseGMode.Value;

                    model.RSBaseGeometry = (Laserocitor.Common.eBASEGEOMETRY)Enum.Parse(typeof(Laserocitor.Common.eBASEGEOMETRY), strBaseGMode);
                }

                var baseGDir = imageParms.XPathSelectElement("/LaserImageParms/ImageParms/RadialScanParms/BaseGeometryDir").Value;

                switch (baseGDir)
                {
                    case "eCW":
                        {
                            model.BaseGDirection = Laserocitor.Common.eDIRECTION.eCW;
                        }
                        break;

                    case "eCCW":
                        {
                            model.BaseGDirection = Laserocitor.Common.eDIRECTION.eCCW;
                        }
                        break;
                }


                element = imageParms.Descendants("BaseGeometryFreq");

                var quadOscFreq = element.FirstOrDefault();

                if (null != quadOscFreq)
                {
                    model.QuadOsc1Freq = Convert.ToInt32(quadOscFreq.Value);
                }

                element = imageParms.Descendants("QuadOsc1Phase");

                var quadOscPhase = element.FirstOrDefault();

                if (null != quadOscPhase)
                {
                    model.GenPhase = Convert.ToInt32(quadOscPhase.Value);
                }


                element = imageParms.Descendants("QuadOsc1Ampl");

                var quadOscAmpl = element.FirstOrDefault();

                if (null != quadOscAmpl)
                {
                    model.QuadOsc1Level = Convert.ToInt32(quadOscAmpl.Value);
                }


                element = imageParms.Descendants("MultGain");

                var multGain = element.FirstOrDefault();

                if (null != multGain)
                {
                    model.ModOscGain = Convert.ToInt32(multGain.Value);
                }


                element = imageParms.Descendants("MultOffset");

                var multOffset = element.FirstOrDefault();

                if (null != multOffset)
                {
                    model.ModOscOffset = Convert.ToInt32(multOffset.Value);
                }


                element = imageParms.Descendants("ModOsc2Waveform");

                var modOsc2WaveForm = element.FirstOrDefault();

                if (null != modOsc2WaveForm)
                {
                    string strModOsc2Waveform = modOsc2WaveForm.Value;

                    model.Osc2Waveform = (Laserocitor.Common.eWAVEFORM)Enum.Parse(typeof(Laserocitor.Common.eWAVEFORM), strModOsc2Waveform);
                }

                var osc2Invert = imageParms.XPathSelectElement("/LaserImageParms/ImageParms/RadialScanParms/Osc2/Invert").Value;

                switch (osc2Invert)
                {
                    case "true":
                        {
                            model.Osc2InvertEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.Osc2InvertEnabled = false;
                        }
                        break;
                }


                element = imageParms.Descendants("Osc2Level");

                var osc2Level = element.FirstOrDefault();

                if (null != osc2Level)
                {
                    model.Osc2Level = Convert.ToInt32(osc2Level.Value);
                }


                element = imageParms.Descendants("Osc2Rate");

                var osc2Rate = element.FirstOrDefault();

                if (null != osc2Rate)
                {
                    model.Osc2Freq = Convert.ToInt32(osc2Rate.Value);
                }

                // Perspective.

                var perspectiveX = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Perspective/X").Value;

                model.PerspectiveCtlV = Convert.ToInt32(perspectiveX);

                var perspectiveY = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Perspective/Y").Value;

                model.PerspectiveCtlH = Convert.ToInt32(perspectiveY);

                var perspective = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Perspective/Auto").Value;

                switch (perspective)
                {
                    case "true":
                        {
                            model.PerspectiveModEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.PerspectiveModEnabled = false;
                        }
                        break;
                }


                var persFreq = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Perspective/Frequency").Value;
                var persAmpl = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Perspective/Amplitude").Value;

                model.AutoPersRate  = Convert.ToInt32(persFreq);
                model.AutoPersLevel = Convert.ToInt32(persAmpl);

                var persDir = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Perspective/Direction").Value;

                switch (persDir)
                {
                    case "eCW":
                        {
                            model.PersModDir = Laserocitor.Common.eDIRECTION.eCW;
                        }
                        break;

                    case "eCCW":
                        {
                            model.PersModDir = Laserocitor.Common.eDIRECTION.eCCW;
                        }
                        break;
                }

                #endregion

                #region Spiral mode parameters.

                element = imageParms.Descendants("SpiralDirection");

                var spiralDir = element.FirstOrDefault();

                if (null != spiralDir)
                {
                    string strSpiralDir = spiralDir.Value;

                    model.SpiralDirection = (Laserocitor.Common.eDIRECTION)Enum.Parse(typeof(Laserocitor.Common.eDIRECTION), strSpiralDir);
                }


                element = imageParms.Descendants("SpiralPersDirection");

                var spiralPersDir = element.FirstOrDefault();

                if (null != spiralPersDir)
                {
                    string strSpiralPersDir = spiralPersDir.Value;

                    model.SpiralPersDirection = (Laserocitor.Common.eDIRECTION)Enum.Parse(typeof(Laserocitor.Common.eDIRECTION), strSpiralPersDir);
                }


                element = imageParms.Descendants("SpiralPersModEnable");

                var spiralPersModEnable = element.FirstOrDefault();

                if (null != spiralPersModEnable)
                {
                    model.SpiralPersModEnable = Convert.ToBoolean(spiralPersModEnable.Value);
                }


                element = imageParms.Descendants("SpiralPersJSXPos");

                var spiralPersJSXPos = element.FirstOrDefault();

                if (null != spiralPersJSXPos)
                {
                    model.SpiralPersJSXPos = Convert.ToDouble(spiralPersJSXPos.Value);
                }


                element = imageParms.Descendants("SpiralPersJSYPos");

                var spiralPersJSYPos = element.FirstOrDefault();

                if (null != spiralPersJSYPos)
                {
                    model.SpiralPersJSYPos = Convert.ToDouble(spiralPersJSYPos.Value);
                }


                element = imageParms.Descendants("SpiralPersModFrequency");

                var spiralPersModFrequency = element.FirstOrDefault();

                if (null != spiralPersModFrequency)
                {
                    model.SpiralPersModFrequency = Convert.ToDouble(spiralPersModFrequency.Value);
                }


                element = imageParms.Descendants("SpiralPersModLevel");

                var spiralPersModLevel = element.FirstOrDefault();

                if (null != spiralPersModLevel)
                {
                    model.SpiralPersModLevel = Convert.ToDouble(spiralPersModLevel.Value);
                }


                element = imageParms.Descendants("SpiralRampFrequency");

                var spiralRampFrequency = element.FirstOrDefault();

                if (null != spiralRampFrequency)
                {
                    model.SpiralRampFrequency = Convert.ToDouble(spiralRampFrequency.Value);
                }


                element = imageParms.Descendants("SpiralRampGain");

                var spiralRampGain = element.FirstOrDefault();

                if (null != spiralRampGain)
                {
                    model.SpiralRampGain = Convert.ToDouble(spiralRampGain.Value);
                }


                element = imageParms.Descendants("SpiralRampOffset");

                var spiralRampOffset = element.FirstOrDefault();

                if (null != spiralRampOffset)
                {
                    model.SpiralRampOffset = Convert.ToDouble(spiralRampOffset.Value);
                }


                element = imageParms.Descendants("SpiralSize");

                var spiralSize = element.FirstOrDefault();

                if (null != spiralSize)
                {
                    model.SpiralSize = Convert.ToDouble(spiralSize.Value);
                }
 
                #endregion
            }

            #region Modifier parameters.

            // Load all the Modifier parameters:

            {
                // Axis rotations:

                var rotX = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/RotationMatrix/XRotation/XAngle").Value;
                var rotY = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/RotationMatrix/YRotation/YAngle").Value;
                var rotZ = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/RotationMatrix/ZRotation/ZAngle").Value;

                model.AxisRotX = Convert.ToInt32(rotX);
                model.AxisRotY = Convert.ToInt32(rotY);
                model.AxisRotZ = Convert.ToInt32(rotZ);


                // Translation:

                var posX = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Translation/XPos").Value;
                var posY = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Translation/YPos").Value;

                model.TranslationJoystickXPos = Convert.ToInt32(posX);
                model.TranslationJoystickYPos = Convert.ToInt32(posY);


                var orbitAngle = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Translation/OrbitAngle").Value;

                model.Orbit = Convert.ToInt32(orbitAngle);


                var posMod = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Translation/Auto").Value;

                if (null != posMod)
                {
                    switch (posMod)
                    {
                        case "true":
                            {
                                model.PosModEnabled = true;
                            }
                            break;

                        case "false":
                            {


                                model.PosModEnabled = false;
                            }
                            break;
                    }
                }
                else
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");
                }

                var coupleOrbit = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Translation/CoupleOrbit");

                if (null != coupleOrbit)
                {
                    switch (coupleOrbit.Value)
                    {
                        case "true":
                            {
                                model.CoupleOrbit = true;
                            }
                            break;

                        case "false":
                            {
                                model.CoupleOrbit = false;
                            }
                            break;
                    }
                }
                else
                {
                    model.CoupleOrbit = false;
                }


                var posDir = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Translation/PosModDirection").Value;

                switch (posDir)
                {
                    case "eCW":
                        {
                            model.AutoPosDir = Laserocitor.Common.eDIRECTION.eCW;
                        }
                        break;

                    case "eCCW":
                        {
                            model.AutoPosDir = Laserocitor.Common.eDIRECTION.eCCW;
                        }
                        break;
                }


                var posModFreq = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Translation/PosModFreq").Value;

                model.AutoPosRate = Convert.ToInt32(posModFreq);


                var posModLevel = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Translation/PosModRadius").Value;

                model.AutoPosLevel = Convert.ToInt32(posModLevel);


                // Color Modulation:

                var colMod = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/Enabled").Value;

                if (null == colMod)
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");
                }

                switch (colMod)
                {
                    case "true":
                        {
                            model.ColorModEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.ColorModEnabled = false;
                        }
                        break;
                }

                // Color Modulation Mode:

                var colModMode = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/ColorModMode").Value;

                if (null == colModMode)
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");
                }

                switch (colModMode)
                {
                    case "eSynchronized":
                        {

                            model.ColorModMode = Laserocitor.Common.eCOLORMODMODE.eSynchronized;
                        }
                        break;

                    case "eUnsynchronized":
                        {
                            model.ColorModMode = Laserocitor.Common.eCOLORMODMODE.eUnsynchronized;
                        }
                        break;
                }

                // Color Modulation Blue Source:

                var colModBlue = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/ColorModBlue").Value;

                if (null == colModBlue)
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");
                }

                switch (colModBlue)
                {
                    case "eOSCAPOS":
                        {
                            model.ColorModOscAssignBlue = Laserocitor.Common.eCOLORMODOSC.eOSCAPOS;
                        }
                        break;

                    case "eOSCANEG":
                        {
                            model.ColorModOscAssignBlue = Laserocitor.Common.eCOLORMODOSC.eOSCANEG;
                        }
                        break;

                    case "eOSCBPOS":
                        {
                            model.ColorModOscAssignBlue = Laserocitor.Common.eCOLORMODOSC.eOSCBPOS;
                        }
                        break;

                    case "eOSCBNEG":
                        {
                            model.ColorModOscAssignBlue = Laserocitor.Common.eCOLORMODOSC.eOSCBNEG;
                        }
                        break;

                    case "eOSCCPOS":
                        {
                            model.ColorModOscAssignBlue = Laserocitor.Common.eCOLORMODOSC.eOSCCPOS;
                        }
                        break;

                    case "eOSCCNEG":
                        {
                            model.ColorModOscAssignBlue = Laserocitor.Common.eCOLORMODOSC.eOSCCNEG;
                        }
                        break;
                }

                // Color Modulation Green Source:

                var colModGreen = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/ColorModGreen").Value;

                if (null == colModBlue)
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");
                }

                switch (colModGreen)
                {
                    case "eOSCAPOS":
                        {
                            model.ColorModOscAssignGreen = Laserocitor.Common.eCOLORMODOSC.eOSCAPOS;
                        }
                        break;

                    case "eOSCANEG":
                        {
                            model.ColorModOscAssignGreen = Laserocitor.Common.eCOLORMODOSC.eOSCANEG;
                        }
                        break;

                    case "eOSCBPOS":
                        {
                            model.ColorModOscAssignGreen = Laserocitor.Common.eCOLORMODOSC.eOSCBPOS;
                        }
                        break;

                    case "eOSCBNEG":
                        {
                            model.ColorModOscAssignGreen = Laserocitor.Common.eCOLORMODOSC.eOSCBNEG;
                        }
                        break;

                    case "eOSCCPOS":
                        {
                            model.ColorModOscAssignGreen = Laserocitor.Common.eCOLORMODOSC.eOSCCPOS;
                        }
                        break;

                    case "eOSCCNEG":
                        {
                            model.ColorModOscAssignGreen = Laserocitor.Common.eCOLORMODOSC.eOSCCNEG;
                        }
                        break;
                }

                // Color Modulation Red Source:

                var colModRed = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/ColorModRed").Value;

                if (null == colModRed)
                {
                    MessageBox.Show("The selected image file cannot be read");
                }

                switch (colModRed)
                {
                    case "eOSCAPOS":
                        {
                            model.ColorModOscAssignRed = Laserocitor.Common.eCOLORMODOSC.eOSCAPOS;
                        }
                        break;

                    case "eOSCANEG":
                        {
                            model.ColorModOscAssignRed = Laserocitor.Common.eCOLORMODOSC.eOSCANEG;
                        }
                        break;

                    case "eOSCBPOS":
                        {
                            model.ColorModOscAssignRed = Laserocitor.Common.eCOLORMODOSC.eOSCBPOS;
                        }
                        break;

                    case "eOSCBNEG":
                        {
                            model.ColorModOscAssignRed = Laserocitor.Common.eCOLORMODOSC.eOSCBNEG;
                        }
                        break;

                    case "eOSCCPOS":
                        {
                            model.ColorModOscAssignRed = Laserocitor.Common.eCOLORMODOSC.eOSCCPOS;
                        }
                        break;

                    case "eOSCCNEG":
                        {
                            model.ColorModOscAssignRed = Laserocitor.Common.eCOLORMODOSC.eOSCCNEG;
                        }
                        break;
                }

                var colorModFreq = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/Frequency").Value;

                model.ColorModFreq = Convert.ToInt32(colorModFreq);

                var colorModAFreq = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/FrequencyA").Value;

                model.OscAFreq = Convert.ToInt32(colorModAFreq);

                var colorModBFreq = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/FrequencyB").Value;

                model.OscBFreq = Convert.ToInt32(colorModBFreq);

                var colorModCFreq = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/FrequencyC").Value;

                model.OscCFreq = Convert.ToInt32(colorModCFreq);

                var colorSync = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/ColorModulation/SyncSource").Value;

                if (null != colorSync)
                {
                    model.ColorModSyncSource = (Laserocitor.Common.eCOLORMODSYNCSOURCE)Enum.Parse(typeof(Laserocitor.Common.eCOLORMODSYNCSOURCE), colorSync);
                }
                else
                {
                    Common.CommonUtils.ShowMessage("Unable to load file", "The selected image file cannot be read");
                }


                // Chopper:

                var chopper = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Chopper/Enabled").Value;

                switch (chopper)
                {
                    case "true":
                        {
                            model.ChopperEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.ChopperEnabled = false;
                        }
                        break;
                }


                var chopFreq = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Chopper/Frequency").Value;

                model.ChopperFrequency = Convert.ToInt32(chopFreq);

                var chopDutyCycle = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Chopper/DutyCycle").Value;

                model.ChopperDutyCycle = Convert.ToInt32(chopDutyCycle);


                var chopperR = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Chopper/EnabledR").Value;

                switch (chopperR)
                {
                    case "true":
                        {
                            model.ChopperREnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.ChopperREnabled = false;
                        }
                        break;
                }


                var chopperG = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Chopper/EnabledG").Value;

                switch (chopperG)
                {
                    case "true":
                        {
                            model.ChopperGEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.ChopperGEnabled = false;
                        }
                        break;
                }


                var chopperB = imageParms.XPathSelectElement("/LaserImageParms/ModifierParms/Chopper/EnabledB").Value;

                switch (chopperB)
                {
                    case "true":
                        {
                            model.ChopperBEnabled = true;
                        }
                        break;

                    case "false":
                        {
                            model.ChopperBEnabled = false;
                        }
                        break;
                }
            }

            #endregion

            return model;
        }
    }
}
