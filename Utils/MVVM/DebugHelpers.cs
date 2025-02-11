// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


using System.Diagnostics;

namespace Laserocitor.Utils.MVVM
{
    class DebugHelpers
    {
        public static void AssertBadEnum(int iEnumIntVal)
        {
            Debug.Assert(false, "Encountered unrecognized enumerated value: " + iEnumIntVal.ToString());
        }
    }
}
