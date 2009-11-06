using System.IO;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests
{
    public abstract class SpecificationBase
    {
        protected static TextWriter Out;

        Establish context = () => Out = new StringWriter();

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