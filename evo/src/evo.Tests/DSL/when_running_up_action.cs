using evo.Core.DSL;
using evo.Core.Extensions;
using evo.Core.Steps;
using Machine.Specifications;

namespace evo.Tests.DSL
{
    public class when_running_up_action_with_two_exec_statements : 
        migration_spec<MigrationWithUpActionAndTwoExecStatements>
    {
        Because of = () => MigrationModel.UpAction();

        It should_have_two_up_steps = () => MigrationModel.UpSteps.Count.ShouldEqual(2);

        It should_contain_two_ExecuteSQLStep_entries =
            () => MigrationModel.UpSteps.Each(step => step.ShouldBeOfType<ExecuteSQLStep>());

        It should_have_zero_down_steps = () => MigrationModel.DownSteps.Count.ShouldEqual(0);
    }

    public class MigrationWithUpActionAndTwoExecStatements : MigrationBase
    {        
        public override void Execute()
        {
            up(()=> {
                exec("CREATE TABLE [Foo] (int ID not null)");           
                exec("CREATE TABLE [Bar] (int ID not null)");
            });
        }
    }
}