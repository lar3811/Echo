using Echo.Abstract;
using Echo.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Waves
{
    public abstract class Base<TWave> : IWave, IWaveBehaviour<TWave> 
        where TWave : Base<TWave>
    {
        private readonly TWave[] _progenitors;
        private readonly List<Vector3> _path = new List<Vector3>();

        public readonly int OriginIndex;

        public Vector3 Direction { get; }
        public Vector3 Location => _path[_path.Count - 1];
        public Vector3 Origin => _path[0];

        public IReadOnlyList<TWave> Progenitors => _progenitors;
        public IReadOnlyList<Vector3> PathSegment => _path;
        
        public Vector3[] FullPath
        {
            get
            {
                var count = _path.Count + OriginIndex + 1;
                for (int i = 0; i < _progenitors.Length; i++)
                {
                    count += _progenitors[i].OriginIndex;
                }
                var output = new Vector3[count];

                _path.CopyTo(output, output.Length - _path.Count);
                for (int i = 0, at = output.Length - _path.Count, origin = OriginIndex; i < _progenitors.Length; i++)
                {
                    var progenitor = _progenitors[i];
                    at -= origin;
                    progenitor._path.CopyTo(0, output, at, origin);
                    origin = progenitor.OriginIndex;
                }
                
                return output;
            }
        }

        

        protected Base(Vector3 start, Vector3 direction,
            ICondition<TWave> acceptanceCondition,
            ICondition<TWave> fadeCondition,
            IPropagationStrategy<TWave> propagationStrategy,
            IUpdateStrategy<TWave> updateStrategy)
        {
            AcceptanceCondition = acceptanceCondition;
            FadeCondition = fadeCondition;
            Propagation = propagationStrategy;
            Update = updateStrategy;

            Direction = direction;
            OriginIndex = -1;
            _path.Add(start);
            _progenitors = new TWave[0];
        }

        protected Base(TWave source, Vector3 direction)
        {
            AcceptanceCondition = source.AcceptanceCondition;
            FadeCondition = source.FadeCondition;
            Propagation = source.Propagation;
            Update = source.Update;

            Direction = direction;
            OriginIndex = source._path.Count - 1;
            _path.Add(source._path[OriginIndex]);
            _progenitors = new TWave[source._progenitors.Length + 1];
            _progenitors[0] = source;
            Array.Copy(source._progenitors, 0, _progenitors, 1, source._progenitors.Length);
        }


        
        public ICondition<TWave> AcceptanceCondition
        {
            get; protected set;
        }

        public ICondition<TWave> FadeCondition
        {
            get; protected set;
        }

        public IPropagationStrategy<TWave> Propagation
        {
            get; protected set;
        }

        public IUpdateStrategy<TWave> Update
        {
            get; protected set;
        }



        public void Relocate(Vector3 to)
        {
            _path.Add(to);
        }
    }
}
