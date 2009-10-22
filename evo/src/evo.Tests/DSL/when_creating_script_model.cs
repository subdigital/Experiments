using evo.Core.DSL;
using Machine.Specifications;

namespace evo.Tests.DSL
{
    [Subject("DSL")]
    public class when_creating_script_model : dsl_spec
    {
        Because of = () => Model = Factory.Create<MigrationBase>(SimpleMigration, null);

        It should_create_a_model = () => Model.ShouldNotBeNull();
    }
}