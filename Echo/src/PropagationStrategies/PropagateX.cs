using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.PropagationStrategies
{
    public sealed class PropagateX<TWave> : PropagationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IDirectionsProvider _provider;
        private readonly IWaveBuilder<TWave> _builder;

        public PropagateX(IDirectionsProvider provider, IWaveBuilder<TWave> builder)
        {
            _provider = provider;
            _builder = builder;
        }

        protected override Parameters[] GetParametersFor(TWave wave)
        {
            var directions = _provider.GetDirections(wave.Location);
            if (directions == null) return null;
            var parameters = new List<Parameters>(directions.Length);
            for (int i = 0; i < directions.Length; i++)
            {
                var direction = directions[i];
                if (direction == wave.Direction || direction == -wave.Direction) continue;
                parameters.Add(new Parameters(_builder, directions[i], Vector3.Zero));
            }
            return parameters.ToArray();
        }
    }
}
