using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowTeamActivityCommand : IEngineCommand
    {
        private readonly IGetters getters;
        private readonly IHistoryItemsCollection historyItemsCollection;

        public ShowTeamActivityCommand(IGetters getters, IHistoryItemsCollection historyItemsCollection)
        {
            this.getters = getters ?? throw new ArgumentNullException(nameof(getters));
            this.historyItemsCollection = historyItemsCollection ?? throw new ArgumentNullException(nameof(historyItemsCollection));
        }

        public string Execute(IList<string> parameters)
        {
            var team = this.getters.GetTeam(parameters[0]);

            if (Validators.IsNullValue(team))
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT,
                    nameof(team)
                    ));
            }
            var returnMessage = historyItemsCollection.ShowTeamActivity(team);

            return returnMessage;
        }
    }
}
