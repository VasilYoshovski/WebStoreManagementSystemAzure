//using System;
//using System.Collections;
//using System.Collections.Generic;
//using WIMSystem.Models.Contracts;

//namespace WIMSystem.Models
//{
//    public class DictionaryContainer<T> : IEnumerable<T> where T:INameble
//    {
//        private IDictionary<string, T> containerList;

//        public IDictionary<string, T> ContainerList
//        {
//            get => new Dictionary<string, T>(containerList);
//        }

//        public IEnumerator<T> GetEnumerator()
//        {
//            foreach (var item in this.containerList)
//            {
//                yield return item.Value;
//            }
//        }

//        public void AddTeam(T newItem)
//        {
//            if (this.containerList.ContainsKey(newItem.Name))
//            {
//                throw new ArgumentException($"{nameof(T)} with that name exist!");
//            }
//            this.containerList.Add(newItem.Name, newItem);
//        }

//        public T this[string index]
//        {
//            get => this.containerList[index];
//            private set
//            {
//                this.containerList[index] = value;
//            }
//        }

//        public void RemoveTeam(T removeItem)
//        {
//            RemoveTeam(removeItem.Name);
//        }


//        public void RemoveTeam(string teamName)
//        {
//            if (containerList.ContainsKey(teamName))
//            {
//                throw new ArgumentOutOfRangeException("There is not such team");
//            }
//            this.containerList.Remove(teamName);
//        }


//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.GetEnumerator();
//        }
//    }
//}
