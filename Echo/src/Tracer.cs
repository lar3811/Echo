using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Echo.Abstract;
using System.Collections;
using Echo.QueueAdapters;

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
                      IEchoFilter acceptable)
            : this(map, spawn, spread, acceptable, null, null)
        { }
        public Tracer(IMap map,
                      IEchoSpawningStrategy spawn,
                      IEchoSpreadingStrategy spread,
                      IEchoFilter acceptable,
                      IEchoFilter fading,
                      IEchoQueue queue)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map));
            if (spawn == null)
                throw new ArgumentNullException(nameof(spawn));
            if (acceptable == null)
                throw new ArgumentNullException(nameof(acceptable));

            _map = map;
            _spawn = spawn;
            _spread = spread;
            _acceptable = acceptable;
            _fading = fading;
            _echos = queue ?? new QueueAdapter();
        }

        

        public IEnumerable<IReadOnlyList<Vector3>> Start(Vector3 start)
        {
            _echos.Clear();
            
            foreach (var direction in _spawn.Execute())
            {
                _echos.Enqueue(new Wave(start, direction));
            }

            var iteration = 0;
            while (_echos.Count > 0)
            {
                iteration++;

                var echo = _echos.Dequeue();
                var current = echo.PathSegment.Last();
                var next = _map.Move(current, echo.Direction);

                if (next == null)
                {
                    continue;
                }

                echo.Age++;
                echo.Location = next.Value;

                if (_acceptable.Is(echo))
                {
                    yield return echo.FullPath;
                    continue;
                }

                if (_fading != null && 
                    _fading.Is(echo))
                {
                    continue;
                }
                
                if (_spread != null)
                {
                    foreach (var direction in _spread.Execute(echo))
                    {
                        _echos.Enqueue(new Wave(echo, direction));
                    }
                }

                _echos.Enqueue(echo);
            }
        }
    }
}
