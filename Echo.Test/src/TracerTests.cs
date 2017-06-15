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

namespace Echo.Test
{
    public class TracerTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new Tracer().Search(Vector3.Zero, Vector3.UnitX).ToList());
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

            var routes = tracer.Search(Vector3.Zero);

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

            var routes = tracer.Search(Vector3.Zero);

            Assert.Empty(routes);
        }

        [Fact]
        public void Test4()
        {
            //Reproduces eternal cycle without fade filter

            var tracer = new Tracer(
                new GridMap(MapTests.Map3_Square3x3),
                new Spawn4x2D(),
                new Spread2x2D(),
                new AreaFilter(new Vector3(2, 2, 0)),
                new GlobalIntersectionsFilter(),
                new QueueAdapter());

            var routes = tracer.Search(Vector3.Zero).ToList();
        }

        [Fact]
        public void Test5()
        {
            var tracer = new Tracer(
                new GridMap(MapTests.Map5_Square3x3_WallWithHole),
                new Spawn4x2D(),
                new Spread2x2D(),
                new AreaFilter(new Vector3(1, 2, 0)),
                null,
                new QueueAdapter());

            var routes = tracer.Search(Vector3.UnitY);

            Assert.Single(routes);
            Assert.True(routes.First().Count == 7);
        }

        [Fact]
        public void Test6()
        {
            var tracer = new Tracer(new Spawn4x2D(), new Spread2x2D());
            var map = new GridMap(MapTests.Map7_Square3x3_SingleObstacle);

            var route = tracer.SearchForShortestPath(map, Vector3.Zero, new Vector3(1, 2, 0));
            Assert.True(route.Count == 4);

            route = tracer.SearchForShortestPath(map, Vector3.Zero, new Vector3(2, 1, 0));
            Assert.True(route.Count == 4);
        }

        [Fact]
        public void Test7()
        {
            var tracer = new Tracer(new Spawn4x2D(), new Spread2x2D());
            var map = new GridMap(MapTests.Map3_Square3x3);

            var route = tracer.SearchForShortestPath(map, Vector3.Zero, new Vector3(1, 2, 0));
            Assert.True(route.Count == 4);

            route = tracer.SearchForShortestPath(map, Vector3.Zero, new Vector3(2, 1, 0));
            Assert.True(route.Count == 4);
        }

        [Fact]
        public void ShortestPathTest()
        {
            var map = MapTests.GenerateCleanMap2D(5, 5);
            var tracer = new Tracer(new Spawn8x2D(), new Spread4x2D());

            for (var i = 1; i < 4; i++)
                map[2, i] = false;

            var route = tracer.SearchForShortestPath(new GridMap(map), new Vector3(0, 2, 0), new Vector3(4, 2, 0));
            
            Assert.StrictEqual(route.Count, 5);
        }
    }
}
