using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.Maps
{
    public class GridMap : IMap<IWave>
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
            :this(accessible.To3D()) { }

        public bool Navigate(IWave wave, out Vector3 destination)
        {
            var from = wave.Location;
            var direction = wave.Direction;
            destination = from;

            if (wave.Direction == Vector3.Zero)
                return false;

            if (from.X < 0 || from.X >= _width ||
                from.Y < 0 || from.Y >= _height ||
                from.Z < 0 || from.Z >= _depth ||
                !_accessible[(int)from.X, (int)from.Y, (int)from.Z])
                return false;

            var x = (int)(from.X + Math.Sign(direction.X));
            var y = (int)(from.Y + Math.Sign(direction.Y));
            var z = (int)(from.Z + Math.Sign(direction.Z));

            if (x < 0 || x >= _width ||
                y < 0 || y >= _height ||
                z < 0 || z >= _depth ||
                !_accessible[x, y, z])
                return false;

            destination = new Vector3(x, y, z);
            return true;
        }
    }
}
