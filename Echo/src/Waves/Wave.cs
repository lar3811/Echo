using Echo.Abstract;
using Echo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Waves
{
    public sealed class Wave : Base<Wave>
    {
        public Wave(Vector3 location, Vector3 direction, 
            ICondition<Wave> acceptanceCondition, 
            ICondition<Wave> fadeCondition, 
            IPropagationStrategy<Wave> propagationStrategy, 
            IUpdateStrategy<Wave> updateStrategy) 
            : base(location, direction, acceptanceCondition, fadeCondition, propagationStrategy, updateStrategy)
        {
        }

        public Wave()
        {

        }
    }
}
