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
    /// Evaluates priority of a wave based on its proximity to designated location.
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
        /// Evaluates given <paramref name="wave"/>.
        /// </summary>
        /// <param name="wave">A wave to evaluate.</param>
        /// <returns>Priority level.</returns>
        public float Evaluate(TWave wave)
        {
            return -Vector3.DistanceSquared(wave.Location, _destination);
        }
    }



    /// <summary>
    /// Evaluates priority of a wave based on the minimal possible length of its complete path.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to evaluate.</typeparam>
    public class PriorityByEstimatedPathLength<TWave> : PriorityQueue<TWave>.IPriorityMeter 
        where TWave : IWave
    {
        private readonly Vector3 _destination;
        private readonly IMap<TWave> _map;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="destination">Destination coordinates.</param>
        /// <param name="map">Map to navigate.</param>
        public PriorityByEstimatedPathLength(Vector3 destination, IMap<TWave> map)
        {
            _destination = destination;
            _map = map;
        }
        
        /// <summary>
        /// Evaluates given <paramref name="wave"/>.
        /// </summary>
        /// <param name="wave">A wave to evaluate.</param>
        /// <returns>Priority level.</returns>
        public float Evaluate(TWave wave)
        {
            Vector3 next;
            if (!_map.Navigate(wave, out next))
                return float.MinValue;

            var path = wave.FullPath;
            var traveled = 0f;
            for (var i = 1; i < path.Length; i++)
                traveled += Vector3.Distance(path[i], path[i - 1]);
            var distance = Vector3.Distance(wave.Location, next) + Vector3.Distance(next, _destination);
            return -(distance + traveled);
        }
    }



    /// <summary>
    /// Evaluates priority of a wave based on its deviation from the vector pointing towards specified destination.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to evaluate.</typeparam>
    public class PriorityByAlignment<TWave> : PriorityQueue<TWave>.IPriorityMeter
        where TWave : IWave
    {
        private readonly Vector3 _destination;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="destination">Destination coordinates.</param>
        public PriorityByAlignment(Vector3 destination)
        {
            _destination = destination;
        }

        /// <summary>
        /// Evaluates given <paramref name="wave"/>.
        /// </summary>
        /// <param name="wave">A wave to evaluate.</param>
        /// <returns>Priority level.</returns>
        public float Evaluate(TWave wave)
        {
            var vector = Vector3.Normalize(_destination - wave.Location);
            return Vector3.Dot(vector, wave.Direction);
        }
    }
}
