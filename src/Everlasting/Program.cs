using System;
using ColoredConsole;
using Everlasting.Core;

namespace Everlasting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            try
            {
                var command = args.Parse();
                ColorConsole.WriteLine("Executing:", command.Type.DarkGreen());
            }
            catch (UnknownCommandTypeException uct)
            {
                ColorConsole.WriteLine(uct.Message.Red());
            }
            catch (InvalidExecutablePathException iep)
            {
                ColorConsole.WriteLine(iep.Message.Red());
            }
            catch (Exception ex)
            {
                ColorConsole.WriteLine(ex.Message.Red());
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("forever.net action exe_path [options]");
            Console.WriteLine();

            ColorConsole.WriteLine("actions:".Green());
            Console.WriteLine();
            Console.WriteLine("\tstart, stop, list");
            Console.WriteLine("\texplain, stopall, clean");
            Console.WriteLine();

            ColorConsole.WriteLine("exe path:".Green());
            Console.WriteLine();
            Console.WriteLine("\tPath to the executable. If its not valid, you will get an error");
            Console.WriteLine();

            ColorConsole.WriteLine("options:".Green());
            Console.WriteLine();
            Console.WriteLine("\t -w --working-dir            Working directory for the executable");
            Console.WriteLine("\t -s --spare-child-processes   If child processes may have been spawned, don't kill them (only for stop/stopall)");
            Console.WriteLine("\t -a --exe-args               Arguments to the executable launced in the background");
        }
    }
}
