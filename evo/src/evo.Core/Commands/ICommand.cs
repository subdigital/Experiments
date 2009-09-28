using System.IO;

namespace evo.Core.Commands
{
    public interface ICommand
    {
        bool IsValid();
        void Execute(TextWriter outputWriter);
    }
}