using Echo.Abstract;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Numerics;

namespace Echo.Queues
{
    public class PriorityQueue<TWave> : IWaveQueue<TWave> where TWave : IWave
    {
        public interface IPriorityMeter
        {
            float Evaluate(TWave wave);
        }



        private readonly SortedDictionary<float, Queue<TWave>> _lookup;
        private readonly IPriorityMeter _meter;
        private int _count;

        

        public PriorityQueue(IPriorityMeter meter)
        {
            _lookup = new SortedDictionary<float, Queue<TWave>>();
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

        public TWave Dequeue()
        {
            if (_count > 0)
            {
                _count--;
                return _lookup.First(kvp => kvp.Value.Count > 0).Value.Dequeue();
            }
            else
            {
                return default(TWave);
            }
        }

        public void Enqueue(TWave wave)
        {
            var priority = _meter.Evaluate(wave);
            Queue<TWave> queue;
            if (_lookup.TryGetValue(priority, out queue))
            {
                queue.Enqueue(wave);
            }
            else
            {
                queue = new Queue<TWave>();
                queue.Enqueue(wave);
                _lookup.Add(priority, queue);
            }
            _count++;
        }
    }
}
