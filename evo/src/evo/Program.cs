using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using evo.Core;
using evo.Core.Commands;
using evo.Core.Extensions;
using Ninject.Parameters;

namespace evo
{
    public class Program
    {
        IKernel _kernel;
        readonly List<string> _args;

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
            InitializeDependencies();

            _args = new List<string>(args);
            Out = Console.Out;
        }

        void InitializeDependencies()
        {
            _kernel = new StandardKernel(
                new StandardDependenciesModule(), 
                new CommandsModule());
        }

        public void Run()
        {
            if(!CheckEmptyArgsAndOutputUsage())
                return;

            var parser = new ArgumentParser(_args);
            
           string configFile = parser.Option("config") ?? "evo.config";
            var configParser = new ConfigParser(_kernel.Get<IFileSystem>(), configFile);
            var options = new EvoOptions();
            if (configParser.ConfigFileExists())
                configParser.SetOptions(options);

            //allow command line args to override config settings
            parser.SetOptions(options);
            if(!parser.IsValid)
            {
                PrintUsage(parser.ErrorMessage);
                return;
            }

            ICommand cmd = GetCommandAndValidate(options);
            if (cmd == null)
                return;          

            RunCommand(cmd);
        }

        void RunCommand(ICommand cmd)
        {
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

        EvoOptions GetOptions()
        {
            var options = new EvoOptions {Command = _args.ObtainAndRemove(0)};

            bool argsValid = ParseArgs(options);
            if(!argsValid)
                return null;

            _kernel.Bind<EvoOptions>().ToConstant(options);
            return options;
        }

        bool CheckEmptyArgsAndOutputUsage()
        {
            if(_args.Count == 0)
            {
                PrintUsage();
                return false;
            }

            return true;
        }

        private bool ParseArgs(EvoOptions options)
        {
            while(_args.Count > 0)
            {
                string arg = _args.ObtainAndRemove(0);

                if(arg.StartsWith("--"))
                {
                    arg = arg.TrimStart('-');
                    if (_args.Count == 0)
                        PrintUsage("Expected value for: " + arg);
                    string value = _args.ObtainAndRemove(0);

                    if (!ProcessArgumentAndValue(arg, value, options))
                        return false;
                }
                else
                {
                    options.AdditionalArgs.Add(arg);
                }
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

        private ICommand GetCommandAndValidate(EvoOptions options)
        {
            var cmd = _kernel.Get<ICommand>(x => x.Name == options.Command, 
                new ConstructorArgument("options", options));

            if(cmd == null)
                PrintUsage("Unknown command: " + options.Command);
            else
            {
                if (!cmd.IsValid())
                {
                    if (!cmd.OutputCommandUsage(Console.Out))
                    {
                        PrintUsage("Invalid arguments for command: " + options.Command);
                    }

                    return null;
                }
            }

            return cmd;
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
