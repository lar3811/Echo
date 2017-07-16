using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.InitializationStrategies
{
    public sealed class Initialize4x2D<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3[] _locations;

        public Initialize4x2D(IWaveBuilder<TWave> builder = null, params Vector3[] locations)
        {
            _builder = builder;
            _locations = locations;
        }

        protected override Parameters[] GetParameters()
        {
            if (_locations == null) return new Parameters[0];

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
