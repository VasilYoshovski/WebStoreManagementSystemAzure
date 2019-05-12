using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Feedback : WorkItem, IFeedback, IWorkItem
    {
        private FeedbackStatusType status;

        public Feedback(string title, string description, int rating, IBoard board)
            : base(title, description, board)
        {
            this.Rating = rating;
            this.Status = FeedbackStatusType.Unscheduled;
        }

        public int Rating { get; set; } //what is rating and how do we check it?

        public FeedbackStatusType Status
        {
            get => this.status;
            set
            {
                this.status = value;
            }
        }

        public void ChangeRating(int rating)
        {
            if (rating < 0)
            {
                throw new ArgumentException("Rating cannot be negative!");
            }

            else
            {
                this.Rating = rating;
            }
        }

        public override void ChangeStatus(string status)
        {
            if (status == null)
            {
                throw new ArgumentException("Status cannot be null or empty!");
            }

            else
            {
                //FeedbackStatusType statusEnum = (FeedbackStatusType)Enum.Parse(typeof(FeedbackStatusType), status, true);
                FeedbackStatusType statusEnum = Enum.Parse<FeedbackStatusType>(status, true);
                this.Status = statusEnum;
            }
        }

        public override Enum GetStatus()
        {
            return this.status;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(base.ToString());
            str.AppendLine($"Rating: {this.Rating}");
            str.AppendLine($"Feedback status: {this.Status}");

            return str.ToString();
        }
    }
}
