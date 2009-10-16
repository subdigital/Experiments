using evo.Core.DSL;
using Rhino.DSL;

namespace evo.Core
{
    public class MigrationRunner
    {
        private readonly string _baseDirectory;
        private readonly DslFactory _factory;

        public MigrationRunner(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
            _factory = new DslFactory();
            _factory.Register<MigrationBase>(new EvoEngine());
        }        

        public void Run(EvoOptions options)
        {
            MigrationBase[] migrations = _factory.CreateAll<MigrationBase>(_baseDirectory);                        
        }
    }
}
