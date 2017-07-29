﻿using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.InitializationStrategies
{
    public sealed class Initialize26x3D<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3[] _locations;

        public Initialize26x3D(IWaveBuilder<TWave> builder, params Vector3[] locations)
        {
            _builder = builder;
            _locations = locations;
        }

        protected override Parameters[] GetParameters()
        {
            if (_locations == null) return null;

            var output = new Parameters[26 * _locations.Length];
            for (int i = 0, j = 0; i < _locations.Length; i++, j = 0)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int z = -1; z <= 1; z++)
                        {
                            if (x == 0 && y == 0 && z == 0) continue;
                            output[8 * i + j++] = new Parameters(
                                _builder, _locations[i], Vector3.Normalize(new Vector3(x, y, z)));
                        }
                    }
                }
            }

            return output;
        }
    }
}
