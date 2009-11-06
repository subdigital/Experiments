using Rhino.DSL;

namespace evo.Core.DSL
{
    public class MigrationFactory : IMigrationFactory
    {
        readonly string _baseDir;
        readonly DslFactory _factory;

        public MigrationFactory(EvoOptions options)
        {
            _baseDir = options.ScriptDirectory;
            _factory = new DslFactory();
            _factory.Register<MigrationBase>(new EvoEngine());
        }

        public MigrationBase[] CreateAll()
        {
            return _factory.CreateAll<MigrationBase>(_baseDir, null);
        }
    }
}