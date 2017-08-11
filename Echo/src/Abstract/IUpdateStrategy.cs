using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Abstract
{
    /// <summary>
    /// Interface for updating wave's state.
    /// </summary>
    /// <typeparam name="TWave">Type of waves.</typeparam>
    public interface IUpdateStrategy<in TWave>
    {
        /// <summary>
        /// Updates state of a given <paramref name="wave"/>.
        /// </summary>
        /// <param name="wave">Wave to update.</param>
        void Execute(TWave wave);
    }
}
