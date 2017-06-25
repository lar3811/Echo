using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IProcessingQueue<T>
    {
        int Count { get; }

        void Clear();
        void Enqueue(T wave);
        T Dequeue();
    }
}
