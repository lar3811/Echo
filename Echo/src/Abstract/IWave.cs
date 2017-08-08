using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Defines properties necessary for wave navigation.
    /// </summary>
    public interface IWave
    {
        /// <summary>
        /// Current location of the wave.
        /// </summary>
        Vector3 Location { get; }
        /// <summary>
        /// Normalized direction of the wave.
        /// </summary>
        Vector3 Direction { get; }
        /// <summary>
        /// Array of traversed waypoints.
        /// </summary>
        Vector3[] FullPath { get; }
        /// <summary>
        /// Positions wave at given location.
        /// </summary>
        /// <param name="location">Coordinates to move wave at.</param>
        void Relocate(Vector3 location);
    }
}
