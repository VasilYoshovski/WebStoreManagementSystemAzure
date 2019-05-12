using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Abstract
{
    public abstract class AssignableWorkItem : WorkItem, IAssignableWorkItem, IWorkItem
    {
        public AssignableWorkItem(string title, string description, PriorityType priority, IBoard board, IPerson assignee = null)
            : base(title, description, board)
        {
            this.Priority = priority;
            this.Assignee = assignee;
        }

        public PriorityType Priority { get; set; }

        public IPerson Assignee { get; private set; }

        public void AssignMember(IPerson member)
        {
            this.Assignee = member ?? throw new ArgumentException("Member cannot be null or empty!");
        }

        public void UnassignMember()
        {
            if (this.Assignee == null)
            {
                throw new ArgumentException(string.Format($"{this.Title} has no assigned member"));

            }
            this.Assignee = null;
        }

        public void ChangePriority(string priority)
        {
            PriorityType priorityEnum = (PriorityType)Enum.Parse(typeof(PriorityType), priority, true);
            this.Priority = priorityEnum;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(base.ToString());

            str.AppendLine($"Priority: {this.Priority}");
            if (this.Assignee == null)
            {
                str.AppendLine("UNASSIGNED item");
            }
            else
            {
                str.AppendLine($"Assigned to: {this.Assignee.PersonName}");
            }

            return str.ToString();
        }

    }
}
