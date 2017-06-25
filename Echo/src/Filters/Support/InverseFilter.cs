using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class InverseFilter<T> : ICondition<T>
    {
        private readonly ICondition<T> _filter;

        public InverseFilter(ICondition<T> filter)
        {
            _filter = filter;
        }

        public bool Check(T subject)
        {
            return !_filter.Check(subject);
        }
    }
}
