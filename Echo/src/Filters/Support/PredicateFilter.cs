using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters.Support
{
    public class PredicateFilter : IEchoFilter
    {
        private readonly Predicate<Wave> _predicate;

        public PredicateFilter(Predicate<Wave> predicate)
        {
            _predicate = predicate;
        }

        public bool Is(Wave echo)
        {
            return _predicate(echo);
        }
    }
}
