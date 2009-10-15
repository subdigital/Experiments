using System.IO;
using System.Text;
using Machine.Specifications;

namespace evo.Tests.Program
{
    [Subject("Program")]
    public class when_running_with_no_arguments : with_null_program
    {
        private static StringBuilder outputBuilder;

        private Because of = () =>
                             {
                                 outputBuilder = new StringBuilder();
                                 program = new evo.Program(new string[] {}) {Out = new StringWriter(outputBuilder)};
                                 program.Run();
                             };

        It should_print_usage =()=> outputBuilder.ToString().ShouldContain("USAGE");
    }
}