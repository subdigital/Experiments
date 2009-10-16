using System.Data;

namespace evo.Core.Providers
{
    public interface IDatabaseProvider
    {
        /// <summary>
        /// Create a new connection string using trusted connection (no username/password)
        /// </summary>
        /// <param name="server">The server name (like localhost\SQLEXPRESS)</param>
        /// <param name="db">The database name</param>
        /// <returns></returns>
        string BuildConnectionString(string server, string db);

        /// <summary>
        /// Create a new connection string using database username &amp; password
        /// </summary>
        /// <param name="server">The server name  (like localhost\SQLEXPRESS)</param>
        /// <param name="db">The name of the database</param>
        /// <param name="username">The database username</param>
        /// <param name="password">The database password</param>
        /// <returns></returns>
        string BuildConnectionString(string server, string db, string username, string password);

        /// <summary>
        /// Returns a connection to the database.
        /// </summary>
        /// <param name="connectionString">This can be constructed manually or created using BuildConnectionString</param>
        /// <returns></returns>
        IDbConnection GetConnection(string connectionString);

        /// <summary>
        /// Verifies the database name contains valid characters.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsValidName(string name);
        string GetCreateDatabaseSyntax(string databaseName);
        string GetDropDatabaseSyntax(string databaseName);
        string GetCreateTableSyntax(string name, params Column[] columns);
    }
}