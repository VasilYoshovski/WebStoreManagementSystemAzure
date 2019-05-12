using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.StoreUserViewModels
{
    public class StoreUsersCollectionViewModel
    {
        public IReadOnlyCollection<StoreUserViewModel> StoreUsers { get; set; }
    }
}
