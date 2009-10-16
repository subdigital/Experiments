using Machine.Specifications;

namespace evo.Tests.Program
{
    public abstract class with_program_and_empty_args : with_null_program
    {
        Establish context = () => program = new evo.Program(new string[]{});
    }
}