using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Interface for providing <see cref="Tracer{TWave}"/> class hints about wave navigation.
    /// </summary>
    public interface IDirectionsProvider
    {
        /// <summary>
        /// Provides directions in which a wave can travel from given <paramref name="location"/>.
        /// </summary>
        /// <param name="location">Location to travel from.</param>
        /// <returns>Array of directions.</returns>
        Vector3[] GetDirections(Vector3 location);
    }
}
