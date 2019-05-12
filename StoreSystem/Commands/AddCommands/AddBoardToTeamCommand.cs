//using System;
//using System.Collections.Generic;
//using WIMSystem.Commands.Contracts;
//using WIMSystem.Commands.Utils;
//using WIMSystem.Models;
//using WIMSystem.Models.Contracts;
//using WIMSystem.Utils;

//namespace WIMSystem.Commands.AddCommands
//{
//    public class AddBoardToTeamCommand : IEngineCommand
//    {
//        private readonly IHistoryEventWriter historyEventWriter;
//        private readonly IGetters getters;

//        public AddBoardToTeamCommand(IHistoryEventWriter historyEventWriter, IGetters getters)
//        {
//            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(
//                                                                                string.Format(
//                                                                                Consts.NULL_OBJECT,
//                                                                                nameof(historyEventWriter)));
//            this.getters = getters ?? throw new ArgumentNullException(
//                                                                string.Format(
//                                                                Consts.NULL_OBJECT,
//                                                                nameof(getters)));
//        }

//        public string Execute(IList<string> parameters)
//        {
//            return Execute(null,null);//TODO
//        }

//        private string Execute(ITeam teamToAddTo, IBoard boardForAdding)
//        {
//            teamToAddTo.AddBoardToTeam(boardForAdding);
//            string output = string.Format(CommandsConsts.ObjectAddedToTeam, nameof(Board), boardForAdding.BoardName, teamToAddTo.TeamName);
//            this.historyEventWriter.AddHistoryEvent(output, board: boardForAdding, team: teamToAddTo);

//            return output;
//        }
//    }
//}
