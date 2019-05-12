using System;
using System.Collections.Generic;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Core.Factories
{
    public class ComponentsFactory : IComponentsFactory
    {
        public ITeam CreateTeam(string teamName)
        {
            return new Team(teamName);
        }

        public IPerson CreatePerson(string personName)
        {
            return new Person(personName);
        }

        public IBoard CreateBoard(string name, ITeam team)
        {
            return new Board(name, team);
        }

        public IBug CreateBug(string title, string description, IList<string> stepsToReproduce, PriorityType priority, BugSeverityType severity, IBoard board, IPerson assignee = null)
        {
            return new Bug(title, description, stepsToReproduce, priority, severity, board);
        }

        public IFeedback CreateFeedback(string title, string description, int rating, IBoard board)
        {
            return new Feedback(title, description, rating, board);
        }

        public IStory CreateStory(string title, string description, PriorityType priority,
            StorySizeType storySize, IBoard board, IPerson assignee = null)
        {
            return new Story(title, description, priority, storySize, board);
        }

        public IComment CreateComment(string message, IPerson author)
        {
            return new Comment(message, author);
        }

        public ICommand CreateCommand(string commandName, IList<string> parameters)
        {
            return new Command(commandName, parameters);
        }

        public IHistoryItem CreateHistoryItem(string description, IPerson member, IBoard board, ITeam team, IWorkItem workItem)
        {
            return new HistoryItem(description, DateTime.Now, member, board, team, workItem);
        }
    }
}
