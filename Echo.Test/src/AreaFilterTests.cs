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
    public class AreaFilterTests
    {
        [Fact]
        public void PointAreaTest()
        {
            var filter = new AreaFilter(Vector3.One);
            Assert.True(filter.Is(new Echo(Vector3.One, Vector3.UnitX)));
            Assert.False(filter.Is(new Echo(Vector3.Zero, Vector3.UnitX)));
        }

        [Fact]
        public void NonPointAreaTest()
        {
            var filter = new AreaFilter(Vector3.Zero, 3, 3);
            Assert.True(filter.Is(new Echo(Vector3.Zero, Vector3.UnitX)));
            Assert.True(filter.Is(new Echo(Vector3.UnitX, Vector3.UnitX)));
            Assert.True(filter.Is(new Echo(-Vector3.UnitX, Vector3.UnitX)));
            Assert.True(filter.Is(new Echo(Vector3.UnitY, Vector3.UnitX)));
            Assert.True(filter.Is(new Echo(-Vector3.UnitY, Vector3.UnitX)));

            Assert.False(filter.Is(new Echo(new Vector3(0, 0, -1), Vector3.UnitX)));
            Assert.False(filter.Is(new Echo(new Vector3(0, 0, 1), Vector3.UnitX)));
            Assert.False(filter.Is(new Echo(new Vector3(0, 2, 0), Vector3.UnitX)));
            Assert.False(filter.Is(new Echo(new Vector3(0, -2, 0), Vector3.UnitX)));
            Assert.False(filter.Is(new Echo(new Vector3(2, 0, 0), Vector3.UnitX)));
            Assert.False(filter.Is(new Echo(new Vector3(-2, 0, 0), Vector3.UnitX)));
        }

    }
}
