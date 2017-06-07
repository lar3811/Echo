using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo
{
    public sealed class Echo
    {
        public int Age { get; internal set; }

        public readonly List<Vector3> Path;
        public readonly Vector3 Direction;
        public readonly int Generation;

        public Echo(Vector3 start, Vector3 direction) 
            : this(new List<Vector3> { start }, direction, 0, 0) { }

        public Echo(Echo source, Vector3 direction) 
            : this(source.Path.ToList(), direction, source.Generation + 1, 0) { }

        public Echo(List<Vector3> path, Vector3 direction, int generation, int age)
        {
            Path = path.ToList();
            Direction = direction;
            Generation = generation;
            Age = age;
        }
    }
}
