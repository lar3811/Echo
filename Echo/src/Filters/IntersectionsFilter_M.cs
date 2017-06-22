using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class IntersectionsFilter_M<TWave> : IWaveFilter<TWave> where TWave : IWave
    {
        public bool Is(TWave wave)
        {
            foreach(var location in wave.FullPath)
            {
                if (wave.Location == location) return true;
            }
            return false;
        }
    }
}
