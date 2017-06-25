﻿using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters
{
    public class GlobalIntersectionsFilter<TWave> : ICondition<TWave> where TWave : IWave
    {
        private readonly HashSet<Vector3> _marked = new HashSet<Vector3>();

        public bool Check(TWave wave)
        {
            return !_marked.Add(wave.Location);
        }
    }
}
