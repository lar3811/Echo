using Echo;
using Echo.SpawningStrategies;
using Echo.SpreadStrategies;
using Echo.Filters.Support;
using Echo.Filters;
using Echo.Queues.Adapters;
using Echo.Maps;
using System;
using System.Linq;
using Xunit;
using System.Numerics;
using Echo.Queues;
using System.Collections.Generic;

namespace Echo.Test
{
    public class QueueAdaptersTests
    {
        [Fact]
        public void Test1()
        {
            var adapter = new PriorityQueue(new PriorityByProximity(Vector3.Zero));

            adapter.Enqueue(new Wave(Vector3.UnitX, Vector3.UnitX));
            adapter.Enqueue(new Wave(Vector3.UnitX + Vector3.UnitY, Vector3.UnitX + Vector3.UnitY));
            adapter.Enqueue(new Wave(Vector3.UnitY, Vector3.UnitY));

            var e1 = adapter.Dequeue();
            var e2 = adapter.Dequeue();
            var e3 = adapter.Dequeue();

            Assert.True(adapter.Count == 0);
            Assert.True(e1.Location == Vector3.UnitX);
            Assert.True(e2.Location == Vector3.UnitY);
            Assert.True(e3.Location == Vector3.UnitX + Vector3.UnitY);
        }

        [Fact]
        public void Test2()
        {
            var adapter = new PriorityQueue(new PriorityByEstimatedPathLength(Vector3.Zero));

            var w1 = new Wave(Vector3.UnitX, Vector3.UnitX);
            var path = w1.PathSegment as List<Vector3>;
            path.Add(Vector3.Zero);
            path.Add(Vector3.UnitX);
            path.Add(2*Vector3.UnitX);

            var w2 = new Wave(Vector3.UnitX, Vector3.UnitX);

            adapter.Enqueue(w1);
            adapter.Enqueue(w2);

            var e1 = adapter.Dequeue();
            var e2 = adapter.Dequeue();

            Assert.True(adapter.Count == 0);
            Assert.True(e1 == w2);
            Assert.True(e2 == w1);
        }

        [Fact]
        public void Test3()
        {
            var adapter = new PriorityQueue(new PriorityByEstimatedPathLength(new Vector3(4, 2, 0)));

            var w1 = new Wave(new Vector3(0, 2, 0), Vector3.UnitY);
            var path = w1.PathSegment as List<Vector3>;
            path.Add(w1.Location + Vector3.UnitX);
            path.Add(w1.Location + Vector3.UnitY);

            var w2 = new Wave(new Vector3(0, 2, 0), Vector3.UnitX + Vector3.UnitY);
            path = w2.PathSegment as List<Vector3>;
            path.Add(w2.Location + Vector3.UnitX + Vector3.UnitY);

            adapter.Enqueue(w1);
            adapter.Enqueue(w2);

            var e1 = adapter.Dequeue();
            var e2 = adapter.Dequeue();

            Assert.True(adapter.Count == 0);
            Assert.True(e1 == w2);
            Assert.True(e2 == w1);
        }
    }
}
