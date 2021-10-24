namespace Quoridor.Core.Moves
{
    using System;
    using Field;

    public class UnknownMove : Move
    {
        public UnknownMove(Color playerColor) : base(playerColor)
        {
        }

        public override MoveValidationResult Validate(IQuoridorField field)
        {
            return MoveValidationResult.Invalid("Unknown command");
        }

        public override void Execute(IQuoridorField field)
        {
            throw new Exception("Can not execute unknown move");
        }
    }
}