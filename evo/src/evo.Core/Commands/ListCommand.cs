using System;
using System.IO;
using System.Text.RegularExpressions;
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

            int? migration = Database.CurrentMigration();

            outputWriter.WriteLine("Migrations");
            outputWriter.WriteLine("----------------------------");
            var files = FileSystem.GetFilesInDirectory(Options.ScriptDirectory, "*.boo");

            foreach (var file in files)
            {
                outputWriter.Write("\t");
                if (IsCurrentMigration(file, migration))
                    outputWriter.Write("=> ");
                else
                    outputWriter.Write("   ");

                outputWriter.WriteLine(file);
            }

            outputWriter.WriteLine();
            outputWriter.WriteLine("Total migrations: " + files.Length);
        }

        bool IsCurrentMigration(string file, int? migration)
        {
            if(!migration.HasValue)
                return false;

            string numberPrefix = Regex.Match(file, @"(?<migration>\d+)-.*").Groups["migration"].Value;
            int fileMigration = Int32.Parse(numberPrefix);

            return migration.Value == fileMigration;
        }
    }
}
