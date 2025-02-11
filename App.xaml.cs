// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.

using System.Windows;
using Laserocitor.AudioInterface;
using Laserocitor.Utils.Diagnostics;
using Laserocitor.Views.Main;

namespace Laserocitor
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Constructor (default).
        /// </summary>
        public App()
        {
            Logger.GetInstance().Start(Logger.OutputFormat.eANSIText, "Log", true);

            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eInfo,
                LogMessage.LogMessageType.eSubCall,
                GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name));


            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eInfo,
                LogMessage.LogMessageType.eSubExit,
                GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name));
        }

        /// <summary>
        /// Handle OnExit event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eInfo,
                LogMessage.LogMessageType.eSubCall,
                GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name));

            AudioSampleWriter.GetInstance().Stop();

            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eInfo, 
                LogMessage.LogMessageType.eSubExit, 
                GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name));

            base.OnExit(e);
        }

        /// <summary>
        /// Handle OnStartup event.
        /// </summary>
        /// <param name="e">StartupEventArs object.</param>
        protected override void OnStartup(StartupEventArgs e) 
        {
            base.OnStartup(e);

            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eInfo, 
                LogMessage.LogMessageType.eSubCall, 
                GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name));

            MainWindow app = new MainWindow();

            app.Show();

            // ReSharper disable once PossibleNullReferenceException
            Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eInfo,
                LogMessage.LogMessageType.eSubExit,
                GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name));
        }
    }
}
