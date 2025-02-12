// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.

using Laserocitor.Models;
using Laserocitor.ViewModels.Laserocitor;

namespace Laserocitor.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    partial class MainWindow
    {
        private LaserocitorVM laserocitorVM;

        /// <summary>
        /// Constructor (default).
        /// </summary>
        public MainWindow()
        {
            laserocitorVM = new LaserocitorVM(new LaserocitorModel());

            DataContext = laserocitorVM;

            InitializeComponent();
        }

        /// <summary>
        /// Required, but unused override.
        /// </summary>
        /// <param name="connectionIdUnused"></param>
        /// <param name="targetUnused"></param>
        public void Connect(int connectionIdUnused, object targetUnused)
        {
            throw new System.NotImplementedException();
        }
    }
}
