using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters.Support
{
    public class ConjunctiveCompositeFilter<TWave> : IWaveFilter<TWave> where TWave : IWave
    {
        private readonly List<IWaveFilter<TWave>> _filters = new List<IWaveFilter<TWave>>();

        public ConjunctiveCompositeFilter(params IWaveFilter<TWave>[] filters)
        {
            _filters.AddRange(filters);
        }

        public bool Is(TWave wave)
        {
            return _filters.All(f => f.Is(wave));
        }
    }
}
