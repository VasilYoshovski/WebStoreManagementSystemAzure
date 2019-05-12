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
    public class CreateTeamCommand : IEngineCommand
    {

        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IWIMTeams wimTeams;
        private readonly IComponentsFactory componentsFactory;

        public CreateTeamCommand(IHistoryEventWriter historyEventWriter, IWIMTeams wimTeams, IComponentsFactory componentsFactory)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.wimTeams = wimTeams ?? throw new ArgumentNullException(nameof(wimTeams));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
        }

        public string Execute(IList<string> parameters)
        {
            var teamName = parameters[0];

            var team = this.componentsFactory.CreateTeam(teamName);
            if (team == null)
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT, nameof(Team)));
            }

            this.wimTeams.AddTeam(team);
            var returnMessage = string.Format(CommandsConsts.ObjectCreated, nameof(Team), teamName);
            this.historyEventWriter.AddHistoryEvent(returnMessage, team: team);

            return returnMessage;
        }
    }
}
