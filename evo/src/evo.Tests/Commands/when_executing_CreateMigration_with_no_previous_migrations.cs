using System.IO;
using evo.Core;
using evo.Core.Commands;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace evo.Tests.Commands
{
    [Subject(typeof(CreateMigrationCommand), "Executing")]
    public class when_executing_CreateMigration_with_no_previous_migrations  : with_FileSystem
    {
        Establish context = () => {
            MockFileSystem.Stub(fs => fs.DirectoryExists("migrations")).Return(true);
            MockFileSystem.Stub(fs => fs.GetFilesInDirectory("migrations", "*.boo")).Return(new string[] {});
            MockFileSystem.Stub(fs => fs.PathCombine(null, null))
                .IgnoreArguments()
                .Do(new PathCombineDelegate(Path.Combine));

            Command = new CreateMigrationCommand(MockFileSystem, MockDatabase,
                                                 new EvoOptions {AdditionalArgs = new[] {"test"}});
        };

        Because of = () => Command.Execute(Out);

        It should_get_numbered_as_001 = () =>
            MockFileSystem.AssertWasCalled(
                fs => fs.CreateFile(null, null),                 
                options => options.Constraints(
                    Is.Equal("migrations\\001-test.boo"),
                    Is.Anything()
                ));

        It should_output_the_created_file_to_the_console =
            () => Out.ToString().ShouldContain("Created migrations\\001-test.boo");

        static ICommand Command;
        delegate string PathCombineDelegate(string x, string y);
    }
}