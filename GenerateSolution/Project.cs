namespace GenerateSolution
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal static class Project
    {
        public static ProjectInfo Create(string solutionPath, string name, IEnumerable<ProjectInfo> dependencies)
        {
            var numberOfClasses = Configuration.NumberOfClasses;

            var id = Guid.NewGuid();

            var projectPath = Path.Combine(solutionPath, name);
            Directory.CreateDirectory(projectPath);

            var classes = new List<ClassInfo>();
            for (var i = 1; i <= numberOfClasses; i++)
            {
                classes.Add(Class.Create(projectPath, "Class" + i.ToString("D4")));
            }

            CreatePackagesConfigFile(projectPath);

            CreateAssemblyInfoFile(projectPath);

            CreateProjectFile(projectPath, name, id, classes, dependencies);

            var projectFilePath = Path.Combine(name, name + ".csproj");
            return new ProjectInfo(id, name, projectFilePath);
        }

        private static void CreatePackagesConfigFile(string projectPath)
        {
            if (Configuration.UseAnalyzers)
            {
                var projectFilePath = Path.Combine(projectPath, "packages.config");
                using (var stream = new StreamWriter(projectFilePath))
                {
                    stream.WriteLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    stream.WriteLine($"<packages>");
                    stream.WriteLine($"  <package id=\"AsyncUsageAnalyzers\" version=\"1.0.0-alpha003\" targetFramework=\"net452\" developmentDependency=\"true\" />");
                    stream.WriteLine($"  <package id=\"Desktop.Analyzers\" version=\"1.1.0\" targetFramework=\"net452\" />");
                    stream.WriteLine($"  <package id=\"Microsoft.AnalyzerPowerPack\" version=\"1.1.0\" targetFramework=\"net452\" /> ");
                    stream.WriteLine($"  <package id=\"Microsoft.CodeAnalysis.FxCopAnalyzers\" version=\"1.1.0\" targetFramework=\"net452\" />");
                    stream.WriteLine($"  <package id=\"StyleCop.Analyzers\" version=\"1.0.0\" targetFramework=\"net452\" developmentDependency=\"true\" />");
                    stream.WriteLine($"  <package id=\"System.Runtime.Analyzers\" version=\"1.0.1\" targetFramework=\"net452\" />");
                    stream.WriteLine($"  <package id=\"System.Runtime.InteropServices.Analyzers\" version=\"1.0.1\" targetFramework=\"net452\" />");
                    stream.WriteLine($"  <package id=\"System.Security.Cryptography.Hashing.Algorithms.Analyzers\" version=\"1.1.0\" targetFramework=\"net452\" />");
                    stream.WriteLine($"</packages>");
                }
            }
        }

        private static void CreateAssemblyInfoFile(string projectPath)
        {
            var path = Path.Combine(projectPath, "Properties");
            Directory.CreateDirectory(path);

            var projectFilePath = Path.Combine(path, "AssemblyInfo.cs");
            using (var stream = new StreamWriter(projectFilePath))
            {
                stream.WriteLine($"// <copyright file=\"AssemblyInfo.cs\" company=\"PlaceholderCompany\">");
                stream.WriteLine($"// Copyright (c) PlaceholderCompany. All rights reserved.");
                stream.WriteLine($"// </copyright>");
                stream.WriteLine($"");
                stream.WriteLine($"using System.Reflection;");
                stream.WriteLine($"");
                stream.WriteLine($"[assembly: AssemblyVersion(\"1.0.0.0\")]");
            }
        }

        private static void CreateProjectFile(string projectPath, string name, Guid id, IEnumerable<ClassInfo> classes, IEnumerable<ProjectInfo> dependencies)
        {
            var projectFilePath = Path.Combine(projectPath, name + ".csproj");
            using (var stream = new StreamWriter(projectFilePath))
            {
                stream.WriteLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                stream.WriteLine($"<Project ToolsVersion=\"14.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                stream.WriteLine($"  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />");
                stream.WriteLine($"  <PropertyGroup>");
                stream.WriteLine($"    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>");
                stream.WriteLine($"    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>");
                stream.WriteLine($"    <ProjectGuid>{{{id.ToString().ToLowerInvariant()}}}</ProjectGuid>");
                stream.WriteLine($"    <OutputType>Library</OutputType>");
                stream.WriteLine($"    <AppDesignerFolder>Properties</AppDesignerFolder>");
                stream.WriteLine($"    <RootNamespace>{name}</RootNamespace>");
                stream.WriteLine($"    <AssemblyName>{name}</AssemblyName>");
                stream.WriteLine($"    <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>");
                stream.WriteLine($"    <FileAlignment>512</FileAlignment>");
                stream.WriteLine($"    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>");
                stream.WriteLine($"  </PropertyGroup>");
                stream.WriteLine($"  <ItemGroup>");
                stream.WriteLine($"    <Reference Include=\"System\" />");
                stream.WriteLine($"  </ItemGroup>");
                stream.WriteLine($"  <ItemGroup>");

                stream.WriteLine($"    <Compile Include=\"Properties\\AssemblyInfo.cs\" />");
                foreach (var @class in classes)
                {
                    stream.WriteLine($"    <Compile Include=\"{@class.FileName}\" />");
                }

                stream.WriteLine($"  </ItemGroup>");
                stream.WriteLine($"  <ItemGroup>");
                stream.WriteLine($"    <None Include=\"packages.config\" />");
                stream.WriteLine($"  </ItemGroup>");
                stream.WriteLine($"  <ItemGroup>");

                foreach (var dependency in dependencies)
                {
                    stream.WriteLine($"    <ProjectReference Include=\"..\\{dependency.FileName}\">");
                    stream.WriteLine($"      <Project>{{{dependency.LowerCaseId}}}</Project>");
                    stream.WriteLine($"      <Name>{dependency.Name}</Name>");
                    stream.WriteLine($"    </ProjectReference>");
                }

                stream.WriteLine($"  </ItemGroup>");

                if (Configuration.UseAnalyzers)
                {
                    stream.WriteLine($"  <ItemGroup>");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\AsyncUsageAnalyzers.1.0.0-alpha003\\analyzers\\dotnet\\AsyncUsageAnalyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\Desktop.Analyzers.1.1.0\\analyzers\\dotnet\\cs\\Desktop.Analyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\Desktop.Analyzers.1.1.0\\analyzers\\dotnet\\cs\\Desktop.CSharp.Analyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\Microsoft.AnalyzerPowerPack.1.1.0\\analyzers\\dotnet\\cs\\Microsoft.AnalyzerPowerPack.Common.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\Microsoft.AnalyzerPowerPack.1.1.0\\analyzers\\dotnet\\cs\\Microsoft.AnalyzerPowerPack.CSharp.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\StyleCop.Analyzers.1.0.0\\analyzers\\dotnet\\cs\\Newtonsoft.Json.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\StyleCop.Analyzers.1.0.0\\analyzers\\dotnet\\cs\\StyleCop.Analyzers.CodeFixes.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\StyleCop.Analyzers.1.0.0\\analyzers\\dotnet\\cs\\StyleCop.Analyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\System.Runtime.Analyzers.1.0.1\\analyzers\\dotnet\\cs\\System.Runtime.Analyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\System.Runtime.Analyzers.1.0.1\\analyzers\\dotnet\\cs\\System.Runtime.CSharp.Analyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\System.Runtime.InteropServices.Analyzers.1.0.1\\analyzers\\dotnet\\cs\\System.Runtime.InteropServices.Analyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\System.Runtime.InteropServices.Analyzers.1.0.1\\analyzers\\dotnet\\cs\\System.Runtime.InteropServices.CSharp.Analyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\System.Security.Cryptography.Hashing.Algorithms.Analyzers.1.1.0\\analyzers\\dotnet\\cs\\System.Security.Cryptography.Hashing.Algorithms.Analyzers.dll\" />");
                    stream.WriteLine($"    <Analyzer Include=\"..\\packages\\System.Security.Cryptography.Hashing.Algorithms.Analyzers.1.1.0\\analyzers\\dotnet\\cs\\System.Security.Cryptography.Hashing.Algorithms.CSharp.Analyzers.dll\" />");
                    stream.WriteLine($"  </ItemGroup>");
                }

                stream.WriteLine($"  <PropertyGroup Condition= \" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">");
                stream.WriteLine($"    <DebugSymbols>true</DebugSymbols>");
                stream.WriteLine($"    <DebugType>full</DebugType>");
                stream.WriteLine($"    <Optimize>false</Optimize>");
                stream.WriteLine($"    <OutputPath>..\\Build\\Debug</OutputPath>");
                stream.WriteLine($"    <DefineConstants>DEBUG;</DefineConstants>");
                stream.WriteLine($"    <ErrorReport>prompt</ErrorReport>");
                stream.WriteLine($"    <WarningLevel>4</WarningLevel>");
                stream.WriteLine($"    <ConsolePause>false</ConsolePause>");
                stream.WriteLine($"    <CheckForOverflowUnderflow>true</CheckForOverflowUnderflow>");
                stream.WriteLine($"    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>");
                stream.WriteLine($"    <NoWarn>SA1652</NoWarn>");
                stream.WriteLine($"  </PropertyGroup>");
                stream.WriteLine($"  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">");
                stream.WriteLine($"    <Optimize>true</Optimize>");
                stream.WriteLine($"    <OutputPath>..\\Build\\Release</OutputPath>");
                stream.WriteLine($"    <ErrorReport>prompt</ErrorReport>");
                stream.WriteLine($"    <WarningLevel>4</WarningLevel>");
                stream.WriteLine($"    <ConsolePause>false</ConsolePause>");
                stream.WriteLine($"    <CheckForOverflowUnderflow>true</CheckForOverflowUnderflow>");
                stream.WriteLine($"    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>");
                stream.WriteLine($"    <NoWarn>SA1652</NoWarn>");
                stream.WriteLine($"  </PropertyGroup>");
                stream.WriteLine($"  <Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />");
                stream.WriteLine($"</Project>");
            }
        }
    }
}
