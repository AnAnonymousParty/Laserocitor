// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.
// Saint Charles, MO  63303. (618) 741-1153. afs.sbl@gmail.com.

using System;

namespace Laserocitor.AudioInterface
{
    /// <summary>
    /// Container for a 16 bit audio sample with left and right components.
    /// </summary>
    public class AudioSample
    {
        private UInt16 leftValue,
                       rightValue;

        /// <summary>
        /// Constructor, given:
        /// </summary>
        /// <param name="lValue">Left value.</param>
        /// <param name="rValue">Right Value.</param>
        public AudioSample(UInt16 lValue, UInt16 rValue)
        {
            leftValue   = lValue;
            rightValue = rValue;
        }

        /// <summary>
        /// Left value property.
        /// </summary>
        public UInt16 LeftValue
        {
            get { return leftValue; }
            private set { }
        }

        /// <summary>
        /// Right value property.
        /// </summary>
        public UInt16 RightValue
        {
            get { return rightValue; }
            private set { }
        }
    }
}
