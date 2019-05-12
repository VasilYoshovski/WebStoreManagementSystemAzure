using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Board : IBoard
    {
        private string boardName;
        private readonly IDictionary<string, IWorkItem> boardWorkItems;
        private ITeam team;

        public Board(string boardName, ITeam team)
        {
            this.Team = team;
            this.BoardName = boardName;
            this.boardWorkItems = new Dictionary<string, IWorkItem>();
        }

        public string BoardName
        {
            get
            {
                return this.boardName;
            }

            private set
            {
                if (value.Length < 5 || value.Length > 10)
                {
                    throw new ArgumentException("Board name should be between 5 and 10 symbols.");
                }

                //Board name should be unique in the team
                if (this.team.BoardList.ContainsKey(value))
                {
                    throw new ArgumentException($"This team already has {this.BoardName} board");
                }
                else
                {
                    this.boardName = value;
                }
            }
        }
        public IDictionary<string, IWorkItem> BoardWorkItems
        {
            get
            {
                return new Dictionary<string, IWorkItem>(this.boardWorkItems);
            }
        }

        public ITeam Team
        {
            get
            {
                return this.team;
            }

            private set
            {
                this.team = value ?? throw new ArgumentException("Team cannot be null!");
            }
        }

        public void AddWorkItemToBoard(IWorkItem workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentException("Work item cannot be null!");
            }
            if (this.boardWorkItems.ContainsKey(workItem.Title))
            {

                throw new ArgumentException(string.Format($"{this.boardName} contains work item with title {workItem.Title}"));
            }
            this.boardWorkItems.Add(workItem.Title, workItem);
        }

        public string ListWorkItems(Type typeFilter, string statusFilter, IPerson filterMember, string sortBy)
        {
            if (typeFilter == null)
            {
                if (!string.IsNullOrEmpty(statusFilter) || filterMember != null || !string.IsNullOrEmpty(sortBy))
                {

                    throw new ArgumentException(string.Format($"Type filter must be specified if status or member filter is used!"));
                }
            }
            IEnumerable<IWorkItem> filteredCollection = this.boardWorkItems.Select(x => x.Value);

            if (typeFilter != null)
            {
                filteredCollection = filteredCollection.Where(x => x.GetType() == typeFilter);
            }
            if (!string.IsNullOrEmpty(statusFilter))
            {
                //var a = this.BoardWorkItems.First().Value.GetStatus().ToString();

                filteredCollection = filteredCollection.Where(x => x.GetStatus().ToString() == statusFilter);
            }
            if (filterMember != null)
            {

                filteredCollection = filteredCollection
                    .Where(x => (x as IAssignableWorkItem).Assignee == filterMember)
                    .Where(x => x != null); 
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                var a = typeFilter.GetProperty(sortBy);
                filteredCollection = filteredCollection.OrderBy(x => a.GetValue(x, null));
            }
            StringBuilder result = new StringBuilder();
            foreach (var item in filteredCollection)
            {
                result.AppendLine(item.ToString());
            }
            return result.ToString();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine($"Board name: {this.BoardName}");
            foreach (var item in this.BoardWorkItems)
            {
                str.AppendLine(item.Value.ToString());
            }

            return str.ToString();
        }

    }
}
