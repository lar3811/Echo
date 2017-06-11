using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class IntersectionsFilter_P : IEchoFilter
    {
        private readonly Dictionary<Wave, HashSet<Vector3>> _paths = new Dictionary<Wave, HashSet<Vector3>>();

        public bool Is(Wave echo)
        {
            HashSet<Vector3> path;
            if (_paths.TryGetValue(echo, out path))
            {
                return !path.Add(echo.Location);
            }
            else
            {
                var set = new HashSet<Vector3>();
                set.Add(echo.Location);
                _paths.Add(echo, set);
                return false;
            }
        }
    }
}
