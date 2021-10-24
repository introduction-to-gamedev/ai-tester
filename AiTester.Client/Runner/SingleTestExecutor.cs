namespace IntroToGameDev.AiTester
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using NLog;

    public class SingleTestExecutor
    {
        private readonly TaskCompletionSource<SingleTestResult> processExitCts = new();

        private readonly StringBuilder errorsBuilder = new StringBuilder();

        private readonly ILogger logger;

        public SingleTestExecutor(Logger logger)
        {
            this.logger = logger;
        }

        public async Task<SingleTestResult> Execute(string command)
        {
            Process process;
            try
            {
                process = Process.Start(command);
                if (process == null)
                {
                    return SingleTestResult.FromError($"Can not start process, please check argument:\n {command}");
                }

                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.EnableRaisingEvents = true;

                process.Exited += OnProcessOnExited;
                process.ErrorDataReceived += (sender, args) => errorsBuilder.Append(args.Data);
                process.StartInfo.UseShellExecute = false;

                process.Start();
                process.BeginErrorReadLine();
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Fatal, e);
                return SingleTestResult.FromError($"Error during starting process, please check argument:\n {command}");
            }

            var playTask = Play(process);

            process.WaitForExit();

            var testResult = playTask.Result;
            if (!testResult.IsCompletedSuccessfully)
            {
                logger.Log(LogLevel.Error, testResult.Error);
                return testResult;
            }

            if (errorsBuilder.Length > 0)
            {
                var errorMessage = errorsBuilder.ToString();
                logger.Log(LogLevel.Error, errorMessage);
                return SingleTestResult.FromError(errorMessage);
            }

            return testResult;
        }

        private void OnProcessOnExited(object? sender, EventArgs args)
        {
            errorsBuilder.Append("Error: provided program terminates unexpectedly");
        }

        private async Task<SingleTestResult> Play(Process process)
        {
            try
            {
                var output = process.StandardOutput;
                var input = process.StandardInput;
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, $"Internal error occured: {e.StackTrace}");
                return new SingleTestResult(TestResultType.InternalError, e.Message);
            }
            finally
            {
                process.Exited -= OnProcessOnExited;
                logger.Log(LogLevel.Info, "Killing client process...");
                process.Kill();
                logger.Log(LogLevel.Info, $"Child process killed: {process.HasExited}");
            }
        }


        private ValueOrError<string> FetchNextCommand(StreamReader sr)
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
                return ValueOrError.FromValue(nextCommand);
            }

            return ValueOrError.FromError<string>("Error: timeout of reading next command reached");

            bool IsNullOrComment(string value)
            {
                return value == null || value.StartsWith("//");
            }
        }
    }
}