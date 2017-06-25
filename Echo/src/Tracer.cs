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
    public class Tracer<TWave> where TWave : IWave, IPropagator<TWave>
    {
        public IMap<TWave> DefaultMap;
        public IProcessingQueue<TWave> DefaultProcessingQueue;



        public Tracer() : this(null, null) { }
        
        public Tracer(IMap<TWave> defaultMap, IProcessingQueue<TWave> defaultProcessingQueue)
        {
            DefaultMap = defaultMap;
            DefaultProcessingQueue = defaultProcessingQueue;
        }



        public IEnumerable<IReadOnlyList<Vector3>> Search(IEnumerable<TWave> initial, IMap<TWave> map, IProcessingQueue<TWave> queue)
        {
            if (initial.Any() == false) yield break;

            map = map ?? DefaultMap;
            queue = queue ?? DefaultProcessingQueue;

            {
                var errors = new List<string>(2);
                if (map == null) errors.Add(nameof(map));
                if (initial == null) errors.Add(nameof(initial));
                if (errors.Count > 0) throw new ArgumentNullException(string.Join(", ", errors));
            }

            if (queue == null)
            {
                queue = new QueueAdapter<TWave>();
                Debug.WriteLine("ECHO: Processing queue is not set. QueueAdapter<" + nameof(TWave) + "> (breadth-first search) will be used.");
            }

            queue.Clear();
            
            foreach (var wave in initial)
            {
                queue.Enqueue(wave);
            }
            
            while (queue.Count > 0)
            {
                var wave = queue.Dequeue();
                Vector3 location;

                if (!map.Navigate(wave, out location))
                {
                    continue;
                }
                
                wave.Relocate(location);
                wave.Update();

                if (wave.IsFading)
                {
                    continue;
                }

                if (wave.IsAcceptable)
                {
                    yield return wave.FullPath;
                    continue;
                }

                var waves = wave.Propagate();
                for (var i = 0; i < waves.Length; i++)
                {
                    queue.Enqueue(waves[i]);
                }

                queue.Enqueue(wave);
            }
        }
    }
}
