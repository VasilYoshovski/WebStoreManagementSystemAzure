using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Bug : AssignableWorkItem, IBug, IAssignableWorkItem, IWorkItem
    {
        private readonly IList<string> stepsToReproduce;

        public Bug(string title, string description, IList<string> stepsToReproduce,
            PriorityType priority, BugSeverityType severity, IBoard board, IPerson assignee = null)
            : base(title, description, priority, board, assignee)
        {
            if (stepsToReproduce == null)
            {
                throw new ArgumentNullException("Steps to reproduce cannot be null!");
            }

            if (!stepsToReproduce.Any())
            {
                throw new ArgumentException("There must be at least one step to reproduce for the bug!");
            }

            this.stepsToReproduce = stepsToReproduce;
            this.Severity = severity;
            this.Status = BugStatusType.Active;

        }

        public IList<string> StepsToReproduce
        {
            get
            {
                return new List<string>(this.stepsToReproduce);
            }
        }

        public BugSeverityType Severity { get; set; }

        public BugStatusType Status { get; set; }

        public void ChangeSeverity(string severity)
        {
            if (severity == null)
            {
                throw new ArgumentNullException("Severity cannot be null or empty!");
            }

            else
            {
                BugSeverityType severityEnum = (BugSeverityType)Enum.Parse(typeof(BugSeverityType), severity, true);
                this.Severity = severityEnum;
            }
        }

        public override void ChangeStatus(string newStatus)
        {
            if (newStatus == null)
            {
                throw new ArgumentNullException("Status cannot be null or empty!");
            }

            else
            {
                //BugStatusType statusEnum = (BugStatusType)Enum.Parse(typeof(BugStatusType), newStatus, true); // LEGACY - има кастване, което се е правило преди дженериците.
                BugStatusType statusEnum = Enum.Parse<BugStatusType>(newStatus);

                this.Status = statusEnum;
            }
        }

        public override Enum GetStatus()
        {
            return this.Status;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(base.ToString());
            if (this.StepsToReproduce.Count>0)
            {
                str.AppendLine($"Steps to reproduce:");
                foreach (var step in this.StepsToReproduce)
                {
                    str.AppendLine("\t"+step);
                }
            }
            str.AppendLine($"Severity: {this.Severity}");
            str.AppendLine($"Bug status: {this.Status}");
                       
            return str.ToString();
        }
    }
}
