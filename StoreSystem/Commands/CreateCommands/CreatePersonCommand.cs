using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Commands.CreateCommands
{
    public class CreatePersonCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IPersonsCollection personList;
        private readonly IComponentsFactory componentsFactory;

        public CreatePersonCommand(IHistoryEventWriter historyEventWriter, IPersonsCollection personList, IComponentsFactory componentsFactory)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(
                                                                                string.Format(
                                                                                CommandsConsts.NULL_OBJECT,
                                                                                nameof(historyEventWriter)));
            this.personList = personList ?? throw new ArgumentNullException(
                                                                string.Format(
                                                                CommandsConsts.NULL_OBJECT,
                                                                nameof(personList)));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(
                                                                string.Format(
                                                                CommandsConsts.NULL_OBJECT,
                                                                nameof(componentsFactory)));
        }

        public string Execute(IList<string> parameters)
        {
            var personName = parameters[0];

            var person = this.componentsFactory.CreatePerson(personName);
            if (person == null)
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT, nameof(Person)));
            }

            this.personList.AddPerson(person);

            var returnMessage = string.Format(CommandsConsts.ObjectCreated, nameof(Person), personName);

            this.historyEventWriter.AddHistoryEvent(returnMessage, person);

            return returnMessage;
        }
    }
}
