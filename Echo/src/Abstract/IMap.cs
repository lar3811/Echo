using System.Numerics;

namespace Echo.Abstract
{
    public interface IMap
    {
        Vector3? Move(Vector3 from, Vector3 direction);
    }
}