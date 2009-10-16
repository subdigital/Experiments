using Rhino.DSL;

namespace evo.Core.DSL
{
    public class EvoEngine : DslEngine
    {
        protected override void CustomizeCompiler(Boo.Lang.Compiler.BooCompiler compiler, Boo.Lang.Compiler.CompilerPipeline pipeline, string[] urls)
        {   
            base.CustomizeCompiler(compiler, pipeline, urls);
        }
    }
}