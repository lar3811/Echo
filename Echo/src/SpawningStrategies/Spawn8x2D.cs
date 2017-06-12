using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.SpawningStrategies
{
    public class Spawn4x2D : IWaveSpawningStrategy
    {
        public Vector3[] Execute()
        {
            return new[] {
                Vector3.UnitX,
                -Vector3.UnitX,
                Vector3.UnitY,
                -Vector3.UnitY
            };
        }
    }
}
