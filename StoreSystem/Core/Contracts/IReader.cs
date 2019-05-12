using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Core.Contracts
{
    public interface IReader
    {
        ICollection<string> Read();
    }
}
