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
    /// <typeparam name="T"></typeparam>
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



        private readonly SortedDictionary<float, Queue<T>> _lookup;
        private readonly IPriorityMeter _meter;
        private int _count;

        

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="meter">Logic for priority evaluation.</param>
        public PriorityQueue(IPriorityMeter meter)
        {
            _lookup = new SortedDictionary<float, Queue<T>>();
            _meter = meter;
        }
        
        /// <summary>
        /// Number of objects in the queue.
        /// </summary>
        public int Count
        {
            get { return _count; }
        }
        
        /// <summary>
        /// Clears all objects from the queue.
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _lookup.Clear();
        }

        /// <summary>
        /// Dequeues an element with highest priority level.
        /// </summary>
        /// <returns>Dequeued element.</returns>
        public T Dequeue()
        {
            if (_count > 0)
            {
                _count--;
                return _lookup.First(kvp => kvp.Value.Count > 0).Value.Dequeue();
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Enqueues the <paramref name="element"/>.
        /// </summary>
        public void Enqueue(T element)
        {
            var priority = _meter.Evaluate(element);
            Queue<T> queue;
            if (_lookup.TryGetValue(priority, out queue))
            {
                queue.Enqueue(element);
            }
            else
            {
                queue = new Queue<T>();
                queue.Enqueue(element);
                _lookup.Add(priority, queue);
            }
            _count++;
        }
    }
}
