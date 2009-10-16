using System.Collections.Generic;
using evo.Core;
using evo.Core.Commands;
using Machine.Specifications;

namespace evo.Tests.Commands
{
    [Subject(typeof(CreateMigrationCommand), "Validation")]
    public class when_validating_CreateMigrationCommand_with_no_additional_args : with_FileSystem
    {
        Establish context = () => Command = new CreateMigrationCommand(MockFileSystem, MockDatabase, new EvoOptions());

        Because of = () => valid = Command.IsValid();

        It should_return_false = () => valid.ShouldBeFalse();

        static ICommand Command;
        static bool valid;        
    }

    [Subject(typeof(CreateMigrationCommand), "Validation")]
    public class when_validating_CreateMigrationCommand_with_args : with_FileSystem
    {
        Establish context = () => Command = new CreateMigrationCommand(MockFileSystem, MockDatabase, 
            new EvoOptions {AdditionalArgs = new List<string> {"test"}});

        Because of = () => valid = Command.IsValid();

        It should_return_true = () => valid.ShouldBeTrue();

        static ICommand Command;
        static bool valid;
    }
}