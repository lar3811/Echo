using Echo.Abstract;
using System.Collections.Generic;

namespace Echo.QueueAdapters
{
    public class QueueAdapter : IWaveQueue
    {
        private readonly Queue<Wave> _queue;

        public QueueAdapter() : this(new Queue<Wave>()) { }
        public QueueAdapter(Queue<Wave> queue)
        {
            _queue = queue;
        }

        public int Count => _queue.Count;

        public void Clear()
        {
            _queue.Clear();
        }

        public Wave Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Enqueue(Wave wave)
        {
            _queue.Enqueue(wave);
        }
    }
}
