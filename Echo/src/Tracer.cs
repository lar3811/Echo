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
using Echo.Conditions;
using Echo.Waves;
using Echo.Maps;
using Echo.InitializationStrategies;

namespace Echo
{
    /// <summary>
    /// Contains logic for processing and queueing waves. 
    /// To start searching for a path invoke <see cref="Search"/> method.
    /// </summary>
    /// <typeparam name="TWave">Type of waves to process (e.g. <see cref="Wave"/>).
    /// Custom wave types must implement both <see cref="IWave"/> and <see cref="IWaveBehaviour{TWave}"/>
    /// interfaces to be applicable.</typeparam>
    public class Tracer<TWave> where TWave : IWave, IWaveBehaviour<TWave>
    {
        private int _iterations;
        private int _waves;
        
        /// <summary>
        /// Indicates how many iterations passed since the last search session started;
        /// </summary>
        public int IterationsPassed => _iterations;
        /// <summary>
        /// Indicates how many wave objects are currently in the queue.
        /// </summary>
        public int ActiveWavesCount => _waves;
        /// <summary>
        /// This map will be searched if none other is provided to the <see cref="Search(IInitializationStrategy{TWave}, IMap{TWave}, IProcessingQueue{TWave})"/> method.
        /// </summary>
        public IMap<TWave> DefaultMap;
        /// <summary>
        /// This queue will be used if none other is provided to the <see cref="Search(IInitializationStrategy{TWave}, IMap{TWave}, IProcessingQueue{TWave})"/> method.
        /// </summary>
        public IProcessingQueue<TWave> DefaultProcessingQueue;
        
        /// <summary>
        /// Creates an instance of the class with default search parameters."/>
        /// </summary>
        /// <param name="defaultMap">This map will be searched if none other is provided to the <see cref="Search(IInitializationStrategy{TWave}, IMap{TWave}, IProcessingQueue{TWave})"/> method.</param>
        /// <param name="defaultProcessingQueue">This queue will be used if none other is provided to the <see cref="Search(IInitializationStrategy{TWave}, IMap{TWave}, IProcessingQueue{TWave})"/> method.</param>
        public Tracer(IMap<TWave> defaultMap, IProcessingQueue<TWave> defaultProcessingQueue)
        {
            DefaultMap = defaultMap;
            DefaultProcessingQueue = defaultProcessingQueue;
        }
        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        public Tracer() : this(null, null) { }
        
        /// <summary>
        /// Initiates searching routine. See remarks section for details.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Propagates waves obtained from <paramref name="initial"/> strategy across given <paramref name="map"/>
        /// until a wave satisfies its <see cref="IWaveBehaviour{TWave}.AcceptanceCondition"/>. The wave is then yield-returned from the method.
        /// </para>
        /// <para>
        /// After <paramref name="initial"/> strategy is executed and produced waves are added to the queue,
        /// tracer starts iterating through it. Each iteration consists of 5 phases: 
        /// Navigation; Update; Fading check; Acceptance check; Propagation.
        /// </para>
        /// <para>
        /// If not limited by LINQ expressions, enumeration continues until processing queue is empty.
        /// Waves are removed from processing queue if they satisfy either <see cref="IWaveBehaviour{TWave}.FadeCondition"/> 
        /// or <see cref="IWaveBehaviour{TWave}.AcceptanceCondition"/> or encounter an obstacle 
        /// during <see cref="IMap{TWave}.Navigate(TWave, out Vector3)"/>. It is important to cull waves
        /// in this manner to prevent infinite cycles and reduce memory use.
        /// </para>
        /// <para>If <paramref name="map"/> is null <see cref="DefaultMap"/> will be used.</para>
        /// <para>If <paramref name="queue"/> is null <see cref="DefaultProcessingQueue"/> will be used.</para>
        /// <para>Finally, if both <paramref name="queue"/> and <see cref="DefaultProcessingQueue"/> are nulls <see cref="QueueAdapter{TWave}"/> will be used.</para>
        /// </remarks>
        /// <param name="initial">Initial set of waves to propagate (e.g. <see cref="Initialize4x2D{TWave}"/>).</param>
        /// <param name="map">Map to navigate (e.g. <see cref="GridMap"/>).</param>
        /// <param name="queue">Queue to store and order waves between iterations (e.g. <see cref="QueueAdapter{TWave}"/>).</param>
        /// <returns>Waves that satisfy their respective <see cref="IWaveBehaviour{TWave}.AcceptanceCondition"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if both <paramref name="map"/> and <see cref="DefaultMap"/> are nulls, or <paramref name="initial"/> is null.</exception>
        public IEnumerable<TWave> Search(IInitializationStrategy<TWave> initial, IMap<TWave> map, IProcessingQueue<TWave> queue)
        {
            _iterations = 0;
            _waves = 0;

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
            else
            {
                queue.Clear();
            }

            {
                var waves = initial.Execute();
                if (waves == null || waves.Length == 0) yield break;
                for (int i = 0; i < waves.Length; i++)
                {
                    queue.Enqueue(waves[i]);
                }
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
                wave.Update?.Execute(wave);

                if (wave.FadeCondition?.Check(wave) == true)
                {
                    continue;
                }

                if (wave.AcceptanceCondition?.Check(wave) == true)
                {
                    yield return wave;
                    continue;
                }

                var waves = wave.Propagation?.Execute(wave);
                if (waves != null)
                {
                    for (var i = 0; i < waves.Length; i++)
                    {
                        if (waves[i] == null) continue;
                        queue.Enqueue(waves[i]);
                    }
                }

                queue.Enqueue(wave);

                _iterations++;
                _waves = queue.Count;
            }
        }
    }
}
