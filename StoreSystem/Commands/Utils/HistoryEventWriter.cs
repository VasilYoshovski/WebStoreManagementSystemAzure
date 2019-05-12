using System;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.Utils
{
    public class HistoryEventWriter : IHistoryEventWriter
    {
        protected readonly IHistoryItemsCollection historyItemsList;
        protected readonly IComponentsFactory factory;

        public HistoryEventWriter(IHistoryItemsCollection historyItemsList, IComponentsFactory factory)
        {
            this.historyItemsList = historyItemsList ?? throw new ArgumentNullException(string.Format(CommandsConsts.NULL_OBJECT, nameof(historyItemsList)));
            this.factory = factory ?? throw new ArgumentNullException(string.Format(CommandsConsts.NULL_OBJECT, nameof(factory)));
        }

        //SingleInstance();

        public void AddHistoryEvent(string description, IPerson member = null, IBoard board = null, ITeam team = null, IWorkItem workItem = null)
        {
            var historyItem = this.factory.CreateHistoryItem(description, member, board, team, workItem) ?? throw new ArgumentNullException(string.Format(CommandsConsts.NULL_OBJECT, nameof(description))); 
            this.historyItemsList.AddHistoryItem(historyItem);
        }
    }
}
