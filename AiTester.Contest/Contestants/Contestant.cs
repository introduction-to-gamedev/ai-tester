namespace AiTester.Contest.Contestants
{
    using System.IO;
    using System.Threading.Tasks;

    public class Contestant
    {
        public string Id { get; init; }

        private StreamWriter input;

        public StreamReader Output { get; }

        public Contestant(StreamWriter input, StreamReader output)
        {
            this.input = input;
            Output = output;
        }

        public Task Send(string command)
        {
            return input.WriteLineAsync(command);
        }
    }
}