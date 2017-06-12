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
    public class VectorTests
    {
        [Fact]
        public void Test1()
        {
            var d1 = Vector3.DistanceSquared(Vector3.UnitX + Vector3.UnitY, Vector3.Zero);
            var d2 = Vector3.DistanceSquared(Vector3.UnitX - Vector3.UnitY, Vector3.Zero);
            var d3 = Vector3.DistanceSquared(-Vector3.UnitX - Vector3.UnitY, Vector3.Zero);
            var d4 = Vector3.DistanceSquared(-Vector3.UnitX + Vector3.UnitY, Vector3.Zero);

            Assert.True(d1 == d2);
            Assert.True(d1 == d3);
            Assert.True(d1 == d4);

            d1 = Vector3.DistanceSquared(2*Vector3.UnitX + Vector3.UnitY, Vector3.Zero);
            d2 = Vector3.DistanceSquared(2*Vector3.UnitX - Vector3.UnitY, Vector3.Zero);
            d3 = Vector3.DistanceSquared(-2*Vector3.UnitX - Vector3.UnitY, Vector3.Zero);
            d4 = Vector3.DistanceSquared(-2*Vector3.UnitX + Vector3.UnitY, Vector3.Zero);
            var d5 = Vector3.DistanceSquared(Vector3.UnitX + 2*Vector3.UnitY, Vector3.Zero);
            var d6 = Vector3.DistanceSquared(Vector3.UnitX - 2*Vector3.UnitY, Vector3.Zero);
            var d7 = Vector3.DistanceSquared(-Vector3.UnitX - 2*Vector3.UnitY, Vector3.Zero);
            var d8 = Vector3.DistanceSquared(-Vector3.UnitX + 2*Vector3.UnitY, Vector3.Zero);
            
            Assert.True(d1 == d2);
            Assert.True(d1 == d3);
            Assert.True(d1 == d4);
            Assert.True(d1 == d5);
            Assert.True(d1 == d6);
            Assert.True(d1 == d7);
            Assert.True(d1 == d8);
        }

    }
}
