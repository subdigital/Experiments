using evo.Core;
using evo.Core.Commands;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Commands
{
    [Subject("Commands")]
    public class when_executing_CreateDatabaseCommand : with_MockDatabase_and_TextWriter
    {
        Establish context = () => {
            var options = new EvoOptions {Database = "TestDatabase"};
            Command = new CreateDatabaseCommand(MockDatabase, options);
        };

        Because of = () => 
                Command.Execute(Out);

        It should_create_the_database_with_the_appropriate_name = () =>
                MockDatabase.AssertWasCalled(d => d.CreateDatabase("TestDatabase"));

        It should_create_the_MigrationInfo_table = () => 
                MockDatabase.AssertWasCalled(d=>d.CreateMigrationTable());

        It should_output_status_to_the_console = () =>
                Out.ToString().ShouldContain("Created database [TestDatabase]");

        static CreateDatabaseCommand Command;
    }
}