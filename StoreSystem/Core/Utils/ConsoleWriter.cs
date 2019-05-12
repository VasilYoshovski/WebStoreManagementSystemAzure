using System;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Core.Utils
{
    internal class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
