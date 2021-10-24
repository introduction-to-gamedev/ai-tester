namespace Quoridor.Core.Pathfinder
{
    using System.Collections.Generic;
    using Field;

    public interface INode<T> where T : INode<T>
    {
        IEnumerable<T> GetAccessibleNeighbours();
    }

    public interface IPathFinder<T> where T : INode<T>
    {
        IList<T> FindPath(T start, T goal);
    }
}