namespace Quoridor.Core.Moves
{
    using System.Linq;
    using Field;
    using IntroToGameDev.AiTester.Utils;

    public class JumpMove : Move
    {
        private Position movePosition;

        private readonly IPossibleJumpMovesProvider jumpMovesProvider = new PossibleJumpMovesProvider();

        public JumpMove(Color playerColor, Position movePosition) : base(playerColor)
        {
            this.movePosition = movePosition;
        }

        public override MoveValidationResult Validate(IQuoridorField field)
        {
            var possibleJumps = jumpMovesProvider.GetPossibleJumpPositions(field, PlayerColor);
            if (!possibleJumps.Contains(movePosition))
            {
                return MoveValidationResult.Invalid("Can not jump to provided position");
            }
            
            return MoveValidationResult.Valid;
        }

        public override void Execute(IQuoridorField field)
        {
            field.MovePawnTo(PlayerColor, movePosition);
        }
    }
}