using System.IO;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    [CommandName("createDatabase")]
    public class CreateDatabaseCommand : CommandBase
    {
        public CreateDatabaseCommand(IDatabaseProvider provider, EvoOptions options) : base(provider, options)
        {         
        }

        public override bool IsValid()
        {
            return DatabaseParametersValid();
        }

        public override void Execute(TextWriter outputWriter)
        {
            string databaseName = Options.Database;
            
            Options.Database = "master";

            ExecuteNonQuery(DatabaseProvider.GetCreateDatabaseSyntax(databaseName));
            
            outputWriter.WriteLine("Created database {0}", databaseName);
        }
    }
}
