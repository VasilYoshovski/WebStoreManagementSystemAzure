using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Story : AssignableWorkItem, IStory, IAssignableWorkItem, IWorkItem
    {
        public Story(string title, string description, PriorityType priority,
            StorySizeType size, IBoard board, IPerson assignee = null) //assignee is optional?
            : base(title, description, priority, board, assignee)
        {
            this.Size = size;
            this.Status = StoryStatusType.NotDone;
        }

        public StorySizeType Size { get; set; }

        public StoryStatusType Status { get; set; }

        public void ChangeSize(string size)
        {
            if (size == null)
            {
                throw new ArgumentException("Size cannot be null or empty!");
            }

            else
            {
                StorySizeType sizeEnum = (StorySizeType)Enum.Parse(typeof(StorySizeType), size, true);
                this.Size = sizeEnum;
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
                StoryStatusType statusEnum = (StoryStatusType)Enum.Parse(typeof(StoryStatusType), status, true);
                this.Status = statusEnum;
            }
        }

        public override Enum GetStatus()
        {
            return this.Status;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(base.ToString());
            str.AppendLine($"Story size: {this.Size}");
            str.AppendLine($"Story status: {this.Status}");
            
            return str.ToString();
        }
    }
}
