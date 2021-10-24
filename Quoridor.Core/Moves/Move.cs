namespace Quoridor.Core.Moves
{
    using Field;

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
}