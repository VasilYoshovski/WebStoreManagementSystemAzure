using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Commands.Contracts
{
    public interface IEngineCommand
    {
        string Execute(IList<string> parameters);
    }
}
