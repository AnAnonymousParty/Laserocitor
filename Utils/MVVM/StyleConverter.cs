// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.
 

using System;
using System.Windows.Data;

namespace Laserocitor.Utils.MVVM
{
    public class StyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.FrameworkElement targetElement = values[0] as System.Windows.FrameworkElement;

            string styleName = values[1] as string;

            if (null == styleName)
            {
                return null;
            }

            System.Windows.Style newStyle = (System.Windows.Style)targetElement.TryFindResource(styleName);

            if (null == newStyle)
            {
                newStyle = (System.Windows.Style)targetElement.TryFindResource("MyDefaultStyleName"); // TODO: find a default style anyway.
            }

            return newStyle;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
