using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.Contracts
{
    public interface IGetters
    {
        IBoard GetBoard(string teamName, string boardAsString);
        IPerson GetMember(ITeam team, string memberAsString);
        IPerson GetPerson(string memberAsString);
        ITeam GetTeam(string teamAsString);
        IWorkItem GetWorkItem(IBoard board, string workItemAsString);
        IAssignableWorkItem GetAssignableWorkItem(IBoard board, string assignableWorkItemTitle);
    }
}