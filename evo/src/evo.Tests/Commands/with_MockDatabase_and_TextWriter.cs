using evo.Core.Providers;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Commands
{
    public abstract class with_MockDatabase_and_TextWriter : SpecificationBase
    {
        protected static IDatabase MockDatabase;

        Establish context = () => {
                                MockDatabase = MockRepository.GenerateStub<IDatabase>();                                
                            };
    }
}