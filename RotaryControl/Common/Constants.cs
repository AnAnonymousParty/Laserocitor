// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System;

namespace RotaryControl.Common
{
    /// <summary>
    /// Constant values used throughout the control.
    /// </summary>
    public class Constants
    {
        public const double constMaximumOuterDialBorderThickness = 30.0;
        public const double constMaximumInnerDialRadius          = 75;
        public const double constMaximumMajorTickIncrement       = 1000;
        public const double constMinimumOuterDialBorderThickness = 0.0;
        public const double constMinimumInnerDialRadius          = 0;
        public const double constMinimumMajorTickIncrement       = 0.1;
        public const double OneTwentyDegreesInRadians            = (Math.PI + Math.PI) / 3;
        public const double ThirtyDegreesInRadians               = Math.PI / 6;

        public const int constMaximumNumberOfMajorTicks = 20;
        public const int constMaximumNumberOfMinorTicks = 20;
        public const int constMinimumNumberOfMajorTicks = 3;
        public const int constMinimumNumberOfMinorTicks = 0;
    }
}
