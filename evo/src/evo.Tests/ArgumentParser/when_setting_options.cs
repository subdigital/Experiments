using evo.Core;
using Machine.Specifications;

namespace evo.Tests.ArgumentParser
{
    [Subject(typeof(evo.ArgumentParser), "Setting options")]
    public class when_setting_options
    {
        Establish context = () => {
            parser = new evo.ArgumentParser("cmd --server SERVER --db DB --config CONFIG --username USER --password PASSWORD");
            options = new EvoOptions();
        };

        Because of = () => parser.SetOptions(options);

        It should_be_valid = () => parser.IsValid.ShouldBeTrue();

        It should_set_server = () => options.ServerName.ShouldEqual("SERVER");
        It should_set_db = () => options.Database.ShouldEqual("DB");
        It should_set_config = () => options.ConfigPath.ShouldEqual("CONFIG");
        It should_set_username = () => options.Username.ShouldEqual("USER");
        It should_set_password = () => options.Password.ShouldEqual("PASSWORD");

        static evo.ArgumentParser parser;
        static EvoOptions options;
    }
}