using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IWorkItem
    {
        int ID { get; }
        string Title { get; }
        string Description { get; }
        IBoard Board { get; }
        IList<IComment> ListOfComments { get; }
        IList<IHistoryItem> ListOfHistoryItems { get; }

        void AddComment(IComment comment);

        void AddHistoryItem(IHistoryItem history);

        void ChangeStatus(string newStatus);

        Enum GetStatus();

        string ToString();

    }
}
