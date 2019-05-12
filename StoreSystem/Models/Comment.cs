using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Comment : IComment
    {
        private string message;
        private readonly IPerson author;

        public Comment(string message, IPerson author)
        {
            if(string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Message cannot be null or empty!");
            }
            if (author == null)
            {
                throw new ArgumentException("Author cannot be null or empty!");
            }
            this.message = message;
            this.author = author;
        }

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public IPerson Author
        {
            get
            {
                return this.author;
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine($"\t{this.Author.PersonName}: {this.Message}");

            return str.ToString();
        }
    }
}
