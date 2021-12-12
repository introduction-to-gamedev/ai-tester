namespace Quoridor.Contest
{
    using System;
    using System.Threading.Tasks;
    using AiTester.Contest;
    using AiTester.Contest.Contestants;
    using Core.Field;
    using Core.Game;
    using Core.Moves;
    using IntroToGameDev.AiTester;
    using NLog;

    public class QuoridorDuelRunner : DuelRunner
    {
        private readonly CommandFetcher fetcher = new CommandFetcher();

        private readonly IMoveConverter moveConverter = new MoveConverter(new PositionConverter());

        public override async Task RunDuel(Contestant contestantA, Contestant contestantB)
        {
            var rnd = new Random();
            var whitePlayer = rnd.NextDouble() > .5 ? contestantA : contestantB;
            var blackPlayer = whitePlayer == contestantA ? contestantB : contestantA;

            await SendColorToContestant(whitePlayer, Color.White);
            await SendColorToContestant(blackPlayer, Color.Black);

            var game = new QuoridorGame(new QuoridorField());
            var field = game.Field;

            while (!game.IsOver)
            {
                var activePlayer = game.ActiveColor == Color.Black ? blackPlayer : whitePlayer;
                var passivePlayer = activePlayer == blackPlayer ? whitePlayer : blackPlayer;
                
                var command = fetcher.FetchNextCommand(Logger, activePlayer.Output);
                if (command == null)
                {
                    throw new Exception();
                }
                
                Logger.Log(LogLevel.Info, $"{activePlayer.Id}:\t {command}");
                var move = moveConverter.ParseMove(command, game.ActiveColor);
                var validationResult = move.Validate(field);
                if (validationResult.IsValid)
                {
                    game.ExecuteMove(move);
                    await passivePlayer.Send(command);
                }
            }

            var winner = game.GetWinnerColor() == Color.Black ? blackPlayer : whitePlayer;
            Logger.Log(LogLevel.Info, $"{winner.Id} wins!");
            
            async Task SendColorToContestant(Contestant contestant, Color color)
            {
                await contestant.Send(color.ToString().ToLower());
                Logger.Log(LogLevel.Info, $"{contestant.Id} is chosen to be {color}");
            }
        }
    }
}