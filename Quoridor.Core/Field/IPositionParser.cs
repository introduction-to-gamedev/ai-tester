namespace Quoridor.Core.Field
{
    using IntroToGameDev.AiTester.Utils;
    using Moves;

    public interface IPositionParser
    {
        Position? TryParseCellPosition(string code);
        
        (Position position, WallType wall)? TryParseWallPosition(string code);
    }

    public class PositionParser : IPositionParser
    {
        public Position? TryParseCellPosition(string code)
        {
            return TryParse(code, 'a', 8);
        }

        public (Position, WallType)? TryParseWallPosition(string code)
        {
            var orientation = code.ToLower()[^1];
            
            if (orientation != 'h' && orientation != 'v')
            {
                return null;
            }

            var wallType = orientation == 'h' ? WallType.Horizontal : WallType.Vertical;

            var position = TryParse(code[0..^1], 's', 7);
            if (!position.HasValue)
            {
                return null;
            }
            
            return (position.Value, wallType);
        }
        
        private Position? TryParse(string code, char startSymbol, int limit)
        {
            if (code == null)
            {
                return null;
            }
            
            code = code.ToLower();
            if (code.Length != 2)
            {
                return null;
            }

            var symbol = code[0];
            var number = code[1];

            var column = symbol - startSymbol;
            var row = number - '1';

            if (row >= 0 && column >= 0 && row <= limit && column <= limit)
            {
                return new Position(row, column);
            }

            return null;
        }
    }
}