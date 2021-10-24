namespace Quoridor.Core.Field
{
    using System.Linq;

    public interface IQuoridorField
    {
        ICell GetCellWithPawn(Color color);
    }

    public class QuoridorField : IQuoridorField
    {
        private readonly Cell[,] cells = new Cell[9, 9];

        public QuoridorField()
        {
            for (var x = 0; x < cells.GetLength(0); x++)
            {
                for (var y = 0; y < cells.GetLength(0); y++)
                {
                    cells[x, y] = new Cell((x, y));
                }
            }

            cells[0, 4].Place(new Pawn(Color.Black));
            cells[8, 4].Place(new Pawn(Color.White));
        }

        
        public ICell GetCellWithPawn(Color color)
        {
            return cells.Cast<Cell>().First(cell => cell.IsOccupied && cell.Pawn.Color == color);
        }
    }
}