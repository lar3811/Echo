using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.InitializationStrategies
{
    public abstract class InitializationStrategyBase<TWave> : IInitializationStrategy<TWave>
        where TWave : new()
    {
        public TWave[] Execute()
        {
            var parameters = GetParameters();
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

        protected abstract Parameters[] GetParameters();

        protected struct Parameters
        {
            public readonly IWaveBuilder<TWave> Builder;
            public readonly Vector3 Location;
            public readonly Vector3 Direction;

            public Parameters(IWaveBuilder<TWave> builder, Vector3 location, Vector3 direction)
            {
                Builder = builder;
                Location = location;
                Direction = direction;
            }

            public Parameters(Vector3 location, Vector3 direction)
                : this(null, location, direction) { }
        }
    }
}
