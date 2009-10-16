using evo.Core;
using evo.Core.Commands;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Commands
{
    [Subject("Commands")]
    public class when_executing_ListCommand : with_ListCommand
    {
        Establish context = () => {
                                MockFileSystem.Stub(fs => fs.DirectoryExists("migrations")).Return(true);
                                MockFileSystem.Stub(fs => fs.GetFilesInDirectory("migrations", "*.boo"))
                                    .Return(MigrationsInDirectory);

                                MockDatabase.Stub(db => db.CurrentMigration()).Return(3);

                                Command = new ListCommand(MockFileSystem, MockDatabase,
                                                          new EvoOptions {ScriptDirectory = "migrations"});
        };

        Because of = () => Command.Execute(Out);

        It should_Output_each_migration = () => {
                                              Out.ToString().ShouldContain("01-migration.boo");
                                              Out.ToString().ShouldContain("02-migration.boo");
                                              Out.ToString().ShouldContain("03-migration.boo");
                                              Out.ToString().ShouldContain("04-migration.boo");        
        };

        It should_check_the_current_migration = () => 
            MockDatabase.AssertWasCalled(db=>db.CurrentMigration());

        It should_put_a_marker_next_to_the_current_migration = () => 
            Out.ToString().ShouldContain("=> 03-migration.boo");

        It should_output_total_migrations = () => Out.ToString().ShouldContain("Total migrations: " + MigrationsInDirectory.Length);

        static string[] MigrationsInDirectory = new[]
                                                    {
                                                        "01-migration.boo",
                                                        "02-migration.boo",
                                                        "03-migration.boo",
                                                        "04-migration.boo"
                                                    };
    }
}