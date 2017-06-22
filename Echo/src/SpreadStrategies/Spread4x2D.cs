using Echo.Abstract;
using System.Collections.Generic;
using System.Numerics;

namespace Echo.SpreadStrategies
{
    public class Spread4x2D<TWave> : IWaveSpreadingStrategy<TWave> where TWave : IWave
    {
        public Vector3[] Execute(TWave wave)
        {
            var directions = new[]
            {
                new Vector3(-wave.Direction.Y, wave.Direction.X, 0),
                new Vector3(wave.Direction.Y, -wave.Direction.X, 0),
                Vector3.Normalize(new Vector3(-wave.Direction.Y, wave.Direction.X, 0) + wave.Direction),
                Vector3.Normalize(new Vector3(wave.Direction.Y, -wave.Direction.X, 0) + wave.Direction)
            };
            return directions;
        }
    }
}
