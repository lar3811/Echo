using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Queues
{
    public class PriorityByProximity : PriorityQueue.IPriorityMeter
    {
        private readonly Vector3 _destination;

        public PriorityByProximity(Vector3 destination)
        {
            _destination = destination;
        }

        public float Evaluate(Wave wave)
        {
            return Vector3.DistanceSquared(wave.Location, _destination);
        }
    }



    public class PriorityByEstimatedPathLength : PriorityQueue.IPriorityMeter
    {
        private readonly Vector3 _destination;

        public PriorityByEstimatedPathLength(Vector3 destination)
        {
            _destination = destination;
        }

        // TODO: OPTIMIZE (caching) + unit step policy
        // TODO: Generic Tracer<TWave> where TWave : Wave or IWave
        public float Evaluate(Wave wave)
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
