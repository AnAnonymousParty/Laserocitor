// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.


namespace Laserocitor.Geometry
{
    /// <summary>
    /// Container class for coordinate values associated with a point.
    /// </summary>
    class Point
    {
        #region Private Variables

        double _dX,
               _dY;

        #endregion


        #region Public properties

        public double XPoint
        {
            get
            {
                return _dX;
            }
            set
            {
                _dX = value;
            }
        }

        public double YPoint
        {
            get
            {
                return _dY;
            }
            set
            {
                _dY = value;
            }
        }

        #endregion


        #region Constructors/Initializers

        public Point(double dX, double dY)
        {
            _dX = dX;
            _dY = dY;
        }

        #endregion
    }
}
