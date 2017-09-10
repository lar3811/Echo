using Echo.Abstract;
using Echo.Conditions;
using Echo.Queues.Adapters;
using Echo.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Echo;
using Echo.Waves;
using Echo.InitializationStrategies;
using Echo.PropagationStrategies;
using Echo.Maps;

namespace Echo
{
    /// <summary>
    /// Provides convinient extension methods for <see cref="Tracer{TWave}"/> class.
    /// </summary>
    public static class TracerExtensions
    {
        /// <summary>
        /// Searches for the shortest path to the point by propagating given waves.
        /// </summary>
        /// <typeparam name="TWave">Type of waves to use (e.g. <see cref="Wave"/>).</typeparam>
        /// <param name="tracer">Tracer to search with.</param>
        /// <param name="initial">Initial set of waves to propagate (e.g. <see cref="Initialize4x2D{TWave}"/>).</param>
        /// <param name="to">Location to reach.</param>
        /// <param name="map">Map to navigate (e.g. <see cref="GridMap"/>); if left null, <see cref="Tracer{TWave}.DefaultMap"/> will be used.</param>
        /// <returns>List of waypoints.</returns>
        /// <exception cref="ArgumentNullException">Thrown if both <paramref name="map"/> parameter and <see cref="Tracer{TWave}.DefaultMap"/> field are null.</exception>
        public static IReadOnlyList<Vector3> FindShortestPath<TWave>(
            this Tracer<TWave> tracer, IInitializationStrategy<TWave> initial, Vector3 to, IMap<TWave> map = null)
            where TWave : IWave, IWaveBehaviour<TWave>
        {
            if (map == null && tracer.DefaultMap == null)
                throw new ArgumentNullException(nameof(map), "ECHO: Either [map] parameter or [Tracer.DefaultMap] field must not be null.");

            map = map ?? tracer.DefaultMap;

            var queue = new PriorityQueue<TWave>(
                new PriorityByEstimatedPathLength<TWave>(to, map),
                new PriorityByAlignment<TWave>(to));
            var wave = tracer.Search(initial, map, queue).FirstOrDefault();
            return wave?.FullPath;
        }
        
        /// <summary>
        /// Searches for the shortest path between two points.
        /// </summary>
        /// <typeparam name="TWave">Type of waves to use (e.g. <see cref="Wave"/>).</typeparam>
        /// <param name="tracer">Tracer to search with.</param>
        /// <param name="from">Location to start from.</param>
        /// <param name="to">Location to reach.</param>
        /// <param name="map">Map to navigate (e.g. <see cref="GridMap"/>); if left null, <see cref="Tracer{TWave}.DefaultMap"/> will be used.</param>
        /// <param name="custom">Wave builder to create waves with (e.g. <see cref="Waves.Base{TWave}.Builder"/>).</param>
        /// <returns>List of waypoints.</returns>
        /// <exception cref="ArgumentNullException">Thrown if both <paramref name="map"/> parameter and <see cref="Tracer{TWave}.DefaultMap"/> field are null.</exception>
        public static IReadOnlyList<Vector3> FindShortestPath<TWave>(
            this Tracer<TWave> tracer, Vector3 from, Vector3 to, IMap<TWave> map = null, IWaveBuilder<TWave> custom = null)
            where TWave : Base<TWave>, new()
        {
            if (map == null && tracer.DefaultMap == null)
                throw new ArgumentNullException(nameof(map), "ECHO: Either [map] parameter or [Tracer.DefaultMap] field must not be null.");

            map = map ?? tracer.DefaultMap;

            var guide = map as IDirectionsProvider;
            var builder = new Base<TWave>.Builder { NestedBuilder = custom };

            IInitializationStrategy<TWave> initial;
            IPropagationStrategy<TWave> propagation;
            if (guide == null)
            {
                initial = new Initialize26x3D<TWave>(builder, from);
                propagation = new Propagate16x3D<TWave>(builder);
            }
            else
            {
                initial = new InitializeX<TWave>(guide, builder, from);
                propagation = new PropagateX<TWave>(guide, builder);
            }

            builder.PropagationStrategy = propagation;
            builder.AcceptanceCondition = new AreaCondition(to);
            builder.FadeCondition = new GlobalIntersectionsCondition();
            return tracer.FindShortestPath(initial, to, map);
        }
    }
}
