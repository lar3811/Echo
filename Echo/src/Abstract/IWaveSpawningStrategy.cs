using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    //TODO: TWave[] Execute(IWaveBuilder builder);
    public interface IWaveSpawningStrategy
    {
        Vector3[] Execute();
    }
}
