using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using evo.Core;
using evo.Core.Commands;

namespace evo
{
    public class Program
    {
        IKernel _kernel;
        List<string> _args;

        public TextWriter Out
        {
            get; set;
        }

        static void Main(string[] args)
        {            
            var program = new Program();
            program.Run(args);
        }

        public Program()
        {
            InitializeDependencies();
            Out = Console.Out;
        }

        public void Run(IEnumerable<string> args)
        {
            _args = new List<string>(args);
            if(!CheckEmptyArgsAndOutputUsage())
                return;

            var parser = new ArgumentParser(_args);
            var options = new EvoOptions();            
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

        void InitializeDependencies()
        {
            _kernel = new StandardKernel(
                new StandardDependenciesModule(), 
                new CommandsModule());
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

        bool CheckEmptyArgsAndOutputUsage()
        {
            if(_args.Count == 0)
            {
                PrintUsage();
                return false;
            }

            return true;
        }

        private ICommand GetCommandAndValidate(EvoOptions options)
        {
            //store the options in the container for other classes that depend on it
            _kernel.Bind<EvoOptions>().ToConstant(options);
            
            //get the command by name
            var cmd = _kernel.Get<ICommand>(x => x.Name == options.Command);

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
