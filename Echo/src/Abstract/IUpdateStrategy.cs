using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IUpdateStrategy<in T>
    {
        void Execute(T subject);
    }
}
