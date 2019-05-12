using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Commands.CreateCommands
{
    public class CreateBugCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateBugCommand(IHistoryEventWriter historyEventWriter, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string Execute(IList<string> parameters)
        {
            var bugTitle = parameters[0];
            var bugDescription = parameters[1];
            var stepsToReproduce = parameters[2].Trim().Split(CommandsConsts.SPLIT_CHAR).ToList();
            var bugPriority = StringToEnum<PriorityType>.Convert(parameters[3]);
            var bugSeverity = StringToEnum<BugSeverityType>.Convert(parameters[4]);
            var teamName = parameters[5];
            var board = this.getter.GetBoard(teamName, parameters[6]);
            IPerson bugAssignee = null;

            var bug = this.componentsFactory.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board, bugAssignee);

            if (bug == null)
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT, nameof(Bug)));
            }

            board.AddWorkItemToBoard(bug);

            string returnMessage = string.Format(CommandsConsts.ObjectCreated, nameof(Bug), bugTitle);

            this.historyEventWriter.AddHistoryEvent(returnMessage, bugAssignee, board, board.Team, bug);

            return returnMessage;
        }
    }
}
