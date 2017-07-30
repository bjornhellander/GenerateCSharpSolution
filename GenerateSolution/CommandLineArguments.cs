namespace GenerateSolution
{
    using CommandLine;

    internal class CommandLineArguments
    {
        [Option('n', "name", DefaultValue = "TestSolution", HelpText = "The name of the generated solution.")]
        public string SolutionName { get; set; }

        [Option('p', "projects", DefaultValue = 200, HelpText = "The number of projects to generate.")]
        public int NumberOfProjects { get; set; }

        [Option('c', "classes", DefaultValue = 30, HelpText = "The number of classes to generate.")]
        public int NumberOfClasses { get; set; }

        [Option('m', "methods", DefaultValue = 20, HelpText = "The number of methods to generate.")]
        public int NumberOfMethods { get; set; }

        [Option('s', "statements", DefaultValue = 30, HelpText = "The number of statements to generate.")]
        public int NumberOfStatements { get; set; }

        [Option('a', "skip-analyzers", DefaultValue = false, HelpText = "Set to true if analyzers should not be added.")]
        public bool SkipAnalyzers { get; set; }
    }
}
