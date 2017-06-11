using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IEchoQueue
    {
        int Count { get; }

        void Clear();
        void Enqueue(Wave echo);
        Wave Dequeue();
    }
}
