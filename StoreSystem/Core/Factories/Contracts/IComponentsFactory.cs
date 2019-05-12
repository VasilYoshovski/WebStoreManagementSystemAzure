using System.Collections.Generic;
using WIMSystem.Core.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Core.Factories.Contracts
{
    public interface IComponentsFactory
    {
        IBoard CreateBoard(string name, ITeam team);

        IBug CreateBug(string title, string description, IList<string> stepsToReproduce, PriorityType priority, BugSeverityType severity, IBoard board, IPerson assignee = null);

        IComment CreateComment(string message, IPerson author);

        IFeedback CreateFeedback(string title, string description, int rating, IBoard board);

        IHistoryItem CreateHistoryItem(string description, IPerson member, IBoard board, ITeam team, IWorkItem workItem);

        IPerson CreatePerson(string personName);

        IStory CreateStory(string title, string description, PriorityType priority, StorySizeType storySize, IBoard board, IPerson assignee = null);

        ITeam CreateTeam(string teamName);

        ICommand CreateCommand(string commandName, IList<string> parameters);
    }
}