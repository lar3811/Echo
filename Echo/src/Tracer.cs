using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Echo.Abstract;
using System.Collections;

namespace Echo
{
    public class Tracer
    {
        private readonly IMap _map;
        private readonly IEchoSpawningStrategy _spawn;
        private readonly IEchoSpreadingStrategy _spread;
        private readonly IEchoFilter _acceptable;
        private readonly IEchoFilter _fading;
        private readonly IEchoQueue _echos;

        public Tracer(IMap map,
                      IEchoSpawningStrategy spawn,
                      IEchoSpreadingStrategy spread,
                      IEchoFilter acceptable,
                      IEchoFilter fading,
                      IEchoQueue queue)
        {
            _map = map;
            _spawn = spawn;
            _spread = spread;
            _acceptable = acceptable;
            _fading = fading;
            _echos = queue;
        }

        

        public void Start(Vector3 start)
        {
            _echos.Clear();
            
            foreach (var direction in _spawn.Execute())
            {
                _echos.Enqueue(new Echo(start, direction));
            }

            var output = new List<List<Vector3>>();
            var iteration = 0;
            while (_echos.Count > 0)
            {
                iteration++;

                var echo = _echos.Dequeue();
                var current = echo.Path.Last();
                var next = _map.Move(current, echo.Direction);

                echo.Age++;
                echo.Path.Add(next);

                if (_acceptable.Is(echo))
                {
                    output.Add(echo.Path);
                    continue;
                }

                if (_fading.Is(echo))
                {
                    continue;
                }
                
                foreach (var direction in _spread.Execute(echo))
                {
                    _echos.Enqueue(new Echo(echo, direction));
                }

                _echos.Enqueue(echo);
            }
        }
    }

    public class Echo
    {
        public int Age = 0;

        public readonly List<Vector3> Path;
        public readonly Vector3 Direction;
        public readonly int Generation;

        internal Echo(Vector3 start, Vector3 direction)
        {
            Path = new List<Vector3> { start };
            Direction = direction;
            Generation = 0;
        }

        internal Echo(Echo source, Vector3 direction)
        {
            Path = source.Path.ToList();
            Direction = direction;
            Generation = source.Generation + 1;
        }
    }
}
