using evo.Core;
using evo.Core.Commands;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Commands
{
    [Subject("Commands")]
    public class when_executing_DropDatabaseCommand : with_MockDatabase_and_TextWriter
    {
        protected static DropDatabaseCommand Command;

        Establish context = () => {
                                Command = new DropDatabaseCommand(MockDatabase, new EvoOptions {Database = "TestDatabase"});
                            };

        Because of = () => Command.Execute(Out);

        It should_drop_the_database = () =>
                                      MockDatabase.AssertWasCalled(d => d.DropDatabase("TestDatabase"));

        It should_ouput_to_the_console = () =>
                                         Out.ToString().ShouldContain("Dropped database [TestDatabase]");
                                    
    }
}