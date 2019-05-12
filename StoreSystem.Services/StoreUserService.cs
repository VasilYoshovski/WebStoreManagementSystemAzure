using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class StoreUserService : IStoreUserService
    {
        private readonly StoreSystemDbContext context;
        private readonly UserManager<StoreUser> userManager;
        private readonly IDatabaseService databaseService;

        public StoreUserService(
            StoreSystemDbContext context,
            UserManager<StoreUser> userManager,
            IDatabaseService databaseService
            )
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.databaseService = databaseService;
        }

        public async Task<StoreUser> GetUserByName(string userName)
        {
            var user = await context.Users
                .Include("Supplier")
                .Include("Client")
                .Include("Employee")
                .Where(x => x.UserName == userName).
                FirstOrDefaultAsync();

            return user;
        }

        public async Task<string> GetUserNameByUserId(string userID)
        {
            var user = await context.StoreUsers.FindAsync(userID);

            return user.UserName;
        }

        public async Task<IReadOnlyList<StoreUser>> GetVisitorUsers()
        {
            return await databaseService.GetUsersByRoleAsync(ROLES.Visitor);
        }

        public async Task<List<StoreUser>> GetAllStoreUsersAsync()
        {
            var query = this.context.StoreUsers
                .OrderBy(x => x.UserName);
            return await query.ToListAsync();
        }

        public async Task<List<StoreUser>> GetAllStoreUsersByFilterAsync(int from, int to, string contains)
        {
            var allStoreUsers = await GetAllStoreUsersAsync();
            if (allStoreUsers != null)
            {
                if (!string.IsNullOrWhiteSpace(contains))
                {
                    contains = contains.Trim().ToLower();
                    var filteredStoreUsers = allStoreUsers
                        .Where(x => x.UserName.ToLower().Contains(contains))
                        .Skip(from)
                        .Take(to);
                    return filteredStoreUsers.ToList();
                }
            }
            return allStoreUsers;
        }
    }
}
