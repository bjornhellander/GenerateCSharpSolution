namespace GenerateSolution
{
    using System;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            ParseArguments(args);

            Solution.Create();
        }

        private static void ParseArguments(string[] args)
        {
            var parser = CommandLine.Parser.Default;
            var options = new CommandLineArguments();
            if (!parser.ParseArgumentsStrict(args, options))
            {
                // Display the default usage information
                // Console.WriteLine(options.GetUsage());
                Environment.Exit(1);
            }

            Configuration.SolutionName = options.SolutionName;
            Configuration.NumberOfProjects = options.NumberOfProjects;
            Configuration.NumberOfClasses = options.NumberOfClasses;
            Configuration.NumberOfMethods = options.NumberOfMethods;
            Configuration.NumberOfStatements = options.NumberOfStatements;
            Configuration.UseAnalyzers = !options.SkipAnalyzers;
        }
    }
}
