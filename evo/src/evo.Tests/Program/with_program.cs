using Machine.Specifications;

namespace evo.Tests.Program
{
    public abstract class with_program : SpecificationBase
    {
        protected static evo.Program program;

        Establish context = () => program = new evo.Program {Out = Out};
        
    }    
}