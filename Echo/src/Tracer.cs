using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Echo.Abstract;
using System.Collections;
using Echo.Queues.Adapters;
using System.Diagnostics;
using Echo.Filters;
using Echo.Waves;

namespace Echo
{
    public class Tracer<TWave> where TWave : IWave
    {
        public IMap<TWave> DefaultMap;
        public IWaveBuilder<TWave> DefaultWaveBuilder;
        public IWaveSpawningStrategy DefaultWaveSpawnStrategy;
        public IWaveSpreadingStrategy<TWave> DefaultWaveSpreadStrategy;
        public IWaveFilter<TWave> DefaultAcceptableWavesFilter;
        public IWaveFilter<TWave> DefaultFadingWavesFilter;
        public IWaveQueue<TWave> DefaultWavesProcessingQueue;



        public Tracer()
            : this(null, null) { }

        public Tracer(IWaveSpawningStrategy defaultWaveSpawnStrategy,
                      IWaveSpreadingStrategy<TWave> defaultWaveSpreadStrategy)
            : this(null, null, defaultWaveSpawnStrategy, defaultWaveSpreadStrategy, null, null, null) { }
        
        public Tracer(IMap<TWave> defaultMap,
                      IWaveBuilder<TWave> defaultWaveBuilder,
                      IWaveSpawningStrategy defaultWaveSpawnStrategy,
                      IWaveSpreadingStrategy<TWave> defaultWaveSpreadStrategy,
                      IWaveFilter<TWave> defaultAcceptableWavesFilter,
                      IWaveFilter<TWave> defaultFadingWavesFilterfading,
                      IWaveQueue<TWave> defaultWavesProcessingQueue)
        {
            DefaultMap = defaultMap;
            DefaultWaveBuilder = defaultWaveBuilder;
            DefaultWaveSpawnStrategy = defaultWaveSpawnStrategy;
            DefaultWaveSpreadStrategy = defaultWaveSpreadStrategy;
            DefaultAcceptableWavesFilter = defaultAcceptableWavesFilter;
            DefaultFadingWavesFilter = defaultFadingWavesFilterfading;
            DefaultWavesProcessingQueue = defaultWavesProcessingQueue;
        }



        public IEnumerable<IReadOnlyList<Vector3>> Search(Vector3 start,
                                                          IMap<TWave> map = null,
                                                          IWaveBuilder<TWave> builder = null,
                                                          IWaveSpawningStrategy spawn = null,
                                                          IWaveSpreadingStrategy<TWave> spread = null,
                                                          IWaveFilter<TWave> acceptable = null,
                                                          IWaveFilter<TWave> fading = null,
                                                          IWaveQueue<TWave> queue = null)
        {
            map = map ?? DefaultMap;
            builder = builder ?? DefaultWaveBuilder;
            spawn = spawn ?? DefaultWaveSpawnStrategy;
            spread = spread ?? DefaultWaveSpreadStrategy;
            acceptable = acceptable ?? DefaultAcceptableWavesFilter;
            fading = fading ?? DefaultFadingWavesFilter;
            queue = queue ?? DefaultWavesProcessingQueue ?? new QueueAdapter<TWave>();
                
            if (fading == null) Debug.WriteLine("ECHO: Fading filter is not set. During results enumeration OutOfMemoryException may occur. To prevent, set filter or limit the enumeration.");
            if (queue == null) Debug.WriteLine("ECHO: Queue is not set. Default implementation (QueueAdapter<" + nameof(TWave) + "> - breadth-first search) will be used.");

            {
                var error = new List<string>();
                if (map == null) error.Add(nameof(map));
                if (spawn == null) error.Add(nameof(spawn));
                if (acceptable == null) error.Add(nameof(acceptable));
                if (error.Count > 0) throw new ArgumentNullException(string.Join(", ", error));
            }

            queue.Clear();
            
            foreach (var direction in spawn.Execute())
            {
                var wave = builder.Create(start, direction);
                queue.Enqueue(wave);
            }

            var iteration = 0;
            while (queue.Count > 0)
            {
                iteration++;

                var wave = queue.Dequeue();
                Vector3 location;

                if (!map.Navigate(wave, out location))
                {
                    continue;
                }
                
                wave.MoveTo(location);

                if (acceptable.Is(wave))
                {
                    yield return wave.FullPath;
                    continue;
                }

                if (fading != null &&
                    fading.Is(wave))
                {
                    continue;
                }
                
                if (spread != null)
                {
                    foreach (var direction in spread.Execute(wave))
                    {
                        queue.Enqueue(builder.Create(wave, direction));
                    }
                }

                queue.Enqueue(wave);
            }
        }
    }
}
