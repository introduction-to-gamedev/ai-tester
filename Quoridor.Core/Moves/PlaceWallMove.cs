namespace Quoridor.Core.Moves
{
    using System.Linq;
    using Field;
    using IntroToGameDev.AiTester.Utils;

    public class PlaceWallMove : Move
    {
        private Position wallPosition;

        private WallType wallType;

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
            
            return MoveValidationResult.Valid;
        }

        public override void Execute(IQuoridorField field)
        {
            field.PlaceWall(new Wall(wallType, wallPosition, PlayerColor));
        }
    }
}