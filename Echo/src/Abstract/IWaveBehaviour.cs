using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Defines properties necessary for the wave management.
    /// </summary>
    /// <typeparam name="TWave">Type of waves.</typeparam>
    public interface IWaveBehaviour<TWave>
    {
        /// <summary>
        /// Indicates if the wave can be returned from search routine.
        /// </summary>
        ICondition<TWave> AcceptanceCondition { get; }
        /// <summary>
        /// Indicates if the wave should be removed from processing queue.
        /// </summary>
        ICondition<TWave> FadeCondition { get; }
        /// <summary>
        /// Defines how the wave is propagated across the map.
        /// </summary>
        IPropagationStrategy<TWave> Propagation { get; }
        /// <summary>
        /// Defines how to update the wave's state.
        /// </summary>
        IUpdateStrategy<TWave> Update { get; }
    }
}
