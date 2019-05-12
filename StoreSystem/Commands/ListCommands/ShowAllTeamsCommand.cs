using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.ListCommands

{ 
    public class ShowAllTeamsCommand : IEngineCommand
    {
        private readonly IWIMTeams wIMTeams;

        public ShowAllTeamsCommand(IWIMTeams wIMTeams)
        {
            this.wIMTeams = wIMTeams ?? throw new ArgumentNullException(nameof(wIMTeams));
        }

        public string Execute(IList<string> parameters)
        {            
            var returnMessage = this.wIMTeams.ShowAllTeams();
            return returnMessage;
        }
    }
}
