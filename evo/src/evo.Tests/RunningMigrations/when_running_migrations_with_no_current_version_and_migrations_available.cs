using System;
using evo.Core;
using evo.Core.DSL;
using evo.Core.Providers;
using Machine.Specifications;
using Rhino.Mocks;

namespace evo.Tests.RunningMigrations
{
    public class when_running_migrations_with_no_current_version_and_migrations_available : SpecificationBase
    {
        Establish context = () => {
            //create database
            var database = Stub<IDatabase>();
            database.Stub(db => db.CurrentMigration()).Return(null);

            //create existing migrations
            var migrationFactory = Stub<IMigrationFactory>();
            migration1 = PartialMock<MigrationBase>();
            migration1.Stub(m => m.Version).Return(1);
            migration1.Stub(m => m.UpAction());

            migration2 = PartialMock<MigrationBase>();
            migration2.Stub(m => m.Version).Return(2);
            migration2.Stub(m => m.UpAction());
    
            migrationFactory.Stub(mf => mf.CreateAll()).Return(new[]{migration1, migration2});

            runner = new MigrationRunner(migrationFactory, database);
        };

        Because of = () => runner.Run();

        It should_execute_both_migration_up_actions_in_order = () =>
            migration1.AssertWasCalled(m => m.UpAction());

        It should_execute_migration_2_up_action = () =>
            migration2.AssertWasCalled(m => m.UpAction());

        static MigrationBase migration1;
        static MigrationBase migration2;
        static MigrationRunner runner;
    }
}