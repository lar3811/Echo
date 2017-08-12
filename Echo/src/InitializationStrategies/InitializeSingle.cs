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
    /// Creates a single wave at provided location.
    /// </summary>
    /// <typeparam name="TWave">Type of the wave to create.</typeparam>
    public sealed class InitializeSingle<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3 _location;
        private readonly Vector3 _direction;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="location">Wave initial location.</param>
        /// <param name="direction">Wave direction</param>
        /// <param name="builder">Wave initialization logic.</param>
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
