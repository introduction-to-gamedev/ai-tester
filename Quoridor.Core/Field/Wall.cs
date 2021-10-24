namespace Quoridor.Core.Field
{
    using System.Collections.Generic;
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

        public IEnumerable<(Position, Position)> GetBlockedCellPairs()
        {
            if (Type == WallType.Horizontal)
            {
                yield return (Position, Position + (1, 0));
                yield return (Position + (0, 1), Position + (1, 1));
                yield break;
            }
            
            yield return (Position, Position + (0, 1));
            yield return (Position + (1, 0), Position + (1, 1));
        } 
    }

    public enum WallType
    {
        Horizontal, Vertical
    }
}