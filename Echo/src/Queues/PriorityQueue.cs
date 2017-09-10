using Echo.Abstract;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Numerics;

namespace Echo.Queues
{
    /// <summary>
    /// Generic queue of elements sorted by estimated priority.
    /// </summary>
    /// <typeparam name="T">Type of elements to store.</typeparam>
    public class PriorityQueue<T> : IProcessingQueue<T>
    {
        /// <summary>
        /// Interface for evaluating element's priority.
        /// </summary>
        public interface IPriorityMeter
        {
            /// <summary>
            /// Evaluates priority of the <paramref name="subject"/>.
            /// </summary>
            /// <param name="subject">Subject to evaluate.</param>
            /// <returns>Priority level. Higher values indicate higher priority level.</returns>
            float Evaluate(T subject);
        }



        private readonly SortedList<float[], T> _lookup;
        private readonly IPriorityMeter[] _meters;

        

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="meters">Logic for priority evaluation. Meters will be invoked in the order they were supplied.</param>
        public PriorityQueue(params IPriorityMeter[] meters)
        {
            _lookup = new SortedList<float[], T>(new PriorityComparer());
            _meters = meters.ToArray();
        }
        
        /// <summary>
        /// Number of objects in the queue.
        /// </summary>
        public int Count
        {
            get { return _lookup.Count; }
        }
        
        /// <summary>
        /// Clears all objects from the queue.
        /// </summary>
        public void Clear()
        {
            _lookup.Clear();
        }

        /// <summary>
        /// Dequeues an element with highest priority level.
        /// </summary>
        /// <returns>Dequeued element.</returns>
        public T Dequeue()
        {
            var element = default(T);
            if (_lookup.Count > 0)
            {
                element = _lookup.First().Value;
                _lookup.RemoveAt(0);
            }
            return element;
        }

        /// <summary>
        /// Enqueues an element.
        /// </summary>
        /// <param name="element">An element to enqueue.</param>
        public void Enqueue(T element)
        {
            var priority = new float[_meters.Length];
            for (int i = 0; i < _meters.Length; i++)
            {
                priority[i] = _meters[i].Evaluate(element);
            }
            _lookup.Add(priority, element);
        }



        private class PriorityComparer : IComparer<float[]>
        {
            public int Compare(float[] priority1, float[] priority2)
            {
                for (int i = 0; i < Math.Min(priority1.Length, priority2.Length); i++)
                {
                    if (priority1[i] < priority2[i]) return 1;
                    else if (priority1[i] > priority2[i]) return -1;
                }

                if (priority1.Length < priority2.Length) return 1;
                else if (priority1.Length > priority2.Length) return -1;

                return 1;
            }
        }
    }
}
