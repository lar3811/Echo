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
    /// Provides straightforward implementation of <see cref="IInitializationStrategy{TWave}"/> interface for subclasses.
    /// </summary>
    /// <typeparam name="TWave"></typeparam>
    public abstract class InitializationStrategyBase<TWave> : IInitializationStrategy<TWave>
        where TWave : new()
    {
        /// <summary>
        /// Retrieves initial set of waves to propagate.
        /// </summary>
        /// <returns>Array of waves.</returns>
        public TWave[] Execute()
        {
            var parameters = GetParameters();
            if (parameters == null) return null;
            var waves = new TWave[parameters.Length];
            for (int i = 0; i < waves.Length; i++)
            {
                var wave = new TWave();
                var parameter = parameters[i];
                waves[i] = wave;
                parameter.Builder?.Build(wave, parameter.Location, parameter.Direction);
            }
            return waves;
        }

        /// <summary>
        /// Each parameter object is used to create individual wave object during <see cref="Execute"/> method call.
        /// </summary>
        /// <returns>Array of parameters.</returns>
        protected abstract Parameters[] GetParameters();

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
            /// Wave initial location.
            /// </summary>
            public readonly Vector3 Location;
            /// <summary>
            /// Wave direction.
            /// </summary>
            public readonly Vector3 Direction;

            /// <summary>
            /// Creates a new instance of the structure.
            /// </summary>
            /// <param name="builder">Wave initialization logic.</param>
            /// <param name="location">Wave initial location</param>
            /// <param name="direction">Wave direction.</param>
            public Parameters(IWaveBuilder<TWave> builder, Vector3 location, Vector3 direction)
            {
                Builder = builder;
                Location = location;
                Direction = direction;
            }

            /// <summary>
            /// Creates a new instance of the structure.
            /// </summary>
            /// <param name="location">Wave initial location</param>
            /// <param name="direction">Wave direction.</param>
            public Parameters(Vector3 location, Vector3 direction)
                : this(null, location, direction) { }
        }
    }
}
