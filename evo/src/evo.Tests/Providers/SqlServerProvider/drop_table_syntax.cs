using Machine.Specifications;

namespace evo.Tests.Providers.SqlServerProvider
{
    [Subject("SqlServerProvider")]
    public class drop_table_syntax : with_SqlServerProvider
    {
        Because of = () => Syntax = SqlServerProvider.GetDropDatabaseSyntax("test");

        It should_be_formatted_correctly = () => Syntax.ShouldEqual("DROP DATABASE [test]");
    }
}