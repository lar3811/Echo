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
using System.Collections.Generic;
using Echo.Waves;

namespace Echo.Test
{
    public class WaveTests
    {


        public struct test : ICloneable
        {
            public int X;

            public object Clone()
            {
                return this;
            }
        }


        public static Wave Snail
        {
            get
            {
                var wave = new Wave(Vector3.Zero, Vector3.UnitX);

                var path = (List<Vector3>)wave.PathSegment;
                path.Add(Vector3.UnitX);

                wave = wave.Spread(new[] { Vector3.UnitY })[0];

                path.Add(2 * Vector3.UnitX);
                path = (List<Vector3>)wave.PathSegment;
                path.Add(Vector3.UnitX + Vector3.UnitY);

                wave = wave.Spread(new[] { -Vector3.UnitX })[0];

                path.Add(Vector3.UnitX + 2 * Vector3.UnitY);
                path = (List<Vector3>)wave.PathSegment;
                path.Add(Vector3.UnitY);
                path.Add(-Vector3.UnitX + Vector3.UnitY);

                return wave;
            }
        }

        public static Wave Cycle
        {
            get
            {
                var wave = new Wave(Vector3.Zero, Vector3.UnitX);

                var path = (List<Vector3>)wave.PathSegment;
                path.Add(Vector3.UnitX);

                wave = wave.Spread(new[] { Vector3.UnitY })[0];

                path.Add(2 * Vector3.UnitX);
                path = (List<Vector3>)wave.PathSegment;
                path.Add(Vector3.UnitX + Vector3.UnitY);

                wave = wave.Spread(new[] { -Vector3.UnitX })[0];

                path.Add(Vector3.UnitX + 2 * Vector3.UnitY);
                path = (List<Vector3>)wave.PathSegment;
                path.Add(Vector3.UnitY);

                wave = wave.Spread(new[] { -Vector3.UnitY })[0];

                path.Add(-Vector3.UnitX + Vector3.UnitY);
                path = (List<Vector3>)wave.PathSegment;
                path.Add(Vector3.Zero);

                return wave;
            }
        }



        [Fact]
        public void SnailTest()
        {
            var wave = Snail;
            Assert.True(wave.Progenitors.Count == 3);
            Assert.True(wave.Progenitors[0] == wave);
            Assert.True(wave.Progenitors[0].FullPath.Length == 5);
            Assert.True(wave.Progenitors[1].FullPath.Length == 4);
            Assert.True(wave.Progenitors[2].FullPath.Length == 3);
        }

        [Fact]
        public void CycleTest()
        {
            var wave = Cycle;
            Assert.True(wave.Progenitors.Count == 4);
            Assert.True(wave.Progenitors[0] == wave);
            Assert.True(wave.Progenitors[0].FullPath.Length == 5);
            Assert.True(wave.Progenitors[1].FullPath.Length == 5);
            Assert.True(wave.Progenitors[2].FullPath.Length == 4);
            Assert.True(wave.Progenitors[3].FullPath.Length == 3);
        }

        [Fact]
        public void Test0()
        {
            var test1 = new test();
            var test2 = (test)test1.Clone();
            test2.X = 1;
            Assert.NotEqual(test1.X, test2.X);
        }
    }
}
