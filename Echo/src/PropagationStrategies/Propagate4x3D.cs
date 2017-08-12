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
    /// Generates 4 new waves from an existing one.
    /// </summary>
    /// <typeparam name="TWave">Type of waves.</typeparam>
    public sealed class Propagate4x3D<TWave> : PropagationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="builder">Wave initialization logic.</param>
        public Propagate4x3D(IWaveBuilder<TWave> builder)
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
                    _builder, new Vector3(wave.Direction.Y, -wave.Direction.X, 0), Vector3.Zero),
                new Parameters(
                    _builder, new Vector3(-wave.Direction.Z, 0, wave.Direction.X), Vector3.Zero),
                new Parameters(
                    _builder, new Vector3(wave.Direction.Z, 0, -wave.Direction.X), Vector3.Zero)
            };
        }
    }
}
