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
        public void TracerTest1()
        {
            var map = MapTests.GenerateCleanMap2D(5, 5);
            map[2, 1] = false;
            map[2, 2] = false;
            map[2, 3] = false;

            var start = new Vector3(0, 2, 0);
            var finish = new Vector3(4, 2, 0);

            var tracer = new Tracer<Wave> { DefaultMap = new GraphMap(map) };
            var path = tracer.FindShortestPath(start, finish);

            Assert.Equal(path.Count, 5);
            Assert.Equal(path[0], start);
            Assert.Equal(path[4], finish);
            Assert.True(path.All(p => map[(int)p.X, (int)p.Y] == true));
        }

        [Fact]
        public void TracerTest2()
        {
            var binary = MapTests.GenerateCleanMap2D(5, 5);
            binary[2, 1] = false;
            binary[2, 2] = false;
            binary[2, 3] = false;

            var start = new Vector3(0, 2, 0);
            var finish = new Vector3(4, 2, 0);

            var map = new GridMap(binary);
            var queue = new PriorityQueue<Wave>(new PriorityByEstimatedPathLength<Wave>(finish));
            var builder = new Base<Wave>.Builder();
            var initial = new InitializeX<Wave>(map, builder, start);
            var propagation = new PropagateX<Wave>(map, builder);
            builder.PropagationStrategy = propagation;
            builder.AcceptanceCondition = new AreaCondition(finish);

            var tracer = new Tracer<Wave> { DefaultMap = map };
            var wave = tracer.Search(initial, map, queue).FirstOrDefault();

            Assert.NotNull(wave);

            var path = wave.FullPath;

            Assert.Equal(path.Length, 5);
            Assert.Equal(path[0], start);
            Assert.Equal(path[4], finish);
            Assert.True(path.All(p => binary[(int)p.X, (int)p.Y] == true));
        }
    }
}
