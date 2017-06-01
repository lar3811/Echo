using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters.Support
{
    public class InverseFilter : IEchoFilter
    {
        private IEchoFilter _filter;

        public InverseFilter(IEchoFilter filter)
        {
            _filter = filter;
        }

        public bool Is(Echo echo)
        {
            return !_filter.Is(echo);
        }
    }
}
