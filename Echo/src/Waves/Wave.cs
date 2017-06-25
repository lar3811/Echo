﻿using Echo.Abstract;
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
    public abstract class Wave : IWave
    {
        private readonly Wave[] _progenitors;
        private readonly List<Vector3> _path = new List<Vector3>();

        public readonly int OriginIndex;

        public Vector3 Direction { get; }
        public Vector3 Location => _path[_path.Count - 1];
        public Vector3 Origin => _path[0];

        public IReadOnlyList<Wave> Progenitors => _progenitors;
        public IReadOnlyList<Vector3> PathSegment => _path;

        public Vector3[] FullPath
        {
            get
            {
                var count = _path.Count;
                for (int i = _progenitors.Length - 2; i >= 0; i--)
                {
                    count += _progenitors[i].OriginIndex;
                }
                var output = new Vector3[count];
                for (int i = _progenitors.Length - 2, at = 0; i >= 0; i--)
                {
                    var child = _progenitors[i];
                    var parent = _progenitors[i + 1];
                    parent._path.CopyTo(0, output, at, child.OriginIndex);
                    at += child.OriginIndex;
                }
                _path.CopyTo(output, output.Length - _path.Count);
                return output;
            }
        }

        

        protected Wave(Vector3 start, Vector3 direction)
        {
            Direction = direction;
            OriginIndex = -1;
            _path.Add(start);
            _progenitors = new Wave[1];
            _progenitors[0] = this;
        }

        protected Wave(Wave source, Vector3 direction)
        {
            Direction = direction;
            OriginIndex = source._path.Count - 1;
            _path.Add(source._path[OriginIndex]);
            _progenitors = new Wave[source._progenitors.Length + 1];
            _progenitors[0] = this;
            Array.Copy(source._progenitors, 0, _progenitors, 1, source._progenitors.Length);
        }





        public abstract bool IsFading { get; }

        public abstract bool IsAcceptable { get; }

        public abstract void Update();

        public void Relocate(Vector3 to)
        {
            _path.Add(to);
        }
    }
}
