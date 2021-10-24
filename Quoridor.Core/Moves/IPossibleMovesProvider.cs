namespace Quoridor.Core.Moves
{
    using System.Collections.Generic;
    using Field;
    using IntroToGameDev.AiTester.Utils;

    public interface IPossibleMovesProvider
    {
        IList<Move> GetPossibleMoves(IQuoridorField field, Color color);
    }

    public class PossibleMovesProvider : IPossibleMovesProvider
    {
        private readonly IPossibleJumpMovesProvider jumpMovesProvider = new PossibleJumpMovesProvider();

        public IList<Move> GetPossibleMoves(IQuoridorField field, Color color)
        {
            var result = new List<Move>();

            foreach (var position in jumpMovesProvider.GetPossibleJumpPositions(field, color))
            {
                result.Add(new JumpMove(color, position));
            }

            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    var pos = (x, y);
                    TryAddWallMove(pos, color, WallType.Horizontal);
                    TryAddWallMove(pos, color, WallType.Vertical);
                }
            }

            var pawnCell = field.GetCellWithPawn(color);
            foreach (var cell in pawnCell.GetAccessibleNeighbours())
            {
                var move = new PawnStepMove(color, cell.Position);
                if (move.Validate(field).IsValid)
                {
                    result.Add(move);
                }
            }

            return result;

            void TryAddWallMove(Position position, Color color, WallType type)
            {
                var wallMove = new PlaceWallMove(color, position, type);
                if (wallMove.Validate(field).IsValid)
                {
                    result.Add(wallMove);
                }
            }
        }
    }
}