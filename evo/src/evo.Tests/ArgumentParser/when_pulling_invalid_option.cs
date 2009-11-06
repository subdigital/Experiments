using Machine.Specifications;

namespace evo.Tests.ArgumentParser
{
    [Subject(typeof(evo.ArgumentParser), "Parsing values")]
    public class when_pulling_invalid_option
    {
        Establish context = () => parser = new evo.ArgumentParser("someCommand someArg");

        Because of = () => value = parser.Option("argThatDoesntExist");

        It should_return_null = () => value.ShouldBeNull();

        static string value;
        static evo.ArgumentParser parser;
    }
}