using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.SpreadStrategies
{
    public class Spread4x2D : IEchoSpreadingStrategy
    {
        public IEnumerable<Vector3> Execute(Echo echo)
        {
            return new[]
            {
                new Vector3(-echo.Direction.Y, echo.Direction.X, 0),
                new Vector3(echo.Direction.Y, -echo.Direction.X, 0),
                new Vector3(-echo.Direction.Y, echo.Direction.X, 0) + echo.Direction,
                new Vector3(echo.Direction.Y, -echo.Direction.X, 0) + echo.Direction
            };
        }
    }
}
