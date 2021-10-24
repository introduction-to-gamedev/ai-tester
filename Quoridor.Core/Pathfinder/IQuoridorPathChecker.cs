namespace Quoridor.Core.Pathfinder
{
    using System.Collections.Generic;
    using System.Linq;
    using Field;

    public interface IQuoridorPathChecker
    {
        bool IsWayToTheEndExists(Color color);
        
        bool PathForBothPlayersExist();

        IEnumerable<ICell> GetGoalCells(Color color);
    }

    public class QuoridorPathChecker : IQuoridorPathChecker
    {
        private readonly IQuoridorField field;

        private readonly IPathFinder<ICell> pathFinder = new AStarPathFinder<ICell>((a, b) => 1);

        public QuoridorPathChecker(IQuoridorField field)
        {
            this.field = field;
        }
        
        
        public bool IsWayToTheEndExists(Color color)
        {
            var goalCells = GetGoalCells(color);
            var start = field.GetCellWithPawn(color);
            return goalCells.Any(cell =>
            {
                var findPath = pathFinder.FindPath(start, cell);
                return findPath != null;
            });
        }

        public bool PathForBothPlayersExist()
        {
            return IsWayToTheEndExists(Color.Black) && IsWayToTheEndExists(Color.White);
        }

        public IEnumerable<ICell> GetGoalCells(Color color)
        {
            for (var column = 0; column < 9; column++)
            {
                var row = color == Color.Black ? 8 : 0;
                yield return field.GetCell((row, column));
            }
        }
    }
}