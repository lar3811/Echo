using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Waves
{
    public sealed class ClearWave : Wave, IPropagator<ClearWave>
    {
        public ClearWave(Vector3 start, Vector3 direction)
            : base(start, direction)
        {
        }

        public ClearWave(ClearWave source, Vector3 direction)
            : base(source, direction)
        {
        }
        


        public ICondition<ClearWave> AcceptanceCondition;
        public ICondition<ClearWave> FadeCondition;
        public IPropagationStrategy<ClearWave> PropagationStrategy;



        public override bool IsAcceptable => AcceptanceCondition.Check(this);

        public override bool IsFading => FadeCondition.Check(this);

        public ClearWave[] Propagate() => PropagationStrategy.Execute(this);

        public override void Update() { }
    }
}
