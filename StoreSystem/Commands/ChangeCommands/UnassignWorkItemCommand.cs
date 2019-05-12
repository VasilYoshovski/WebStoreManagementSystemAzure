using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;
using WIMSystem.Models;
using WIMSystem.Commands.Utils;
using WIMSystem.Utils;

namespace WIMSystem.Commands.ChangeCommands
{
    public class UnassignWorkItemCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IGetters getter;

        public UnassignWorkItemCommand(IHistoryEventWriter historyEventWriter, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string Execute(IList<string> parameters)
        {
            var teamName = parameters[0];
            var board = this.getter.GetBoard(teamName, parameters[1]);
            var workItem = this.getter.GetAssignableWorkItem(board, parameters[2]);

            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,nameof(WorkItem)));
            }

            var member = workItem.Assignee;
            workItem.UnassignMember();
            member.MemberWorkItems.Remove(workItem);
            var returnMessage = string.Format(CommandsConsts.WorkItemUnAssigned, workItem.Title, member.PersonName);

            this.historyEventWriter.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

            return returnMessage;
        }
    }
}
