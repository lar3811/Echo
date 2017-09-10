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
    /// Generates 16 new waves (4 orthogonal and 12 diagonal) from an existing one.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to create.</typeparam>
    public sealed class Propagate16x3D<TWave> : PropagationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="builder">Wave initialization logic (e.g. <see cref="Waves.Base{TWave}.Builder"/>).</param>
        public Propagate16x3D(IWaveBuilder<TWave> builder)
        {
            _builder = builder;
        }

        protected override Parameters[] GetParametersFor(TWave wave)
        {
            var output = new Parameters[16];
            output[0] = new Parameters(
                _builder, new Vector3(-wave.Direction.Y, wave.Direction.X, 0), Vector3.Zero);
            output[1] = new Parameters(
                _builder, new Vector3(wave.Direction.Y, -wave.Direction.X, 0), Vector3.Zero);
            output[2] = new Parameters(
                _builder, new Vector3(-wave.Direction.Z, 0, wave.Direction.X), Vector3.Zero);
            output[3] = new Parameters(
                _builder, new Vector3(wave.Direction.Z, 0, -wave.Direction.X), Vector3.Zero);
            output[4] = new Parameters(
                _builder, Vector3.Normalize(output[0].Direction + output[2].Direction), Vector3.Zero);
            output[5] = new Parameters(
                _builder, Vector3.Normalize(output[0].Direction + output[3].Direction), Vector3.Zero);
            output[6] = new Parameters(
                _builder, Vector3.Normalize(output[1].Direction + output[2].Direction), Vector3.Zero);
            output[7] = new Parameters(
                _builder, Vector3.Normalize(output[1].Direction + output[3].Direction), Vector3.Zero);

            for (int i = 8; i < 16; i++)
            {
                output[i] = new Parameters(
                    _builder, Vector3.Normalize(output[i - 8].Direction + wave.Direction), Vector3.Zero);
            }

            return output;
        }
    }
}
