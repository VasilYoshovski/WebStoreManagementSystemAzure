using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowBoardActivityCommand : IEngineCommand
    {
        private readonly IGetters getters;
        private readonly IHistoryItemsCollection historyItemsCollection;

        public ShowBoardActivityCommand(IGetters getters, IHistoryItemsCollection historyItemsCollection)
        {
            this.getters = getters;
            this.historyItemsCollection = historyItemsCollection;
        }


        public string Execute(IList<string> parameters)
        {
            var teamName = parameters[0];
            var boardName = parameters[1];
            var board = this.getters.GetBoard(teamName, boardName);

            if (Validators.IsNullValue(board))
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT,
                    nameof(board)
                    ));
            }
            var returnMessage = this.historyItemsCollection.ShowBoardActivity(board);
            return returnMessage;
        }
    }
}
