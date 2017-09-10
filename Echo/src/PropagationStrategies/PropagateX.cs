using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.PropagationStrategies
{
    /// <summary>
    /// Creates a wave for every direction available at progenitor's location.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to create.</typeparam>
    public sealed class PropagateX<TWave> : PropagationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IDirectionsProvider _provider;
        private readonly IWaveBuilder<TWave> _builder;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="provider">Directions provider (e.g. <see cref="Maps.GridMap"/>).</param>
        /// <param name="builder">Wave initialization logic (e.g. <see cref="Waves.Base{TWave}.Builder"/>).</param>
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
