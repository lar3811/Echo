using System.Numerics;

namespace Echo.Abstract
{
    /// <summary>
    /// Interface for waves navigation.
    /// </summary>
    /// <typeparam name="TWave">Type of waves.</typeparam>
    public interface IMap<in TWave> where TWave : IWave
    {
        /// <summary>
        /// If <paramref name="wave"/> can proceed, its new location should be provided through <paramref name="destination"/> parameter.
        /// </summary>
        /// <param name="wave">Wave to navigate.</param>
        /// <param name="destination">New location of the wave.</param>
        /// <returns>True if <paramref name="wave"/> can proceed further, false otherwise.</returns>
        bool Navigate(TWave wave, out Vector3 destination);
    }
}