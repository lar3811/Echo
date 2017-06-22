using Echo.Abstract;
using System.Collections.Generic;

namespace Echo.Queues.Adapters
{
    public class QueueAdapter<TWave> : IWaveQueue<TWave> where TWave : IWave
    {
        private readonly Queue<TWave> _queue;

        public QueueAdapter() : this(new Queue<TWave>()) { }
        public QueueAdapter(Queue<TWave> queue)
        {
            _queue = queue;
        }

        public int Count => _queue.Count;

        public void Clear()
        {
            _queue.Clear();
        }

        public TWave Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Enqueue(TWave wave)
        {
            _queue.Enqueue(wave);
        }
    }
}
