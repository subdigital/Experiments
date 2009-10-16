using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace evo.Core.Providers
{
    public class SqlServerProvider : IDatabaseProvider
    {     
        public string BuildConnectionString(string server, string db)
        {
            var builder = new SqlConnectionStringBuilder
                              {
                                  DataSource = server,
                                  InitialCatalog = db,
                                  IntegratedSecurity = true
                              };

            return builder.ConnectionString;
        }

        public string BuildConnectionString(string server, string db, string username, string password)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = db,
                UserID = username,
                Password = password
            };

            return builder.ConnectionString;
        }

        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public bool IsValidName(string name)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_.1234567890";
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

        public string GetCreateTableSyntax(string name, params Column[] columns)
        {
            string columnsDefinition = "";
            foreach (var column in columns)
            {
                columnsDefinition += GetColumnDefinition(column);
                columnsDefinition += ",";
            }

            columnsDefinition = columnsDefinition.TrimEnd(',');

            string syntax = string.Format("CREATE TABLE [{0}] ({1}\r\n)", name, columnsDefinition);
            return syntax;
        }

        string GetColumnDefinition(Column column)
        {
            var sb = new StringBuilder();
            sb.Append("\r\n\t");
            sb.AppendFormat("[{0}] {1}", column.Name, GetDatabaseType(column));
            if(!column.Nullable)
                sb.Append(" NOT");
            sb.Append(" NULL");

            if(column.PrimaryKey)
                sb.Append(" PRIMARY KEY");

            if (column.Identity)
                sb.Append(" IDENTITY(1,1)");

            if (column.Unique)
                sb.Append(" UNIQUE");

            if (column.Default != null)
                sb.AppendFormat(" DEFAULT {0}", column.Default);

            return sb.ToString();
        }

        string GetDatabaseType(Column column)
        {
            switch (column.Type)
            {
                case DbType.Int16:
                    return "tinyint";
                case DbType.Int32:
                    return "int";
                case DbType.Int64:
                    return "bigint";

                case DbType.Boolean:
                    return "bit";

                case DbType.StringFixedLength:
                    return string.Format("nchar({0})", column.Length);

                case DbType.String:
                    if (column.Length < 4000)
                        return string.Format("nvarchar({0})", column.Length);
                    return "text";

                case DbType.DateTime:
                    return string.Format("datetime");

                case DbType.Double:                
                    return string.Format("float");

                case DbType.Decimal:
                    return string.Format("decimal({0}, {1})", column.Precision, column.Scale);

                case DbType.Guid:
                    return "uniqueidentifier";

                case DbType.Currency:
                    return "money";

                case DbType.Xml:
                    return "xml";

                case DbType.Binary:                    
                    return string.Format("varbinary({0})", 
                        column.Length <= 8000 ? column.Length.ToString() : "max");

                default:
                    throw new ArgumentOutOfRangeException("Don't know how to handle DbType: " + column.Type);
            }            
        }

        public string GetCurrentMigrationSyntax()
        {
            return "SELECT MAX(version) FROM MigrationInfo";
        }
    }
}
