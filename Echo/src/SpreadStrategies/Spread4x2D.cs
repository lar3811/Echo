using Echo.Abstract;
using System.Collections.Generic;
using System.Numerics;

namespace Echo.SpreadStrategies
{
    public class Spread4x2D : IEchoSpreadingStrategy
    {
        public Vector3[] Execute(Wave echo)
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
