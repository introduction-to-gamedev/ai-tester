namespace Quoridor.Core.Field
{
    using System;
    using System.Collections.Generic;
    using IntroToGameDev.AiTester.Utils;

    public class Cell : ICell
    {
        public Position Position { get; }
        public bool IsOccupied => Pawn != null;
        public Pawn Pawn { get; private set; }

        private readonly Func<ICell, IEnumerable<ICell>> neighboursGetter;

        public Cell(Position position, Func<ICell, IEnumerable<ICell>> neighboursGetter)
        {
            Position = position;
            this.neighboursGetter = neighboursGetter;
        }

        public void BlockWayTo(Cell cell)
        {
            
        }

        public void Place(Pawn pawn)
        {
            Pawn = pawn;
        }

        public IEnumerable<ICell> GetAccessibleNeighbours()
        {
            return neighboursGetter(this);
        }

        public void ClearPawn()
        {
            Pawn = null;
        }
    }
}