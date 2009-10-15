using Machine.Specifications;

namespace evo.Tests.Providers.SqlServerProvider
{
    [Subject("SqlServerProvider")]
    public class create_database_syntax : with_SqlServerProvider
    {     
        Because of = () => Syntax = SqlServerProvider.GetCreateDatabaseSyntax("test");

        It should_be_formatted_correctly = () => Syntax.ShouldEqual("CREATE DATABASE [test]");
    }
}