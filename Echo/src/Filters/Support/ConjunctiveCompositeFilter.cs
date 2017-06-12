using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters.Support
{
    public class ConjunctiveCompositeFilter : IWaveFilter
    {
        private readonly List<IWaveFilter> _filters = new List<IWaveFilter>();

        public ConjunctiveCompositeFilter(params IWaveFilter[] filters)
        {
            _filters.AddRange(filters);
        }

        public bool Is(Wave wave)
        {
            return _filters.All(f => f.Is(wave));
        }
    }
}
