using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Conditions
{
    /// <summary>
    /// Checks if a wave is located inside specified area.
    /// </summary>
    public class AreaCondition : ICondition<IWave>
    {
        private Vector3 _p1;
        private Vector3 _p2;

        /// <summary>
        /// Creates an instance of the class with target area specified by two points.
        /// </summary>
        /// <param name="p1">Close top left point of the target area.</param>
        /// <param name="p2">Far bottom right point of the target area.</param>
        public AreaCondition(Vector3 p1, Vector3 p2)
        {
            _p1 = p1;
            _p2 = p2;
        }
        /// <summary>
        /// Creates an instance of the class with one-point target area.
        /// </summary>
        /// <param name="location">Target area location.</param>
        public AreaCondition(Vector3 location) : this(location, 0, 0, 0) { }
        /// <summary>
        /// Creates an instance of the class with target area specified by center, width, height and depth.
        /// </summary>
        /// <param name="center">Center of the target area.</param>
        /// <param name="width">Width of the target area.</param>
        /// <param name="height">Height of the target area.</param>
        /// <param name="depth">Depth of the target area.</param>
        public AreaCondition(Vector3 center, float width, float height, float depth = 0)
        {
            _p1 = new Vector3(center.X - width / 2, center.Y - height / 2, center.Z - depth / 2);
            _p2 = new Vector3(center.X + width / 2, center.Y + height / 2, center.Z + depth / 2);
        }

        /// <summary>
        /// Checks if the <paramref name="wave"/> is located inside specified area (bounds included).
        /// </summary>
        /// <param name="wave">Wave to check.</param>
        /// <returns>True if the <paramref name="wave"/> is located inside specified area, false otherwise.</returns>
        public bool Check(IWave wave)
        {
            var p = wave.Location;
            return p.X >= _p1.X && p.X <= _p2.X &&
                   p.Y >= _p1.Y && p.Y <= _p2.Y &&
                   p.Z >= _p1.Z && p.Z <= _p2.Z;
        }
    }
}
