using System;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.Utils
{
    public class Getters : IGetters
    {
        protected readonly IPersonsCollection personList;
        protected readonly IWIMTeams wimTeams;

        public Getters(IPersonsCollection personList, IWIMTeams wimTeams)
        {
            this.personList = personList ?? throw new ArgumentNullException(
                                                                   string.Format(
                                                             CommandsConsts.NULL_OBJECT,
                                                                nameof(personList)));
            this.wimTeams = wimTeams ?? throw new ArgumentNullException(
                                                                string.Format(
                                                                CommandsConsts.NULL_OBJECT,
                                                                nameof(wimTeams)));
        }

        public IPerson GetPerson(string memberAsString)
        {
            if (!this.personList.Contains(memberAsString))
            {
                throw new ArgumentNullException(
                            string.Format(
                            CommandsConsts.NoPersonFound,
                            memberAsString));

            }
            var member = this.personList[memberAsString];
            return member;

        }

        public IPerson GetMember(ITeam team, string memberAsString)
        {
            if (!this.wimTeams.TeamsList.ContainsKey(team.TeamName))
            {
                throw new ArgumentException(
                            string.Format(
                            CommandsConsts.NoTeamFound,
                            team.TeamName));
            }
            if (!team.MemberList.Contains(this.GetPerson(memberAsString)))
            {
                throw new ArgumentNullException(
                            string.Format(
                            CommandsConsts.NoPersonInTeamFound,
                            memberAsString));
            }
            var person = this.GetPerson(memberAsString);//this.personList[memberAsString];
            
            return person;
        }

        public ITeam GetTeam(string teamAsString)
        {
            if (!this.wimTeams.TeamsList.ContainsKey(teamAsString))
            {
                throw new ArgumentException(
                            string.Format(
                            CommandsConsts.NoTeamFound,
                            teamAsString));

            }
            var team = this.wimTeams[teamAsString];
            return team;
        }

        public IBoard GetBoard(string teamName, string boardAsString)
        {
            //var teamResult = this.wimTeams.TeamsList
            //                .Select(team => team.Value)
            //                .Where(team => team.BoardList.Keys.Any(board => board == boardAsString))
            //                .Single();
            if (!this.wimTeams.TeamsList.ContainsKey(teamName))
            {
                throw new ArgumentException(
                            string.Format(
                            CommandsConsts.NoTeamFound,
                            teamName));
            }

            if (!this.wimTeams.TeamsList[teamName].BoardList.ContainsKey(boardAsString))
            {
                throw new ArgumentException(
                            string.Format(
                            CommandsConsts.NoBoardFound,
                            boardAsString));
            }

            var boardResult = this.wimTeams[teamName].BoardList[boardAsString];
            return boardResult;

        }

        public IWorkItem GetWorkItem(IBoard board, string workItemAsString)
        {
            if (!board.BoardWorkItems.ContainsKey(workItemAsString))
            {
                throw new ArgumentException(
                            string.Format(
                            CommandsConsts.NoWorkItemFound,
                            workItemAsString));
            }
            return board.BoardWorkItems[workItemAsString];
        }

        public IAssignableWorkItem GetAssignableWorkItem(IBoard board, string assignableWorkItemTitle)
        {
            if (board.BoardWorkItems[assignableWorkItemTitle] is IAssignableWorkItem)
            {
                return (IAssignableWorkItem)board.BoardWorkItems[assignableWorkItemTitle];
            }

            throw new ArgumentException(string.Format($"{board.BoardWorkItems[assignableWorkItemTitle].GetType().Name} is not assignable work item!"));
        }
    }
}
