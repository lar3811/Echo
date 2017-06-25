using Echo.Abstract;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Numerics;

namespace Echo.Queues
{
    public class PriorityQueue<T> : IProcessingQueue<T>
    {
        public interface IPriorityMeter
        {
            float Evaluate(T subject);
        }



        private readonly SortedDictionary<float, Queue<T>> _lookup;
        private readonly IPriorityMeter _meter;
        private int _count;

        

        public PriorityQueue(IPriorityMeter meter)
        {
            _lookup = new SortedDictionary<float, Queue<T>>();
            _meter = meter;
        }



        public int Count
        {
            get { return _count; }
        }

        public void Clear()
        {
            _count = 0;
            _lookup.Clear();
        }

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
