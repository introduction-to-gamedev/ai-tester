namespace Quoridor.Core.Field
{
    public class Pawn
    {
        public Color Color { get; }

        public Pawn(Color color)
        {
            Color = color;
        }
    }

    public enum Color
    {
        White, Black
    }
}