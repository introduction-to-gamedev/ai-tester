namespace Quoridor.Core.Field
{
    using IntroToGameDev.AiTester.Utils;
    using Moves;

    public interface IPositionConverter
    {
        Position? TryParseCellPosition(string code);
        
        (Position position, WallType wall)? TryParseWallPosition(string code);

        string CellPositionToCode(Position position);
        
        string WallPositionToCode(Position position, WallType type);
    }

    public class PositionConverter : IPositionConverter
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

        public string CellPositionToCode(Position position)
        {
            return $"{(char)('A' + position.Column)}{position.Row + 1}";
        }

        public string WallPositionToCode(Position position, WallType type)
        {
            return $"{(char)('S' + position.Column)}{position.Row + 1}{(type == WallType.Horizontal? 'h' : 'v')}";
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