using evo.Core;
using Machine.Specifications;

namespace evo.Tests.ArgumentParser
{
    [Subject(typeof(evo.ArgumentParser), "Database commands")]
    public class when_parsing_database_commands
    {
        Establish context = () => {
            Options = new EvoOptions();
            Parser = new evo.ArgumentParser("someCommand --db SomeDB --server localhost");
        };

        Because of = () => Parser.SetOptions(Options);

        It should_set_database = () => Options.Database.ShouldEqual("SomeDB");

        It should_set_server = () => Options.ServerName.ShouldEqual("localhost");

        It should_default_to_trusted = () => Options.TrustedConnection.ShouldBeTrue();

        static evo.ArgumentParser Parser;
        static EvoOptions Options;
    }
}