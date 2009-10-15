using System.IO;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected IDatabase Database { get; set; }
        protected EvoOptions Options { get; set; }

        public CommandBase(IDatabase database, EvoOptions options)
        {
            Database = database;
            Options = options;
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
    }
}