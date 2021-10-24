namespace Quoridor.AiTester
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Field;
    using Core.Moves;
    using Core.Pathfinder;
    using IntroToGameDev.AiTester.Utils;

    interface IMoveChoosingStrategy
    {
        Move ChooseMove(IList<Move> possibleMoves, IQuoridorField field, Color myColor);
    }

    class SimpleMoveChoosingStrategy : IMoveChoosingStrategy
    {
        private readonly IPathFinder<ICell> pathFinder = new AStarPathFinder<ICell>((a, b) => 1);

        private Random random = new Random();

        public Move ChooseMove(IList<Move> possibleMoves, IQuoridorField field, Color myColor)
        {
            var pathChecker = new QuoridorPathChecker(field);
            var myCell = field.GetCellWithPawn(myColor);
            var opponentsCell = field.GetCellWithPawn(myColor.Opposite());

            if (random.NextDouble() > .7)
            {
                var wall = TryPlaceWall();
                if (wall != null)
                {
                    return TryPlaceWall();
                }    
            }

            return MoveToGoal();

            Move TryPlaceWall()
            {
                var routes = GetRoutesByLength(myColor.Opposite())
                    .Where(path => path != null)
                    .Where(path => path.Count >= 2)
                    .OrderBy(route => route.Count)
                    .ToList();

                if (!routes.Any())
                {
                    return null;
                }

                var shortest = routes.First();
                var first = shortest[0];
                var second = shortest[1];

                if (first.Position.Row == second.Position.Row)
                {
                    var wallPosition = new Position(first.Position.Row,
                        Math.Min(first.Position.Column, second.Position.Column));
                    var move = TryGetMove(wallPosition, WallType.Vertical);
                    if (move != null)
                    {
                        return move;
                    }

                    move = TryGetMove(wallPosition + (-1, 0), WallType.Vertical);
                    if (move != null)
                    {
                        return move;
                    }
                }
                else
                {
                    var wallPosition = new Position(Math.Min(first.Position.Row, second.Position.Row) - 1,
                        first.Position.Column);
                    var move = TryGetMove(wallPosition, WallType.Horizontal);
                    if (move != null)
                    {
                        return move;
                    }

                    move = TryGetMove(wallPosition + (0, -1), WallType.Vertical);
                    if (move != null)
                    {
                        return move;
                    }
                }

                return null;

                Move TryGetMove(Position pos, WallType wallType)
                {
                    return possibleMoves.OfType<PlaceWallMove>().FirstOrDefault(wallMove =>
                        wallMove.WallPosition == pos && wallMove.WallType == wallType);
                }
            }

            Move MoveToGoal()
            {
                var routes = GetRoutesByLength(myColor).Where(path => path != null).OrderBy(route => route.Count)
                    .ToList();

                var goalCell = routes.First()[0];
                var move = possibleMoves.OfType<PawnStepMove>()
                    .FirstOrDefault(move => move.MovePosition == goalCell.Position);
                if (move != null)
                {
                    return move;
                }

                return possibleMoves.OfType<JumpMove>()
                    .First(jumpMove => jumpMove.JumpOverPosition == goalCell.Position);
            }

            IEnumerable<IList<ICell>> GetRoutesByLength(Color color)
            {
                foreach (var goalCell in pathChecker.GetGoalCells(color))
                {
                    yield return pathFinder.FindPath(myCell, goalCell);
                }
            }
        }
    }
}