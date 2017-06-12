using Echo.Abstract;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Numerics;

namespace Echo.QueueAdapters
{
    public class QueueOrderedByDistance : IWaveQueue
    {
        private readonly SortedDictionary<float, Queue<Wave>> _lookup = new SortedDictionary<float, Queue<Wave>>();

        private Vector3 _location;
        private int _count;



        public Vector3 Location
        {
            get { return _location; }
            set
            {
                Clear();
                _location = value;
            }
        }



        public QueueOrderedByDistance(Vector3 location)
        {
            Location = location;
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
            _count++;
            var distance = Vector3.DistanceSquared(wave.Location, _location);
            Queue<Wave> queue;
            if (_lookup.TryGetValue(distance, out queue))
            {
                queue.Enqueue(wave);
            }
            else
            {
                queue = new Queue<Wave>();
                queue.Enqueue(wave);
                _lookup.Add(distance, queue);
            }
        }
    }
}
