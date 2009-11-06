using Machine.Specifications;

namespace evo.Tests.ArgumentParser
{
    [Subject(typeof(evo.ArgumentParser), "Parsing values")]
    public class when_parsing_arg_with_missing_value
    {
        Establish context = () => parser = new evo.ArgumentParser("command --argWithoutValue");

        Because of = () => value = parser.Option("argWithoutValue");

        It should_not_be_valid = () => parser.IsValid.ShouldBeFalse();

        It should_contain_an_error_message = () => parser.ErrorMessage.ShouldEqual("Expected value for: argWithoutValue");

        It should_return_null_for_Value = () => value.ShouldBeNull();

        static evo.ArgumentParser parser;
        static string value;
    }
}