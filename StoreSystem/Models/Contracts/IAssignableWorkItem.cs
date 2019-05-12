using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IAssignableWorkItem : IWorkItem
    {
        IPerson Assignee { get; }

        PriorityType Priority { get; set; }

        void AssignMember(IPerson member);

        void UnassignMember();

        void ChangePriority(string priority);
    }
}
