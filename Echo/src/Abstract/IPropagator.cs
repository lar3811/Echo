using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IPropagator<out T>
    {
        T[] Propagate();
    }
}
