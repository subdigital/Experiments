using Boo.Lang.Compiler;
using Rhino.DSL;

namespace evo.Core.DSL
{
    public class EvoEngine : DslEngine
    {
        protected override void CustomizeCompiler(BooCompiler compiler, CompilerPipeline pipeline, 
            string[] urls)
        {
            int step = 1;
            pipeline.Insert(step++, new ImplicitBaseClassCompilerStep(typeof (MigrationBase), "Execute", "evo.Core.DSL"));
            pipeline.Insert(step++, new AutoImportCompilerStep("System", "evo.Core", "evo.Core.DSL"));
            pipeline.Insert(step++, new UseSymbolsStep());
            pipeline.Insert(step++, new AutoReferenceFilesCompilerStep());
        }
    }
}