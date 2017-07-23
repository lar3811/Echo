﻿using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.PropagationStrategies
{
    public sealed class Propagate4x2D<TWave> : PropagationStrategyBase<TWave>
        where TWave : IWave, new()
    {
        private readonly IWaveBuilder<TWave> _builder;

        public Propagate4x2D(IWaveBuilder<TWave> builder)
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
                    _builder, Vector3.Normalize(new Vector3(-wave.Direction.Y, wave.Direction.X, 0) + wave.Direction), Vector3.Zero),
                new Parameters(
                    _builder, Vector3.Normalize(new Vector3(wave.Direction.Y, -wave.Direction.X, 0) + wave.Direction), Vector3.Zero)
            };
        }
    }
}
