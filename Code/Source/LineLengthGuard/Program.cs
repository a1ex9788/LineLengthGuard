using McMaster.Extensions.CommandLineUtils;
using Project.Properties;
using System;
using System.Threading.Tasks;

namespace LineLengthGuard
{
    internal static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                using CommandLineApplication commandLineApplication = new CommandLineApplication
                {
                    Name = "project",
                };

                commandLineApplication.HelpOption();

                DefineCommand(commandLineApplication);

                await commandLineApplication.ExecuteAsync(args);

                return 0;
            }
#pragma warning disable S2221 // "Exception" should not be caught
#pragma warning disable CA1031 // Do not catch general exception types
            catch
#pragma warning restore CA1031 // Do not catch general exception types
#pragma warning restore S2221 // "Exception" should not be caught
            {
                await Console.Error.WriteLineAsync("An unexpected error was produced.");

                return -1;
            }
        }

        private static void DefineCommand(CommandLineApplication commandLineApplication)
        {
            commandLineApplication.OnExecute(() =>
            {
                Console.WriteLine(Resources.ProjectTemplate);
            });
        }
    }
}