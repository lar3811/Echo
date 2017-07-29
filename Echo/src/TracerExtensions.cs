using Echo.Abstract;
using Echo.Filters;
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
    public static class TracerExtensions
    {
        public static IReadOnlyList<Vector3> FindShortestPath<TWave>(
            this Tracer<TWave> tracer, IInitializationStrategy<TWave> initial, Vector3 to, IMap<TWave> map = null)
            where TWave : IWave, IWaveBehaviour<TWave>
        {
            if (map == null && tracer.DefaultMap == null)
                throw new ArgumentNullException(nameof(map), "ECHO: Either [map] parameter or [Tracer.DefaultMap] field must not be null.");

            var queue = new PriorityQueue<TWave>(new PriorityByEstimatedPathLength<TWave>(to));
            var wave = tracer.Search(initial, map, queue).FirstOrDefault();
            return wave?.FullPath;
        }



        public static IReadOnlyList<Vector3> FindShortestPath<TWave>(
            this Tracer<TWave> tracer, Vector3 from, Vector3 to, IMap<TWave> map, IWaveBuilder<TWave> custom)
            where TWave : Base<TWave>, new()
        {

            if (map == null && tracer.DefaultMap == null)
                throw new ArgumentNullException(nameof(map), "ECHO: Either [map] parameter or [Tracer.DefaultMap] field must not be null.");

            var guide = (map ?? tracer.DefaultMap) as IDirectionsProvider;
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
            return tracer.FindShortestPath(initial, to, map);
        }



        public static IReadOnlyList<Vector3> FindShortestPath<TWave>(
            this Tracer<TWave> tracer, Vector3 from, Vector3 to, IMap<TWave> map)
            where TWave : Base<TWave>, new()
        {
            return tracer.FindShortestPath(from, to, map, null);
        }

        public static IReadOnlyList<Vector3> FindShortestPath<TWave>(
            this Tracer<TWave> tracer, Vector3 from, Vector3 to, IWaveBuilder<TWave> custom)
            where TWave : Base<TWave>, new()
        {
            return tracer.FindShortestPath(from, to, null, custom);
        }

        public static IReadOnlyList<Vector3> FindShortestPath<TWave>(
            this Tracer<TWave> tracer, Vector3 from, Vector3 to)
            where TWave : Base<TWave>, new()
        {
            return tracer.FindShortestPath(from, to, null, null);
        }
    }
}
