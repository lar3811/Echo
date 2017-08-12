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
    /// <summary>
    /// Creates a wave for every direction available at given locations.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to create.</typeparam>
    public sealed class InitializeX<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IDirectionsProvider _provider;
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3[] _locations;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="provider">Directions provider (e.g. <see cref="Maps.GraphMap"/>).</param>
        /// <param name="builder">Wave initialization logic.</param>
        /// <param name="locations">Locations where waves should be created.</param>
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
