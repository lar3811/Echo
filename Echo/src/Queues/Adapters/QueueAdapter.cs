using Echo.Abstract;
using System.Collections.Generic;

namespace Echo.Queues.Adapters
{
    /// <summary>
    /// Wrapper for the <see cref="Queue{T}"/> class.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to store.</typeparam>
    public class QueueAdapter<TWave> : IProcessingQueue<TWave> where TWave : IWave
    {
        private readonly Queue<TWave> _queue;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        public QueueAdapter() : this(new Queue<TWave>()) { }

        /// <summary>
        /// Creates an instance of the class from an existing <see cref="Queue{T}"/> object.
        /// </summary>
        public QueueAdapter(Queue<TWave> queue)
        {
            _queue = queue;
        }

        /// <summary>
        /// Number of waves in the queue.
        /// </summary>
        public int Count => _queue.Count;

        /// <summary>
        /// Clears all waves from the queue.
        /// </summary>
        public void Clear()
        {
            _queue.Clear();
        }

        /// <summary>
        /// Dequeues first wave in the queue.
        /// </summary>
        /// <returns>Dequeued wave.</returns>
        public TWave Dequeue()
        {
            return _queue.Dequeue();
        }

        /// <summary>
        /// Enqueues a wave.
        /// </summary>
        /// <param name="wave">A wave to enqueue.</param>
        public void Enqueue(TWave wave)
        {
            _queue.Enqueue(wave);
        }
    }
}
