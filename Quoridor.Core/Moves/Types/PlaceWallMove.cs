namespace Quoridor.Core.Moves
{
    using System.Linq;
    using Field;
    using IntroToGameDev.AiTester.Utils;
    using Pathfinder;

    public class PlaceWallMove : Move
    {
        public Position WallPosition { get; }

        public WallType WallType { get; }
        
        public PlaceWallMove(Color playerColor, Position wallPosition, WallType wallType) : base(playerColor)
        {
            WallPosition = wallPosition;
            WallType = wallType;
        }

        public override MoveValidationResult Validate(IQuoridorField field)
        {
            if (field.Walls.Any(wall => wall.Position == WallPosition))
            {
                return MoveValidationResult.Invalid("There is already a wall in provided position");
            }

            if (field.Walls.Count(wall => wall.PlayerColor == PlayerColor) == 10)
            {
                return MoveValidationResult.Invalid("You can place only 10 walls in game");
            }

            if (WallType == WallType.Horizontal
                && field.Walls.Any(wall => wall.Type == WallType.Horizontal
                                           && (wall.Position == WallPosition + (0, 1) ||
                                               wall.Position == WallPosition + (0, -1))))
            {
                return MoveValidationResult.Invalid("This position is blocked by horizontal wall nearby");
            }
            
            if (WallType == WallType.Vertical
                && field.Walls.Any(wall => wall.Type == WallType.Vertical
                                           && (wall.Position == WallPosition + (1, 0) ||
                                               wall.Position == WallPosition + (-1, 0))))
            {
                return MoveValidationResult.Invalid("This position is blocked by horizontal wall nearby");
            }

            var wall = new Wall(WallType, WallPosition, PlayerColor);
            field.PlaceWall(wall);
            var checker = new QuoridorPathChecker(field);
            var wayExists = checker.PathForBothPlayersExist();
            field.RemoveWall(wall);

            if (!wayExists)
            {
                return MoveValidationResult.Invalid("Wall can not block path to goal cells");
            }
            
            return MoveValidationResult.Valid;
        }

        public override void Execute(IQuoridorField field)
        {
            field.PlaceWall(new Wall(WallType, WallPosition, PlayerColor));
        }
    }
}