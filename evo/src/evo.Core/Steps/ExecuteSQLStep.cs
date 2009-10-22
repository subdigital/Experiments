using evo.Core.Providers;

namespace evo.Core.Steps
{
    public class ExecuteSQLStep : IMigrationStep
    {
        readonly string _sql;

        public ExecuteSQLStep(string sql)
        {
            _sql = sql;
        }

        public void Run(IDatabase database)
        {
            database.ExecuteQuery(_sql);
        }
    }
}