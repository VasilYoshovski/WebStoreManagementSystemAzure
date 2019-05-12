using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Team : ITeam
    {

        private string teamName;
        private readonly IList<IPerson> memberList;
        private readonly IDictionary<string, IBoard> boardList;


        public Team(string teamName)
        {
            this.TeamName = teamName;

            this.memberList = new List<IPerson>();
            this.boardList = new Dictionary<string, IBoard>();
        }

        public string TeamName
        {
            get
            {
                return this.teamName;
            }

            private set
            {
                if (value.Length < 3 || value.Length > 25)
                {
                    throw new ArgumentException("Teams name should be between 3 and 25 symbols.");
                }
                this.teamName = value;
            }
        }

        public IList<IPerson> MemberList
        {
            get
            {
                return new List<IPerson>(this.memberList);
            }
        }

        public IDictionary<string, IBoard> BoardList
        {
            get
            {
                return new Dictionary<string, IBoard>(this.boardList);
            }

        }

        public void AddMemberToTeam(IPerson member)
        {
            if (member == null)
            {
                throw new ArgumentException("Member cannot be null!");
            }

            if (this.memberList.Contains(member))
            {
                throw new ArgumentException($"This team already has {member.PersonName}");
            }

            else
            {
                member.IsAssignedToTeam = true;
                this.memberList.Add(member);

                //return $"{member.MemberName} is added to {this.TeamName} member list";
            }
        }

        public void RemoveMemberFromTeam(IPerson member)
        {
            if (member == null)
            {
                throw new ArgumentException($"Member can not be null.");

            }

            if (!this.memberList.Contains(member))
            {
                throw new ArgumentException($"This team does not has {member.PersonName} member.");
            }

            else
            {
                member.IsAssignedToTeam = false;
                this.memberList.Remove(member);
                //return $"{member.MemberName} is removed from {this.TeamName} member list";
            }
        }

        public void AddBoardToTeam(IBoard board)
        {
            if (board == null)
            {
                throw new ArgumentException("Board cannot be null!");
            }
           //if (this.boardList.ContainsKey(board.BoardName))
           //{
           //    throw new ArgumentException($"This team already has {board.BoardName} board");
           //}

            else
            {
                this.boardList.Add(board.BoardName, board);

                //$"{board.BoardName} is added to {this.TeamName} board list";
            }
        }

        public void RemoveBoardFromTeam(IBoard board)
        {
            if (board == null)
            {
                throw new ArgumentException("Board cannot be null!");
            }
            if (!this.boardList.ContainsKey(board.BoardName))
            {
                throw new ArgumentException($"This {this.TeamName} team doesn't have  {board.BoardName} board!");
            }

            else
            {
                this.boardList.Remove(board.BoardName);
                //return $"{board.BoardName} is added to {this.TeamName} board list";
            }
        }

        public string ShowAllTeamMembers()
        {
            StringBuilder str = new StringBuilder();
            if (this.MemberList.Count == 0)
            {
                str.AppendLine($"There are no members in team {this.TeamName}!");
            }
            else
            {
                str.AppendLine($"Members in team {this.TeamName}:");
                foreach (var member in this.MemberList)
                {
                    str.AppendLine(member.PersonName);
                }
            }
            return str.ToString();
        }

        public string ShowAllTeamBoards()
        {
            StringBuilder str = new StringBuilder();
            if (this.BoardList.Count == 0)
            {
                str.AppendLine($"There are no boards in team {this.TeamName}!");
            }
            else
            {
                str.AppendLine($"Boards in team {this.TeamName}:");
                foreach (var board in this.BoardList)
                {
                    str.AppendLine(" - "+board.Value.BoardName);
                }
            }

            return str.ToString();
        }
        
        public override string ToString()
        {
            //StringBuilder str = new StringBuilder();
            //str.AppendLine($"Team name: {this.TeamName}");
            //str.AppendLine("Members in the team:");
            //if (this.MemberList.Count>0)
            //{
            //    foreach (var member in this.MemberList)
            //    {
            //        str.AppendLine(" - " + member.ToString());
            //    }
            //}
            //else
            //{
            //    str.AppendLine("There are no members in the team!");
            //}
            //str.AppendLine("Boards in the team:");
            //if (this.BoardList.Count>0)
            //{
            //    foreach (var board in this.BoardList)
            //    {
            //        str.AppendLine(board.ToString());
            //    }
            //}
            //else
            //{
            //    str.AppendLine("There are no boards in the team!");
            //}

            //return str.ToString();
            return string.Concat(ShowAllTeamMembers(), ShowAllTeamBoards());
        }

    }
}
