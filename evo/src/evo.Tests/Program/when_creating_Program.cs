using System;
using Machine.Specifications;

namespace evo.Tests.Program
{
    [Subject("Program")]
    public class when_creating_Program : with_program_and_empty_args
    {
        private Because of = () => program = new evo.Program(new string[] {});
        
        It should_output_to_the_console =()=> 
                                         program.Out.ShouldEqual(Console.Out);
    }
}