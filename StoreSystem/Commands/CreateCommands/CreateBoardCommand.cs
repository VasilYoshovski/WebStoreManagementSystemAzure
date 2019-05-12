using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;


namespace WIMSystem.Commands.CreateCommands
{
    public class CreateBoardCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateBoardCommand(IHistoryEventWriter historyEventWriter, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string Execute(IList<string> parameters)
        {
            var boardName = parameters[0];
            var teamToAddTo = this.getter.GetTeam(parameters[1]);

            var board = this.componentsFactory.CreateBoard(boardName, teamToAddTo);

            if (board == null)
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT, nameof(Board)));
            }
                       
            teamToAddTo.AddBoardToTeam(board);
            var returnMessage = string.Format(CommandsConsts.ObjectCreated, nameof(Board), boardName);
            this.historyEventWriter.AddHistoryEvent(returnMessage, board: board, team: teamToAddTo);

            return returnMessage;
        }
    }
}
