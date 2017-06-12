using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class InverseFilter : IWaveFilter
    {
        private readonly IWaveFilter _filter;

        public InverseFilter(IWaveFilter filter)
        {
            _filter = filter;
        }

        public bool Is(Wave wave)
        {
            return !_filter.Is(wave);
        }
    }
}
