using System;

namespace evo.Core
{
    public interface IFileSystem
    {
        bool DirectoryExists(string directory);
        string[] GetFilesInDirectory(string directory, string pattern);
    }
}