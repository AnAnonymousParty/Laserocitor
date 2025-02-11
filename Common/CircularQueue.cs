// For the record . . .                                                                                                          
//                                                                                                                               
// Copyright © 2025 by Laserocitors.                                                                        
//                                                                                                                               
// All rights reserved.  No part of this program may be reproduced, stored in a
// retrieval system or transmitted, in any form or by any means, including but 
// not limited to electronic, mechanical, photocopying, recording, or otherwise, 
// without the express prior written consent of Laserocitors afs@laserocitors.com.
 

using System;

namespace Laserocitor.Common
{
    /// <summary>
    /// Generic circular queue.
    /// </summary>
    /// <typeparam name="T">Support queue of elements of any type.</typeparam>
    public class CircularQueue<T>
    {
        private readonly T[] circularQueue;  // The buffer.

        private bool overflowable;  // true == allow overflow by overwriting the last value. false = trigger exception on buffer full.

        private int queueInNdx;   // Index of next available queue slot to add an element.
        private int queueOutNdx;  // Index of next available queue slot to get an element.
        private int queueSize;    // Size of queue.

        /// <summary>
        /// Constructor, given:
        /// </summary>
        /// <param name="maxElements">Size of queue.</param>
        /// <param name="allowOverFlow">true = allow overflow. false = overflow triggers exception on EnQueue.</param>
        public CircularQueue(int maxElements, bool allowOverFlow)
        {
            queueSize    = maxElements;
            overflowable = allowOverFlow;

            circularQueue = new T[queueSize];

            ClearQueue();
        }

        /// <summary>
        /// Clear the queue.
        /// </summary>>
        public void ClearQueue()
        {
            lock (this)
            {
                queueInNdx = queueOutNdx = 0;
            }
        }

        /// <summary>
        /// Attempt to get element from the queue.
        /// </summary>
        /// <returns>The next available element, or default value if the queue is empty.</returns>
        public T DeQueue()
        {
            lock (this)
            {
                if (queueInNdx == queueOutNdx)
                {
                    return default;
                }

                T element = circularQueue[queueOutNdx];

                queueOutNdx = (queueOutNdx + 1) % circularQueue.Length;

                return element;
            }
        }

        /// <summary>
        /// Attempt to add an element to the queue.
        /// </summary>
        /// <param name="element">Element of the type the queue supports to be added to the queue.</param>
        /// <exception cref="InvalidOperationException">If the queue is full and overflow is not allowed.</exception>
        public void EnQueue(T element)
        {
            lock (this)
            {
                if ((queueInNdx + 1) % circularQueue.Length == queueOutNdx)
                {
                    if (false == overflowable)
                    {
                        throw new InvalidOperationException("Buffer full");
                    }

                    circularQueue[queueInNdx] = element;

                    return;
                }

                circularQueue[queueInNdx] = element;

                queueInNdx = (queueInNdx + 1) % circularQueue.Length;
            }
        }

        /// <summary>
        /// Check to see if the queue is empty.
        /// </summary>
        /// <returns>true = queue is empty, false = queue is not empty.</returns>
        public bool IsEmpty()
        {
            return (queueInNdx == queueOutNdx);
        }

        /// <summary>
        /// Check to see if the queue is full.
        /// </summary>
        /// <returns>true = queue is full, false = queue is not full.</returns>
        public bool IsFull()
        {
            lock (this)
            {
                if ((queueInNdx + 1) % circularQueue.Length == queueOutNdx)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Get queue size.
        /// </summary>
        /// <returns>Queue size.</returns>
        public int GetSize()
        {
            return queueSize;
        }
    }
}