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
    public class AreaFilterTest
    {
        public static bool[,] Area1_HorizontalLane3
        {
            get
            {
                return new bool[3, 1] { { true }, { true }, { true } };
            }
        }

        public static bool[,] Area2_VerticalLane3
        {
            get
            {
                return new bool[1, 3] { { true, true, true } };
            }
        }

        [Fact]
        public void PointAreaTest()
        {
            var filter = new AreaFilter(Vector3.One);
            Assert.True(filter.Is(new Echo(Vector3.One, Vector3.UnitX)));
            Assert.False(filter.Is(new Echo(Vector3.Zero, Vector3.UnitX)));
        }
    }
}
