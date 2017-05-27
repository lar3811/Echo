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

        public Vector3 Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                if (value == Vector3.Zero)
                    throw new ArgumentException($"{typeof(SpawnSingle).FullName}: Invalid direction supplied.");
                _direction = Vector3.Normalize(value);
            }
        }

        public SpawnSingle(Vector3 direction)
        {
            Direction = direction;
        }

        public IEnumerable<Vector3> Execute()
        {
            return new[] { Direction };
        }
    }
}
