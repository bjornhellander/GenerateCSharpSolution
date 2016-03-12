namespace GenerateSolution
{
    class Program
    {
        private const string solutionName = "TestSolution";
        private const int numberOfProjects = 1;

        static void Main(string[] args)
        {
            Solution.Create(solutionName, numberOfProjects);
        }
    }
}
