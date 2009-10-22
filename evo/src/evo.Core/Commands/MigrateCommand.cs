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
            if(Options.AdditionalArgs.Count > 0)
            {
                if (!EnsureIntegerArgs()) 
                    return false;
            }

            return true;
        }

        bool EnsureIntegerArgs()
        {
            foreach (var arg in Options.AdditionalArgs)
            {
                int val;
                if(!Int32.TryParse(arg, out val))
                    return false;
            }
            return true;
        }

        public override void Execute(TextWriter outputWriter)
        {

        }
    }
}
