using System.IO;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    [CommandName("createDatabase")]
    public class CreateDatabaseCommand : CommandBase
    {
        public CreateDatabaseCommand(IDatabase database, EvoOptions options) : base(database, options)
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
            Database.ResetConnectionDetailsFrom(Options);

            Database.CreateDatabase(databaseName);
            
            outputWriter.WriteLine("Created database [{0}]", databaseName);
        }
    }
}
