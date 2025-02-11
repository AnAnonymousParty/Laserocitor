// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;
using System.Windows.Input;

namespace Laserocitor.Utils.MVVM
{
    /// <summary>
    /// Class that provides command handling from UI elements contained in a View.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Public Members.
        
        /// <summary>
        /// Constructor, given action object.
        /// </summary>
        /// <param name="action">Action object to be called on command invocation.</param>
        public RelayCommand(Action<object> action)
        {
            _action = action;
        }

        /// <summary>
        /// Constructor, given action and predicate objects.
        /// </summary>
        /// <param name="action">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> action, Predicate<object> canExecute)
        {
            if (null == action)
            {
                throw new ArgumentNullException("RelayCommand(Action, Predicate) action");
            }
            
            _action     = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Determine if command can be executed.
        /// </summary>
        /// <param [IN] name="parameter"></param>
        /// <returns>
        /// true  = command can be executed.
        /// false = command cannot be executed.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add 
            { 
                CommandManager.RequerySuggested += value; 
            }

            remove 
            {
                CommandManager.RequerySuggested -= value; 
            }
        }

        /// <summary>
        /// Execute command.
        /// </summary>
        /// <param name="parameter">
        /// Object containing function to be executed.
        /// </param>
        public void Execute(object parameter)
        {
            _action(parameter);
        }

        #endregion

        #region Private members.
        private readonly Action<object>    _action;
        private readonly Predicate<object> _canExecute;
        #endregion
    }
}
