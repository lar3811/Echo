using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters.Support
{
    public class ConjunctiveCompositeFilter : IEchoFilter
    {
        private readonly List<IEchoFilter> _filters = new List<IEchoFilter>();

        public ConjunctiveCompositeFilter(params IEchoFilter[] filters)
        {
            _filters.AddRange(filters);
        }

        public bool Is(Echo echo)
        {
            return _filters.All(f => f.Is(echo));
        }
    }
}
