using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IWaveBuilder<TWave>
    {
        void Build(TWave wave, Vector3 location, Vector3 direction);

        void Build(TWave wave, TWave progenitor, Vector3 direction, Vector3 offset);
    }
}
