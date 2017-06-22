using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class PredicateFilter<TWave> : IWaveFilter<TWave> where TWave : IWave
    {
        private readonly Predicate<TWave> _predicate;

        public PredicateFilter(Predicate<TWave> predicate)
        {
            _predicate = predicate;
        }

        public bool Is(TWave wave)
        {
            return _predicate(wave);
        }
    }
}
