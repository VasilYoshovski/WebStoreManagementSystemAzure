using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class HistoryItem : IHistoryItem
    {
        private string description;
        private readonly DateTime creationDate;
        private readonly IPerson member;
        private readonly IBoard board;
        private readonly ITeam team;
        private readonly IWorkItem workItem;

        public HistoryItem(string description, DateTime creationDate, IPerson member, IBoard board, ITeam team, IWorkItem workItem)
        {
            this.Description = description;
            this.creationDate = creationDate;
            this.member = member;
            this.board = board;
            this.team = team;
            this.workItem = workItem;
        }
        public IWorkItem WorkItem    
        {                            
            get => this.workItem;    
        }                           

        public string Description
        {
            get => this.description;
            set
            {
                this.description = value;
            }
        }
        public DateTime CreationDate
        {
            get => this.creationDate;
        }
        public IPerson Member
        {
            get => this.member;
        }
        public IBoard Board
        {
            get => this.board;
        }
        public ITeam Team
        {
            get => this.team;
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine($"Date and time: {this.CreationDate}");
            str.AppendLine($"Team: {this.Team.TeamName}");
            str.AppendLine($"Board: {this.Board.BoardName}");
            str.AppendLine($"Member: {this.Member.PersonName}");
            str.AppendLine($"WorkItem:{this.WorkItem.Title}");
            str.AppendLine($"Description: {this.Description}");
            
            return str.ToString();
        }

        public string FilteredBy(HistoryItemFilterType filterType)
        {
            StringBuilder str = new StringBuilder();
                str.AppendLine($"Date and time: {this.CreationDate}");
                str.AppendLine($"Description: {this.Description}");
            
            if (filterType!=HistoryItemFilterType.team&&this.team!=null)
            {
                str.AppendLine($"Team: {this.team.TeamName}");
            }
            else if (filterType!=HistoryItemFilterType.board&&this.board!=null)
            {
                str.AppendLine($"Board: {this.board.BoardName}");
            }
            else if (filterType!=HistoryItemFilterType.person&&this.member!=null)
            {
                str.AppendLine($"Member: {this.member.PersonName}");
            }
            else if (filterType!=HistoryItemFilterType.workitem&&this.workItem!=null)
            {                                                                        
                str.AppendLine($"WorkItem:{this.WorkItem.Title}");                   
            }

            return str.ToString();
        }    
    }
}
