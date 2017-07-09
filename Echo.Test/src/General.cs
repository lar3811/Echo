using Echo.Abstract;
using Echo.Filters;
using Echo.Maps;
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
        public void Test1()
        {
            var map = MapTests.GenerateCleanMap2D(4, 4);
            var tracer = new Tracer<Wave>();
            var initial = new Wave[]
            {
                new Wave(Vector3.Zero, Vector3.UnitX, new AreaFilter(new Vector3(3, 3, 0)), null, new WavePropagationStrategy(), null)
            };
            var route = tracer.Search(initial, new GridMap<Wave>(map), new QueueAdapter<Wave>()).First();
        }

        public class WavePropagationStrategy : IPropagationStrategy<Wave>
        {
            public Wave[] Execute(Wave source)
            {
                var directions = new SpreadStrategies.Spread2x2D<Wave>().Execute(source);
                var output = new Wave[directions.Length];
                for (int i = 0; i < directions.Length; i++)
                {
                    output[i] = new Wave(source, directions[i]);
                }
                return output;
            }
        }
    }
}
