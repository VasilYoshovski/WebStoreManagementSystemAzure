using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IBoard
    {
        string BoardName { get; }

        ITeam Team { get; }

        IDictionary<string,IWorkItem> BoardWorkItems { get; }

        void AddWorkItemToBoard(IWorkItem workItem);

        string ListWorkItems(Type typeFilter, string statusFilter, IPerson filterMember, string sortBy);

        string ToString();
    }
}
