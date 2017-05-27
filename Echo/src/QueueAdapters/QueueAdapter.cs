using Echo.Abstract;
using System.Collections.Generic;

namespace Echo.QueueAdapters
{
    public class QueueAdapter : IEchoQueue
    {
        private readonly Queue<Echo> _queue;

        public QueueAdapter() : this(new Queue<Echo>()) { }
        public QueueAdapter(Queue<Echo> queue)
        {
            _queue = queue;
        }

        public int Count => _queue.Count;

        public void Clear()
        {
            _queue.Clear();
        }

        public Echo Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Enqueue(Echo echo)
        {
            _queue.Enqueue(echo);
        }
    }
}
