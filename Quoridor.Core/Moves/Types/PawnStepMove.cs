namespace Quoridor.Core.Moves
{
    using System.Linq;
    using Field;
    using IntroToGameDev.AiTester.Utils;

    public class PawnStepMove : Move
    {
        public Position MovePosition { get; }

        public PawnStepMove(Color playerColor, Position movePosition) : base(playerColor)
        {
            MovePosition = movePosition;
        }

        public override MoveValidationResult Validate(IQuoridorField field)
        {
            var currentCell = field.GetCellWithPawn(PlayerColor);
            var currentPos = currentCell.Position;
            if (currentPos == MovePosition)
            {
                return MoveValidationResult.Invalid("Can not move pawn to it's current location");
            }

            var cell = field.GetCell(MovePosition);
            if (cell.IsOccupied)
            {
                return MoveValidationResult.Invalid("Can not move pawn to occupied cell");
            }

            var neighbours = currentCell.GetAccessibleNeighbours().ToList();
            if (!neighbours.Contains(cell))
            {
                return MoveValidationResult.Invalid("Can not move to inaccessible cell");
            }
            
            return MoveValidationResult.Valid;
        }

        public override void Execute(IQuoridorField field)
        {
            field.MovePawnTo(PlayerColor, MovePosition);
        }
    }
}