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
        public IWaveSpawningStrategy DefaultWaveSpawnStrategy;
        public IWaveSpreadingStrategy DefaultWaveSpreadStrategy;
        public IWaveFilter DefaultAcceptableWavesFilter;
        public IWaveFilter DefaultFadingWavesFilter;
        public IWaveQueue DefaultWavesProcessingQueue;



        public Tracer()
            : this(null, null) { }

        public Tracer(IWaveSpawningStrategy defaultWaveSpawnStrategy,
                      IWaveSpreadingStrategy defaultWaveSpreadStrategy)
            : this(null, defaultWaveSpawnStrategy, defaultWaveSpreadStrategy, null, null, null) { }
        
        public Tracer(IMap defaultMap,
                      IWaveSpawningStrategy defaultWaveSpawnStrategy,
                      IWaveSpreadingStrategy defaultWaveSpreadStrategy,
                      IWaveFilter defaultAcceptableWavesFilter,
                      IWaveFilter defaultFadingWavesFilterfading,
                      IWaveQueue defaultWavesProcessingQueue)
        {
            DefaultMap = defaultMap;
            DefaultWaveSpawnStrategy = defaultWaveSpawnStrategy;
            DefaultWaveSpreadStrategy = defaultWaveSpreadStrategy;
            DefaultAcceptableWavesFilter = defaultAcceptableWavesFilter;
            DefaultFadingWavesFilter = defaultFadingWavesFilterfading;
            DefaultWavesProcessingQueue = defaultWavesProcessingQueue;
        }



        public IEnumerable<IReadOnlyList<Vector3>> Search(Vector3 start,
                                                          IMap map = null,
                                                          IWaveSpawningStrategy spawn = null,
                                                          IWaveSpreadingStrategy spread = null,
                                                          IWaveFilter acceptable = null,
                                                          IWaveFilter fading = null,
                                                          IWaveQueue queue = null)
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

                var wave = queue.Dequeue();
                var current = wave.PathSegment.Last();
                var next = map.Move(current, wave.Direction);

                if (next == null)
                {
                    wave.IsActive = false;
                    continue;
                }

                wave.Age++;
                wave.Location = next.Value;

                if (acceptable.Is(wave))
                {
                    yield return wave.FullPath;
                    continue;
                }

                if (fading != null &&
                    fading.Is(wave))
                {
                    wave.IsActive = false;
                    continue;
                }
                
                if (spread != null)
                {
                    foreach (var direction in spread.Execute(wave))
                    {
                        queue.Enqueue(new Wave(wave, direction));
                    }
                }

                queue.Enqueue(wave);
            }
        }
    }
}
