namespace Quoridor.Core.Moves
{
    using Field;

    public interface IMoveParser
    {
        Move ParseMove(string input, Color color); 
    }

    public class MoveParser : IMoveParser
    {
        private readonly IPositionParser positionParser;

        public MoveParser(IPositionParser positionParser)
        {
            this.positionParser = positionParser;
        }

        public Move ParseMove(string input, Color color)
        {
            var commands = input.Split(" ");
            if (commands.Length != 2)
            {
                return new UnknownMove(color);
            }

            var argument = commands[1];
            switch (commands[0])
            {
                case "move":
                    var cellPosition = positionParser.TryParseCellPosition(argument);
                    if (cellPosition.HasValue)
                    {
                        return new PawnStepMove(color, cellPosition.Value);
                    };
                    break;
                
                case "jump":
                    cellPosition = positionParser.TryParseCellPosition(argument);
                    if (cellPosition.HasValue)
                    {
                        return new JumpMove(color, cellPosition.Value);
                    }
                    break;
                
                case "wall":
                    var wallPosition = positionParser.TryParseWallPosition(argument);
                    if (wallPosition.HasValue)
                    {
                        var valueTuple = wallPosition.Value;
                        return new PlaceWallMove(color, valueTuple.position, valueTuple.wall);
                    }
                    break;
            }

            return new UnknownMove(color);
        }
    }
}