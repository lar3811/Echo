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
    /// Provides means for combining <see cref="ICondition{T}"/> objects.
    /// </summary>
    /// <typeparam name="T">Type of condition's subjects.</typeparam>
    public class DisjunctiveCompositeCondition<T> : ICondition<T>
    {
        private readonly List<ICondition<T>> _conditions = new List<ICondition<T>>();

        /// <summary>
        /// Creates an instance of the class from other <see cref="ICondition{T}"/> objects.
        /// </summary>
        /// <param name="conditions">Conditions to disjunct.</param>
        public DisjunctiveCompositeCondition(params ICondition<T>[] conditions)
        {
            _conditions.AddRange(conditions);
        }

        /// <summary>
        /// Returns result of OR operation over all underlying conditions with which this object was constructed.
        /// </summary>
        /// <param name="subject">Subject to test conditions for.</param>
        /// <returns>True if <paramref name="subject"/> meets at least one underlying condition and false otherwise.</returns>
        public bool Check(T subject)
        {
            return _conditions.Any(f => f.Check(subject));
        }
    }
}
