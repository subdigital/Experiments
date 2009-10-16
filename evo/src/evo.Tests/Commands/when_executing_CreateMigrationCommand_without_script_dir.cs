using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Commands
{
    [Subject("Commands")]
    public class when_executing_CreateMigrationCommand_without_script_dir : with_CreateMigrationCommand
    {
        Establish context = () => {
                                MockFileSystem.Stub(fs => fs.DirectoryExists("migrations")).Return(false);
                                MockFileSystem.Stub(fs => fs.GetFilesInDirectory("migrations", "*.boo"))
                                    .Return(new string[] {});
        };


        Because of = () => Command.Execute(Out);

        It should_create_the_directory = () => MockFileSystem.AssertWasCalled(fs=>fs.CreateDirectory("migrations"));
    }
}