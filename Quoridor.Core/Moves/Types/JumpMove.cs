namespace Quoridor.Core.Moves
{
    using System.Linq;
    using Field;
    using IntroToGameDev.AiTester.Utils;

    public class JumpMove : Move
    {
        public Position MovePosition { get;  }
        public Position JumpOverPosition { get;  }

        private readonly IPossibleJumpMovesProvider jumpMovesProvider = new PossibleJumpMovesProvider();

        public JumpMove(Color playerColor, Position movePosition, Position jumpOverPosition) : base(playerColor)
        {
            MovePosition = movePosition;
            JumpOverPosition = jumpOverPosition;
        }
        
        public JumpMove(Color playerColor, Position movePosition) : base(playerColor)
        {
            MovePosition = movePosition;
            JumpOverPosition = new Position(-1, -1);
        }

        public override MoveValidationResult Validate(IQuoridorField field)
        {
            var possibleJumps = jumpMovesProvider.GetPossibleJumpPositions(field, PlayerColor).Select(tuple => tuple.jump);
            if (!possibleJumps.Contains(MovePosition))
            {
                return MoveValidationResult.Invalid("Can not jump to provided position");
            }
            
            return MoveValidationResult.Valid;
        }

        public override void Execute(IQuoridorField field)
        {
            field.MovePawnTo(PlayerColor, MovePosition);
        }
    }
}