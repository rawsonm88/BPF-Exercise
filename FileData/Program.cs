using System;
using System.Collections.Generic;
using System.Linq;
using FileData.Exceptions;
using FileData.Parsers;
using ThirdPartyTools;

namespace FileData
{
    /// <summary>
    /// Entrypoint for a Console application to provide information about a given file
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            var parser = new CommandLineParser();
            parser.AddActionType("version");
            parser.AddActionType("size");

            List<string> actions;

            try
            {
                actions = parser.Parse(args);
            }
            catch (AmbiguousArgumentException ex)
            {
                Console.WriteLine($"There's been an error, we couldn't understand the argument {ex.Message}");
                return;
            }

            var filePath = args.LastOrDefault();

            if (actions.Count == 0 || string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Provide a command line argument");
                Console.WriteLine();
                Console.WriteLine("\t--version\t\tDisplays the version of the file specified");
                Console.WriteLine("\t--size\t\tDisplays the size of the file specified in MB");

                return;
            }

            var fileDetails = new FileDetails();

            foreach(var action in actions)
            {
                if (action == "version")
                {
                    var version = fileDetails.Version(filePath);
                    Console.WriteLine($"File version: {version}");
                }
                else if (action == "size")
                {
                    var size = fileDetails.Size(filePath);
                    Console.WriteLine($"File size: {size}");
                }
            }
        }
    }
}
