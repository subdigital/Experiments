using Machine.Specifications;

namespace evo.Tests.Program
{
    [Subject("Program")]
    public class when_running_with_no_arguments : with_program
    {
        private Because of = () => program.Run(new string[]{});

        It should_print_usage =()=> Out.ToString().ShouldContain("USAGE");
    }
}