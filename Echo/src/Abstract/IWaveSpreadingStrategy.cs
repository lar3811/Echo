using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    //TODO: TWave[] Execute(TWave wave, IWaveBuilder builder)
    public interface IWaveSpreadingStrategy<TWave> where TWave : IWave
    {
        Vector3[] Execute(TWave wave);
    }
}
