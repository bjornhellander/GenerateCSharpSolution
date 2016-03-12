﻿using System;
using System.IO;

namespace GenerateSolution
{
    class Project
    {
        public static ProjectInfo Create(string solutionPath, string name)
        {
            var id = Guid.NewGuid();

            var projectPath = Path.Combine(solutionPath, name);
            Directory.CreateDirectory(projectPath);

            CreateSourceFile(projectPath, "File1.cs");

            CreateProjectFile(projectPath, name, id);

            var projectFilePath = Path.Combine(name, name + ".csproj");
            return new ProjectInfo(id, name, projectFilePath);
        }

        private static void CreateSourceFile(string projectPath, string name)
        {
            var sourceFilePath = Path.Combine(projectPath, name);
            using (var stream = new StreamWriter(sourceFilePath))
            {
            }
        }

        private static void CreateProjectFile(string projectPath, string name, Guid id)
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
                stream.WriteLine($"    <ProjectGuid>{{{id}}}</ProjectGuid>");
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
                stream.WriteLine($"    <Compile Include=\"File1.cs\" />");
                stream.WriteLine($"  </ItemGroup>");
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
                stream.WriteLine($"  </PropertyGroup>");
                stream.WriteLine($"  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">");
                stream.WriteLine($"    <Optimize>true</Optimize>");
                stream.WriteLine($"    <OutputPath>..\\Build\\Release</OutputPath>");
                stream.WriteLine($"    <ErrorReport>prompt</ErrorReport>");
                stream.WriteLine($"    <WarningLevel>4</WarningLevel>");
                stream.WriteLine($"    <ConsolePause>false</ConsolePause>");
                stream.WriteLine($"    <CheckForOverflowUnderflow>true</CheckForOverflowUnderflow>");
                stream.WriteLine($"    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>");
                stream.WriteLine($"  </PropertyGroup>");
                stream.WriteLine($"  <Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />");
                stream.WriteLine($"</Project>");
            }
        }
    }
}