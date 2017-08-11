using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Interface for providing <see cref="Tracer{TWave}"/> with initial set of waves to propagate.
    /// </summary>
    /// <typeparam name="TWave">Type of waves.</typeparam>
    public interface IInitializationStrategy<TWave>
    {
        /// <summary>
        /// Retrieves initial set of waves to propagate.
        /// </summary>
        /// <returns>Array of waves.</returns>
        TWave[] Execute();
    }
}
