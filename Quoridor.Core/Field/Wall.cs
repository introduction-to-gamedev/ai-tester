namespace Quoridor.Core.Field
{
    using IntroToGameDev.AiTester.Utils;

    public class Wall
    {
        public WallType Type { get; }
        
        public Position Position { get; }

        public Wall(WallType type, Position position)
        {
            Type = type;
            Position = position;
        }
    }

    public enum WallType
    {
        Horizontal, Vertical
    }
}