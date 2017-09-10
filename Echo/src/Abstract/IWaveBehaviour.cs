using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Defines properties necessary for <see cref="Tracer{TWave}"/> class to manage waves.
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
        /// Defines how the wave is propagated (multiplied).
        /// </summary>
        IPropagationStrategy<TWave> Propagation { get; }
        /// <summary>
        /// Defines how to update state of the wave.
        /// </summary>
        IUpdateStrategy<TWave> Update { get; }
    }
}
