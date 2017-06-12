using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IWaveSpawningStrategy
    {
        Vector3[] Execute();
    }
}
