using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Web.Constants;
using System.Linq;

namespace StoreSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            SeedDatabaseWithRoles(host, ROLES.Roles());
            SeedDatabaseWithAdmin(host);
            
            host.Run();
        }

        private static void SeedDatabaseWithRoles(IWebHost host, string[] initialRoles)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StoreSystemDbContext>();
                foreach (var roleTmp in initialRoles)
                {
                    if (dbContext.Roles.All(u => u.Name != roleTmp))
                    {
                        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                        roleManager.CreateAsync(new IdentityRole { Name = roleTmp }).Wait();
                    }
                }
            }
        }

        private static void SeedDatabaseWithAdmin(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StoreSystemDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<StoreUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                SeedDatabaseWithRoles(host, ROLES.Roles());

                if (dbContext.Users.All(u => u.Email != StoreConstants.ADMIN_EMAIL))
                {
                    var adminUser = new StoreUser
                    {
                        UserName = StoreConstants.ADMIN_EMAIL,
                        Email = StoreConstants.ADMIN_EMAIL
                    };
                    var result = userManager.CreateAsync(adminUser, StoreConstants.ADMIN_PASSWORD).Result;
                    if (result.Succeeded)
                    {
                        result = userManager.AddToRoleAsync(adminUser, ROLES.Admin).Result;
                    }
                    if (result.Succeeded)
                    {
                        //logger.LogInformation("User created a new account with password.");

                        //var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                        //await emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                        //await signInManager.SignInAsync(user, isPersistent: false);
                        //logger.LogInformation("User created a new account with password.");
                        //return RedirectToLocal(returnUrl);
                    }
                    //AddErrors(result);
                }

                if (dbContext.Users.All(u => u.Email != StoreConstants.OFFICE_STAFF_EMAIL))
                {
                    var adminUser = new StoreUser
                    {
                        UserName = StoreConstants.OFFICE_STAFF_EMAIL,
                        Email = StoreConstants.OFFICE_STAFF_EMAIL
                    };
                    var result = userManager.CreateAsync(adminUser, StoreConstants.OFFICE_STAFF_PASSWORD).Result;
                    if (result.Succeeded)
                    {
                        result = userManager.AddToRoleAsync(adminUser, ROLES.OfficeStaff).Result;
                    }
                    if (result.Succeeded)
                    {
                        //logger.LogInformation("User created a new account with password.");

                        //var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                        //await emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                        //await signInManager.SignInAsync(user, isPersistent: false);
                        //logger.LogInformation("User created a new account with password.");
                        //return RedirectToLocal(returnUrl);
                    }
                    //AddErrors(result);
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
