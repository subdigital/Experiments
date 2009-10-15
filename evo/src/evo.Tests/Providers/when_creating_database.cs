using evo.Core.Providers;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Providers
{
    [Subject("Database")]
    public class when_creating_database : with_Database
    {
        Establish context = () => {
            Provider.Stub(p => p.GetCreateDatabaseSyntax("test")).Return(CreateDatabaseCommand);
            Provider.Stub(p => p.GetCreateTableSyntax("MigrationInfo", new Column[] {}))
                .IgnoreArguments().Return(CreateMigrationTableCommand);
        };

        Because of = () => Database.CreateDatabase("test");

        It should_request_create_table_syntax_from_the_provider = () => 
            Provider.AssertWasCalled(p=>p.GetCreateDatabaseSyntax("test"));

        It should_request_create_MigrationInfo_table_syntax_from_the_provider = () =>
            Provider.AssertWasCalled(p => p.GetCreateTableSyntax("MigrationInfo", new Column[] { }), 
                options => options.IgnoreArguments());

        It should_create_database = () => 
            Database.AssertWasCalled(d => d.ExecuteQuery(CreateDatabaseCommand));
        
        It should_create_migration_table = () => 
            Database.AssertWasCalled(d => d.ExecuteQuery(CreateMigrationTableCommand));
                    
        const string CreateDatabaseCommand = "CREATE DATABASE [Test]";
        const string CreateMigrationTableCommand = "CREATE TABLE MigrationInfo ()";
    }
}