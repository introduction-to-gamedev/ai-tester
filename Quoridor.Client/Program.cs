using System;

namespace Quoridor.Client
{
    using Core.Field;
    using Core.Game;

    class Program
    {
        static void Main(string[] args)
        {
            
            var myColor = ReadColor();
            var game = new QuoridorGame(new QuoridorField());

            if (myColor == Color.White)
            {
                MakeMove(game, myColor);
            }
        }

        private static void MakeMove(QuoridorGame game, Color myColor)
        {
            
        }
        
        private static Color ReadColor()
        {
            var line = Console.ReadLine();
            if (line == "white")
            {
                return Color.White;
            }

            if (line == "black")
            {
                return Color.Black;
            }

            throw new ArgumentException($"{line} is not a valid color");
        }
    }
}