using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Providers
{
    [Subject("Database")]
    public class when_creating_database : with_Database
    {
        Establish context = () => Provider.Stub(p => p.GetCreateDatabaseSyntax("test")).Return(CreateDatabaseCommand);        

        Because of = () => Database.CreateDatabase("test");

        It should_request_create_table_syntax_from_the_provider = () => 
            Provider.AssertWasCalled(p=>p.GetCreateDatabaseSyntax("test"));

        
        It should_create_database = () => 
            Database.AssertWasCalled(d => d.ExecuteQuery(CreateDatabaseCommand));
                            
        const string CreateDatabaseCommand = "CREATE DATABASE [Test]";
    }
}