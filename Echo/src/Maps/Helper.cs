using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Maps
{
    public static class Helper
    {
        public static bool[,,] To3D(this bool[,] map2D)
        {
            var map = new bool[map2D.GetLength(0), map2D.GetLength(1), 1];

            for (var i = 0; i < map2D.GetLength(0); i++)
            {
                for (int j = 0; j < map2D.GetLength(1); j++)
                {
                    map[i, j, 0] = map2D[i, j];
                }
            }

            return map;
        }



        public static bool[,] To2D(this bool[,,] map3D)
        {
            if (map3D.GetLength(2) != 1)
                throw new ArgumentException("[map3D] should be of size (x; y; 1).");

            var map = new bool[map3D.GetLength(0), map3D.GetLength(1)];

            for (var i = 0; i < map3D.GetLength(0); i++)
            {
                for (int j = 0; j < map3D.GetLength(1); j++)
                {
                    map[i, j] = map3D[i, j, 0];
                }
            }

            return map;
        }
    }
}
