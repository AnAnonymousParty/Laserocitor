// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;
using System.Threading;

using Laserocitor.Common;
using Laserocitor.Utils.Diagnostics;
using NAudio.Wave;

namespace Laserocitor.AudioInterface
{
    /// <summary>
    /// This singleton class is responsible for providing the audio
    /// sample sending thread and functions to start and stop it.
    /// </summary>
    internal class AudioSampleWriter
    {
        private static readonly object _lock = new object();

        private static AudioSampleWriter _instance = null;

        private bool ok2Run;

        private CircularQueue<AudioSample> sampleQueue;

        private WaveFormat waveFormat;

        private BufferedWaveProvider bufferedWaveProvider;

        private WaveOut waveOut;  

        /// <summary>
        /// Obtain a reference to the singleton instance of this class.
        /// </summary>
        /// <returns>The sole instance of this class.</returns>
        public static AudioSampleWriter GetInstance()
        {
            if (null == _instance)
            {
                lock (_lock)
                {
                    if (null == _instance)
                    {
                        _instance = new AudioSampleWriter();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Start the audio sample writing thread..
        /// </summary>
        /// <param sampleQueue>A CircularQueue object that provides audio samples.</param>
        public void Start(CircularQueue<AudioSample> sampleQueue)
        {
            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                                                    LogMessage.LogMessageType.eSubCall,
                                                    GetType().FullName,
                                                    System.Reflection.MethodBase.GetCurrentMethod().Name));
            this.sampleQueue = sampleQueue;

            ok2Run = true;

            Thread thread = new Thread(new ThreadStart(AudioWriter));

            thread.Start();

            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                                                    LogMessage.LogMessageType.eSubExit,
                                                    GetType().FullName,
                                                    System.Reflection.MethodBase.GetCurrentMethod().Name));
        }

        /// <summary>
        /// Stop the audio sample writing thread.
        /// </summary>
        public void Stop()
        {
            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                                                    LogMessage.LogMessageType.eSubCall,
                                                    GetType().FullName,
                                                    System.Reflection.MethodBase.GetCurrentMethod().Name));

            ok2Run = false;

            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                                                    LogMessage.LogMessageType.eSubExit,
                                                    GetType().FullName,
                                                    System.Reflection.MethodBase.GetCurrentMethod().Name));
        }

        /// <summary>
        /// This function provides the guts of the thread that writes audio data
        /// samples to the system audio interface.
        /// </summary>
        private void AudioWriter()
        {
            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                                                    LogMessage.LogMessageType.eSubCall,
                                                    GetType().FullName,
                                                    System.Reflection.MethodBase.GetCurrentMethod().Name));
            waveOut.Init(bufferedWaveProvider);
            waveOut.Play();

            while (true == ok2Run)
            {
                if (true == sampleQueue.IsEmpty())
                {
                    continue;
                }

                AudioSample sample = sampleQueue.DeQueue();

                byte[] sampleBuffer = new byte[2 * sizeof(UInt16)];

                byte[] bytes = new byte[sizeof(UInt16)];

                bytes = BitConverter.GetBytes(sample.LeftValue);

                sampleBuffer[0] = bytes[0];
                sampleBuffer[1] = bytes[1];

                bytes = BitConverter.GetBytes(sample.RightValue);

                sampleBuffer[2] = bytes[0];
                sampleBuffer[3] = bytes[1];

                try
                {
                    bufferedWaveProvider.AddSamples(sampleBuffer, 0, sampleBuffer.Length);
                }
                catch(Exception ex)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eWarning,
                                                            LogMessage.LogMessageType.eException,
                                                            GetType().FullName,
                                                            System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                            ex.ToString()));
                }
            }

            waveOut.Stop();
            
            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                                                    LogMessage.LogMessageType.eSubExit,
                                                    GetType().FullName,
                                                    System.Reflection.MethodBase.GetCurrentMethod().Name));
        }

        /// <summary>
        /// Constructor (private),
        /// </summary>
        private AudioSampleWriter()
        {
            waveFormat = new WaveFormat(44100, 16, 2);

            bufferedWaveProvider = new BufferedWaveProvider(waveFormat);

            waveOut = new WaveOut();

            waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
        }

        /// <summary>
        /// Handle playback stopped event.
        /// </summary>
        /// <param name="senderUnused">Object identifying event source (unused).</param>
        /// <param name="argsUnused">StoppedEventArgs object for conveying arguments (unused).</param>
        private void WaveOut_PlaybackStopped(object senderUnused, StoppedEventArgs argsUnused)
        {
            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                                                    LogMessage.LogMessageType.eSubCall,
                                                    GetType().FullName,
                                                    System.Reflection.MethodBase.GetCurrentMethod().Name));
            try
            {
                waveOut.Dispose();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleNullReferenceException
                Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eWarning,
                                                        LogMessage.LogMessageType.eException,
                                                        GetType().FullName,
                                                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                        ex.ToString()));
            }

            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eDebug,
                                                    LogMessage.LogMessageType.eSubExit,
                                                    GetType().FullName,
                                                    System.Reflection.MethodBase.GetCurrentMethod().Name));
        }
    }
}
