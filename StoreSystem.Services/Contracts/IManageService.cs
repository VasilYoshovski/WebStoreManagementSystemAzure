using Microsoft.AspNetCore.Identity;
using StoreSystem.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreSystem.Services.Contracts
{
    public interface IManageService
    {
        Task<IdentityRole> FindRoleByNameAsync(string roleString);
        Task<IdentityRole> FindRoleByIDAsync(int RoleID);
        Task<IdentityRole> CreateRoleAsync(string roleString, bool save = true);
        Task<ICollection<StoreUser>> GetListOfAllUsersByRoleAsync(string roleName);
        Task<List<StoreUser>> GetAllUsersAsync();
        Task<List<StoreUser>> GetAllUsersByFilterAsync(int from, int to, string contains);
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<List<IdentityRole>> GetAllRolesByFilterAsync(int from, int to, string contains);
    }
}
