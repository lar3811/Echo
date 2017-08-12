using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.Maps
{
    /// <summary>
    /// Provides basic logic for building and navigating cell fields.
    /// </summary>
    public class GridMap : IMap<IWave>, IDirectionsProvider
    {
        private readonly Vector3[] _directions;

        private readonly bool[,,] _accessible;

        private readonly int _width;
        private readonly int _height;
        private readonly int _depth;
        
        /// <summary>
        /// Creates an instance of this class from 3D boolean table.
        /// </summary>
        /// <param name="accessible">Boolean table: true - clear; false - obstacle.</param>
        public GridMap(bool[,,] accessible)
        {
            _width = accessible.GetLength(0);
            _height = accessible.GetLength(1);
            _depth = accessible.GetLength(2);
            _accessible = accessible;
            
            if (_depth > 1)
            {
                _directions = new Vector3[26];
                for (int x = -1, i = 0; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int z = -1; z <= 1; z++)
                        {
                            if (x == 0 && y == 0 && z == 0) continue;
                            _directions[i++] = Vector3.Normalize(new Vector3(x, y, z));
                        }
                    }
                }
            }
            else
            {
                _directions = new Vector3[8];
                for (int x = -1, i = 0; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0) continue;
                        _directions[i++] = Vector3.Normalize(new Vector3(x, y, 0));
                    }
                }
            }
        }

        /// <summary>
        /// Creates an instance of this class from 2D boolean table.
        /// </summary>
        /// <param name="accessible">Boolean table: true - clear; false - obstacle.</param>
        public GridMap(bool[,] accessible)
            :this(accessible.To3D()) { }
        
        /// <summary>
        /// Looks for a cell adjacent to the <paramref name="wave"/> in its direction.
        /// If such cell is found it is returned through the <paramref name="destination"/> parameter.
        /// </summary>
        /// <param name="wave">A wave to navigate.</param>
        /// <param name="destination">New location of the <paramref name="wave"/>.</param>
        /// <returns>True if navigation is possible, false otherwise.</returns>
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

        /// <summary>
        /// Provides directions in which a wave can travel from given <paramref name="location"/>.
        /// </summary>
        /// <param name="location">Location to travel from.</param>
        /// <returns>Array of directions.</returns>
        public Vector3[] GetDirections(Vector3 location)
        {
            if (_accessible[(int)location.X, (int)location.Y, (int)location.Z])
                return _directions;
            else
                return null;
        }
    }
}
