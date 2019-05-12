using System;
using System.Collections.Generic;

using WIMSystem.Core.Contracts;

namespace WIMSystem.Core
{
    internal class Command : ICommand
    {
        private string name;
        private ICollection<string> parameters;

        public Command(string commandName, ICollection<string> parameters)
        {
            this.name = commandName;
            this.parameters = parameters;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Command Name cannot be null or empty.");
                }

                this.name = value;
            }
        }

        public IList<string> Parameters
        {
            get
            {
                return new List<string>(this.parameters);
            }
        }

        
    }
}
