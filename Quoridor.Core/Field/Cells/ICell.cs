namespace Quoridor.Core.Field
{
    using System.Collections.Generic;
    using IntroToGameDev.AiTester.Utils;

    public interface ICell
    {
        Position Position { get; }
        
        bool IsOccupied { get; }
        
        Pawn Pawn { get; }

        void ClearPawn();

        void Place(Pawn pawn);

        IEnumerable<ICell> GetAccessibleNeighbours();

        bool HasWayTo(ICell cell);
        
        void BlockWayTo(ICell cell);
        
        void UnblockWayTo(ICell cell);
    }
}