using Machine.Specifications;

namespace evo.Tests.ArgumentParser
{
    [Subject(typeof(evo.ArgumentParser), "Parsing values")]
    public class when_pulling_valid_option
    {
        Establish context = () => parser = new evo.ArgumentParser("someCommand --someArg someValue");

        Because of = () => value = parser.Option("someArg");

        It should_return_the_correct_value = () => value.ShouldEqual("someValue");

        static string value;
        static evo.ArgumentParser parser;
    }
}