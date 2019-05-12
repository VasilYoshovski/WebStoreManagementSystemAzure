using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;

namespace StoreSystem.Services
{
    public interface IStoreUserService
    {
        Task<IReadOnlyList<StoreUser>> GetVisitorUsers();
        Task<string> GetUserNameByUserId(string userID);
        Task<List<StoreUser>> GetAllStoreUsersAsync();
        Task<List<StoreUser>> GetAllStoreUsersByFilterAsync(int from, int to, string contains);
        Task<StoreUser> GetUserByName(string userName);
    }
}