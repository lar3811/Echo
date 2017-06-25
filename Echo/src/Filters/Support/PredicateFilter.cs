using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class PredicateFilter<T> : ICondition<T>
    {
        private readonly Predicate<T> _predicate;

        public PredicateFilter(Predicate<T> predicate)
        {
            _predicate = predicate;
        }

        public bool Check(T subject)
        {
            return _predicate(subject);
        }
    }
}
