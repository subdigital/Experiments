using System.IO;
using evo.Core.Extensions;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    [CommandName("list")]
    public class ListCommand : CommandBase
    {         
        public IFileSystem FileSystem { get; set; }

        public ListCommand(IFileSystem fileSystem, IDatabase database, EvoOptions options) : base(database, options)
        {
            FileSystem = fileSystem;
        }

        public override bool IsValid()
        {
            //don't need db connection or anything else
            return true;
        }

        public override void Execute(TextWriter outputWriter)
        {
            if(!FileSystem.DirectoryExists(Options.ScriptDirectory))
            {
                outputWriter.WriteLine(string.Format("The script directory {0} doesn't exist.  Have you created any migrations yet?", 
                    Options.ScriptDirectory));    

                outputWriter.WriteLine("You can change this directory by providing the --scriptsdir option");
                return;
            }

            outputWriter.WriteLine("Migrations");
            outputWriter.WriteLine("----------------------------");
            var files = FileSystem.GetFilesInDirectory(Options.ScriptDirectory, "*.boo");
            files.Each(file => outputWriter.WriteLine("\t" + file));

            outputWriter.WriteLine();
            outputWriter.WriteLine("Total migrations: " + files.Length);
        }
    }
}
