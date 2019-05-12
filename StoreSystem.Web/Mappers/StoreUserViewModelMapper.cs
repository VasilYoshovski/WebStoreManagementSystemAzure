using StoreSystem.Data.Models;
using StoreSystem.Web.Models.StoreUserViewModels;

namespace StoreSystem.Web.Mappers
{
    public class StoreUserViewModelMapper : IViewModelMapper<StoreUser, StoreUserViewModel>
    {
        public StoreUserViewModel MapFrom(StoreUser entity)
             => new StoreUserViewModel
             {
                 UserName = entity.UserName,
                 Phone = entity.PhoneNumber,
                 Email = entity.Email,
                 UserID = entity.Id,
                 IsProtected = false,
                 Role = "ROLE IS NOT SET!"
             };
    }
}
