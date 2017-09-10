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
        /// If <paramref name="wave"/> can proceed method returns <c>true</c> and
        /// new estimated location of the wave is provided via <paramref name="destination"/> parameter.
        /// Otherwise method returns <c>false</c>.
        /// </summary>
        /// <param name="wave">A wave to navigate.</param>
        /// <param name="destination">New location of the <paramref name="wave"/>.</param>
        /// <returns>True if navigation is possible, false otherwise.</returns>
        bool Navigate(TWave wave, out Vector3 destination);
    }
}