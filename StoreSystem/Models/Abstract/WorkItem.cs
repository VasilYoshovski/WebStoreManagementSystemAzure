using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Utils;

namespace WIMSystem.Models.Abstract
{
    public abstract class WorkItem : IWorkItem
    {
        private readonly int id;
        private string title;
        private string description;
        private readonly IBoard board;
        private readonly IList<IComment> listOfComments;
        private readonly IList<IHistoryItem> listOfHistoryItems;

        public WorkItem(string title, string description, IBoard board)
        {
            this.board = board;
            this.id = IDProvider.GenerateUniqueID();
            this.Title = title;
            this.Description = description;
            this.listOfComments = new List<IComment>();
            this.listOfHistoryItems = new List<IHistoryItem>();
        }

        public int ID
        {
            get
            {
                return this.id;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Title", "Title cannot be empty!");
                }

                if (value.Length < 10 || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("Title", "Title should be more than 10 and lesser than 50 characters long!");
                }
                if (this.board.BoardWorkItems.ContainsKey(value)) //
                {
                    throw new ArgumentException($"This board already contains Work Item with name {value}!");
                }
                else
                {
                    this.title = value;
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Description cannot be empty!");
                }

                if (value.Length < 10 || value.Length > 500)
                {
                    throw new ArgumentException("Description should be more than 10 and lesser than 500 characters long!");
                }

                this.description = value;
            }
        }

        public IList<IComment> ListOfComments { get => new List<IComment>(this.listOfComments); }

        public IList<IHistoryItem> ListOfHistoryItems { get; }

        public IBoard Board
        {
            get
            {
                return this.board;
            }
        }

        public void AddComment(IComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentException("Comment cannot be empty!");
            }

            this.listOfComments.Add(comment);
        }

        public void AddHistoryItem(IHistoryItem history)
        {
            if (history == null)
            {
                throw new ArgumentNullException("History");
            }

            this.listOfHistoryItems.Add(history);
        }

        public abstract void ChangeStatus(string newStatus);

        public abstract Enum GetStatus();

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(new string('=', 15));
            str.AppendLine($"Team: {this.Board.Team.TeamName}");
            str.AppendLine($"Board: {this.Board.BoardName}");
            str.AppendLine(new string('=',15));

            str.AppendLine($"Type: {this.GetType().Name}");
            str.AppendLine($"Work Item ID: {this.ID}");
            str.AppendLine($"Title: {this.Title}");
            str.AppendLine($"Description: {this.Description}");
            if (this.listOfComments.Count > 0)
            {
                str.AppendLine("Comments:");
                foreach (var comment in this.listOfComments)
                {
                    str.Append(comment.ToString());
                    //str.AppendLine($"\t Author: {comment.Author}");
                    //str.AppendLine($"\t Comment: {comment.Message}");
                }
            }

            return str.ToString();
        }
    }
}
