using System.IO;
using CommandLine;
using System.Collections.Generic;

namespace Everlasting.Core
{
    public static class ArgumentParser
    {
        public static Command Parse(this string[] args)
        {
            if (args == null || args.Length == 0)
                return null;

            var command = new Command();
            bool isValid = true;
            try
            {
                isValid = Parser.Default.ParseArgumentsStrict(args, command);
            }
            catch
            {
                return null;
            }

            if (!isValid)
                return null;

            if (!validTypes.Contains(command.Type))
                throw new UnknownCommandTypeException($"Unknown command type: {command.Type}.");

            var isExcluded = false;
            if (excludedExeTokens.Contains(command.ExecutablePath))
                isExcluded = true;

            if (!isExcluded && !File.Exists(command.ExecutablePath))
                throw new InvalidExecutablePathException($"Path {command.ExecutablePath} is not valid.");

            return command;
        }

        private static List<string> validTypes = new List<string>
        {
            "start",
            "stop",
            "list",
            "explain",
            "stopall",
            "clean"
        };

        private static List<string> excludedExeTokens = new List<string>
        {
            "dotnet",
            "dotnet run"
        };
    }

    public class Command
    {
        [ValueOption(0)]
        public string Type { get; set; }
        [ValueOption(1)]
        public string ExecutablePath { get; set; }
        [Option('w', "working-dir", DefaultValue = "", Required = false, HelpText = "Working directory for the executable to run from")]
        public string WorkingDirectory { get; set; }
        [Option('s', "spare-child-processes", DefaultValue = false, Required = false, HelpText = "Should the child applications forked be killed")]
        public bool SpareChildProcesses { get; set; }
        [Option('a', "exe-args", DefaultValue = "", Required = false, HelpText = "Arguments to be passed to the executable")]
        public string ExecutableArgs { get; set; }
    }
}
