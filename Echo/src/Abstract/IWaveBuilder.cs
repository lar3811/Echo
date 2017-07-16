using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IWaveBuilder<in TWave>
    {
        void Build(TWave wave, TWave progenitor, Vector3 direction, Vector3 offset);
    }
}
