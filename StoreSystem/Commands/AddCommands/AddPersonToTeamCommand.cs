using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;

namespace WIMSystem.Commands.AddCommands
{
    public class AddPersonToTeamCommand : IEngineCommand
    {
        protected readonly IGetters getter;
        protected readonly IHistoryEventWriter historyEventWriter;

        public AddPersonToTeamCommand(IGetters getter, IHistoryEventWriter historyEventWriter)
        {
            this.getter = getter ?? throw new ArgumentNullException(
                string.Format(CommandsConsts.NULL_OBJECT,nameof(getter)));
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(
                string.Format(CommandsConsts.NULL_OBJECT,nameof(historyEventWriter)));
        }

        public string Execute(IList<string> parameters)
        {
            var memberForAdding = this.getter.GetPerson(parameters[0]);
            var teamToAddTo = this.getter.GetTeam(parameters[1]);

            teamToAddTo.AddMemberToTeam(memberForAdding);

            var returnMessage = string.Format(CommandsConsts.ObjectAddedToTeam, nameof(Person), memberForAdding.PersonName, teamToAddTo.TeamName);
            this.historyEventWriter.AddHistoryEvent(returnMessage, memberForAdding, null, teamToAddTo);

            return returnMessage;
        }

    }
}
