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
    public class ChangeRatingOfFeedbackCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IGetters getter;

        public ChangeRatingOfFeedbackCommand(IHistoryEventWriter historyEventWriter, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string Execute(IList<string> parameters)
        {
            var teamName = parameters[0];
            var board = this.getter.GetBoard(teamName, parameters[1]);
            var workItem = this.getter.GetWorkItem(board, parameters[2]);
            var rating = int.Parse(parameters[3]);

            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(WorkItem)));
            }

            if (!(workItem is IFeedback))
            {
                throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Feedback)}!"));
            }

           ((IFeedback)workItem).Rating = rating;

            var returnMessage = string.Format(CommandsConsts.FeedbackRatingChange, workItem.Title, rating);

            this.historyEventWriter.AddHistoryEvent(returnMessage, null, workItem.Board, workItem.Board.Team, workItem);

            return returnMessage;
        }
    }
}
