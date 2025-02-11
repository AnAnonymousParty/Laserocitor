// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.



using System;
using System.Windows;

namespace Laserocitor.UserControls.ScreenParms
{
    /// <summary>
    /// Interaction logic for ScreenParms.xaml.
    /// </summary>
    public partial class ScreenParms
    {
        /// <summary>
        /// Constructor (default).
        /// </summary>
        public ScreenParms()
        {
            InitializeComponent();

            UpdateUI();
        }

        /// <summary>
        /// Unused required override to prevent warnings.
        /// </summary>
        /// <remarks>
        /// Attaches events and names to XAML-initiated content.
        /// </remarks>
        /// <param name="connectionIdUnused">An identifier token to distinguish calls.</param>
        /// <param name="targetUnused">The target to connect events and names to.</param>
        public void Connect(int connectionIdUnused, object targetUnused)
        {
            throw new NotImplementedException();
        }

        #region Dependency Properties

        #region Screen Background Color Blue property.

        public static readonly DependencyProperty SPBgColorBProperty = DependencyProperty.Register("SPBgColorB",
                                                                                                   typeof(int),
                                                                                                   typeof(ScreenParms),
                                                                                                   new PropertyMetadata(0));
        public int SPBgColorB
        {
            get { return (int)GetValue(SPBgColorBProperty); }
            set { SetValue(SPBgColorBProperty, value); }
        }

        #endregion

        #region Screen Background Color Green property.

        public static readonly DependencyProperty SPBgColorGProperty = DependencyProperty.Register("SPBgColorG",
                                                                                                   typeof(int),
                                                                                                   typeof(ScreenParms),
                                                                                                   new PropertyMetadata(0));
        public int SPBgColorG
        {
            get { return (int)GetValue(SPBgColorGProperty); }
            set { SetValue(SPBgColorGProperty, value); }
        }

        #endregion

        #region Screen Background Color Red property.

        public static readonly DependencyProperty SPBgColorRProperty = DependencyProperty.Register("SPBgColorR",
                                                                                                   typeof(int),
                                                                                                   typeof(ScreenParms),
                                                                                                   new PropertyMetadata(0));
        public int SPBgColorR
        {
            get { return (int)GetValue(SPBgColorRProperty); }
            set { SetValue(SPBgColorRProperty, value); }
        }

        #endregion

        #region Screen Background Color RGB property.

        public static readonly DependencyProperty SPBgColorRGBProperty = DependencyProperty.Register("SPBgColorRGB",
                                                                                                     typeof(int),
                                                                                                     typeof(ScreenParms),
                                                                                                     new PropertyMetadata(0));
        public int SPBgColorRGB
        {
            get { return (int)GetValue(SPBgColorRGBProperty); }
            set { SetValue(SPBgColorRGBProperty, value); }
        }

        #endregion

        #region Section Title Text property.

        public static readonly DependencyProperty SectionTitleTextProperty = DependencyProperty.Register("SectionTitleText",
                                                                                                         typeof(string),
                                                                                                         typeof(ScreenParms),
                                                                                                         new PropertyMetadata(""));
        public String SectionTitleText
        {
            get { return (String)GetValue(SectionTitleTextProperty); }
            set { SetValue(SectionTitleTextProperty, value); }
        }

        #endregion

        #endregion

        /// <summary>
        /// Ensure the UI reflects the current state of the system.
        /// </summary>
        private void UpdateUI()
        {

        }
    }
}
