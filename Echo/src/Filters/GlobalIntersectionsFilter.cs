using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class GlobalIntersectionsFilter : IWaveFilter
    {
        private readonly HashSet<Vector3> _marked = new HashSet<Vector3>();

        public bool Is(Wave wave)
        {
            return !_marked.Add(wave.Location);
        }
    }
}
