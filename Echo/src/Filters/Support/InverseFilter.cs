using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class InverseFilter<TWave> : IWaveFilter<TWave> where TWave : IWave
    {
        private readonly IWaveFilter<TWave> _filter;

        public InverseFilter(IWaveFilter<TWave> filter)
        {
            _filter = filter;
        }

        public bool Is(TWave wave)
        {
            return !_filter.Is(wave);
        }
    }
}
