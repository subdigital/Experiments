namespace evo.Core
{
    public interface IFileSystem
    {
        bool DirectoryExists(string directory);
        string[] GetFilesInDirectory(string directory, string pattern);
        void CreateDirectory(string directory);
        void CreateFile(string path, string contents);
        string PathCombine(string leftPart, string rightPart);
    }
}