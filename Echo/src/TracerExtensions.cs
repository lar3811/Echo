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

namespace Echo
{
    public static class TracerExtensions
    {
        //public static IEnumerable<IReadOnlyList<Vector3>> Search(this Tracer tracer, Vector3 from, Vector3 to)
        //{
        //    var paths = tracer.Search(null, from, to);
        //    foreach (var path in paths) yield return path;
        //}

        //public static IEnumerable<IReadOnlyList<Vector3>> Search(this Tracer tracer, Vector3 from, IWaveFilter to)
        //{
        //    var paths = tracer.Search(null, from, to);
        //    foreach (var path in paths) yield return path;
        //}

        //public static IEnumerable<IReadOnlyList<Vector3>> Search(this Tracer tracer, IMap map, Vector3 from, Vector3 to)
        //{
        //    var paths = tracer.Search(map, from, new AreaFilter(to));
        //    foreach (var path in paths) yield return path;
        //}

        //public static IEnumerable<IReadOnlyList<Vector3>> Search(this Tracer tracer, IMap map, Vector3 from, IWaveFilter to)
        //{
        //    var paths = tracer.Search(from, map, acceptable: to);
        //    foreach (var path in paths) yield return path;
        //}

        //public static IReadOnlyList<Vector3> SearchForShortestPath(this Tracer tracer, IMap map, Vector3 from, Vector3 to, IWaveFilter fading = null)
        //{
        //    var queue = new PriorityQueue(new PriorityByEstimatedPathLength(to));
        //    var acceptable = new AreaFilter(to);
        //    var paths = tracer.Search(from, map, null, null, acceptable, fading, queue);
        //    return paths.FirstOrDefault();
        //}
    }
}
