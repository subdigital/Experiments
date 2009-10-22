using System.IO;
using evo.Core.DSL;
using Machine.Specifications;
using Rhino.DSL;

namespace evo.Tests.DSL
{
    public abstract class dsl_spec
    {
        Establish context = () =>
        {
            Factory = new DslFactory();
            Factory.Register<MigrationBase>(new EvoEngine());
        };


        protected static DslFactory Factory;
        protected static MigrationBase Model;

        protected static string BaseFolder =  "DSL\\Scripts";
        protected static string SimpleMigration = Path.Combine(BaseFolder, "simple_migration.boo");
    }
}