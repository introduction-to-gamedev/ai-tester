using System;

namespace Quoridor.Client
{
    using Core.Field;
    using Core.Game;
    using Core.Moves;

    class TwoPlayersQuoridorClient
    {
        static void Main(string[] args)
        {
            new TwoPlayersQuoridorCommandLineRunner().RunSingleGame();
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