using Echo;
using Echo.SpawningStrategies;
using Echo.SpreadStrategies;
using Echo.Filters.Support;
using Echo.Filters;
using Echo.QueueAdapters;
using Echo.Maps;
using System;
using System.Linq;
using Xunit;
using System.Numerics;

namespace Echo.Test
{
    public class QueueAdaptersTests
    {
        [Fact]
        public void Test1()
        {
            var adapter = new QueueOrderedByDistance(Vector3.Zero);

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
    }
}
