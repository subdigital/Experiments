using Machine.Specifications;

namespace evo.Tests.Program
{
    public class when_running_with_unknown_command : with_program
    {
        Because of = () => program.Run(new[]{"some_unknown_command"});

        It should_print_an_error_message = () => Out.ToString().ShouldContain("Unknown command: some_unknown_command");
    }
}