using evo.Core;
using evo.Core.Providers;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Providers
{
    public class with_Database
    {
        protected static IDatabase Database;
        protected static IDatabaseProvider Provider;

        Establish context = () => {
                                var options = new EvoOptions();
                                Provider = MockRepository.GenerateStub<IDatabaseProvider>();
                                Database = MockRepository.GeneratePartialMock<Database>(Provider, options);

                                Database.Stub(x => x.ExecuteQuery("")).IgnoreArguments();
                            };
    }
}