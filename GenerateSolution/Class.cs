using System.IO;

namespace GenerateSolution
{
    class Class
    {
        public static ClassInfo Create(string projectPath, string name)
        {
            var fileName = name + ".cs";
            var sourceFilePath = Path.Combine(projectPath, fileName);
            using (var stream = new StreamWriter(sourceFilePath))
            {
                stream.WriteLine($"using System;");
                stream.WriteLine($"");
                stream.WriteLine($"namespace MyNamespace");
                stream.WriteLine($"{{");
                stream.WriteLine($"    public class {name}");
                stream.WriteLine($"    {{");

                for (var i = 1; i <= Configuration.NumberOfMethods; i++)
                {
                    stream.WriteLine($"");
                    stream.WriteLine($"        public void Func{i}()");
                    stream.WriteLine($"        {{");
                    for (var j = 0; j < Configuration.NumberOfStatements; j++)
                    {
                        stream.WriteLine($"            Console.WriteLine(DateTime.Now);");
                    }
                    stream.WriteLine($"        }}");
                    stream.WriteLine($"");
                }

                stream.WriteLine($@"    }}");
                stream.WriteLine($"}}");
            }

            return new ClassInfo(name, fileName);
        }
    }

    class ClassInfo
    {
        public ClassInfo(string name, string fileName)
        {
            Name = name;
            FileName = fileName;
        }

        public string Name { get; }

        public string FileName { get; }
    }
}
