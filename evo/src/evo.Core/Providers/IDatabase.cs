using System;
using System.Data;

namespace evo.Core.Providers
{
    public interface IDatabase
    {
        string BuildConnectionString();
        IDbConnection GetConnection();
        int? CurrentMigration();
        void ExecuteQuery(string query);
        T ExecuteScalar<T>(string query);
        void CreateDatabase(string name);
        void DropDatabase(string name);
        void Use(string databaseName);
        void CreateMigrationTable();
    }
}
