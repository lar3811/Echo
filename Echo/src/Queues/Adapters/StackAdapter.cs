using Echo;
using Echo.Abstract;
using System.Collections.Generic;

namespace Echo.Queues.Adapters
{
    /// <summary>
    /// Wrapper for the <see cref="Stack{T}"/> class.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to store.</typeparam>
    public class StackAdapter<TWave> : IProcessingQueue<TWave> where TWave : IWave
    {
        private readonly Stack<TWave> _stack;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        public StackAdapter() : this(new Stack<TWave>()) { }

        /// <summary>
        /// Creates an instance of the class from an existing <see cref="Stack{T}"/> object.
        /// </summary>
        public StackAdapter(Stack<TWave> stack)
        {
            _stack = stack;
        }

        /// <summary>
        /// Number of waves in the queue.
        /// </summary>
        public int Count => _stack.Count;

        /// <summary>
        /// Clears all waves from the queue.
        /// </summary>
        public void Clear()
        {
            _stack.Clear();
        }

        /// <summary>
        /// Dequeues last wave in the queue.
        /// </summary>
        /// <returns>Dequeued wave.</returns>
        public TWave Dequeue()
        {
            return _stack.Pop();
        }

        /// <summary>
        /// Enqueues a wave.
        /// </summary>
        /// <param name="wave">A wave to enqueue.</param>
        public void Enqueue(TWave wave)
        {
            _stack.Push(wave);
        }
    }
}
