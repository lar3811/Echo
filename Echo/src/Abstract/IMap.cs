using System.Numerics;

namespace Echo.Abstract
{
    public interface IMap
    {
        bool CanPass(Vector3 from, Vector3 to);
        Vector3 Move(Vector3 from, Vector3 direction);
    }
}