using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IWaveFilter<TWave> where TWave : IWave
    {
        bool Is(TWave wave);
    }
}
