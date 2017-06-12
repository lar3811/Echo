using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class PredicateFilter : IWaveFilter
    {
        private readonly Predicate<Wave> _predicate;

        public PredicateFilter(Predicate<Wave> predicate)
        {
            _predicate = predicate;
        }

        public bool Is(Wave wave)
        {
            return _predicate(wave);
        }
    }
}
