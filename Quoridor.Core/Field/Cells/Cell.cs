namespace Quoridor.Core.Field
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IntroToGameDev.AiTester.Utils;

    public class Cell : ICell
    {
        public Position Position { get; }
        public bool IsOccupied => Pawn != null;
        public Pawn Pawn { get; private set; }

        private readonly Func<Position, ICell> cellGetter;

        private readonly List<ICell> blockedNeighbours = new();

        public Cell(Position position, Func<Position, ICell> cellGetter)
        {
            Position = position;
            this.cellGetter = cellGetter;
        }

        public bool HasWayTo(ICell cell)
        {
            return GetAccessibleNeighbours().Contains(cell);
        }

        public void BlockWayTo(ICell cell)
        {
            blockedNeighbours.Add(cell);
        }

        public void UnblockWayTo(ICell cell)
        {
            blockedNeighbours.Remove(cell);   
        } 

        public void Place(Pawn pawn)
        {
            Pawn = pawn;
        }

        public IEnumerable<ICell> GetAccessibleNeighbours()
        {
            return GetAllNeighbours().Where(c => c != null).Where(c => !blockedNeighbours.Contains(c));
        }

        public void ClearPawn()
        {
            Pawn = null;
        }

        private IEnumerable<ICell> GetAllNeighbours()
        {
            yield return cellGetter(Position + (1, 0));
            yield return cellGetter(Position + (0, 1));
            yield return cellGetter(Position + (-1, 0));
            yield return cellGetter(Position + (0, -1));
        }
    }
}