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
                stream.WriteLine($@"
namespace MyNamespace
{{
    public class {name}
    {{
    }}
}}");
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
