namespace Quoridor.Core.Field
{
    using IntroToGameDev.AiTester.Utils;

    public class Wall
    {
        public WallType Type { get; }
        
        public Position Position { get; }
        
        public Color PlayerColor { get; }

        public Wall(WallType type, Position position, Color playerColor)
        {
            Type = type;
            Position = position;
            PlayerColor = playerColor;
        }
    }

    public enum WallType
    {
        Horizontal, Vertical
    }
}