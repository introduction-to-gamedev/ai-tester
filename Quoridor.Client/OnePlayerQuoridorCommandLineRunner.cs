namespace Quoridor.Client
{
    using System;
    using Core.Field;
    using Core.Game;
    using Core.Moves;

    public class OnePlayerQuoridorCommandLineRunner
    {
        private Color myColor;

        private readonly Random random = new();

        private readonly QuoridorGame game = new(new QuoridorField());

        private readonly IMoveConverter moveConverter = new MoveConverter(new PositionConverter());

        private readonly IPossibleMovesProvider movesProvider = new PossibleMovesProvider();
        
        public void RunSingleGame()
        {
            var field = game.Field;
            myColor = ReadColor();

            while (!game.IsOver)
            {
                Move move;
                
                if (game.ActiveColor == myColor)
                {
                    var moves = movesProvider.GetPossibleMoves(field, myColor);
                    move = moves[random.Next(moves.Count)];
                    Console.WriteLine(moveConverter.GetCode(move));
                }
                else
                {
                    var command = Console.ReadLine();
                    move = moveConverter.ParseMove(command, game.ActiveColor);
                }

                var validationResult = move.Validate(field);
                if (validationResult.IsValid)
                {
                    game.ExecuteMove(move);
                }
                else
                {
                    throw new Exception($"Wrong move provided: {validationResult.Error}");
                }
            }
        }

        private Color ReadColor()
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