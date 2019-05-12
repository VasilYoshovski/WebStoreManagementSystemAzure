using WIMSystem.Core.Contracts;

namespace WIMSystem.Core
{
    public interface ICommandParser
    {
        ICommand Parse(string input);
    }
}