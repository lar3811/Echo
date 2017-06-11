using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Filters.Support
{
    public class DisjunctiveCompositeFilter : IEchoFilter
    {
        private readonly List<IEchoFilter> _filters = new List<IEchoFilter>();

        public DisjunctiveCompositeFilter(params IEchoFilter[] filters)
        {
            _filters.AddRange(filters);
        }

        public bool Is(Wave echo)
        {
            return _filters.Any(f => f.Is(echo));
        }
    }
}
