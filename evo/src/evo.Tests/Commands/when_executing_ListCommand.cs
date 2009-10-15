using evo.Core;
using evo.Core.Commands;
using Machine.Specifications;

namespace evo.Tests.Commands
{
    [Subject("Commands")]
    public class when_executing_ListCommand : with_ListCommand
    {
        Establish context = () => 
                                Command = new ListCommand(MockFileSystem, MockDatabase,
                                                          new EvoOptions {ScriptDirectory = "migrations"});

        Because of = () => Command.Execute(Out);
    }
}