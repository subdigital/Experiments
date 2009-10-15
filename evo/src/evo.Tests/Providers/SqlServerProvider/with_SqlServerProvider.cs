using evo.Core.Providers;
using Machine.Specifications;

namespace evo.Tests.Providers.SqlServerProvider
{
    public abstract class with_SqlServerProvider
    {
        protected static string Syntax;
        protected static IDatabaseProvider SqlServerProvider;

        Establish context = () => SqlServerProvider = new Core.Providers.SqlServerProvider();
    }

}