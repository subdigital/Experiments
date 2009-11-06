using System;
using System.Data;

namespace evo.Core.Providers
{
    public class Database : IDatabase
    {
        private IDatabaseProvider _provider;
        string _server;
        string _db;
        string _username;
        string _password;
        bool _trustedConnection;

        public Database(IDatabaseProvider provider, EvoOptions options)
        {
            _provider = provider;
            ParseOptions(options);
        }

        void ParseOptions(EvoOptions options)
        {
            _server = options.ServerName;
            _db = options.Database;
            _username = options.Username;
            _password = options.Password;
            _trustedConnection = options.TrustedConnection;
        }

        public string BuildConnectionString()
        {
            if (_trustedConnection)
                return _provider.BuildConnectionString(_server, _db);
            return _provider.BuildConnectionString(_server, _db, _username, _password);
        }

        public void Use(string dbName)
        {
            _db = dbName;
        }

        public IDbConnection GetConnection()
        {
            return _provider.GetConnection(BuildConnectionString());
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
            
            ExecuteQuery(createDbSql);
        }   
    
        public void CreateMigrationTable()
        {
            var createMigrationTableSql = _provider.GetCreateTableSyntax("MigrationInfo",
                new Column("version", DbType.Int64, false)
                );
            ExecuteQuery(createMigrationTableSql);
        }   

        public void DropDatabase(string name)
        {
            var dropDbSql = _provider.GetDropDatabaseSyntax(name);
            ExecuteQuery(dropDbSql);
        }
    }
}