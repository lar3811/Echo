using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class DisjunctiveCompositeFilter : IWaveFilter
    {
        private readonly List<IWaveFilter> _filters = new List<IWaveFilter>();

        public DisjunctiveCompositeFilter(params IWaveFilter[] filters)
        {
            _filters.AddRange(filters);
        }

        public bool Is(Wave wave)
        {
            return _filters.Any(f => f.Is(wave));
        }
    }
}
