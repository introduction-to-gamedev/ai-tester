namespace Quoridor.Core.Moves
{
    using Field;

    public interface IMoveConverter
    {
        Move ParseMove(string input, Color color);

        string GetCode(Move move);
    }

    public class MoveConverter : IMoveConverter
    {
        private readonly IPositionConverter positionConverter;

        public MoveConverter(IPositionConverter positionConverter)
        {
            this.positionConverter = positionConverter;
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
                    var cellPosition = positionConverter.TryParseCellPosition(argument);
                    if (cellPosition.HasValue)
                    {
                        return new PawnStepMove(color, cellPosition.Value);
                    };
                    break;
                
                case "jump":
                    cellPosition = positionConverter.TryParseCellPosition(argument);
                    if (cellPosition.HasValue)
                    {
                        return new JumpMove(color, cellPosition.Value);
                    }
                    break;
                
                case "wall":
                    var wallPosition = positionConverter.TryParseWallPosition(argument);
                    if (wallPosition.HasValue)
                    {
                        var valueTuple = wallPosition.Value;
                        return new PlaceWallMove(color, valueTuple.position, valueTuple.wall);
                    }
                    break;
            }

            return new UnknownMove(color);
        }

        public string GetCode(Move move)
        {
            switch (move)
            {
                case JumpMove jump:
                    return $"jump {positionConverter.CellPositionToCode(jump.MovePosition)}";
                case PlaceWallMove wall:
                    return $"wall {positionConverter.WallPositionToCode(wall.WallPosition, wall.WallType)}";
                case PawnStepMove step:
                    return $"move {positionConverter.CellPositionToCode(step.MovePosition)}";
            }

            return "unknown";
        }
    }
}