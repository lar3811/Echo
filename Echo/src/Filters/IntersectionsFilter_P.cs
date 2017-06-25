using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class IntersectionsFilter_P<TWave> : ICondition<TWave> where TWave : IWave
    {
        private readonly Dictionary<TWave, HashSet<Vector3>> _paths = new Dictionary<TWave, HashSet<Vector3>>();

        public bool Check(TWave wave)
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
