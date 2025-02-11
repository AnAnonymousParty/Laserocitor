// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;
using System.IO;

namespace Laserocitor.Utils.Diagnostics
{
    /// <summary>
    /// Logger.
    /// 
    /// This singleton class handles all diagnostic logging for the application.
    /// 
    /// Unless otherwise specified, logs are written plain text format to a file named 'Log.txt' in the current output directory.
    /// 
    /// An alternate path/file name can be specified via a parameter in the Start() function. If an extension is provided it will
    /// be used, otherwise the extension is set to .txt or .xml depending on the selected output format.
    /// 
    /// There are three output formats available:
    /// 
    /// ANSIText:  Plain text with ANSI color coding to indicate the message severity.
    /// PlainText: Plain text without ANSI color sequences.
    /// XML:       XML formatted message.
    /// 
    /// The format is specified via a parameter to the Start() function.
    /// </summary>
    public sealed class Logger
    {
        private static readonly object _lock = new object();

        private static Logger _instance = null;

        private OutputFormat _format = OutputFormat.ePlainText;

        private String _filePathName;

        /// <summary>
        /// Output file format.
        /// </summary>
        public enum OutputFormat
        {
            eANSIText,
            ePlainText,
            eXML
        }

        /// <summary>
        /// Constructor (private),
        /// </summary>
        private Logger()
        {
            _filePathName = "Log.txt";  
        }

        /// <summary>
        /// Obtain a reference to the singleton instance of this class.
        /// </summary>
        /// <returns>The sole instance of this class.</returns>
        public static Logger GetInstance()
        {
            if (null == _instance) 
            {
                lock(_lock)
                {
                    if (null == _instance) 
                    {
                        _instance = new Logger();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Start the logger.
        /// </summary>
        /// <param name="format">The desired log message format.</param>
        /// <param name="filePathName">
        /// String containing the path and name of the log file. 
        /// If an null or empty string is provided the default path will be the current execution directory
        /// and the file name will be 'Log'.
        /// If the path/name has no extension, .txt or .xml will be added based on the specified format.
        /// </param>
        /// <param name="deleteExistingLog">true = Delete the given file, if it exists.</param>
        public void Start(OutputFormat format, String filePathName, bool deleteExistingLog)
        {
            _format = format;

            if (false == string.IsNullOrEmpty(filePathName))
            {
                _filePathName = filePathName;
            }

            if (false == Path.HasExtension(filePathName))
            {
                if (OutputFormat.eXML == format)
                {
                    _filePathName += ".xml";
                }
                else
                {
                    _filePathName += ".txt";
                }
            }

            if (true == deleteExistingLog)
            {
                if (true == File.Exists(_filePathName))
                {
                    File.Delete(_filePathName);
                }
            }
        }

        /// <summary>
        /// Append a log message.
        /// </summary>
        /// <param name="logMessage">A LogMessage object containing the message to be logged.</param>
        public void Log(LogMessage logMessage)
        {
            lock (this)
            {
                File.AppendAllText(_filePathName, logMessage.GetMessageText(_format) + Environment.NewLine);

                using (FileStream s2 = new FileStream(_filePathName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var streamWriter = new StreamWriter(s2))
                    {
                        streamWriter.WriteLine(logMessage.GetMessageText(_format) + Environment.NewLine);
                    }

                    s2.Close();
                }
            }
        }
    }
}
