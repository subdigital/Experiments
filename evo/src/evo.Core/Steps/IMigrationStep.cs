using evo.Core.Providers;

namespace evo.Core.Steps
{
    public interface IMigrationStep
    {
        void Run(IDatabase database);
    }
}