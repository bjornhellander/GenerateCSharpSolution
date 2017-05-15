namespace GenerateSolution
{
    using System.IO;

    internal static class Class
    {
        public static ClassInfo Create(string projectPath, string name)
        {
            var fileName = name + ".cs";
            var sourceFilePath = Path.Combine(projectPath, fileName);
            using (var stream = new StreamWriter(sourceFilePath))
            {
                stream.WriteLine($"// <copyright file=\"{name}.cs\" company=\"PlaceholderCompany\">");
                stream.WriteLine($"// Copyright (c) PlaceholderCompany. All rights reserved.");
                stream.WriteLine($"// </copyright>");
                stream.WriteLine($"");
                stream.WriteLine($"namespace MyNamespace");
                stream.WriteLine($"{{");
                stream.WriteLine($"    using System;");
                stream.WriteLine($"");
                stream.WriteLine($"    public class {name}");
                stream.WriteLine($"    {{");

                for (var i = 1; i <= Configuration.NumberOfMethods; i++)
                {
                    if (i > 1)
                    {
                        stream.WriteLine($"");
                    }

                    CreateMethod(stream, i);
                }

                stream.WriteLine($@"    }}");
                stream.WriteLine($"}}");
            }

            return new ClassInfo(name, fileName);
        }

        private static void CreateMethod(StreamWriter stream, int i)
        {
            stream.WriteLine($"        public void Func{i}()");
            stream.WriteLine($"        {{");

            for (var j = 0; j < Configuration.NumberOfStatements; j++)
            {
                stream.WriteLine($"            Console.WriteLine(DateTime.Now);");
            }

            stream.WriteLine($"        }}");
        }
    }
}
