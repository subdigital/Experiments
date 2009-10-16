using System;
using System.Data;

namespace evo.Core.Providers
{
    public class Column
    {
        public Column(string name, DbType type, bool nullable)
        {
            Name = name;
            Type = type;
            Nullable = nullable;
        }

        public Column(string name, DbType type, int length, bool nullable)
            :this(name, type, nullable)
        {
            Length = length;
        }

        public string Name { get; set; }
        public DbType Type { get; set; }
        public int Length { get; set; }
        public bool Nullable { get; set; }
        public bool PrimaryKey { get; set; }
        public bool Identity { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public string Default { get; set; }
        public bool Unique { get; set; }
    }
}