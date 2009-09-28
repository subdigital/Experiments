using System;
using System.Data;
using System.Data.SqlClient;

namespace evo.Core.Providers
{
    public class SqlServerProvider : IDatabaseProvider
    {
        public string BuildConnectionString(EvoOptions options)
        {
            var builder = new SqlConnectionStringBuilder
                              {
                                  DataSource = options.ServerName,
                                  InitialCatalog = options.Database
                              };

            if (options.TrustedConnection)
                builder.IntegratedSecurity = true;
            else
            {
                builder.UserID = options.Username;
                builder.Password = options.Password;
            }

            return builder.ConnectionString;
        }

        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public bool IsValidName(string name)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_.";
            foreach (char c in name)
                if (!validChars.Contains(c.ToString()))
                    return false;

            return true;
        }

        public string GetCreateDatabaseSyntax(string databaseName)
        {
            AssertValidName(databaseName);

            return string.Format("CREATE DATABASE [{0}]", databaseName);
        }

        private void AssertValidName(string databaseName)
        {
            if (!IsValidName(databaseName))
                throw new EvoException("Invalid database name " + databaseName);
        }

        public string GetDropDatabaseSyntax(string databaseName)
        {
            AssertValidName(databaseName);
            return string.Format("DROP DATABASE [{0}]", databaseName);
        }
    }
}
