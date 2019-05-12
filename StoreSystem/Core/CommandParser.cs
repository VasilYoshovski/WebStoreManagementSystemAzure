using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Factories.Contracts;

namespace WIMSystem.Core
{
    public class CommandParser : ICommandParser
    {

        private const string SplitCommandSymbol = "\" \"";
        private readonly IComponentsFactory factory;

        public CommandParser(IComponentsFactory factory)
        {
            this.factory = factory;
        }

        public ICommand Parse(string input)
        {
            ICommand newCommand = null;
            var indexOfFirstSeparator = input.IndexOf(SplitCommandSymbol);

            if (indexOfFirstSeparator < 0)
            {
                newCommand = factory.CreateCommand(input.Trim('"'),new List<string>());
                return newCommand;
            }

            var name = input.Substring(0, indexOfFirstSeparator).Trim('"');
            var parameters = input.Substring(indexOfFirstSeparator + 2)
                .Trim('"')
                .Split(new[] { SplitCommandSymbol }, StringSplitOptions.RemoveEmptyEntries);
            newCommand = factory.CreateCommand(name, parameters);
            return newCommand;
        }
    }
}
