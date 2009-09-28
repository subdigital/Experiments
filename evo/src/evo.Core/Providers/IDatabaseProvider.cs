using System.Data;

namespace evo.Core.Providers
{
    public interface IDatabaseProvider
    {
        string BuildConnectionString(EvoOptions options);
        IDbConnection GetConnection(string connectionString);
        bool IsValidName(string name);
        string GetCreateDatabaseSyntax(string databaseName);
        string GetDropDatabaseSyntax(string databaseName);
    }
}