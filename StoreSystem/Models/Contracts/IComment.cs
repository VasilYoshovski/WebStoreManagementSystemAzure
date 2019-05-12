using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IComment
    {
        string Message { get; }

        IPerson Author { get; }

        string ToString();
    }
}
