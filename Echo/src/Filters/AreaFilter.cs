using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class AreaFilter : IWaveFilter
    {
        private Vector3 _p1;
        private Vector3 _p2;

        public AreaFilter(Vector3 p1, Vector3 p2)
        {
            _p1 = p1;
            _p2 = p2;
        }

        public AreaFilter(Vector3 location) : this(location, 0, 0, 0) { }

        public AreaFilter(Vector3 center, float width, float height, float depth = 0)
        {
            _p1 = new Vector3(center.X - width / 2, center.Y - height / 2, center.Z - depth / 2);
            _p2 = new Vector3(center.X + width / 2, center.Y + height / 2, center.Z + depth / 2);
        }
        
        public bool Is(Wave wave)
        {
            var p = wave.Location;
            return p.X >= _p1.X && p.X <= _p2.X &&
                   p.Y >= _p1.Y && p.Y <= _p2.Y &&
                   p.Z >= _p1.Z && p.Z <= _p2.Z;
        }
    }
}
