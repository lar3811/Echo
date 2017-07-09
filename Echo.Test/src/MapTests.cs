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
        public static bool[,] GenerateCleanMap2D(int columns, int rows)
        {
            var map = new bool[columns, rows];
            for (var i = 0; i < columns; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    map[i, j] = true;
                }
            }
            return map;
        }

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
    }
}
