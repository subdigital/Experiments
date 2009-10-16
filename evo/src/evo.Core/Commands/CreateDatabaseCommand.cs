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
            Database.Use("master");
            Database.CreateDatabase(Options.Database);
            
            Database.Use(Options.Database);
            Database.CreateMigrationTable();
            
            outputWriter.WriteLine("Created database [{0}]", Options.Database);
        }
    }
}
