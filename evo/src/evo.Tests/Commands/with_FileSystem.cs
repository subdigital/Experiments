using evo.Core;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Commands
{
    public abstract class with_FileSystem : with_MockDatabase_and_TextWriter
    {
        protected static IFileSystem MockFileSystem;

        Establish context = () => MockFileSystem = MockRepository.GenerateStub<IFileSystem>();
    }
}