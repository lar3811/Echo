using Echo.Abstract;
using Echo.Filters;
using Echo.InitializationStrategies;
using Echo.Maps;
using Echo.PropagationStrategies;
using Echo.Queues;
using Echo.Queues.Adapters;
using Echo.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Echo.Test
{
    public class General
    {
        [Fact]
        public void TracerTest()
        {
            var map = MapTests.GenerateCleanMap2D(5, 5);
            map[2, 1] = false;
            map[2, 2] = false;
            map[2, 3] = false;

            var start = new Vector3(0, 2, 0);
            var finish = new Vector3(4, 2, 0);

            var tracer = new Tracer<Wave> { DefaultMap = new GridMap<Wave>(map) };
            var path = tracer.FindShortestPath(start, finish);

            Assert.Equal(path.Count, 5);
            Assert.Equal(path[0], start);
            Assert.Equal(path[4], finish);
            Assert.True(path.All(p => map[(int)p.X, (int)p.Y] == true));
        }        
    }
}
