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
    /// Checks if a wave has intersections in its path (memory efficient).
    /// </summary>
    public class IntersectionsCondition_M : ICondition<IWave>
    {
        /// <summary>
        /// Checks if the <paramref name="wave"/> has intersections in its path.
        /// </summary>
        /// <param name="wave">Wave to check.</param>
        /// <returns>True if the <paramref name="wave"/> has intersections in its path, false otherwise.</returns>
        public bool Check(IWave wave)
        {
            foreach(var location in wave.FullPath)
            {
                if (wave.Location == location) return true;
            }
            return false;
        }
    }
}
