﻿using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.Maps
{
    public class GridMap : IMap
    {
        private readonly bool[,,] _accessible;

        private readonly int _width;
        private readonly int _height;
        private readonly int _depth;

        public GridMap(bool[,,] accessible)
        {
            _width = accessible.GetLength(0);
            _height = accessible.GetLength(1);
            _depth = accessible.GetLength(2);
            _accessible = accessible;
        }
        public GridMap(bool[,] accessible)
        {
            _width = accessible.GetLength(0);
            _height = accessible.GetLength(1);
            _depth = 1;
            _accessible = new bool[_width, _height, _depth];

            for (var i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _accessible[i, j, 0] = accessible[i, j];
                }
            }
        }

        public Vector3? Move(Vector3 from, Vector3 direction)
        {
            if (direction == Vector3.Zero)
                return from;

            var x = (int)(from.X + Math.Sign(direction.X));
            var y = (int)(from.Y + Math.Sign(direction.Y));
            var z = (int)(from.Z + Math.Sign(direction.Z));

            if (x < 0 || x >= _width ||
                y < 0 || y >= _height ||
                z < 0 || z >= _depth ||
                !_accessible[x, y, z])
                return null;

            return new Vector3(x, y, z);
        }
    }
}