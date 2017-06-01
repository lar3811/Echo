using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class StuckFilter : IEchoFilter
    {
        public bool Is(Echo echo)
        {
            if (echo.Path.Count < 2) return false;

            var p1 = echo.Path[echo.Path.Count - 1];
            var p2 = echo.Path[echo.Path.Count - 2];

            return p1 == p2;
        }
    }
}
