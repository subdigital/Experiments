using System;
using System.Collections.Generic;
using System.Linq;
using evo.Core.DSL;
using evo.Core.Extensions;
using evo.Core.Providers;
using evo.Core.Steps;
using Rhino.DSL;

namespace evo.Core
{
    public class MigrationRunner
    {
        private readonly string _baseDirectory;
        readonly IDatabase _database;
        private readonly DslFactory _factory;

        public MigrationRunner(string baseDirectory, IDatabase database)
        {
            _baseDirectory = baseDirectory;
            _database = database;
            _factory = new DslFactory();
            _factory.Register<MigrationBase>(new EvoEngine());
        }

        public void Run()
        {
            Run(null);
        }

        public void Run(int? targetVersion)
        {
            int currentVersion = _database.CurrentMigration() ?? 0;
            var migrations = _factory.CreateAll<MigrationBase>(_baseDirectory);

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

        void MigrateUp(IEnumerable<MigrationBase> migrations)
        {
            foreach (var migration in migrations.OrderBy(m=>m.Version))
            {
                migration.UpAction();
                RunSteps(migration.UpSteps);
            }
        }

        void MigrateDown(IEnumerable<MigrationBase> migrations)
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
