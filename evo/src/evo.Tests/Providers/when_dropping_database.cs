using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Providers
{
    [Subject("Database")]
    public class when_dropping_database : with_Database
    {
        Establish context = () => Provider.Stub(p=>p.GetDropDatabaseSyntax("test"))
            .Return(DropDatabaseCommand);

        Because of = () => Database.DropDatabase("test");

        It should_get_the_syntax_from_the_provider = () => Provider.AssertWasCalled(p=>p.GetDropDatabaseSyntax("test"));

        It should_drop_the_database = () => Database.AssertWasCalled(d=>d.ExecuteQuery(DropDatabaseCommand));

        const string DropDatabaseCommand = "DROP DATABASE [test]";
    }
}