namespace GenerateSolution
{
    using System.IO;

    internal class ClassInfo
    {
        public ClassInfo(string name, string fileName)
        {
            this.Name = name;
            this.FileName = fileName;
        }

        public string Name { get; }

        public string FileName { get; }
    }
}
