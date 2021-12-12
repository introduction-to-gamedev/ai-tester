namespace IntroToGameDev.AiTester.Utils
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using NLog;

    public class ProcessCreator
    {
        private readonly string command;

        private readonly ILogger logger;

        public ProcessCreator(string command)
        {
            this.command = command;
        }

        public Process Create(Action<string> onErrorReceived)
        {
            try
            {
                var args = command.Split(" ");
                var process = Process.Start(args.First(), args.Skip(1));

                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.EnableRaisingEvents = true;

                process.Exited += OnProcessOnExited;
                process.ErrorDataReceived += (_, args) => onErrorReceived(args.Data);
                process.StartInfo.UseShellExecute = false;

                process.Start();
                process.BeginErrorReadLine();
                
                return process;
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Fatal, e);
                return null;
            }
            
            void OnProcessOnExited(object? sender, EventArgs args)
            {
                onErrorReceived("Error: provided program terminates unexpectedly");
            }
        }
    }
}