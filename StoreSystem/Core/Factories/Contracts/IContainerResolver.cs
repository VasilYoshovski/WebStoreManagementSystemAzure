using WIMSystem.Commands.Contracts;

namespace WIMSystem.Core.Factories
{
    public interface IContainerResolver<T>
    {
        T GetService(string namedService);
    }
}