namespace Quoridor.Core.Moves
{
    using System.Collections.Generic;
    using System.Linq;
    using Field;
    using IntroToGameDev.AiTester.Utils;

    public interface IPossibleJumpMovesProvider
    {
        IEnumerable<Position> GetPossibleJumpPositions(IQuoridorField field, Color color);
    }

    public class PossibleJumpMovesProvider : IPossibleJumpMovesProvider
    {
        public IEnumerable<Position> GetPossibleJumpPositions(IQuoridorField field, Color color)
        {
            var pawn = field.GetCellWithPawn(color);
            foreach (var offset in GetOffsets())
            {
                var cellWithOffset = field.GetCell(pawn.Position + offset);
                if (cellWithOffset == null || !cellWithOffset.IsOccupied) 
                {
                    continue;
                }

                if (!pawn.HasWayTo(cellWithOffset))
                {
                    //this means wall
                    continue;
                }

                var jumpCandidate = field.GetCell(cellWithOffset.Position + offset);
                if (jumpCandidate != null && cellWithOffset.HasWayTo(jumpCandidate))
                {
                    yield return jumpCandidate.Position;
                }
                else
                {
                    if (pawn.Position.Column == cellWithOffset.Position.Column)
                    {
                        var candidate = TryGetAccessibleCell(cellWithOffset, (0, 1));
                        if (candidate != null)
                        {
                            yield return candidate.Position;
                        }
                        
                        candidate = TryGetAccessibleCell(cellWithOffset, (0, -1));
                        if (candidate != null)
                        {
                            yield return candidate.Position;
                        }
                        
                    }
                    else
                    {
                        var candidate = TryGetAccessibleCell(cellWithOffset, (1, 0));
                        if (candidate != null)
                        {
                            yield return candidate.Position;
                        }
                        
                        candidate = TryGetAccessibleCell(cellWithOffset, (-1, 0));
                        if (candidate != null)
                        {
                            yield return candidate.Position;
                        }
                    }
                }

                ICell TryGetAccessibleCell(ICell cellA, Position offset)
                {
                    var position = cellA.Position;
                    var cellB = field.GetCell(position + offset);
                    if (cellB == null)
                    {
                        return null;
                    }

                    if (cellA.HasWayTo(cellB))
                    {
                        return cellB;
                    }

                    return null;
                }
            }
        }

        private IEnumerable<Position> GetOffsets()
        {
            yield return (1, 0);
            yield return (-1, 0);
            yield return (0, 1);
            yield return (0, -1);
        }
    }
}