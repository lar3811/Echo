using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IWaveQueue
    {
        int Count { get; }

        void Clear();
        void Enqueue(Wave wave);
        Wave Dequeue();
    }
}
