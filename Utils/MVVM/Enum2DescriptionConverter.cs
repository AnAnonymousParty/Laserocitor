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
    /// <summary>
    /// This class is a kind of IValueConverter that converts an 
    /// enumerated value to its descriptive string and vice-versa.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(String))]
    public class Enum2DescriptionConverter : IValueConverter
    {
        /// <summary>
        /// Convert enumerated value to a string.
        /// </summary>
        /// <param name="value">Object containing value to be converted.</param>
        /// <param name="targetType">Desired type to be returned.</param>
        /// <param name="parameterUnused">Object containing any parameters (unused).</param>
        /// <param name="culture">CultureInfo object.</param>
        /// <returns>String containing converted result.</returns>
        public object Convert(object value, Type targetType, object parameterUnused, System.Globalization.CultureInfo culture)
        {
            Enum myEnum = (Enum)value;

            return GetEnumDescription(myEnum); ;
        }

        /// <summary>
        /// Convert string to enumerated value.
        /// </summary>
        /// <param name="value">Object containing string to be converted.</param>
        /// <param name="targetType">Desired type to be returned.</param>
        /// <param name="parameterUnused">Object containing any parameters (unused).</param>
        /// <param name="culture">CultureInfo object.</param>
        /// <returns>Object converted result.</returns>
        public object ConvertBack(object value, Type targetType, object parameterUnused, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }

        /// <summary>
        /// Given an enumerated value, return its descriptive string.
        /// </summary>
        /// <param name="enumObj">An enumerated value.</param>
        /// <returns>The equivalent descriptive string.</returns>
        private static String GetEnumDescription(Enum enumObj)
        {
            System.Reflection.FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            if (null == fieldInfo)
            {
                return enumObj.ToString();
            }

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (0 == attribArray.Length)
            {
                return enumObj.ToString();
            }

            System.ComponentModel.DescriptionAttribute attrib = attribArray[0] as System.ComponentModel.DescriptionAttribute;

            return attrib.Description;
        }
    }
}