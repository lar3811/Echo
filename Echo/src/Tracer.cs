using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Echo.Abstract;
using System.Collections;
using Echo.QueueAdapters;
using System.Diagnostics;
using Echo.Filters;

namespace Echo
{
    public class Tracer
    {
        public IMap DefaultMap;
        public IEchoSpawningStrategy DefaultWaveSpawnStrategy;
        public IEchoSpreadingStrategy DefaultWaveSpreadStrategy;
        public IEchoFilter DefaultAcceptableWavesFilter;
        public IEchoFilter DefaultFadingWavesFilter;
        public IEchoQueue DefaultWavesProcessingQueue;



        public Tracer(IMap defaultMap,
                      IEchoSpawningStrategy defaultWaveSpawnStrategy,
                      IEchoSpreadingStrategy defaultWaveSpreadStrategy,
                      IEchoFilter defaultAcceptableWavesFilter,
                      IEchoFilter defaultFadingWavesFilterfading,
                      IEchoQueue defaultWavesProcessingQueue)
        {
            DefaultMap = defaultMap;
            DefaultWaveSpawnStrategy = defaultWaveSpawnStrategy;
            DefaultWaveSpreadStrategy = defaultWaveSpreadStrategy;
            DefaultAcceptableWavesFilter = defaultAcceptableWavesFilter;
            DefaultFadingWavesFilter = defaultFadingWavesFilterfading;
            DefaultWavesProcessingQueue = defaultWavesProcessingQueue;
        }



        public IEnumerable<IReadOnlyList<Vector3>> Search(Vector3 from, Vector3 to)
        {
            var paths = Search(null, from, to);
            foreach (var path in paths) yield return path;
        }

        public IEnumerable<IReadOnlyList<Vector3>> Search(Vector3 from, AreaFilter to)
        {
            var paths = Search(null, from, to);
            foreach (var path in paths) yield return path;
        }

        public IEnumerable<IReadOnlyList<Vector3>> Search(IMap map, Vector3 from, Vector3 to)
        {
            var paths = Search(map, from, new AreaFilter(to));
            foreach (var path in paths) yield return path;
        }

        public IEnumerable<IReadOnlyList<Vector3>> Search(IMap map, Vector3 from, AreaFilter to)
        {
            var paths = Search(from, map, acceptable: to);
            foreach (var path in paths) yield return path;
        }

        public IEnumerable<IReadOnlyList<Vector3>> Search(Vector3 start,
                                                          IMap map = null,
                                                          IEchoSpawningStrategy spawn = null,
                                                          IEchoSpreadingStrategy spread = null,
                                                          IEchoFilter acceptable = null,
                                                          IEchoFilter fading = null,
                                                          IEchoQueue queue = null)
        {
            map = map ?? DefaultMap;
            spawn = spawn ?? DefaultWaveSpawnStrategy;
            spread = spread ?? DefaultWaveSpreadStrategy;
            acceptable = acceptable ?? DefaultAcceptableWavesFilter;
            fading = fading ?? DefaultFadingWavesFilter;
            queue = queue ?? DefaultWavesProcessingQueue ?? new QueueAdapter();
                
            if (fading == null) Debug.WriteLine("ECHO: Fading filter is not set. During results enumeration OutOfMemoryException may occur. To prevent, set filter or limit the enumeration.");
            if (queue == null) Debug.WriteLine("ECHO: Queue is not set. Default implementation (QueueAdapter - breadth-first search) will be used.");

            {
                var error = new List<string>();
                if (map == null) error.Add(nameof(map));
                if (spawn == null) error.Add(nameof(spawn));
                if (acceptable == null) error.Add(nameof(acceptable));
                if (error.Count > 0) throw new ArgumentNullException(string.Join(", ", error));
            }

            queue.Clear();
            
            foreach (var direction in DefaultWaveSpawnStrategy.Execute())
            {
                queue.Enqueue(new Wave(start, direction));
            }

            var iteration = 0;
            while (queue.Count > 0)
            {
                iteration++;

                var echo = queue.Dequeue();
                var current = echo.PathSegment.Last();
                var next = map.Move(current, echo.Direction);

                if (next == null)
                {
                    echo.IsActive = false;
                    continue;
                }

                echo.Age++;
                echo.Location = next.Value;

                if (acceptable.Is(echo))
                {
                    yield return echo.FullPath;
                    continue;
                }

                if (fading != null &&
                    fading.Is(echo))
                {
                    echo.IsActive = false;
                    continue;
                }
                
                if (spread != null)
                {
                    foreach (var direction in spread.Execute(echo))
                    {
                        queue.Enqueue(new Wave(echo, direction));
                    }
                }

                queue.Enqueue(echo);
            }
        }
    }
}
