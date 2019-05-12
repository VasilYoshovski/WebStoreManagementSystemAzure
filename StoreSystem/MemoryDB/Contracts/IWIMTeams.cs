using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public interface IWIMTeams
    {
        ITeam this[string index] { get; }

        IDictionary<string, ITeam> TeamsList { get; }

        void AddTeam(ITeam newTeam);

        void RemoveTeam(ITeam removeTeam);

        void RemoveTeam(string teamName);

        bool Contains(string teamName);

        string ShowAllTeams();

        string ToString();
    }
}