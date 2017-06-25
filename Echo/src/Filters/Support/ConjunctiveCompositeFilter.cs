using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters.Support
{
    public class ConjunctiveCompositeFilter<T> : ICondition<T>
    {
        private readonly List<ICondition<T>> _filters = new List<ICondition<T>>();

        public ConjunctiveCompositeFilter(params ICondition<T>[] filters)
        {
            _filters.AddRange(filters);
        }

        public bool Check(T subject)
        {
            return _filters.All(f => f.Check(subject));
        }
    }
}
