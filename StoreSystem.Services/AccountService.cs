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
    public class AccountService : IAccountService
    {
        private readonly StoreSystemDbContext context;
        private readonly DatabaseService databaseService;

        public AccountService(StoreSystemDbContext context, DatabaseService databaseService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.databaseService = databaseService;
        }
    }
}
