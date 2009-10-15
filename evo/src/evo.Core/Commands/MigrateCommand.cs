using System;
using System.IO;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    [CommandName("migrate")]
    public class MigrateCommand : CommandBase
    {
        public MigrateCommand(IDatabase database, EvoOptions options) : base(database, options)
        {
        }

        public override bool IsValid()
        {
            if(Options.AdditionalArgs.Length > 0)
            {
                foreach (var arg in Options.AdditionalArgs)
                {
                    int val;
                    if(!Int32.TryParse(arg, out val))
                        return false;
                }
            }

            return true;
        }

        public override void Execute(TextWriter outputWriter)
        {
        }
    }
}
