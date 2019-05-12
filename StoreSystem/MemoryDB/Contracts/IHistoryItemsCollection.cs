using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models.Contracts
{
    public interface IHistoryItemsCollection
    {
        ICollection<IHistoryItem> HistoryItemsList { get; }

        void AddHistoryItem(IHistoryItem newHistoryItem);
        bool Contains(IHistoryItem historyItem);
        IEnumerator<IHistoryItem> GetEnumerator();
        string ShowTeamActivity(ITeam team);
        string ShowBoardActivity(IBoard board);
        string ShowPersonActivity(IPerson person);
    }
}