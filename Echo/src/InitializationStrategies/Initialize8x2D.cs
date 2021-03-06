﻿using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.InitializationStrategies
{
    /// <summary>
    /// Creates 8 waves (4 aligned with either X or Y axis and 4 diagonal) at every provided location.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to create.</typeparam>
    public sealed class Initialize8x2D<TWave> : InitializationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;
        private readonly Vector3[] _locations;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="builder">Wave initialization logic (e.g. <see cref="Waves.Base{TWave}.Builder"/>).</param>
        /// <param name="locations">Locations where waves should be created.</param>
        public Initialize8x2D(IWaveBuilder<TWave> builder, params Vector3[] locations)
        {
            _builder = builder;
            _locations = locations;
        }

        protected override Parameters[] GetParameters()
        {
            if (_locations == null) return null;

            var output = new Parameters[8 * _locations.Length];
            for (int i = 0, j = 0; i < _locations.Length; i++, j = 0)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0) continue;
                        output[8 * i + j++] = new Parameters(
                            _builder, _locations[i], Vector3.Normalize(new Vector3(x, y, 0)));
                    }
                }
            }

            return output;
        }
    }
}
