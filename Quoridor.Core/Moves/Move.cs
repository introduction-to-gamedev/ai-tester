namespace Quoridor.Core.Moves
{
    using Field;
    using IntroToGameDev.AiTester.Utils;

    public abstract class Move
    {
        public Color PlayerColor { get; }

        protected Move(Color playerColor)
        {
            PlayerColor = playerColor;
        }

        public abstract MoveValidationResult Validate(IQuoridorField field);

        public abstract void Execute(IQuoridorField field);
    }

    public class UnknownMove : Move
    {
        public UnknownMove(Color playerColor) : base(playerColor)
        {
        }

        public override MoveValidationResult Validate(IQuoridorField field)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(IQuoridorField field)
        {
            throw new System.NotImplementedException();
        }
    }

    public class JumpMove : Move
    {
        private Position movePosition;

        public JumpMove(Color playerColor, Position movePosition) : base(playerColor)
        {
            this.movePosition = movePosition;
        }

        public override MoveValidationResult Validate(IQuoridorField field)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(IQuoridorField field)
        {
            throw new System.NotImplementedException();
        }
    }
}