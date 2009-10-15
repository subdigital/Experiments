using System.Data;
using evo.Core.Providers;
using Machine.Specifications;

namespace evo.Tests.Providers.SqlServerProvider
{
    [Subject("SqlServerProvider")]
    public class create_basic_table_syntax : with_SqlServerProvider
    {
        Because of = () => Syntax = SqlServerProvider.GetCreateTableSyntax("Foo",
                                                                           new Column("Id", DbType.Int32, false) { PrimaryKey = true },
                                                                           new Column("Name", DbType.String, 255, true),
                                                                           new Column("DateCreated", DbType.DateTime, false) {Default="getdate()"}
                                        );

        It should_have_the_correct_syntax = () => Syntax.ShouldEqual(
                                                      "CREATE TABLE [Foo] ("+
                                                      "\r\n\t[Id] int NOT NULL PRIMARY KEY," + 
                                                      "\r\n\t[Name] nvarchar(255) NULL,"  +
                                                      "\r\n\t[DateCreated] datetime NOT NULL DEFAULT getdate()" +
                                                      "\r\n)");
    }
}