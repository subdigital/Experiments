using System;

namespace evo.Core.Commands
{
    public class CommandNameAttribute : Attribute
    {
        public string Name { get; set; }

        public CommandNameAttribute(string name)
        {
            Name = name;
        }
    }
}