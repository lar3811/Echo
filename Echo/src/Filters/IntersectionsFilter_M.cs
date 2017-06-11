using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class IntersectionsFilter_M : IEchoFilter
    {
        public bool Is(Wave echo)
        {
            foreach(var location in echo.FullPath)
            {
                if (echo.Location == location) return true;
            }
            return false;
        }
    }
}
