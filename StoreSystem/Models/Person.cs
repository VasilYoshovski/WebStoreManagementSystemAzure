using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Person : IPerson
    {

        private string personName;
        private IList<IWorkItem> memberWorkItem;
        private bool isAssignedToTeam;


        public Person(string personName)
        {
            this.PersonName = personName;
            this.memberWorkItem = new List<IWorkItem>();
            this.IsAssignedToTeam = this.isAssignedToTeam;
        }

        public string PersonName
        {
            get
            {
                return this.personName;
            }

            private set
            {
                if (value.Length < 5 || value.Length > 15)
                {
                    throw new ArgumentException("Members name should be between 5 and 15 symbols.");
                }

                this.personName = value;
            }
        }

        public IList<IWorkItem> MemberWorkItems
        {
            get
            {
                //return new List<IWorkItem>(this.memberWorkItem);
                return this.memberWorkItem;
            }
        }



        public bool IsAssignedToTeam
        {
            get
            {
                return this.isAssignedToTeam;
            }

            set
            {
                this.isAssignedToTeam = value;
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine($"Person Name: {this.PersonName}");
            str.AppendLine($"Assigned to team: {this.IsAssignedToTeam}");
            if (this.MemberWorkItems.Count > 0)
            {
                str.AppendLine("Work items:");
                foreach (var workItem in this.MemberWorkItems)
                {
                    str.AppendLine($"In Board: {workItem.Board.BoardName}");
                    str.AppendLine(workItem.Description);
                }
            }

            return str.ToString();
        }

    }
}
