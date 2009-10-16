using evo.Core;
using evo.Core.Commands;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Commands
{
    [Subject("Commands")]
    public class when_executing_ListCommand_with_invalid_script_directory : with_ListCommand
    {
        const string InvalidScriptDirectory = "migrations";

        Establish context = () => {
                                MockFileSystem.Stub(fs => fs.DirectoryExists(InvalidScriptDirectory)).Return(false);
                                Command = new ListCommand(MockFileSystem, MockDatabase,
                                                          new EvoOptions {ScriptDirectory = InvalidScriptDirectory});
                            };

        Because of = () => Command.Execute(Out);

        It should_output_an_error = () => Out.ToString().ShouldContain(string.Format("The script directory {0} doesn't exist", InvalidScriptDirectory));

        It should_NOT_try_to_get_files_in_the_directory = () => 
                                                          MockFileSystem.AssertWasNotCalled(fs=>fs.GetFilesInDirectory(null, null), options=>options.IgnoreArguments());

    }
}