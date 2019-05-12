using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IStory : IAssignableWorkItem, IWorkItem
    {
        StorySizeType Size { get; set; }
        StoryStatusType Status { get; set; }

        void ChangeSize(string size);
    }
}
