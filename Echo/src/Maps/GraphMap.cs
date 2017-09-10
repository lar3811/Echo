using Echo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Echo.Maps
{
    /// <summary>
    /// Contains basic logic for building and navigating graph strctures made of linked <see cref="GraphMap.Node"/> objects.
    /// </summary>
    public class GraphMap : IMap<IWave>, IDirectionsProvider
    {
        /// <summary>
        /// Represents node in the <see cref="GraphMap"/> structure.
        /// </summary>
        public class Node
        {
            private readonly Dictionary<Vector3, Node> _adjacent = new Dictionary<Vector3, Node>();

            /// <summary>
            /// Direction-Node dictionary of nodes that are immediately linked to this one.
            /// </summary>
            public IReadOnlyDictionary<Vector3, Node> Adjacent => _adjacent;
            /// <summary>
            /// Coordinates of this node.
            /// </summary>
            public readonly Vector3 Position;

            /// <summary>
            /// Adds a one-way link from this node to the given one.
            /// </summary>
            /// <param name="direction">Direction in which linked node is located in relation to this node.</param>
            /// <param name="node">A node to link to.</param>
            public void LinkAdjacent(Vector3 direction, Node node)
            {
                _adjacent.Add(Vector3.Normalize(direction), node);
            }

            /// <summary>
            /// Removes link between this node and the one located in given direction.
            /// </summary>
            /// <param name="direction">Direction to unlink.</param>
            public void UnlinkAdjacent(Vector3 direction)
            {
                _adjacent.Remove(direction);
            }

            /// <summary>
            /// Creates an instance of the class.
            /// </summary>
            /// <param name="position">Coordinates of this node.</param>
            public Node(Vector3 position)
            {
                Position = position;
            }

            /// <summary>
            /// Provides textual representation of this object.
            /// </summary>
            /// <returns>Textual representation of this object.</returns>
            public override string ToString()
            {
                return Position.ToString();
            }
        }

        private readonly Dictionary<Vector3, Node> _nodes = new Dictionary<Vector3, Node>();



        /// <summary>
        /// Creates an instance of this class from 3D boolean table.
        /// </summary>
        /// <param name="accessible">Boolean table: true - clear; false - obstacle.</param>
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

        /// <summary>
        /// Creates an instance of this class from 2D boolean table.
        /// </summary>
        /// <param name="accessible">Boolean table: true - clear; false - obstacle.</param>
        public GraphMap(bool[,] accessible)
            : this(accessible.To3D()) { }

        /// <summary>
        /// Creates an instance of this class from given node trees.
        /// </summary>
        /// <param name="roots">Roots of the node trees.</param>
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
        
        /// <summary>
        /// Looks for a node adjacent to the <paramref name="wave"/> in its direction.
        /// If such node is found its coordinates are returned via <paramref name="destination"/> parameter.
        /// </summary>
        /// <param name="wave">A wave to navigate.</param>
        /// <param name="destination">New location of the <paramref name="wave"/>.</param>
        /// <returns>True if navigation is possible, false otherwise.</returns>
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

        /// <summary>
        /// Provides directions in which a wave can travel from given <paramref name="location"/>.
        /// </summary>
        /// <param name="location">Location to travel from.</param>
        /// <returns>Array of directions.</returns>
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
