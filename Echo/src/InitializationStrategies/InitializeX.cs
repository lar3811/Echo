using Echo.Abstract;
using Echo.InitializationStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.InitializationStrategies
{
    public sealed class InitializeX<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IDirectionsProvider _provider;
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3[] _locations;

        public InitializeX(IDirectionsProvider provider, IWaveBuilder<TWave> builder, params Vector3[] locations)
        {
            _provider = provider;
            _builder = builder;
            _locations = locations;
        }

        protected override Parameters[] GetParameters()
        {
            if (_locations == null || _locations.Length == 0) return null;
            var parameters = new List<Parameters>();
            for (int i = 0; i < _locations.Length; i++)
            {
                var location = _locations[i];
                var directions = _provider.GetDirections(location);
                if (directions == null) continue;
                for (int j = 0; j < directions.Length; j++)
                {
                    parameters.Add(new Parameters(_builder, location, directions[j]));
                }
            }
            return parameters.ToArray();
        }
    }
}
