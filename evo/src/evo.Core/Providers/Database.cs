using System;
using System.Data;

namespace evo.Core.Providers
{
    public class Database : IDatabase
    {
        private IDatabaseProvider _provider;
        private EvoOptions _options;

        public Database(IDatabaseProvider provider, EvoOptions options)
        {
            _provider = provider;
            _options = options;
        }

        public string ConnectionString
        {
            get { return _provider.BuildConnectionString(_options); }
        }

        public void ResetConnectionDetailsFrom(EvoOptions options)
        {
            _options = options;
        }

        public IDbConnection GetConnection()
        {
            return _provider.GetConnection(ConnectionString);
        }

        public IDbCommand GetCommand(string query)
        {
            var cmd = GetConnection().CreateCommand();
            cmd.CommandText = query;
            return cmd;
        }

        public IDbCommand GetCommand(string query, params IDataParameter[] parameters)
        {
            var cmd = GetCommand(query);
            foreach (var parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            return cmd;
        }

        public int? CurrentMigration()
        {
            return ExecuteScalar<int?>("SELECT MAX(version) FROM MigrationInfo");
        }

        public void ExecuteCommand(IDbCommand cmd)
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public virtual void ExecuteQuery(string query)
        {
            ExecuteCommand(GetCommand(query));
        }

        public virtual T ExecuteScalar<T>(string query)
        {
            var cmd = GetCommand(query);
            cmd.Connection.Open();
            object value = cmd.ExecuteScalar();
            cmd.Connection.Close();

            return (T) value;
        }

        public void CreateDatabase(string name)
        {
            var createDbSql = _provider.GetCreateDatabaseSyntax(name);
            var createMigrationTableSql = _provider.GetCreateTableSyntax("MigrationInfo", 
                new Column("version", DbType.Int64, false)
                );

            ExecuteQuery(createDbSql);
            ExecuteQuery(createMigrationTableSql);
        }       

        public void DropDatabase(string name)
        {
            var dropDbSql = _provider.GetDropDatabaseSyntax(name);
            ExecuteQuery(dropDbSql);
        }
    }
}