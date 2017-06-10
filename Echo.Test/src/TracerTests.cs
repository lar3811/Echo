﻿using Echo;
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
    public class TracerTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new Tracer(null, null, null, null, null, null));
        }

        [Fact]
        public void Test2()
        {
            var tracer = new Tracer(
                new GridMap(MapTests.Map1_HorizontalLane3),
                new Spawn4x2D(),
                new Spread2x2D(),
                new AreaFilter(new Vector3(2, 0, 0)),
                null,
                new QueueAdapter());

            var routes = tracer.Start(Vector3.Zero);

            Assert.Single(routes);

            var route = routes.First();

            Assert.True(route.Count == 3);
            Assert.True(route[0] == Vector3.Zero);
            Assert.True(route[1] == Vector3.UnitX);
            Assert.True(route[2] == new Vector3(2, 0, 0));
        }

        [Fact]
        public void Test3()
        {
            var tracer = new Tracer(
                new GridMap(MapTests.Map3_Square3x3_HorizontalWall),
                new Spawn4x2D(),
                new Spread2x2D(),
                new AreaFilter(new Vector3(2, 2, 0)),
                null,
                new QueueAdapter());

            var routes = tracer.Start(Vector3.Zero);

            Assert.Empty(routes);
        }

        [Fact]
        public void Test4()
        {
            //Reproduces eternal cycle

            var tracer = new Tracer(
                new GridMap(MapTests.Map3_Square3x3),
                new Spawn4x2D(),
                new Spread2x2D(),
                new AreaFilter(new Vector3(2, 2, 0)),
                null,
                new QueueAdapter());

            var routes = tracer.Start(Vector3.Zero);
        }
    }
}
