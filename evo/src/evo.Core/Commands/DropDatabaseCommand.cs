using System.IO;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    [CommandName("dropDatabase")]
    public class DropDatabaseCommand : CommandBase
    {
        public DropDatabaseCommand(IDatabase database, EvoOptions options) : base(database, options)
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

            Database.DropDatabase(databaseName);
            outputWriter.WriteLine("Dropped database [{0}]", databaseName);
        }
    }
}
