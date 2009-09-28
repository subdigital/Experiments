using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using evo.Core;
using evo.Core.Commands;
using evo.Core.Extensions;
using evo.Core.Providers;

namespace evo
{
    public class Program
    {
        private readonly IDatabaseProvider _databaseProvider = new SqlServerProvider(); 
        private readonly List<string> _args;

        public TextWriter Out
        {
            get; set;
        }

        static void Main(string[] args)
        {
            var program = new Program(args);
            program.Run();
        }

        public Program(IEnumerable<string> args)
        {            
            _args = new List<string>(args);
            Out = Console.Out;
        }

        public void Run()
        {
            if(_args.Count == 0)
            {
                PrintUsage();
                return;
            }

            var options = new EvoOptions();
            options.Command = _args.ObtainAndRemove(0);

            bool argsValid = ParseArgs(options);
            if(!argsValid)
                return;
            
            ICommand cmd = GetCommand(options);
            if (cmd == null)
            {
                PrintUsage("Unknown command: " + options.Command);
                return;
            }

            if(!cmd.IsValid())
            {
                PrintUsage("Invalid arguments for command: " + options.Command);
                return;
            }

            try
            {
                cmd.Execute(Out);
            }
            catch(EvoException evoExc)
            {
                Out.WriteLine("ERROR: " + evoExc.FriendlyError);
            }
            catch(Exception exc)
            {
                Out.WriteLine("ERROR:" + exc);
            }
        }

        private bool ParseArgs(EvoOptions options)
        {
            while(_args.Count > 0)
            {
                string arg = _args.ObtainAndRemove(0);
                if(!arg.StartsWith("--"))
                {
                    PrintUsage("Unexpected token: " + arg);
                    return false;
                }
                arg = arg.TrimStart('-');

                if(_args.Count == 0)
                    PrintUsage("Expected value for: " + arg);

                string value = _args.ObtainAndRemove(0);
                
                if (!ProcessArgumentAndValue(arg, value, options)) 
                    return false;
            }

            return true;
        }

        private bool ProcessArgumentAndValue(string arg, string value, EvoOptions options)
        {
            switch(arg)
            {
                case "server" :
                    options.ServerName = value;
                    break;

                case "db":
                    options.Database = value;
                    break;

                case "username":
                    options.Database = value;
                    break;

                case "password":
                    options.Database = value;
                    break;

                default:
                    PrintUsage("Unknown argument:" + arg);
                    return false;
            }
            return true;
        }

        private ICommand GetCommand(EvoOptions options)
        {
            var commandAssembly = typeof(ICommand).Assembly;
            var commands = commandAssembly.GetTypesImplementing<ICommand>()
                .Where(c=>c.HasAttribute<CommandNameAttribute>());

            var validCommandNames = commands.Select(c => c.GetAttribute<CommandNameAttribute>().Name);

            if(! validCommandNames.Contains(options.Command))
            {
                return null;
            }

            Type CommandType = commands.Where(c => c.GetAttribute<CommandNameAttribute>().Name == options.Command).First();
            return (CommandBase)Activator.CreateInstance(CommandType, _databaseProvider, options);
        }

        private void PrintUsage()
        {
            PrintUsage(string.Empty);
        }

        private void PrintUsage(string additionalMessage)
        {
            Out.WriteLine(additionalMessage);
            Out.WriteLine("USAGE: ");
            Out.WriteLine();
            Out.WriteLine("evo [command] [args]");
            Out.WriteLine();
            Out.WriteLine("Commands: ");
            Out.WriteLine("  list");
            Out.WriteLine("  create");
            Out.WriteLine("  run");
            Out.WriteLine("  createDatabase");
            Out.WriteLine("  dropDatabase");
            Out.WriteLine();
            Out.WriteLine("Arguments: ");
            Out.WriteLine("  --config <config_file>     Specifies a configuration file to configure [evo]");
            Out.WriteLine("  --server <servername>      Specifies the database server to use.  Overrides config.");
            Out.WriteLine("  --db <dbname>              Specifies the database to use.  Overrides config.");            
            Out.WriteLine("  --u <username>             Specifies the db username.  Overrides config.");
            Out.WriteLine("  --p <password>             Specifies the db password.  Overrides config.");            
        }
    }
}
