using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Interface for wave initialization.
    /// </summary>
    /// <typeparam name="TWave">Type of waves.</typeparam>
    public interface IWaveBuilder<TWave>
    {
        /// <summary>
        /// Used to initialize new waves.
        /// </summary>
        /// <param name="wave">Wave to initialize.</param>
        /// <param name="location">Location to place wave at.</param>
        /// <param name="direction">Direction of the wave.</param>
        void Build(TWave wave, Vector3 location, Vector3 direction);

        /// <summary>
        /// Used to initialize generated waves.
        /// </summary>
        /// <param name="wave">Wave to initialize</param>
        /// <param name="progenitor">Wave to inherit properties from.</param>
        /// <param name="direction">Direction of the wave.</param>
        /// <param name="offset">Offset of the wave relative to its <paramref name="progenitor"/>.</param>
        void Build(TWave wave, TWave progenitor, Vector3 direction, Vector3 offset);
    }
}
