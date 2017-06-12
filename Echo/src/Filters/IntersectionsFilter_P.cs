using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class IntersectionsFilter_P : IWaveFilter
    {
        private readonly Dictionary<Wave, HashSet<Vector3>> _paths = new Dictionary<Wave, HashSet<Vector3>>();

        public bool Is(Wave wave)
        {
            HashSet<Vector3> path;
            if (_paths.TryGetValue(wave, out path))
            {
                return !path.Add(wave.Location);
            }
            else
            {
                var set = new HashSet<Vector3>();
                set.Add(wave.Location);
                _paths.Add(wave, set);
                return false;
            }
        }
    }
}
