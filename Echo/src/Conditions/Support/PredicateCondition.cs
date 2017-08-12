using Echo;
using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conditions.Support
{
    /// <summary>
    /// Provides means to define simple conditions as <see cref="Predicate{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of condition's subjects.</typeparam>
    public class PredicateCondition<T> : ICondition<T>
    {
        private readonly Predicate<T> _predicate;

        /// <summary>
        /// Creates an instance of the class from <see cref="Predicate{T}"/> object.
        /// </summary>
        /// <param name="predicate">Predicate to wrap.</param>
        public PredicateCondition(Predicate<T> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// Returns result of testing underlying predicate for <paramref name="subject"/>.
        /// </summary>
        /// <param name="subject">Subject to test condition for.</param>
        /// <returns>Predicate test result.</returns>
        public bool Check(T subject)
        {
            return _predicate(subject);
        }
    }
}
