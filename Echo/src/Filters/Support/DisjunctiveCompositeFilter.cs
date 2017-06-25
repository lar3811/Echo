using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wave.Filters.Support
{
    public class DisjunctiveCompositeFilter<T> : ICondition<T>
    {
        private readonly List<ICondition<T>> _filters = new List<ICondition<T>>();

        public DisjunctiveCompositeFilter(params ICondition<T>[] filters)
        {
            _filters.AddRange(filters);
        }

        public bool Check(T subject)
        {
            return _filters.Any(f => f.Check(subject));
        }
    }
}
