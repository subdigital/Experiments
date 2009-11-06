using System;
using Machine.Specifications;

namespace evo.Tests.Program
{
    [Subject("Program")]
    public class when_creating_Program
    {
        Because of = () => program = new evo.Program();

        It should_output_to_the_console =()=> program.Out.ShouldEqual(Console.Out);

        static evo.Program program;
    }
}