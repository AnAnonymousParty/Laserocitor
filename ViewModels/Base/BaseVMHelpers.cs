// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Laserocitor.ViewModels.Base
{
    /// <summary>
    /// Helper functions for the base VM.
    /// </summary>
    public static class BaseVMHelpers
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static string StripLeft(this string value, int length)
        {
            return value.Substring(length, value.Length - length);
        }

        public static void Raise(this PropertyChangedEventHandler eventHandler, object source, string propertyName)
        {
            var handlers = eventHandler;

            if (null != handlers)
            {
                handlers(source, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void Raise(this EventHandler eventHandler, object source)
        {
            var handlers = eventHandler;

            if (null != handlers)
            {
                handlers(source, EventArgs.Empty);
            }
        }

        public static void Register(this INotifyPropertyChanged model, string propertyName, Action whenChanged)
        {
            model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    whenChanged();
                }
            };
        }
    }
}
