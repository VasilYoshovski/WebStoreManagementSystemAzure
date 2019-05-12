using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Core.Factories
{
    public class CommandsFactory : ICommandsFactory
    {
        private readonly IContainerResolver<IEngineCommand> containerResolver;

        public CommandsFactory(IContainerResolver<IEngineCommand> containerResolver)
        {
            this.containerResolver = containerResolver ?? throw new ArgumentNullException(nameof(containerResolver));
        }

        public IEngineCommand GetCommand(string commandName)
        {

            if (Validators.IsNullorEmptyValue(commandName))
            {
                throw new ArgumentException(string.Format(CommandsConsts.EmptyCommand, commandName));
            }

            try
            {
                return this.containerResolver.GetService(commandName);
            }
            catch (Exception)
            {
                throw new ArgumentException(string.Format(CommandsConsts.InvalidCommand, commandName));
            }

        }
    }
}
