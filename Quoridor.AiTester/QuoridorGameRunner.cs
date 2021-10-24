namespace Quoridor.AiTester
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Core.Field;
    using Core.Game;
    using Core.Moves;
    using IntroToGameDev.AiTester;
    using NLog;

    public class QuoridorGameRunner : GameRunner
    {
        private readonly Random random = new();
        
        private readonly QuoridorGame game = new(new QuoridorField());
        
        private readonly IMoveConverter moveConverter = new MoveConverter(new PositionConverter());
        
        private readonly IPossibleMovesProvider movesProvider = new PossibleMovesProvider();

        private readonly IMoveChoosingStrategy strategy = new SimpleMoveChoosingStrategy();
        
        public override async Task<SingleTestResult> Play(ILogger logger, StreamWriter input, StreamReader output)
        {
            var playersColor = random.NextDouble() > .5? Color.Black : Color.White;
            var myColor = playersColor == Color.Black ? Color.White : Color.Black;
            
            logger.Log(LogLevel.Info, $"Chosen color for player: {playersColor}");
            await input.WriteLineAsync(playersColor.ToString().ToLower());
            var field = game.Field;
            
            while (!game.IsOver)
            {
                Move move;
                
                if (game.ActiveColor == playersColor)
                {
                    var command = FetchNextCommand(logger, output);
                    if (command == null)
                    {
                        return SingleTestResult.FromError("Could not fetch next command");
                    }
                    logger.Log(LogLevel.Info, $"<- {command}");
                    move = moveConverter.ParseMove(command, game.ActiveColor);
                }
                else
                {
                    var moves = movesProvider.GetPossibleMoves(field, myColor);
                    move = strategy.ChooseMove(moves, field, myColor);
                    var code = moveConverter.GetCode(move);
                    
                    logger.Log(LogLevel.Info, $"-> {code}");
                    await input.WriteLineAsync(code);
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

            return new SingleTestResult(game.GetWinnerColor() == myColor ? TestResultType.Loss : TestResultType.Win);
        }
    }
}