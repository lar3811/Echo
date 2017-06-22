﻿using System.Numerics;

namespace Echo.Abstract
{
    public interface IMap<TWave> where TWave : IWave
    {
        bool Navigate(TWave wave, out Vector3 destination);
    }
}