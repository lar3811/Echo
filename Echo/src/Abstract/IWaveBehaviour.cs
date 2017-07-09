using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    public interface IWaveBehaviour<TWave>
    {
        ICondition<TWave> AcceptanceCondition { get; }
        ICondition<TWave> FadeCondition { get; }
        IPropagationStrategy<TWave> Propagation { get; }
        IUpdateStrategy<TWave> Update { get; }
    }
}
