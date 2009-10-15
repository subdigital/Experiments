using System;
using evo.Core.Commands;
using evo.Core.Extensions;
using Ninject.Modules;

namespace evo
{
    public class CommandsModule : NinjectModule
    {
        public override void Load()
        {
            var asm = typeof (ICommand).Assembly;

            foreach(var commandType in asm.GetTypesImplementing<ICommand>())
            {
                string commandName = GetCommandName(commandType);
                Bind<ICommand>().To(commandType).Named(commandName);
            }
        }

        string GetCommandName(Type commandType)
        {
            string defaultName = commandType.Name.Replace("Command", "");
            string commandName = commandType.HasAttribute<CommandNameAttribute>() ? 
                commandType.GetAttribute<CommandNameAttribute>().Name : 
                defaultName;
            return commandName;
        }
    }
}