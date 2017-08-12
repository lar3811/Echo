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
    /// Generates 2 new waves from an existing one.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to create.</typeparam>
    public sealed class Propagate2x2D<TWave> : PropagationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="builder">Wave initialization logic.</param>
        public Propagate2x2D(IWaveBuilder<TWave> builder)
        {
            _builder = builder;
        }

        protected override Parameters[] GetParametersFor(TWave wave)
        {
            return new[]
            {
                new Parameters(
                    _builder, new Vector3(-wave.Direction.Y, wave.Direction.X, 0), Vector3.Zero),
                new Parameters(
                    _builder, new Vector3(wave.Direction.Y, -wave.Direction.X, 0), Vector3.Zero)
            };
        }
    }
}
