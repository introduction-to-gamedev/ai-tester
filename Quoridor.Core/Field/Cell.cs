namespace Quoridor.Core.Field
{
    using IntroToGameDev.AiTester.Utils;

    public interface ICell
    {
        Position Position { get; }
        
        bool IsOccupied { get; }
        
        Pawn Pawn { get; }
    }
    
    public class Cell : ICell
    {
        public Position Position { get; }
        public bool IsOccupied => Pawn != null;
        public Pawn Pawn { get; private set; }

        public Cell(Position position)
        {
            Position = position;
        }

        public void BlockWayTo(Cell cell)
        {
            
        }

        public void Place(Pawn pawn)
        {
            Pawn = pawn;
        }

        public void ClearPawn()
        {
            Pawn = null;
        }
    }
}