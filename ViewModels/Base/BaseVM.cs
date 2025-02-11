// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

using Laserocitor.Utils.Diagnostics;

namespace Laserocitor.ViewModels.Base
{
    /// <summary>
    /// The basic View Model. 
    /// <br/>
    /// This base View Model implements the INotifyPropertyChanged interface to handle property change notifications
    /// needed for the MVVM framework. It is expected that all other View Models will be derived from this class.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly bool _isInDesignMode;

        /// <summary>
        /// Constructor (static).
        /// </summary>
        static BaseViewModel()
        {
            var prop = DesignerProperties.IsInDesignModeProperty;

            _isInDesignMode
                    = (bool)DependencyPropertyDescriptor
                    .FromProperty(prop, typeof(FrameworkElement))
                    .Metadata.DefaultValue;
        }

        /// <summary>
        /// Handle the OnPropertyChanged event.
        /// </summary>
        /// <param name="propertyName">String contating the name of the property that triggered the event.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = PropertyChanged;

            if (null == handler)
            {
                return;
            }

            var e = new PropertyChangedEventArgs(propertyName);

            handler(this, e);
        }

        /// <summary>
        /// Constructor (default).
        /// </summary>
        public BaseViewModel()
        {
        }

        /// <summary>
        /// Verify the the named property exists and log a warning message if it does not.
        /// </summary>
        /// <param name="propertyName">String containing the name of the property to be verified.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, 
            // public, instance property on this object. 

            if (null == TypeDescriptor.GetProperties(this)[propertyName])
            {
                // ReSharper disable once PossibleNullReferenceException
                Logger.GetInstance().Log(new LogMessage(LogMessage.Severity.eWarning,  
                    LogMessage.LogMessageType.eSubInfo, 
                    GetType().FullName, 
                    System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "Attempt to access unrecognized property '" + propertyName + "' failed"));
            }
        }

        #region Public Properties

        public bool IsInDesignMode
        {
            get
            {
                return _isInDesignMode;
            }
        }

        #endregion
    }
}