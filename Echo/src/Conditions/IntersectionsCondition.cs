using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Conditions
{
    /// <summary>
    /// Checks if a wave has intersections in its path.
    /// </summary>
    public class IntersectionsCondition_P : ICondition<IWave>
    {
        private readonly Dictionary<IWave, HashSet<Vector3>> _paths = new Dictionary<IWave, HashSet<Vector3>>();

        /// <summary>
        /// Checks if the <paramref name="wave"/> has intersections in its path.
        /// </summary>
        /// <param name="wave">Wave to check.</param>
        /// <returns>True if the <paramref name="wave"/> has intersections in its path, false otherwise.</returns>
        public bool Check(IWave wave)
        {
            HashSet<Vector3> path;
            if (_paths.TryGetValue(wave, out path))
            {
                return !path.Add(wave.Location);
            }
            else
            {
                var set = new HashSet<Vector3>(wave.FullPath);
                _paths.Add(wave, set);
                return false;
            }
        }
    }
}
