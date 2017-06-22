using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Queues
{
    public class PriorityByProximity<TWave> : PriorityQueue<TWave>.IPriorityMeter where TWave : IWave
    {
        private readonly Vector3 _destination;

        public PriorityByProximity(Vector3 destination)
        {
            _destination = destination;
        }

        public float Evaluate(TWave wave)
        {
            return Vector3.DistanceSquared(wave.Location, _destination);
        }
    }



    public class PriorityByEstimatedPathLength<TWave> : PriorityQueue<TWave>.IPriorityMeter where TWave : IWave
    {
        private readonly Vector3 _destination;

        public PriorityByEstimatedPathLength(Vector3 destination)
        {
            _destination = destination;
        }

        // TODO: OPTIMIZE (caching) + unit step policy
        public float Evaluate(TWave wave)
        {
            var path = wave.FullPath;
            var traveled = 0f;
            for (var i = 1; i < path.Length; i++)
                traveled += Vector3.Distance(path[i], path[i - 1]);
            var distance = Vector3.Distance(wave.Location, _destination);
            return distance + traveled;
        }
    }
}
