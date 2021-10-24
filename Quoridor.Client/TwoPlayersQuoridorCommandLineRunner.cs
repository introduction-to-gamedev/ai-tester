namespace Quoridor.Client
{
    using System;
    using System.Text;
    using Core.Field;
    using Core.Game;
    using Core.Moves;
    using IntroToGameDev.AiTester.Utils;

    public class TwoPlayersQuoridorCommandLineRunner
    {
        private readonly QuoridorGame game = new QuoridorGame(new QuoridorField());

        private readonly IMoveParser moveParser = new MoveParser(new PositionParser());

        public void RunSingleGame()
        {
            Console.WriteLine("Hi! Let's play Quoridor:");

            while (!game.IsOver)
            {
                DrawField();
                var move = ReadMove(game.ActiveColor);
                game.ExecuteMove(move);
            }

            DrawField();
            Console.WriteLine($"Player {game.GetWinnerColor()} wins!");
        }

        private Move ReadMove(Color activeColor)
        {
            return moveParser.ParseMove(Console.ReadLine(), activeColor);
        }

        private void DrawField()
        {
            var whitePawn = game.Field.GetCellWithPawn(Color.White);
            var blackPawn = game.Field.GetCellWithPawn(Color.Black);

            var size = 9;
            var field = new string[size * 2];

            var spacesAmount = 5;

            Console.WriteLine($"+  {GetRowWithBlanks(i => (char) ('A' + i), spacesAmount, size)}");
            for (var x = 1; x <= size; x++)
            {
                var row = x - 1;
                field[row * 2] = $"{x}  {GetRowWithBlanks(y => GetCellSymbol(row, y), spacesAmount, size)}";
                field[row * 2 + 1] = new string(' ', (spacesAmount + 1) * size);
            }

            foreach (var wall in game.Field.Walls)
            {
                var startOffset = 3;
                var index = wall.Position.Row * 2 + 1;
                var pos = wall.Position.Column * (spacesAmount + 1) + 3 + startOffset;
                var row = field[index].ToCharArray();

                if (wall.Type == WallType.Horizontal)
                {
                    for (int i = pos - 3; i <= pos + 3; i++)
                    {
                        row[i] = '=';
                    }

                    
                }
                else
                {
                    field[index - 1] = ReplaceCharInString(field[index - 1], pos, '|');
                    field[index + 1] = ReplaceCharInString(field[index + 1], pos, '|');
                    row[pos] = '|';
                }
                
                field[index] = new string(row);
            }

            foreach (var s in field)
            {
                Console.WriteLine(s);
            }

            string ReplaceCharInString(string s, int index, char c)
            {
                var array = s.ToCharArray();
                array[index] = c;
                return new string(array);
            }

            string GetRowWithBlanks(Func<int, char> getSymbol, int spaces, int repeat)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < repeat - 1; i++)
                {
                    sb.Append(getSymbol(i));
                    sb.Append(' ', spaces);
                }

                sb.Append(getSymbol(repeat));
                return sb.ToString();
            }

            char GetCellSymbol(int x, int y)
            {
                var pos = new Position(x, y);
                if (pos == whitePawn.Position)
                {
                    return 'W';
                }

                if (pos == blackPawn.Position)
                {
                    return 'B';
                }

                return '*';
            }
        }
    }
}