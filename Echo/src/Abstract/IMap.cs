using System.Numerics;

namespace Echo.Abstract
{
    public interface IMap
    {
        bool Navigate(Wave wave, out Vector3 destination);
    }
}