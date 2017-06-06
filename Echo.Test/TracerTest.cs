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
    public class TracerTest
    {
        [Fact]
        public void TestMethod1()
        {
            var map = new GridMap(new bool[3, 1] { { true }, { true }, { true } });
            var tracer = new Tracer(
                map, 
                new Spawn4x2D(), 
                new Spread2x2D(), 
                new AreaFilter(new Vector3(2, 0, 0)), 
                new InverseFilter(new AreaFilter(Vector3.Zero, new Vector3(2, 0, 0))), 
                new StackAdapter()); 
        }
    }
}
