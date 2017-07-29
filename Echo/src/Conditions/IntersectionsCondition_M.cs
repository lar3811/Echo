using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Conditions
{
    public class IntersectionsCondition_M : ICondition<IWave>
    {
        public bool Check(IWave wave)
        {
            foreach(var location in wave.FullPath)
            {
                if (wave.Location == location) return true;
            }
            return false;
        }
    }
}
