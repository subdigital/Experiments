using Rhino.Mocks;

namespace evo.Tests
{
    public abstract class SpecificationBase
    {
        protected static T Stub<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        protected static T PartialMock<T>() where T : class
        {
            return MockRepository.GeneratePartialMock<T>();
        }
    }
}