namespace IntroToGameDev.AiTester
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NLog;

    public class SingleTestExecutor
    {
        private readonly TaskCompletionSource<SingleTestResult> processExitCts = new();

        private readonly StringBuilder errorsBuilder = new();

        private readonly ILogger logger;

        private readonly IGameRunner gameRunner;

        public SingleTestExecutor(Logger logger, IGameRunner gameRunner)
        {
            this.logger = logger;
            this.gameRunner = gameRunner;
        }

        public async Task<SingleTestResult> Execute(string command)
        {
            Process process;
            try
            {
                string[] args = command.Split(" ");
                process = Process.Start(args.First(), args.Skip(1));
                if (process == null)
                {
                    return SingleTestResult.FromError($"Can not start process, please check argument:\n {command}");
                }

                process.StartInfo.WorkingDirectory = Directory.GetParent(args.First()).FullName;
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

                return await gameRunner.Play(logger, input, output);
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
            }
        }


      
    }
}