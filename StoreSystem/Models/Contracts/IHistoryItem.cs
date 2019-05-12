using System;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IHistoryItem
    {
        string Description { get; set; }
        IWorkItem WorkItem { get; }
        DateTime CreationDate { get; }
        IPerson Member { get; }
        IBoard Board { get; }
        ITeam Team { get; }
        string FilteredBy(HistoryItemFilterType historyItemFilterType);
        string ToString();
    }
}
