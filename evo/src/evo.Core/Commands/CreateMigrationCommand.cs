using System;
using System.IO;
using System.Text;
using evo.Core.Extensions;
using evo.Core.Providers;

namespace evo.Core.Commands
{
    [CommandName("create")]
    public class CreateMigrationCommand : CommandBase
    {
        const int NumberOfDigits = 3;
        readonly IFileSystem _filesystem;

        public CreateMigrationCommand(IFileSystem filesystem, IDatabase database, EvoOptions options) : base(database, options)
        {
            _filesystem = filesystem;
        }

        public override bool IsValid()
        {
            return Options.AdditionalArgs.Count >= 1;
        }

        public override bool OutputCommandUsage(TextWriter outputWriter)
        {
            outputWriter.WriteLine("USAGE:");
            outputWriter.WriteLine("$ evo create <migration name>");
            outputWriter.WriteLine("The migration will be automatically numbered and placed in the script directory. (The migration name can have spaces in it)");
            outputWriter.WriteLine("By default it will be placed in a migrations directory.  To customize, provide the --scriptsdir <dir> option");

            return true;
        }

        public override void Execute(TextWriter outputWriter)
        {
            if (!_filesystem.DirectoryExists(Options.ScriptDirectory))
                _filesystem.CreateDirectory(Options.ScriptDirectory);

            string[] existingMigrations = _filesystem.GetFilesInDirectory(Options.ScriptDirectory, "*.boo");
            int nextMigrationNumber = existingMigrations.Length + 1;

            string name = ConcatArgsToMakeName();
            string migrationFilename = BuildMigrationFilename(nextMigrationNumber, name);

            string fullPath = _filesystem.PathCombine(Options.ScriptDirectory, migrationFilename);

            string template = GetMigrationTemplate(nextMigrationNumber, name);

            _filesystem.CreateFile(fullPath, template);

            outputWriter.WriteLine("Created {0}", fullPath);            
        }

        string GetMigrationTemplate(int migrationNumber, string migrationName)
        {
            var resourceStream = GetType().Assembly.GetManifestResourceStream("evo.Core.Resources.Migration.template");
            if(resourceStream == null)
                throw new InvalidOperationException("Could not find migration template in the assembly");

            StreamReader reader = new StreamReader(resourceStream);
            var template = reader.ReadToEnd();

            template = template.Replace("{NAME}", migrationName);
            template = template.Replace("{NUMBER}", migrationNumber.ToString());

            return template;
        }

        string ConcatArgsToMakeName()
        {
            var nameBuilder = new StringBuilder();
            Options.AdditionalArgs.Each(a => nameBuilder.Append(a + " "));
            return nameBuilder.ToString().Trim();
        }

        string BuildMigrationFilename(int migrationNumber, string migrationName)
        {
            return string.Format("{0}-{1}.boo",
                                 migrationNumber.ToString().PadLeft(NumberOfDigits, '0'),
                                 migrationName.Replace(' ', '-')
                );
        }
    }
}