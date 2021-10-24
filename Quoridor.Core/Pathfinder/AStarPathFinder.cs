namespace Quoridor.Core.Pathfinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AStarPathFinder<T> : IPathFinder<T> where T : INode<T>
    {
        private readonly Func<T, T, int> heuristicsCounter;

        public AStarPathFinder(Func<T, T, int> heuristicsCounter)
        {
            this.heuristicsCounter = heuristicsCounter;
        }

        public IList<T> FindPath(T start, T goal)
        {
            var path = new List<T>();

            var open = new List<NodeMetadata<T>>();
            var closed = new List<NodeMetadata<T>>();

            var current = new NodeMetadata<T>(start);
            open.Add(current);

            while (open.Count != 0 && closed.All(node => !node.Has(goal)))
            {
                current = open[0];
                open.Remove(current);
                closed.Add(current);
                var neighbours = current.Node.GetAccessibleNeighbours();

                foreach (var neighbour in neighbours)
                {
                    if (closed.Any(n => n.Has(neighbour)))
                    {
                        continue;
                    }

                    if (open.Any(n => n.Has(neighbour)))
                    {
                        continue;
                    }

                    var node = new NodeMetadata<T>(neighbour)
                    {
                        Parent = current,
                        Heuristics = heuristicsCounter(neighbour, goal)
                    };
                    open.Add(node);
                    open = open.OrderBy(n => n.Heuristics).ToList();
                }
            }

            if (closed.All(node => !node.Has(goal)))
            {
                return null;
            }

            // if all good, return path
            var temp = closed[closed.IndexOf(current)];
            if (temp == null) return null;
            do
            {
                path.Insert(0, temp.Node);
                temp = temp.Parent;
            } while (temp != null && !temp.Has(start));

            return path;
        }

        private class NodeMetadata<T> where T : INode<T>
        {
            public NodeMetadata<T> Parent { get; set; }

            public int Heuristics { get; set; }

            public T Node { get; }

            public bool Has(T node)
            {
                return Node.Equals(node);
            }

            public NodeMetadata(T node)
            {
                Node = node;
            }
        }
    }
}