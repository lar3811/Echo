using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface ICondition<in T>
    {
        bool Check(T subject);
    }
}
