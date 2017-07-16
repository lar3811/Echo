using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.InitializationStrategies
{
    public sealed class Initialize8x2D<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3[] _locations;

        public Initialize8x2D(IWaveBuilder<TWave> builder = null, params Vector3[] locations)
        {
            _builder = builder;
            _locations = locations;
        }

        protected override Parameters[] GetParameters()
        {
            if (_locations == null) return new Parameters[0];

            var output = new Parameters[8 * _locations.Length];
            for (int i = 0; i < _locations.Length; i++)
            {
                output[8 * i + 0] = new Parameters(_builder, _locations[i], Vector3.UnitX);
                output[8 * i + 1] = new Parameters(_builder, _locations[i], Vector3.Normalize(Vector3.UnitX + Vector3.UnitY));
                output[8 * i + 2] = new Parameters(_builder, _locations[i], Vector3.UnitY);
                output[8 * i + 3] = new Parameters(_builder, _locations[i], Vector3.Normalize(-Vector3.UnitX + Vector3.UnitY));
                output[8 * i + 4] = new Parameters(_builder, _locations[i], -Vector3.UnitX);
                output[8 * i + 5] = new Parameters(_builder, _locations[i], Vector3.Normalize(-Vector3.UnitX - Vector3.UnitY));
                output[8 * i + 6] = new Parameters(_builder, _locations[i], -Vector3.UnitY);
                output[8 * i + 7] = new Parameters(_builder, _locations[i], Vector3.Normalize(Vector3.UnitX - Vector3.UnitY));
            }

            return output;
        }
    }
}
