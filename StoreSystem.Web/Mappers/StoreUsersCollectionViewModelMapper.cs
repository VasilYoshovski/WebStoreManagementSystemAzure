using StoreSystem.Data.Models;
using StoreSystem.Web.Models.StoreUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class StoreUsersCollectionViewModelMapper : IViewModelMapper<List<StoreUser>, StoreUsersCollectionViewModel>
    {
        private readonly IViewModelMapper<StoreUser, StoreUserViewModel> storeUserMapper;

        public StoreUsersCollectionViewModelMapper(IViewModelMapper<StoreUser, StoreUserViewModel> storeUserMapper)
        {
            this.storeUserMapper = storeUserMapper ?? throw new ArgumentNullException(nameof(storeUserMapper));
        }

        public StoreUsersCollectionViewModel MapFrom(List<StoreUser> entity)
             => new StoreUsersCollectionViewModel
             {
                 StoreUsers = entity.Select(d => this.storeUserMapper.MapFrom(d)).ToList()
             };
    }
}
