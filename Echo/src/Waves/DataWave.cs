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
    public sealed class DataWave<TData> : Wave, IPropagator<DataWave<TData>> 
        where TData : ICloneable<TData>
    {
        public TData Data { get; set; }

        public DataWave(Vector3 start, Vector3 direction, TData data = default(TData))
            : base(start, direction)
        {
            Data = data;
        }

        public DataWave(DataWave<TData> source, Vector3 direction)
            : base(source, direction)
        {
            Data = source.Data.Clone();
        }



        public ICondition<DataWave<TData>> AcceptanceCondition;
        public ICondition<DataWave<TData>> FadeCondition;
        public IUpdateStrategy<DataWave<TData>> UpdateStrategy;
        public IPropagationStrategy<DataWave<TData>> PropagationStrategy;



        public override bool IsAcceptable => AcceptanceCondition.Check(this);

        public override bool IsFading => FadeCondition.Check(this);

        public override void Update() => UpdateStrategy.Execute(this);

        public DataWave<TData>[] Propagate() => PropagationStrategy.Execute(this);
    }
}
