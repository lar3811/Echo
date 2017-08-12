using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Conditions.Support
{
    /// <summary>
    /// Provides means for combining <see cref="ICondition{T}"/> objects.
    /// </summary>
    /// <typeparam name="T">Type of condition's subjects.</typeparam>
    public class ConjunctiveCompositeCondition<T> : ICondition<T>
    {
        private readonly List<ICondition<T>> _conditions = new List<ICondition<T>>();

        /// <summary>
        /// Creates an instance of the class from other <see cref="ICondition{T}"/> objects.
        /// </summary>
        /// <param name="conditions">Conditions to conjunct.</param>
        public ConjunctiveCompositeCondition(params ICondition<T>[] conditions)
        {
            _conditions.AddRange(conditions);
        }

        /// <summary>
        /// Returns result of AND operation over all underlying conditions with which this object was constructed.
        /// </summary>
        /// <param name="subject">Subject to test conditions for.</param>
        /// <returns>True if <paramref name="subject"/> meets all underlying conditions and false otherwise.</returns>
        public bool Check(T subject)
        {
            return _conditions.All(f => f.Check(subject));
        }
    }
}
