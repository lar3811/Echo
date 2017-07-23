using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.InitializationStrategies
{
    public sealed class InitializeSingle<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3 _location;
        private readonly Vector3 _direction;

        public InitializeSingle(Vector3 location, Vector3 direction, IWaveBuilder<TWave> builder)
        {
            _builder = builder;
            _location = location;
            _direction = direction;
        }

        protected override Parameters[] GetParameters()
        {
            return new[] { new Parameters(_builder, _location, _direction) };
        }
    }
}
