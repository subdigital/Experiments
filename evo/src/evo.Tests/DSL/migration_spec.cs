using evo.Core.DSL;
using Machine.Specifications;

namespace evo.Tests.DSL
{
    public abstract class migration_spec<TMigrationModel> : SpecificationBase where TMigrationModel : MigrationBase, new()
    {
        protected static MigrationBase MigrationModel;

        Establish context = () => {
            MigrationModel = new TMigrationModel();
            MigrationModel.Execute();
        };
    }
}