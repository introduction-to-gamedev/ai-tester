namespace Quoridor.Core.Field
{
    using System.Collections.Generic;
    using System.Linq;
    using IntroToGameDev.AiTester.Utils;

    public interface IQuoridorField
    {
        ICell GetCellWithPawn(Color color);

        ICell GetCell(Position position);

        void MovePawnTo(Color pawnColor, Position position);

        void PlaceWall(Wall wall);
        
        void RemoveWall(Wall wall);
        
        IReadOnlyList<Wall> Walls { get; }
    }

    public class QuoridorField : IQuoridorField
    {
        private readonly Cell[,] cells = new Cell[9, 9];


        public IReadOnlyList<Wall> Walls => walls;

        private List<Wall> walls = new();

        public QuoridorField()
        {
            for (var x = 0; x < cells.GetLength(0); x++)
            {
                for (var y = 0; y < cells.GetLength(0); y++)
                {
                    cells[x, y] = new Cell((x, y), GetCell);
                }
            }

            cells[0, 4].Place(new Pawn(Color.Black));
            cells[8, 4].Place(new Pawn(Color.White));
        }

       

        public ICell GetCellWithPawn(Color color)
        {
            return cells.Cast<Cell>().First(cell => cell.IsOccupied && cell.Pawn.Color == color);
        }

        public ICell GetCell(Position position)
        {
            if (position.Row < 0 || position.Row >= cells.GetLength(0) || position.Column < 0 ||
                position.Column >= cells.GetLength(1))
            {
                return null;
            }

            return cells[position.Row, position.Column];
        }

        public void MovePawnTo(Color pawnColor, Position position)
        {
            var cell = GetCell(position);
            var currentCell = GetCellWithPawn(pawnColor);
            var pawn = currentCell.Pawn;
            
            currentCell.ClearPawn();
            cell.Place(pawn);
        }

        public void PlaceWall(Wall wall)
        {
            walls.Add(wall);
            foreach (var (a, b) in wall.GetBlockedCellPairs())
            {
                BlockMoveBetween(GetCell(a), GetCell(b));
            }

            void BlockMoveBetween(ICell a, ICell b)
            {
                a.BlockWayTo(b);
                b.BlockWayTo(a);
            }
        }
        
        public void RemoveWall(Wall wall)
        {
            walls.Remove(wall);
            foreach (var (a, b) in wall.GetBlockedCellPairs())
            {
                UnblockMoveBetween(GetCell(a), GetCell(b));
            }

            void UnblockMoveBetween(ICell a, ICell b)
            {
                a.UnblockWayTo(b);
                b.UnblockWayTo(a);
            }
        }

    }
}