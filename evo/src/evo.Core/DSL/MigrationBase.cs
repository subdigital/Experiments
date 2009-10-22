using System;
using System.Collections.Generic;
using System.Diagnostics;
using evo.Core.Steps;

namespace evo.Core.DSL
{
    public abstract class MigrationBase
    {
        IList<IMigrationStep> _currentStepList;

        protected MigrationBase()
        {
            UpSteps = new List<IMigrationStep>();
            DownSteps = new List<IMigrationStep>();
        }

        public string Name
        {
            get; private set;
        }

        public int Version
        {
            get; private set;
        }

        public Action UpAction
        {
            get; private set;
        }

        public Action DownAction
        {
            get; private set;
        }

        public IList<IMigrationStep> UpSteps
        {
            get; private set;
        }

        public IList<IMigrationStep> DownSteps
        {
            get; private set;
        }

        protected void migration(int version)
        {
            Version = version;
        }

        protected void migration(int version, string name)
        {
            Version = version;
            Name = name;
        }

        protected void up(Action up)
        {
            _currentStepList = UpSteps;
            UpAction = up;
        }

        protected void down(Action down)
        {
            _currentStepList = DownSteps;
            DownAction = down;
        }

        protected void exec(string sql)
        {
            _currentStepList.Add(new ExecuteSQLStep(sql));
        }

        protected void log(string text)
        {
            Debug.WriteLine(text);
        }

        public abstract void Execute();
    }
}
