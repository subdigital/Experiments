using System.IO;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    [CommandName("dropDatabase")]
    public class DropDatabaseCommand : CommandBase
    {
        public DropDatabaseCommand(IDatabaseProvider provider, EvoOptions options) : base(provider, options)
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
            ExecuteNonQuery(DatabaseProvider.GetDropDatabaseSyntax(databaseName));           
            outputWriter.WriteLine("Dropped database {0}", databaseName);
        }
    }
}
