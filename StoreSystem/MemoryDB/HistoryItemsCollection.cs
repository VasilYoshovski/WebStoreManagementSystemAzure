using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class HistoryItemsCollection : IEnumerable, IHistoryItemsCollection
    {
        private readonly ICollection<IHistoryItem> historyItemsList;

        //private readonly static IHistoryItemsCollection instance;

        //static HistoryItemsCollection()
        //{
        //    instance = new HistoryItemsCollection();
        //}

        public HistoryItemsCollection()
        {
            this.historyItemsList = new List<IHistoryItem>();
        }

        //public static IHistoryItemsCollection Instance
        //{
        //    get { return instance; }
        //}

        public ICollection<IHistoryItem> HistoryItemsList
        {
            get => new List<IHistoryItem>(this.historyItemsList);
        }

        public IEnumerator<IHistoryItem> GetEnumerator()
        {
            foreach (var item in this.historyItemsList)
            {
                yield return item;
            }
        }

        public void AddHistoryItem(IHistoryItem newHistoryItem)
        {
            this.historyItemsList.Add(newHistoryItem);
        }

        public bool Contains(IHistoryItem historyItem)
        {
            return this.historyItemsList.Contains(historyItem);

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ShowBoardActivity(IBoard board)
        {
            StringBuilder str = new StringBuilder();

            //var a = this.historyItemsList
            //    .Where(x => x.Board != null)
            //    .Where(x => x.Board == board);

            var filteredActivity = this.HistoryItemsList
                .Where(x => x.Board != null)
                .Where(x => x.Team.TeamName == board.Team.TeamName);

            filteredActivity = filteredActivity.Where(x => x.Board.BoardName == board.BoardName);

            str.AppendLine($"Activity history for board: {board.BoardName} in team: {board.Team.TeamName}");
            str.AppendLine(new string('=', 15));
            if (filteredActivity.Count() == 0)
            {
                str.AppendLine("There is no history!");
            }
            else
            {
                foreach (var item in filteredActivity)
                {
                    str.AppendLine(item.FilteredBy(HistoryItemFilterType.board));
                }

            }

            return str.ToString();
        }

        public string ShowTeamActivity(ITeam team)
        {
            StringBuilder str = new StringBuilder();
            var filteredActivity = this.HistoryItemsList
                .Where(x => x.Team != null)
                .Where(x => x.Team.TeamName == team.TeamName);

            str.AppendLine($"Activity history for team: {team.TeamName}");
            str.AppendLine(new string('=', 15));
            if (filteredActivity.Count() == 0)
            {
                str.AppendLine("There is no history!");
            }
            else
            {
                foreach (var item in filteredActivity)
                {
                    str.AppendLine(item.FilteredBy(HistoryItemFilterType.team));
                }
            }

            return str.ToString();
        }

        public string ShowPersonActivity(IPerson person)
        {
            var str = new StringBuilder();

            var filteredActivity = this.HistoryItemsList
                .Where(x => x.Member != null)
                .Where(x => x.Member.PersonName == person.PersonName);


            str.AppendLine($"Activity history for person: {person.PersonName}");
            str.AppendLine(new string('=', 15));
            if (filteredActivity.Count() == 0)
            {
                str.AppendLine("There is no history!");
            }
            else
            {
                foreach (var activity in filteredActivity)
                {
                    str.AppendLine(activity.FilteredBy(HistoryItemFilterType.person));
                }
            }

            return str.ToString();
        }

    }
}
