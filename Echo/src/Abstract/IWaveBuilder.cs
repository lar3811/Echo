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
        /// Initializes given wave as a new one (and not as a progeny of another wave).
        /// This method is generally used to produce initial set of waves to propagate later.
        /// </summary>
        /// <param name="wave">Wave to initialize.</param>
        /// <param name="location">Initial location of the wave.</param>
        /// <param name="direction">Direction of the wave.</param>
        void Build(TWave wave, Vector3 location, Vector3 direction);

        /// <summary>
        /// Initializes given wave as a progeny (and not as an independent new wave).
        /// This method is generally used to propagate an existing wave.
        /// </summary>
        /// <param name="wave">Wave to initialize.</param>
        /// <param name="progenitor">Wave to inherit properties from.</param>
        /// <param name="direction">Direction of the wave.</param>
        /// <param name="offset">Location offset relative to the <paramref name="progenitor"/>.</param>
        void Build(TWave wave, TWave progenitor, Vector3 direction, Vector3 offset);
    }
}
