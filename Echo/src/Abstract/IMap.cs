using System.Numerics;

namespace Echo.Abstract
{
    public interface IMap<in TWave> where TWave : IWave
    {
        bool Navigate(TWave wave, out Vector3 destination);
    }
}