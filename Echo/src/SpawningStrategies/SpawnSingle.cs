using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.SpawningStrategies
{
    public class SpawnSingle : IEchoSpawningStrategy
    {
        private Vector3 _direction;

        public SpawnSingle(Vector3 direction)
        {
            _direction = direction;
        }

        public IEnumerable<Vector3> Execute()
        {
            return new[] { _direction };
        }
    }
}
