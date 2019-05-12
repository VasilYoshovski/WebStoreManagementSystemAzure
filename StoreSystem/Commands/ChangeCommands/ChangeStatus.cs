using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;
using WIMSystem.Utils;
using WIMSystem.Models;
using WIMSystem.Commands.Utils;

namespace WIMSystem.Commands.ChangeCommands
{
    public class ChangeStatus : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IGetters getter;

        public ChangeStatus(IHistoryEventWriter historyEventWriter, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string Execute(IList<string> parameters)
        {
            var teamName = parameters[0];
            var board = this.getter.GetBoard(teamName, parameters[1]);
            var workItem = this.getter.GetWorkItem(board, parameters[2]);
            var status = parameters[3];

            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,nameof(WorkItem)));
            }

            workItem.ChangeStatus(status);

            var returnMessage = string.Format(CommandsConsts.WorkItemStatusChange, workItem.Title, status);

            IPerson member = null;

            if (workItem is IAssignableWorkItem)
            {
                member = (workItem as IAssignableWorkItem).Assignee;
            }

            this.historyEventWriter.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

            return returnMessage;
        }

    }
}
