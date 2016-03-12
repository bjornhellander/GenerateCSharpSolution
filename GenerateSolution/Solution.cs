using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GenerateSolution
{
    class Solution
    {
        public static void Create()
        {
            var solutionName = Configuration.SolutionName;
            var numberOfProjects = Configuration.NumberOfProjects;

            var basePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..");
            var solutionPath = Path.Combine(basePath, solutionName);
            CreateSolutionFolder(solutionPath);
            var projectInfos = CreateProjects(solutionPath, numberOfProjects);
            CreateSolutionFile(solutionPath, solutionName, projectInfos);
        }

        private static void CreateSolutionFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive: true);
            }

            Directory.CreateDirectory(path);
        }

        private static IEnumerable<ProjectInfo> CreateProjects(string solutionPath, int numberOfProjects)
        {
            var result = new List<ProjectInfo>();

            for (var i = 1; i <= numberOfProjects; i++)
            {
                var name = "Project" + i.ToString("D4");
                result.Add(Project.Create(solutionPath, name));
            }

            return result;
        }

        private static void CreateSolutionFile(string solutionPath, string name, IEnumerable<ProjectInfo> projects)
        {
            var solutionFileName = Path.Combine(solutionPath, name + ".sln");
            using (var stream = new StreamWriter(solutionFileName, false, System.Text.Encoding.UTF8))
            {
                stream.WriteLine($"Microsoft Visual Studio Solution File, Format Version 12.00");
                stream.WriteLine($"# Visual Studio 14");
                stream.WriteLine($"VisualStudioVersion = 14.0.24720.0");
                stream.WriteLine($"MinimumVisualStudioVersion = 10.0.40219.1");
                foreach (var project in projects)
                {
                    var extraId = Guid.NewGuid().ToString().ToUpperInvariant();
                    stream.WriteLine($"Project(\"{{{extraId}}}\") = \"{project.Name}\", \"{project.FileName}\", \"{{{project.UpperCaseId}}}\"");
                    stream.WriteLine($"EndProject");
                }
                stream.WriteLine($"Global");
                stream.WriteLine($"\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
                stream.WriteLine($"\t\tDebug|Any CPU = Debug|Any CPU");
                stream.WriteLine($"\t\tRelease|Any CPU = Release|Any CPU");
                stream.WriteLine($"\tEndGlobalSection");
                stream.WriteLine($"\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
                foreach (var project in projects)
                {
                    stream.WriteLine($"\t\t{{{project.UpperCaseId}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
                    stream.WriteLine($"\t\t{{{project.UpperCaseId}}}.Debug|Any CPU.Build.0 = Debug|Any CPU");
                    stream.WriteLine($"\t\t{{{project.UpperCaseId}}}.Release|Any CPU.ActiveCfg = Release|Any CPU");
                    stream.WriteLine($"\t\t{{{project.UpperCaseId}}}.Release|Any CPU.Build.0 = Release|Any CPU");
                }
                stream.WriteLine($"\tEndGlobalSection");
                stream.WriteLine($"EndGlobal");
            }
        }
    }

    class ProjectInfo
    {
        private readonly Guid id;

        public ProjectInfo(Guid id, string name, string fileName)
        {
            this.id = id;
            Name = name;
            FileName = fileName;
        }

        public string LowerCaseId
        {
            get { return id.ToString().ToLowerInvariant(); }
        }

        public string UpperCaseId
        {
            get { return id.ToString().ToUpperInvariant(); }
        }

        public string Name { get; private set; }

        public string FileName { get; private set; }
    }
}
