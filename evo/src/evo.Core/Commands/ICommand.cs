using System.IO;

namespace evo.Core.Commands
{
    public interface ICommand
    {
        bool OutputCommandUsage(TextWriter outputTextWriter);
        bool IsValid();
        void Execute(TextWriter outputWriter);
    }
}