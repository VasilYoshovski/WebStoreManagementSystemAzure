using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IPersonsCollection
    {
        IDictionary<string, IPerson> Persons { get; }
        void AddPerson(IPerson newPerson);
        string ShowAllPeople();
        bool Contains(string name);
        IPerson this[string index] { get; }
    }
}
