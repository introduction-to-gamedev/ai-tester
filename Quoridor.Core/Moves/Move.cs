namespace Quoridor.Core.Moves
{
    using Field;
    using IntroToGameDev.AiTester.Utils;

    public abstract class Move
    {
        protected Color PlayerColor;

        protected Move(Color playerColor)
        {
            PlayerColor = playerColor;
        }
    }

    public class UnknownMove : Move
    {
        public UnknownMove(Color playerColor) : base(playerColor)
        {
        }
    }

    public class JumpMove : Move
    {
        private Position movePosition;
        
        public JumpMove(Color playerColor, Position movePosition) : base(playerColor)
        {
            this.movePosition = movePosition;
        }
    }
    
    public class PlaceWallMove : Move
    {
        private Position wallPosition;

        private WallType wallType;
        
        public PlaceWallMove(Color playerColor, Position wallPosition, WallType wallType) : base(playerColor)
        {
            this.wallPosition = wallPosition;
            this.wallType = wallType;
        }
    }
    
    public class PawnStepMove : Move
    {
        private Position movePosition;

        public PawnStepMove(Color playerColor, Position movePosition) : base(playerColor)
        {
            this.movePosition = movePosition;
        }
    }
}