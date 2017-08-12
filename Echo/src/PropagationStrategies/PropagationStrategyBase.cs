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
    /// Provides straightforward implementation of <see cref="IPropagationStrategy{TWave}"/> interface for subclasses.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to create.</typeparam>
    public abstract class PropagationStrategyBase<TWave> : IPropagationStrategy<TWave>
        where TWave : new()
    {
        /// <summary>
        /// Generates waves from given <paramref name="progenitor"/>.
        /// </summary>
        /// <param name="progenitor">A wave to multiply.</param>
        /// <returns>Array of waves.</returns>
        public TWave[] Execute(TWave progenitor)
        {
            if (progenitor == null) return null;
            var parameters = GetParametersFor(progenitor);
            if (parameters == null) return null;
            var waves = new TWave[parameters.Length];
            for (int i = 0; i < waves.Length; i++)
            {
                var wave = new TWave();
                var parameter = parameters[i];
                waves[i] = wave;
                parameter.Builder?.Build(wave, progenitor, parameter.Direction, parameter.Offset);
            }
            return waves;
        }

        /// <summary>
        /// Each parameter object is used to create individual wave object during <see cref="Execute"/> method call.
        /// </summary>
        /// <param name="wave">Progenitor wave.</param>
        /// <returns></returns>
        protected abstract Parameters[] GetParametersFor(TWave wave);

        /// <summary>
        /// Defines values necessary to construct wave objects.
        /// </summary>
        protected struct Parameters
        {
            /// <summary>
            /// Wave initialization logic.
            /// </summary>
            public readonly IWaveBuilder<TWave> Builder;

            /// <summary>
            /// Wave direction.
            /// </summary>
            public readonly Vector3 Direction;

            /// <summary>
            /// Location offset relative to the progenitor.
            /// </summary>
            public readonly Vector3 Offset;

            /// <summary>
            /// Creates an instance of the structure.
            /// </summary>
            /// <param name="builder">Wave initialization logic.</param>
            /// <param name="direction">Wave direction.</param>
            /// <param name="offset">Location offset relative to the progenitor.</param>
            public Parameters(IWaveBuilder<TWave> builder, Vector3 direction, Vector3 offset)
            {
                Builder = builder;
                Direction = direction;
                Offset = offset;
            }

            /// <summary>
            /// Creates an instance of the structure.
            /// </summary>
            /// <param name="builder">Wave initialization logic.</param>
            /// <param name="direction">Wave direction.</param>
            public Parameters(IWaveBuilder<TWave> builder, Vector3 direction)
                : this(builder, direction, Vector3.Zero) { }

            /// <summary>
            /// Creates an instance of the structure.
            /// </summary>
            /// <param name="direction">Wave direction.</param>
            public Parameters(Vector3 direction)
                : this(null, direction, Vector3.Zero) { }
        }
    }
}
