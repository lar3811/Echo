using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Interface for providing <see cref="Tracer{TWave}"/> with new waves to propagate.
    /// </summary>
    /// <typeparam name="TWave">Type of waves.</typeparam>
    public interface IPropagationStrategy<TWave>
    {
        /// <summary>
        /// Generates a set of waves from given <paramref name="progenitor"/>.
        /// </summary>
        /// <param name="progenitor">Wave to generate new ones from.</param>
        /// <returns>A set of new waves.</returns>
        TWave[] Execute(TWave progenitor);
    }
}
