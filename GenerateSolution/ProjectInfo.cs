namespace GenerateSolution
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal class ProjectInfo
    {
        private readonly Guid id;

        public ProjectInfo(Guid id, string name, string fileName)
        {
            this.id = id;
            this.Name = name;
            this.FileName = fileName;
        }

        public string LowerCaseId
        {
            get { return this.id.ToString().ToLowerInvariant(); }
        }

        public string UpperCaseId
        {
            get { return this.id.ToString().ToUpperInvariant(); }
        }

        public string Name { get; private set; }

        public string FileName { get; private set; }
    }
}
