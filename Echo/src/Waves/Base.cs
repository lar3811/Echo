using Echo.Abstract;
using Echo.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Waves
{
    public abstract class Base<TWave> : IWave, IWaveBehaviour<TWave> 
        where TWave : Base<TWave>
    {
        private TWave[] _progenitors;
        private List<Vector3> _path = new List<Vector3>();

        public int OriginIndex { get; private set; }
        public Vector3 Direction { get; private set; }
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



        public class Builder : IWaveBuilder<TWave>
        {
            public ICondition<TWave> AcceptanceCondition;
            public ICondition<TWave> FadeCondition;
            public IPropagationStrategy<TWave> PropagationStrategy;
            public IUpdateStrategy<TWave> UpdateStrategy;

            public void Build(TWave wave, Vector3 location, Vector3 direction)
            {
                wave.AcceptanceCondition = AcceptanceCondition;
                wave.FadeCondition = FadeCondition;
                wave.Propagation = PropagationStrategy;
                wave.Update = UpdateStrategy;

                wave.Direction = direction;
                wave.OriginIndex = -1;
                wave._path.Add(location);
                wave._progenitors = new TWave[0];

                if (wave.AcceptanceCondition == null)
                {
                    Debug.WriteLine($"ECHO: Initial acceptance condition is not set for [{wave}]. Enumeration may not return any value.");
                }
                if (wave.FadeCondition == null)
                {
                    Debug.WriteLine($"ECHO: Initial fade condition is not set for [{wave}]. Infinite loops within processing queue may occur.");
                }
                if (wave.Propagation == null)
                {
                    Debug.WriteLine($"ECHO: Initial propagation strategy is not set for [{wave}]. It will not spawn any additional waves.");
                }
                if (wave.Update == null)
                {
                    Debug.WriteLine($"ECHO: Initial update condition is not set for [{wave}].");
                }
            }

            public void Build(TWave wave, TWave progenitor, Vector3 direction, Vector3 offset)
            {
                wave.AcceptanceCondition = progenitor.AcceptanceCondition;
                wave.FadeCondition = progenitor.FadeCondition;
                wave.Propagation = progenitor.Propagation;
                wave.Update = progenitor.Update;

                wave.Direction = direction;
                wave.OriginIndex = progenitor._path.Count - 1;
                wave._path.Add(progenitor._path[wave.OriginIndex] + offset);
                wave._progenitors = new TWave[progenitor._progenitors.Length + 1];
                wave._progenitors[0] = progenitor;
                Array.Copy(progenitor._progenitors, 0, wave._progenitors, 1, progenitor._progenitors.Length);
            }
        }

        

        public ICondition<TWave> AcceptanceCondition { get; set; }

        public ICondition<TWave> FadeCondition { get; set; }

        public IPropagationStrategy<TWave> Propagation { get; set; }

        public IUpdateStrategy<TWave> Update { get; set; }



        public void Relocate(Vector3 to)
        {
            _path.Add(to);
        }



        public override string ToString()
        {
            return $"{GetType().Name} {Direction} {Location}";
        }
    }
}
