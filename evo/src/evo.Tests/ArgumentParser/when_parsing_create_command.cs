using evo.Core;
using Machine.Specifications;

namespace evo.Tests.ArgumentParser
{
    [Subject(typeof(evo.ArgumentParser), "Create command")]
    public class when_parsing_create_command
    {
        Establish context = () => {
            Options = new EvoOptions();
            Parser = new evo.ArgumentParser(new[] {"create", "test"});
        };

        Because of = () => Parser.SetOptions(Options);

        It should_be_valid = () => Parser.IsValid.ShouldBeTrue();

        It should_parse_command = () => Options.Command.ShouldEqual("create");

        It should_parse_one_additional_arg = () => Options.AdditionalArgs.Count.ShouldEqual(1);
        
        It should_parse_arg = () => Options.AdditionalArgs[0].ShouldEqual("test");

        static evo.ArgumentParser Parser;
        static EvoOptions Options;
    }
}