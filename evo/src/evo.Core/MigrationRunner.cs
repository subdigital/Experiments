using System.Collections.Generic;
using System.Linq;
using evo.Core.DSL;
using evo.Core.Extensions;
using evo.Core.Providers;
using evo.Core.Steps;

namespace evo.Core
{
    public class MigrationRunner
    {
        readonly IMigrationFactory _migrationFactory;
        readonly IDatabase _database;

        public MigrationRunner(IMigrationFactory migrationFactory, IDatabase database)
        {
            _migrationFactory = migrationFactory;
            _database = database;
        }

        public void Run()
        {
            Run(null);
        }

        public void Run(int? targetVersion)
        {
            int currentVersion = _database.CurrentMigration() ?? 0;
            var migrations = _migrationFactory.CreateAll();

            if(targetVersion == null)
                targetVersion = migrations.Max(m => m.Version);

            bool up = currentVersion <= targetVersion;

            var migrationsToRun = migrations
                .Where(m => m.Version.Between(currentVersion, targetVersion.Value));

            if (up)
                MigrateUp(migrationsToRun);
            else
                MigrateDown(migrationsToRun);
        }

        public virtual void MigrateUp(IEnumerable<MigrationBase> migrations)
        {
            foreach (var migration in migrations.OrderBy(m=>m.Version))
            {
                migration.UpAction();
                RunSteps(migration.UpSteps);
            }
        }

        public virtual void MigrateDown(IEnumerable<MigrationBase> migrations)
        {
            foreach (var migration in migrations.OrderByDescending(m => m.Version))
            {
                migration.DownAction();
                RunSteps(migration.DownSteps);
            }
        }

        void RunSteps(IList<IMigrationStep> migrationSteps)
        {
        }
    }
}
