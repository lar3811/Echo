using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Maps
{
    public static class Helper
    {
        /// <summary>
        /// Converts 2D boolean table to 3D boolean table.
        /// </summary>
        /// <param name="map2D">2D boolean table.</param>
        /// <returns>3D boolean table.</returns>
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

        /// <summary>
        /// Converts 3D boolean table to 2D boolean table.
        /// </summary>
        /// <param name="map3D">3D boolean table.</param>
        /// <returns>2D boolean table.</returns>
        public static bool[,] To2D(this bool[,,] map3D)
        {
            if (map3D.GetLength(2) != 1)
                throw new ArgumentException("ECHO: Parameter [map3D] should be of size (W; H; 1).");

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
