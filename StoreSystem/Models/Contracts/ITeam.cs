using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface ITeam
    {
        string TeamName { get;  }
        IList<IPerson> MemberList { get; }
        IDictionary<string,IBoard> BoardList { get; }
        void AddBoardToTeam(IBoard board);
        void AddMemberToTeam(IPerson member);
        void RemoveBoardFromTeam(IBoard board);
        void RemoveMemberFromTeam(IPerson member);
        string ShowAllTeamMembers();
        string ShowAllTeamBoards();
        string ToString();

    }
}
