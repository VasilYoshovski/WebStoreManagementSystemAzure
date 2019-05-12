using WIMSystem.Commands.Contracts;

namespace WIMSystem.Core.Factories.Contracts
{
    public interface ICommandsFactory
    {
        IEngineCommand GetCommand(string commandName);
    }
}