namespace evo.Core.DSL
{
    public interface IMigrationFactory
    {
        MigrationBase[] CreateAll();
    }
}