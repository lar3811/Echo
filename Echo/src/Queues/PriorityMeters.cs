using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Queues
{
    /// <summary>
    /// Evaluates priority of a wave based on its proximity to the designated location.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to evaluate.</typeparam>
    public class PriorityByProximity<TWave> : PriorityQueue<TWave>.IPriorityMeter 
        where TWave : IWave
    {
        private readonly Vector3 _destination;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="destination">Destination coordinates.</param>
        public PriorityByProximity(Vector3 destination)
        {
            _destination = destination;
        }
        
        /// <summary>
        /// Evaluates geiven <paramref name="wave"/>.
        /// </summary>
        /// <param name="wave">A wave to evaluate.</param>
        /// <returns>Priority level.</returns>
        public float Evaluate(TWave wave)
        {
            return Vector3.DistanceSquared(wave.Location, _destination);
        }
    }



    /// <summary>
    /// Evaluates priority of a wave based on minimal possible length of its complete path.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to evaluate.</typeparam>
    public class PriorityByEstimatedPathLength<TWave> : PriorityQueue<TWave>.IPriorityMeter 
        where TWave : IWave
    {
        private readonly Vector3 _destination;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="destination">Destination coordinates.</param>
        public PriorityByEstimatedPathLength(Vector3 destination)
        {
            _destination = destination;
        }
        
        /// <summary>
        /// Evaluates geiven <paramref name="wave"/>.
        /// </summary>
        /// <param name="wave">A wave to evaluate.</param>
        /// <returns>Priority level.</returns>
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
