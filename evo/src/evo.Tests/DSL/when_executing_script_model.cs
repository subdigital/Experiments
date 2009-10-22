using evo.Core.DSL;
using Machine.Specifications;

namespace evo.Tests.DSL
{
    [Subject("DSL")]
    public class when_executing_script_model : dsl_spec
    {
        Establish context = () => Model = Factory.Create<MigrationBase>(SimpleMigration, null);

        Because of = () => Model.Execute();

        It should_set_the_version = () => Model.Version.ShouldEqual(1);

        It should_set_the_name = () => Model.Name.ShouldEqual("Simple Migration");

        It should_set_up_action = () => Model.UpAction.ShouldNotBeNull();

        It should_set_down_action = () => Model.DownAction.ShouldNotBeNull();
    }
}