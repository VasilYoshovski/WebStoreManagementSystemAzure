using System;
using System.Collections.Generic;
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
    public class CreateStoryCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateStoryCommand(IHistoryEventWriter historyEventWriter, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string Execute(IList<string> parameters)
        {
            var storyTitle = parameters[0];
            var storyDescription = parameters[1];
            var storyPriority = StringToEnum<PriorityType>.Convert(parameters[2]);
            var storySize = StringToEnum<StorySizeType>.Convert(parameters[3]);
            var teamName = parameters[4];
            var board = this.getter.GetBoard(teamName, parameters[5]);
            IPerson storyAssignee = null; 

            var story = this.componentsFactory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, board, storyAssignee);
            if (story == null)
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT, nameof(Story)));
            }

            board.AddWorkItemToBoard(story);

            string returnMessage = string.Format(CommandsConsts.ObjectCreated, nameof(Story), storyTitle);

            this.historyEventWriter.AddHistoryEvent(returnMessage, storyAssignee, board, board.Team);

            return returnMessage;
        }


    }
}
