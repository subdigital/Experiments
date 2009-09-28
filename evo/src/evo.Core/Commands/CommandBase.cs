using System;
using System.Data;
using System.IO;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected IDatabaseProvider DatabaseProvider { get; set; }
        protected EvoOptions Options { get; set; }

        public CommandBase(IDatabaseProvider provider, EvoOptions options)
        {
            Options = options;
            DatabaseProvider = provider;            
        }

        public abstract bool IsValid();
        public abstract void Execute(TextWriter outputWriter);

        protected bool DatabaseParametersValid()
        {
            if (Options.ServerName == null || Options.Database == null)
                return false;

            if(Options.TrustedConnection)
                return true;

            return (Options.Username != null && Options.Password != null);
        }

        protected void ExecuteInOpenConnection(Action<IDbConnection> dbAction)
        {
            var connectionString = DatabaseProvider.BuildConnectionString(Options);
            using (var cn = DatabaseProvider.GetConnection(connectionString))
            {
                cn.Open();
                dbAction(cn);    
                cn.Close();
            }
        }

        protected void ExecuteNonQuery(string sqlQuery)
        {
            ExecuteInOpenConnection(conn =>
                                        {
                                            using(var cmd = conn.CreateCommand())
                                            {
                                                cmd.CommandText = sqlQuery;
                                                cmd.ExecuteNonQuery();
                                            }
                                        });
        }
    }
}