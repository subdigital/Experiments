using System;
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

        public void CreateDirectory(string directory)
        {
            Directory.CreateDirectory(directory);
        }

        public void CreateFile(string path, string contents)
        {
            using(var writer = File.CreateText(path))
            {
                writer.Write(contents);    
            }
        }

        public string PathCombine(string leftPart, string rightPart)
        {
            return Path.Combine(leftPart, rightPart);
        }

        public bool FileExists(string filename)
        {
            return File.Exists(filename);
        }
    }
}