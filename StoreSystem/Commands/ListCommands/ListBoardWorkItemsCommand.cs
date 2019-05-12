using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.ListCommands
{
    public class ListBoardWorkItemsCommand : IEngineCommand
    {
        private readonly IGetters getter;

        public ListBoardWorkItemsCommand(IGetters getter)
        {
            this.getter = getter;
        }

        public string Execute(IList<string> parameters)
        {

            var teamName = parameters[0];
            var board = this.getter.GetBoard(teamName, parameters[1]);

            Type filterType = null;
            string filterStatus = null;
            IPerson filterAssignee = null;
            string sortBy = null;   // "filterType:Story" "filterStatus:High" "filterAssignee:Gosho" "sortBy:Title"

            for (int i = 2; i < parameters.Count; i++)
            {
                var paramOption = parameters[i].Split(new[] { ':', }, StringSplitOptions.RemoveEmptyEntries);
                switch (paramOption[0])
                {
                    case "filterType":
                        {
                            var typeAsString = "WIMSystem.Models." + paramOption[1];
                            var curAssembly = typeof(Engine).Assembly;
                            filterType = curAssembly.GetType(typeAsString, false, true) ??
                                throw new ArgumentException("Undefined type {0}", paramOption[1]);
                            break;
                        }
                    case "filterStatus":
                        {
                            filterStatus = paramOption[1];
                            break;
                        }
                    case "filterAssignee":
                        {
                            filterAssignee = this.getter.GetMember(getter.GetTeam(teamName), parameters[1]);
                            break;
                        }
                    case "sortBy":
                        {
                            sortBy = paramOption[1];
                            break;
                        }
                }
            }

            string returnMessage = board.ListWorkItems(filterType, filterStatus, filterAssignee, sortBy);

            return returnMessage;
        }
    }
}
