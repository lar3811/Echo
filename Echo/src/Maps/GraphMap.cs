using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.Maps
{
    public class GraphMap : IMap<IWave>, IDirectionsProvider
    {
        public class Node
        {
            private readonly Dictionary<Vector3, Node> _adjacent = new Dictionary<Vector3, Node>();
            public IReadOnlyDictionary<Vector3, Node> Adjacent => _adjacent;

            public readonly Vector3 Position;

            public void LinkAdjacent(Vector3 direction, Node node)
            {
                _adjacent.Add(Vector3.Normalize(direction), node);
            }

            public void UnlinkAdjacent(Vector3 direction)
            {
                _adjacent.Remove(direction);
            }

            public Node(Vector3 position)
            {
                Position = position;
            }

            public override string ToString()
            {
                return Position.ToString();
            }
        }

        private readonly Dictionary<Vector3, Node> _nodes = new Dictionary<Vector3, Node>();



        public GraphMap(bool[,,] accessible)
        {
            for (int x = 0; x < accessible.GetLength(0); x++)
            {
                for (int y = 0; y < accessible.GetLength(1); y++)
                {
                    for (int z = 0; z < accessible.GetLength(2); z++)
                    {
                        if (!accessible[x, y, z]) continue;
                        var node = new Node(new Vector3(x, y, z));

                        for (int sx = -1; sx <= 1; sx++)
                        {
                            for (int sy = -1; sy <= 1; sy++)
                            {
                                for (int sz = -1; sz <= 1; sz++)
                                {
                                    Node neighbour;
                                    var offset = new Vector3(x + sx, y + sy, z + sz);
                                    if (!_nodes.TryGetValue(offset, out neighbour)) continue;
                                    neighbour.LinkAdjacent(node.Position - neighbour.Position, node);
                                    node.LinkAdjacent(neighbour.Position - node.Position, neighbour);
                                }
                            }
                        }
                        _nodes.Add(node.Position, node);
                    }
                }
            }
        }
        public GraphMap(bool[,] accessible)
            : this(accessible.To3D()) { }

        public GraphMap(params Node[] roots)
        {
            var stack = new Stack<Node>();
            foreach (var root in roots)
            {
                stack.Push(root);
                while (stack.Count > 0)
                {
                    var node = stack.Pop();
                    if (_nodes.ContainsKey(node.Position)) continue;
                    _nodes.Add(node.Position, node);
                    foreach (var adjacent in node.Adjacent.Values)
                    {
                        stack.Push(adjacent);
                    }
                }
            }
        }


        
        public bool Navigate(IWave wave, out Vector3 destination)
        {
            destination = wave.Location;

            Node from;
            if (!_nodes.TryGetValue(wave.Location, out from)) return false;

            Node to;
            if (!from.Adjacent.TryGetValue(wave.Direction, out to)) return false;

            destination = to.Position;
            return true;
        }

        public Vector3[] GetDirections(Vector3 location)
        {
            Node node;
            if (_nodes.TryGetValue(location, out node))
                return node.Adjacent.Keys.ToArray();
            else
                return null;
        }
    }
}
