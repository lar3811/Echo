using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IWave
    {
        Vector3 Location { get; }
        Vector3 Direction { get; }
        Vector3[] FullPath { get; }

        void Relocate(Vector3 location);
    }
}
