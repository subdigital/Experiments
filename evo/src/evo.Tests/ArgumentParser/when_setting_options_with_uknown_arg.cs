using evo.Core;
using Machine.Specifications;

namespace evo.Tests.ArgumentParser
{
    [Subject(typeof(evo.ArgumentParser), "Setting options")]
    public class when_setting_options_with_uknown_arg
    {
        Establish context = () =>
        {
            parser = new evo.ArgumentParser("cmd --foo bar");
            options = new EvoOptions();
        };

        Because of = () => parser.SetOptions(options);

        It should_be_NOT_be_valid = () => parser.IsValid.ShouldBeFalse();

        It should_set_error_message = () => parser.ErrorMessage.ShouldEqual("Unknown argument:foo");

        static evo.ArgumentParser parser;
        static EvoOptions options;
    }
}