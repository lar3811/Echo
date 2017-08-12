using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Interface for a generic queue.
    /// </summary>
    /// <typeparam name="T">Type of objects to enqueue.</typeparam>
    public interface IProcessingQueue<T>
    {
        /// <summary>
        /// Number of objects in the queue.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Clears all objects from the queue.
        /// </summary>
        void Clear();

        /// <summary>
        /// Enqueues an object.
        /// </summary>
        /// <param name="element">Object to enqueue.</param>
        void Enqueue(T element);

        /// <summary>
        /// Dequeues an object.
        /// </summary>
        /// <returns>Dequeued object.</returns>
        T Dequeue();
    }
}
