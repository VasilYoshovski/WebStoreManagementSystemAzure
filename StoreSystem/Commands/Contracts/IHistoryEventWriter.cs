using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.Contracts
{
    public interface IHistoryEventWriter
    {
        void AddHistoryEvent(string description, IPerson member = null, IBoard board = null, ITeam team = null, IWorkItem workItem = null);
    }
}