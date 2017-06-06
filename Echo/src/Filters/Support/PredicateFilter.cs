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
        private readonly Predicate<Echo> _predicate;

        public PredicateFilter(Predicate<Echo> predicate)
        {
            _predicate = predicate;
        }

        public bool Is(Echo echo)
        {
            return _predicate(echo);
        }
    }
}
