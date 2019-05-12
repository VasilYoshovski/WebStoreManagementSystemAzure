using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class PersonsCollection : IPersonsCollection
    {
        // The single instance
        private readonly IDictionary<string, IPerson> personsList;
        //private readonly static IPersonsCollection instance;

        //static PersonsCollection()
        //{
        //    instance = new PersonsCollection();
        //}

        public PersonsCollection()
        {
            this.personsList = new Dictionary<string, IPerson>();
        }

        //public static IPersonsCollection Instance
        //{
        //    get { return instance; }
        //}

        public IDictionary<string, IPerson> Persons
        {
            get
            {
                return new Dictionary<string, IPerson>(this.personsList);
            }
        }

        public void AddPerson(IPerson newPerson)
        {
            if (this.personsList.ContainsKey(newPerson.PersonName))
            {
                throw new ArgumentException($"{nameof(Person)} with that name exist!");
            }
            this.personsList.Add(newPerson.PersonName, newPerson);
        }

        public string ShowAllPeople()
        {
            var output = new StringBuilder();

            if (this.Persons.Count == 0)
            {
                output.AppendLine("There are no people registered yet!");
            }

            else
            {
                output.AppendLine("Registered people:");

                foreach (var person in this.Persons)
                {
                    output.AppendLine($"-{person.Value.PersonName}");
                }
            }

            return output.ToString();
        }

        public bool Contains(string name)
        {
            return this.personsList.ContainsKey(name);
        }

        public IPerson this[string index]
        {
            get => this.personsList[index];
            private set
            {
                this.personsList[index] = value;
            }
        }

        public override string ToString()
        {

            StringBuilder str = new StringBuilder();
            if (this.Persons.Count > 0)
            {
                foreach (var person in this.Persons)
                {
                    str.AppendLine(this.Persons.Values.ToString());
                }
            }
            else
            {
                str.AppendLine("Thare are no persons in the application");
            }

            return str.ToString();
        }
    }
}
