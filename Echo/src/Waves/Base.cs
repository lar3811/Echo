﻿using Echo.Abstract;
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

        

        protected Base(Vector3 location, Vector3 direction,
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
            _path.Add(location);
            _progenitors = new TWave[0];
        }

        protected Base()
        {

        }



        public class Builder : IWaveBuilder<TWave>
        {
            private IWaveBuilder<TWave> _nested;

            public Builder(IWaveBuilder<TWave> nested = null)
            {
                _nested = nested;
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

                if (_nested != null)
                    _nested.Build(wave, progenitor, direction, offset);
            }
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