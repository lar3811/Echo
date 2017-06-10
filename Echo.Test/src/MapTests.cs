using Echo.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Echo.Test
{
    public class MapTests
    {
        #region Maps
        public static bool[,] Map0_SingleCell
        {
            get
            {
                return new bool[1, 1] { { true } };
            }
        }

        public static bool[,] Map1_HorizontalLane3
        {
            get
            {
                return new bool[3, 1] { { true }, { true }, { true } };
            }
        }

        public static bool[,] Map2_VerticalLane3
        {
            get
            {
                return new bool[1, 3] { { true, true, true } };
            }
        }

        public static bool[,] Map3_Square3x3
        {
            get
            {
                return new bool[3, 3]
                {
                    { true, true, true },
                    { true, true, true },
                    { true, true, true }
                };
            }
        }

        public static bool[,] Map3_Square3x3_HorizontalWall
        {
            get
            {
                return new bool[3, 3]
                {
                    { true, false, true },
                    { true, false, true },
                    { true, false, true }
                };
            }
        }

        public static bool[,] Map4_Square3x3_DiagonalWall
        {
            get
            {
                return new bool[3, 3]
                {
                    { true, true, false },
                    { true, false, true },
                    { false, true, true }
                };
            }
        }

        public static bool[,] Map5_Square3x3_WallWithHole
        {
            get
            {
                return new bool[3, 3]
                {
                    { true, true, false },
                    { true, false, true },
                    { true, true, true }
                };
            }
        }

        public static bool[,] Map6_Square3x3_WallWithHole
        {
            get
            {
                return new bool[3, 3]
                {
                    { true, true, false },
                    { true, true, true },
                    { false, true, true }
                };
            }
        }

        public static bool[,] Map7_Square3x3_SingleObstacle
        {
            get
            {
                return new bool[3, 3]
                {
                    { true, true, true },
                    { true, false, true },
                    { true, true, true }
                };
            }
        }
        #endregion



        [Fact]
        public void GridMap0Test()
        {
            var map = new GridMap(Map0_SingleCell);

            Assert.Null(map.Move(Vector3.Zero, -Vector3.UnitX));
            Assert.Null(map.Move(Vector3.Zero, Vector3.UnitX));
            Assert.Null(map.Move(Vector3.Zero, -Vector3.UnitY));
            Assert.Null(map.Move(Vector3.Zero, Vector3.UnitY));

            Assert.Null(map.Move(Vector3.UnitX, -Vector3.UnitX));
            Assert.Null(map.Move(-Vector3.UnitX, Vector3.UnitX));
            Assert.Null(map.Move(Vector3.UnitY, -Vector3.UnitY));
            Assert.Null(map.Move(-Vector3.UnitY, Vector3.UnitY));

            Assert.Null(map.Move(Vector3.UnitX + Vector3.UnitY, -Vector3.UnitX - Vector3.UnitY));
            Assert.Null(map.Move(Vector3.UnitX - Vector3.UnitY, -Vector3.UnitX + Vector3.UnitY));
            Assert.Null(map.Move(-Vector3.UnitX + Vector3.UnitY, Vector3.UnitX - Vector3.UnitY));
            Assert.Null(map.Move(-Vector3.UnitX - Vector3.UnitY, Vector3.UnitX + Vector3.UnitY));
        }

        [Fact]
        public void GridMap1Test()
        {
            var map = new GridMap(Map1_HorizontalLane3);

            Assert.Null(map.Move(Vector3.Zero, -Vector3.UnitY));
            Assert.Null(map.Move(Vector3.Zero, Vector3.UnitY));
            Assert.Null(map.Move(Vector3.Zero, -Vector3.UnitX));

            Assert.Equal(map.Move(Vector3.Zero, Vector3.UnitX), Vector3.UnitX);
        }

        [Fact]
        public void GridMap2Test()
        {
            var map = new GridMap(Map2_VerticalLane3);

            Assert.Null(map.Move(Vector3.Zero, -Vector3.UnitX));
            Assert.Null(map.Move(Vector3.Zero, Vector3.UnitX));
            Assert.Null(map.Move(Vector3.Zero, -Vector3.UnitY));

            Assert.Equal(map.Move(Vector3.Zero, Vector3.UnitY), Vector3.UnitY);
        }

        [Fact]
        public void GridMap3Test()
        {
            var map = new GridMap(Map3_Square3x3_HorizontalWall);

            Assert.Null(map.Move(Vector3.Zero, -Vector3.UnitX));
            Assert.Null(map.Move(Vector3.Zero, -Vector3.UnitY));
            Assert.Null(map.Move(Vector3.Zero, Vector3.UnitY));

            Assert.Equal(map.Move(Vector3.Zero, Vector3.UnitX), Vector3.UnitX);
        }
    }
}
