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
    public class CreateCommentCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateCommentCommand(IHistoryEventWriter historyEventWriter, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string Execute(IList<string> parameters)
        {
            var teamName = parameters[0];
            var boardName = parameters[1];
            var workItemTitle = parameters[2];
            var message = parameters[3];
            var authorName = parameters[4];

            var team = this.getter.GetTeam(parameters[0]);
            var board = this.getter.GetBoard(teamName, parameters[1]);
            var workItem = this.getter.GetWorkItem(board, parameters[2]);
            var author = this.getter.GetMember(team, parameters[4]);

            var comment = this.componentsFactory.CreateComment(message, author);
            if (comment == null)
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT, nameof(Comment)));
            }

            workItem.AddComment(comment);

            string returnMessage = string.Format(CommandsConsts.CommentAdded, message, authorName, workItemTitle);

            this.historyEventWriter.AddHistoryEvent(returnMessage, author, board, team, workItem);

            return returnMessage;
        }
    }
}
