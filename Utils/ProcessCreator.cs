namespace IntroToGameDev.AiTester.Utils
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using NLog;

    public class ProcessCreator
    {
        private readonly string folder;
        private readonly string command;

        private readonly ILogger logger;

        public ProcessCreator(string folder, string command)
        {
            this.folder = folder;
            this.command = command;
        }

        public Process Create(Action<string> onErrorReceived)
        {
            try
            {
                var fullCommand = Path.Combine(folder, command);
                var args = fullCommand.Split(" ");
                var process = Process.Start(args.First(), args.Skip(1));
                Process.Start(new ProcessStartInfo(args.First()));

                Environment.SetEnvironmentVariable("TIME_PER_MOVE", "4500");
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = Directory.GetParent(args.First()).FullName;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.EnableRaisingEvents = true;
                process.StartInfo.UseShellExecute = false;

                process.Exited += OnProcessOnExited;
                process.ErrorDataReceived += (_, args) => onErrorReceived(args.Data);
                process.StartInfo.UseShellExecute = false;

                process.Start();
                process.BeginErrorReadLine();
                
                return process;
            }
            catch (Exception e)
            {
                //logger.Log(LogLevel.Fatal, e);
                return null;
            }
            
            void OnProcessOnExited(object? sender, EventArgs args)
            {
                onErrorReceived("Error: provided program terminates unexpectedly");
            }
        }
    }
}