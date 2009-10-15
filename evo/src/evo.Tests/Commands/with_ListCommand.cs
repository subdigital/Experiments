using evo.Core;
using evo.Core.Commands;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.Commands
{
    public abstract class with_ListCommand : with_MockDatabase_and_TextWriter
    {
        protected static IFileSystem MockFileSystem;
        protected static ListCommand Command;

        Establish context = () => MockFileSystem = MockRepository.GenerateStub<IFileSystem>();
    }
}