using System;
using System.IO;
using System.Text;
using Machine.Specifications;

namespace evo.Tests
{    
    public abstract class with_null_program
    {
        protected static Program program;
    }

    public abstract class with_program_and_empty_args : with_null_program
    {
        Establish context = () => program = new Program(new string[]{});
    }

    public abstract class with_list_command
    {
        
    }

    [Subject("Program")]
    public class when_creating : with_program_and_empty_args
    {
        private Because of = () => program = new Program(new string[] {});
        
        It should_output_to_the_console =()=> 
            program.Out.ShouldEqual(Console.Out);
    }

    [Subject("Program")]
    public class when_running_with_no_arguments : with_null_program
    {
        private static StringBuilder outputBuilder;

        private Because of = () =>
                                 {
                                     outputBuilder = new StringBuilder();
                                     program = new Program(new string[] {}) {Out = new StringWriter(outputBuilder)};
                                     program.Run();
                                 };

        It should_print_usage =()=> outputBuilder.ToString().ShouldContain("USAGE");
    }

    [Subject("Program")]
    public class when_running_without_database_args : with_list_command
    {
        
    }
}