using Echo.Abstract;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Numerics;

namespace Echo.Queues
{
    public class PriorityQueue : IWaveQueue
    {
        public interface IPriorityMeter
        {
            float Evaluate(Wave wave);
        }



        private readonly SortedDictionary<float, Queue<Wave>> _lookup;
        private readonly IPriorityMeter _meter;
        private int _count;

        

        public PriorityQueue(IPriorityMeter meter)
        {
            _lookup = new SortedDictionary<float, Queue<Wave>>();
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

        public Wave Dequeue()
        {
            if (_count > 0)
            {
                _count--;
                return _lookup.First(kvp => kvp.Value.Count > 0).Value.Dequeue();
            }
            else
            {
                return null;
            }
        }

        public void Enqueue(Wave wave)
        {
            var priority = _meter.Evaluate(wave);
            Queue<Wave> queue;
            if (_lookup.TryGetValue(priority, out queue))
            {
                queue.Enqueue(wave);
            }
            else
            {
                queue = new Queue<Wave>();
                queue.Enqueue(wave);
                _lookup.Add(priority, queue);
            }
            _count++;
        }
    }
}
