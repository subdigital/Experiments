using System.IO;

namespace evo.Core
{
    public class Filesystem : IFileSystem
    {
        public bool DirectoryExists(string directory)
        {
            return Directory.Exists(directory);
        }

        public string[] GetFilesInDirectory(string directory, string pattern)
        {
            return Directory.GetFiles(directory, pattern);
        }

    }
}