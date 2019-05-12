using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IPerson
    {
        string PersonName { get; }

        IList<IWorkItem> MemberWorkItems { get; }

        bool IsAssignedToTeam { get; set; }

        string ToString();
    }
}
