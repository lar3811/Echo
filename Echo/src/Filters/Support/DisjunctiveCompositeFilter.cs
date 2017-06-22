using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class DisjunctiveCompositeFilter<TWave> : IWaveFilter<TWave> where TWave : IWave
    {
        private readonly List<IWaveFilter<TWave>> _filters = new List<IWaveFilter<TWave>>();

        public DisjunctiveCompositeFilter(params IWaveFilter<TWave>[] filters)
        {
            _filters.AddRange(filters);
        }

        public bool Is(TWave wave)
        {
            return _filters.Any(f => f.Is(wave));
        }
    }
}
