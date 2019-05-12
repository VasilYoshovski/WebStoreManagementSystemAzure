using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class WIMTeams : IEnumerable<ITeam>, IWIMTeams
    {
        private readonly IDictionary<string, ITeam> teamsList;

      //  private readonly static IWIMTeams instance;

        //static WIMTeams()
        //{
        //    instance = new WIMTeams();
        //}

        public WIMTeams()
        {
            this.teamsList = new Dictionary<string, ITeam>();
        }

        //public static IWIMTeams Instance
        //{
        //    get { return instance; }
        //}

        public IDictionary<string, ITeam> TeamsList
        {
            get => new Dictionary<string, ITeam>(this.teamsList);
        }
        
        public void AddTeam(ITeam newTeam)
        {
            this.teamsList.Add(newTeam.TeamName, newTeam);
        }
                       
        public ITeam this[string index]
        {
            get => this.teamsList[index];
            private set
            {
                this.teamsList[index] = value;
            }
        }

        public void RemoveTeam(ITeam removeTeam)
        {
            this.RemoveTeam(removeTeam.TeamName);
        }
        
        public void RemoveTeam(string teamName)
        {
            if (this.teamsList.ContainsKey(teamName))
            {
                throw new ArgumentException($"Team with {teamName} does not exists");
            }
            this.teamsList.Remove(teamName);
        }

        public bool Contains(string teamName)
        {
            return (this.teamsList.ContainsKey(teamName));

        }

        public IEnumerator<ITeam> GetEnumerator()
        {
            foreach (var item in this.teamsList)
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ShowAllTeams()
        {
            StringBuilder str = new StringBuilder();
            if (this.TeamsList.Count == 0)
            {
                str.AppendLine("There are no teams created!");
            }
            else
            {
                str.AppendLine("All Teams:");
                foreach (var item in this.TeamsList)
                {
                    str.AppendLine(" - "+item.Value.TeamName);
                }
            }
            return str.ToString();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var team in this.TeamsList)
            {
                str.AppendLine($"Team name: {team.Value.TeamName}");
                str.AppendLine("  List of members:");
                foreach (var member in team.Value.MemberList)
                {
                    str.AppendLine($"    {member.PersonName}");
                }
                str.AppendLine("  List of boards:");
                foreach (var board in team.Value.BoardList)
                {
                    str.AppendLine($"    {board.Value.BoardName}");
                    str.AppendLine("      items in the board:");
                    foreach (var item in board.Value.BoardWorkItems)
                    {
                        str.AppendLine($"        {item.Value.Title}: {item.Value.Description}");
                    }
                }
            }

            return str.ToString();
        }
    }
}
