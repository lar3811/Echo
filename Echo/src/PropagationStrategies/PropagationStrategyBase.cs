using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.PropagationStrategies
{
    public abstract class PropagationStrategyBase<TWave> : IPropagationStrategy<TWave>
        where TWave : new()
    {
        public TWave[] Execute(TWave progenitor)
        {
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

        protected abstract Parameters[] GetParametersFor(TWave wave);

        protected struct Parameters
        {
            public readonly IWaveBuilder<TWave> Builder;
            public readonly Vector3 Direction;
            public readonly Vector3 Offset;

            public Parameters(IWaveBuilder<TWave> builder, Vector3 direction, Vector3 offset)
            {
                Builder = builder;
                Direction = direction;
                Offset = offset;
            }

            public Parameters(IWaveBuilder<TWave> builder, Vector3 direction)
                : this(builder, direction, Vector3.Zero) { }

            public Parameters(Vector3 direction)
                : this(null, direction, Vector3.Zero) { }
        }
    }
}
