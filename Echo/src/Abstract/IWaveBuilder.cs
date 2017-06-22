using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IWaveBuilder<TWave> where TWave : IWave
    {
        TWave Create(Vector3 location, Vector3 direction);
        //TODO: TWave Create(TWave wave, Vector3 location, Vector3 direction); -- OR -- TWave Create(TWave wave, Vector3 offset, Vector3 direction);
        TWave Create(TWave wave, Vector3 direction);
    }
}
