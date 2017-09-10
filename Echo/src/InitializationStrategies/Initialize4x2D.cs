using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.InitializationStrategies
{
    /// <summary>
    /// Creates 4 waves (all aligned with either X or Y axis) at every provided location.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to create.</typeparam>
    public sealed class Initialize4x2D<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3[] _locations;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="builder">Wave initialization logic (e.g. <see cref="Waves.Base{TWave}.Builder"/>).</param>
        /// <param name="locations">Locations where waves should be created.</param>
        public Initialize4x2D(IWaveBuilder<TWave> builder, params Vector3[] locations)
        {
            _builder = builder;
            _locations = locations;
        }

        protected override Parameters[] GetParameters()
        {
            if (_locations == null) return null;
            var output = new Parameters[4 * _locations.Length];
            for (int i = 0; i < _locations.Length; i++)
            {
                output[4 * i + 0] = new Parameters(_builder, _locations[i], Vector3.UnitX);
                output[4 * i + 1] = new Parameters(_builder, _locations[i], Vector3.UnitY);
                output[4 * i + 2] = new Parameters(_builder, _locations[i], -Vector3.UnitX);
                output[4 * i + 3] = new Parameters(_builder, _locations[i], -Vector3.UnitY);
            }
            return output;
        }
    }
}
