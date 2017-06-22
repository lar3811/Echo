using Echo.Abstract;
using Echo.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.Waves
{
    public class WaveBuilder : IWaveBuilder<Wave>
    {
        public Wave Create(Vector3 location, Vector3 direction)
        {
            return new Wave(location, direction);
        }

        public Wave Create(Wave wave, Vector3 direction)
        {
            return new Wave(wave, direction);
        }



        public class Generic<TData> : IWaveBuilder<Wave.Generic<TData>> where TData : ICloneable<TData>
        {
            public Wave.Generic<TData> Create(Vector3 location, Vector3 direction)
            {
                return new Wave.Generic<TData>(location, direction);
            }

            public Wave.Generic<TData> Create(Wave.Generic<TData> wave, Vector3 direction)
            {
                return new Wave.Generic<TData>(wave, direction);
            }
        }
    }
}
