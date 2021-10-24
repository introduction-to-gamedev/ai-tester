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

    public static class ColorExtensions
    {
        public static Color Opposite(this Color color)
        {
            return color == Color.Black ? Color.White : Color.Black;
        }
    }
}