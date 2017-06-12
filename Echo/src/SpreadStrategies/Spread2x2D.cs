using Echo.Abstract;
using System.Collections.Generic;
using System.Numerics;

namespace Echo.SpreadStrategies
{
    public class Spread2x2D : IWaveSpreadingStrategy
    {
        public Vector3[] Execute(Wave wave)
        {
            return new[] 
            {
                new Vector3(-wave.Direction.Y, wave.Direction.X, 0),
                new Vector3(wave.Direction.Y, -wave.Direction.X, 0)
            };
        }
    }
}
