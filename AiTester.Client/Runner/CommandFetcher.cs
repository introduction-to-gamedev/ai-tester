namespace IntroToGameDev.AiTester
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using NLog;

    public class CommandFetcher
    {
        public string? FetchNextCommand(ILogger logger, StreamReader sr)
        {
            string nextCommand = null;
            while (IsComment(nextCommand))
            {
                nextCommand = new TimedOutReader(sr).ReadLine(5000);
                if (nextCommand != null && nextCommand.StartsWith("//"))
                {
                    logger.Log(LogLevel.Info, nextCommand);
                }

                if (nextCommand == null)
                {
                    return null;
                }
            }

            if (nextCommand != null)
            {
                return nextCommand;
            }

            return null;

            bool IsComment(string value)
            {
                return value == null || value.StartsWith("//");
            }
        }
        
        class TimedOutReader
        {
            private Thread inputThread;
            private AutoResetEvent getInput;
            private AutoResetEvent gotInput;
            private string input;

            private StreamReader streamReader;

            public TimedOutReader(StreamReader streamReader)
            {
                this.streamReader = streamReader;
            }

            public void Prepare()
            {
                getInput = new AutoResetEvent(false);
                gotInput = new AutoResetEvent(false);
                inputThread = new Thread(Read)
                {
                    IsBackground = true
                };
                inputThread.Start();
            }

            private void Read()
            {
                while (true)
                {
                    getInput.WaitOne();
                    input = streamReader.ReadLine();
                    gotInput.Set();
                }
            }

            // omit the parameter to read a line without a timeout
            public string ReadLine(int timeOutMillisecs = Timeout.Infinite)
            {
                Prepare();
                
                getInput.Set();
                var success = gotInput.WaitOne(timeOutMillisecs);
                if (success)
                {
                    return input;
                }

                return null;
            }
        }
    }

    
}