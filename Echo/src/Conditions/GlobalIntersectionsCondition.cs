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
    /// Checks if a wave's current location is already traversed by another wave.
    /// </summary>
    public class GlobalIntersectionsCondition : ICondition<IWave>
    {
        private readonly HashSet<Vector3> _marked = new HashSet<Vector3>();

        /// <summary>
        /// Checks if the <paramref name="wave"/>'s current location is already traversed by another wave.
        /// </summary>
        /// <param name="wave">Wave to check.</param>
        /// <returns>True if the <paramref name="wave"/>'s current location is already traversed by another wave</returns>
        public bool Check(IWave wave)
        {
            return !_marked.Add(wave.Location);
        }
    }
}
