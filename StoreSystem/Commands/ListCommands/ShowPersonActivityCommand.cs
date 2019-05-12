using System;
using System.Collections.Generic;
using WIMSystem.Commands.Utils;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowPersonActivityCommand : IEngineCommand
    {
        private readonly IGetters getter;
        private readonly IHistoryItemsCollection historyItemsCollection;

        public ShowPersonActivityCommand(IGetters getter, IHistoryItemsCollection historyItemsCollection)
        {
            this.getter = getter;
            this.historyItemsCollection = historyItemsCollection;
        }

        public string Execute(IList<string> parameters)
        {
            var person = this.getter.GetPerson(parameters[0]);
            return this.Execute(person);
        }

        private string Execute(IPerson person)
        {
            if (Validators.IsNullValue(person))
            {
                throw new ArgumentException(string.Format(CommandsConsts.NULL_OBJECT,nameof(person)));
            }
            var returnMessage= historyItemsCollection.ShowPersonActivity(person);
           
            return returnMessage;
        }
    }
}
