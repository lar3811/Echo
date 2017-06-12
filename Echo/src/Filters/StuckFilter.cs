using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class StuckFilter : IWaveFilter
    {
        public bool Is(Wave wave)
        {
            if (wave.PathSegment.Count < 2) return false;

            var p1 = wave.PathSegment[wave.PathSegment.Count - 1];
            var p2 = wave.PathSegment[wave.PathSegment.Count - 2];

            return p1 == p2;
        }
    }
}
