namespace Quoridor.Core.Moves
{
    using System.Linq;
    using Field;
    using IntroToGameDev.AiTester.Utils;
    using Pathfinder;

    public class PlaceWallMove : Move
    {
        private readonly Position wallPosition;

        private readonly WallType wallType;
        
        public PlaceWallMove(Color playerColor, Position wallPosition, WallType wallType) : base(playerColor)
        {
            this.wallPosition = wallPosition;
            this.wallType = wallType;
        }

        public override MoveValidationResult Validate(IQuoridorField field)
        {
            if (field.Walls.Any(wall => wall.Position == wallPosition))
            {
                return MoveValidationResult.Invalid("There is already a wall in provided position");
            }

            if (field.Walls.Count(wall => wall.PlayerColor == PlayerColor) == 10)
            {
                return MoveValidationResult.Invalid("You can place only 10 walls in game");
            }

            if (wallType == WallType.Horizontal
                && field.Walls.Any(wall => wall.Type == WallType.Horizontal
                                           && (wall.Position == wallPosition + (0, 1) ||
                                               wall.Position == wallPosition + (0, -1))))
            {
                return MoveValidationResult.Invalid("This position is blocked by horizontal wall nearby");
            }
            
            if (wallType == WallType.Vertical
                && field.Walls.Any(wall => wall.Type == WallType.Vertical
                                           && (wall.Position == wallPosition + (1, 0) ||
                                               wall.Position == wallPosition + (-1, 0))))
            {
                return MoveValidationResult.Invalid("This position is blocked by horizontal wall nearby");
            }

            var wall = new Wall(wallType, wallPosition, PlayerColor);
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
            field.PlaceWall(new Wall(wallType, wallPosition, PlayerColor));
        }
    }
}