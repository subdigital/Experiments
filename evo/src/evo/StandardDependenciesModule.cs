using evo.Core;
using evo.Core.Providers;
using Ninject.Modules;

namespace evo
{
    public class StandardDependenciesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileSystem>().To<Filesystem>();
            Bind<IDatabaseProvider>().To<SqlServerProvider>();
            Bind<IDatabase>().To<Database>();
        }
    }
}