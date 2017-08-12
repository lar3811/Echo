using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// An abstract condition.
    /// </summary>
    /// <typeparam name="T">Type of condition's subjects.</typeparam>
    public interface ICondition<in T>
    {
        /// <summary>
        /// Checks if specific subject meets the condition.
        /// </summary>
        /// <param name="subject">Subject to test condition for.</param>
        /// <returns>True if condition met, false otherwise.</returns>
        bool Check(T subject);
    }
}
