using System;
using System.Collections.Generic;
using WIMSystem.Utils;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models.Contracts;
using WIMSystem.Commands.Utils;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowAllTeamBoardsCommand : IEngineCommand
    {
        protected readonly IGetters getters;
        protected readonly IHistoryItemsCollection historyItemsCollection;

        public ShowAllTeamBoardsCommand(IGetters getters, IHistoryItemsCollection historyItemsCollection)
        {
            this.getters = getters ?? throw new ArgumentNullException(
                string.Format(CommandsConsts.NULL_OBJECT,
                nameof(getters)));
            this.historyItemsCollection = historyItemsCollection ?? throw new ArgumentNullException(
                string.Format(CommandsConsts.NULL_OBJECT,
                nameof(historyItemsCollection)));
        }

        public string Execute(IList<string> parameters)
        {
            var team = this.getters.GetTeam(parameters[0]);

            if (Validators.IsNullValue(team))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(team)
                    ));
            }
            var returnMessage = team.ShowAllTeamBoards();
            return returnMessage;
        }
    }
}
