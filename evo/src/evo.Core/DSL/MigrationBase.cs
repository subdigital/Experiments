using System;

namespace evo.Core.DSL
{
    public abstract class MigrationBase
    {
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
            UpAction = up;
        }

        protected void down(Action down)
        {
            DownAction = down;
        }

        protected void exec(string sql)
        {
            //execute sql
        }
    }
}
