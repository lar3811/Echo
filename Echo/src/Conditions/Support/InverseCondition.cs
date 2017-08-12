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
    /// Modifies output of another <see cref="ICondition{T}"/> object.
    /// </summary>
    /// <typeparam name="T">Type of condition's subjects.</typeparam>
    public class InverseCondition<T> : ICondition<T>
    {
        private readonly ICondition<T> _condition;

        /// <summary>
        /// Creates an instance of the class from another <see cref="ICondition{T}"/> object.
        /// </summary>
        /// <param name="condition">A condition to invert.</param>
        public InverseCondition(ICondition<T> condition)
        {
            _condition = condition;
        }

        /// <summary>
        /// Returns inverted result of testing the underlying condition.
        /// </summary>
        /// <param name="subject">Subject to test condition for.</param>
        /// <returns>False if <paramref name="subject"/> meets underlying condition and true otherwise.</returns>
        public bool Check(T subject)
        {
            return !_condition.Check(subject);
        }
    }
}
