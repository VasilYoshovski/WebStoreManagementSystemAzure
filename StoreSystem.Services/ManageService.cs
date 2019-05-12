using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class ManageService : IManageService
    {
        private readonly StoreSystemDbContext context;
        private readonly IDatabaseService databaseService;

        public ManageService(StoreSystemDbContext context, IDatabaseService databaseService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.databaseService = databaseService;
        }

        public async Task<IdentityRole> FindRoleByNameAsync(string roleString)
        {
            return await this.context.Roles.FirstOrDefaultAsync(x => x.Name == roleString);
        }

        public async Task<IdentityRole> FindRoleByIDAsync(int RoleID)
        {
            return await this.context.Roles.FindAsync(RoleID);
        }

        public async Task<IdentityRole> CreateRoleAsync(string roleString, bool save = true)
        {
            var Role = await this.context.Roles.FirstOrDefaultAsync(x => x.Name == roleString);

            if (Role == null)
            {
                Role = new IdentityRole { Name = roleString };
                context.Roles.Add(Role);
                if (save)
                {
                    await context.SaveChangesAsync();
                }
            }
            return Role;
        }

        public async Task<ICollection<StoreUser>> GetListOfAllUsersByRoleAsync(string roleName)
        {
            return await this.databaseService.GetUsersByRoleAsync(roleName);
        }

        public async Task<List<StoreUser>> GetAllUsersAsync()
        {
            var query = this.context.Users
                .OrderBy(x => x.UserName);
            return await query.ToListAsync();
        }

        public async Task<List<StoreUser>> GetAllUsersByFilterAsync(int from, int to, string contains)
        {
            var allUsers = await GetAllUsersAsync();
            if (allUsers != null)
            {
                if (!string.IsNullOrWhiteSpace(contains))
                {
                    contains = contains.Trim().ToLower();
                    var filteredUsers = allUsers
                        .Where(x => x.UserName.ToLower().Contains(contains))
                        .Skip(from)
                        .Take(to);
                    return filteredUsers.ToList();
                }
            }
            return allUsers;
        }

        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            var query = this.context.Roles
                .OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<List<IdentityRole>> GetAllRolesByFilterAsync(int from, int to, string contains)
        {
            var allRoles = await GetAllRolesAsync();
            if (allRoles != null)
            {
                if (!string.IsNullOrWhiteSpace(contains))
                {
                    contains = contains.Trim().ToLower();
                    var filteredRoles = allRoles
                        .Where(x => x.Name.ToLower().Contains(contains))
                        .Skip(from)
                        .Take(to);
                    return filteredRoles.ToList();
                }
            }
            return allRoles;
        }
    }
}
