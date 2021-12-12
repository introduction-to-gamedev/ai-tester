namespace IntroToGameDev.AiTester
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using NLog;

    public class CommandFetcher
    {
        public string? FetchNextCommand(ILogger logger, StreamReader sr)
        {
            var delayTask = Task.Delay(TimeSpan.FromSeconds(1));
            string nextCommand = null;
            while (IsNullOrComment(nextCommand) && !delayTask.IsCompleted)
            {
                nextCommand = sr.ReadLine();
                if (nextCommand != null && nextCommand.StartsWith("//"))
                {
                    logger.Log(LogLevel.Info, nextCommand);
                }
            }

            if (nextCommand != null)
            {
                return nextCommand;
            }

            return null;

            bool IsNullOrComment(string value)
            {
                return value == null || value.StartsWith("//");
            }
        }
    }
}