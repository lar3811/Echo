using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conditions.Support
{
    public class PredicateCondition<T> : ICondition<T>
    {
        private readonly Predicate<T> _predicate;

        public PredicateCondition(Predicate<T> predicate)
        {
            _predicate = predicate;
        }

        public bool Check(T subject)
        {
            return _predicate(subject);
        }
    }
}
