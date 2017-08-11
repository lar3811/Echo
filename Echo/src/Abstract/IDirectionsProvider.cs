using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Interface for providing <see cref="Tracer{TWave}"/> with hints about navigation.
    /// </summary>
    public interface IDirectionsProvider
    {
        /// <summary>
        /// Should provide directions in which a wave can travel from given <paramref name="location"/>.
        /// </summary>
        /// <param name="location">Location to travel from.</param>
        /// <returns>Array of directions in which a wave can travel from given <paramref name="location"/>.</returns>
        Vector3[] GetDirections(Vector3 location);
    }
}
