using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Services.Contracts;
using StoreSystem.Services.DatabaseServices;
using StoreSystem.Services.DatabaseServices.Contracts;
using StoreSystem.Services.Providers;
using StoreSystem.Web.Mappers;
using StoreSystem.Web.Services;

namespace StoreSystem.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            bool useAzure = this.Configuration.GetSection("EnableAZURE").GetValue<bool>("IsEnabled");
            if (useAzure)
            {
                services.AddDbContext<StoreSystemDbContext>(options =>
                    options.UseSqlServer(this.Configuration.GetConnectionString("AzureConnection")));
            }
            else
            {
                services.AddDbContext<StoreSystemDbContext>(options =>
                    options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddIdentity<StoreUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreSystemDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IStoreUserService, StoreUserService>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<IManageService, ManageService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IDatabaseJSONConnectivity<string>, DatabaseJSONConnectivity<string>>();

            services.AddTransient<IDateTimeNowProvider, DateTimeNowProvider>();
            services.AddCustomMappers();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Admin/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Admin/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "savepdf",
                    template: "savepdf",
                    defaults: new { controller = "PdfCreatorController", action = "CreatePDF" });

                routes.MapRoute(
                    name: "create_address",
                    template: "createAddress/{newAddress}",
                    defaults: new { controller = "Addresses", action = "Create" });

                routes.MapRoute(
                    name: "create_city",
                    template: "createCity/{newAddress}",
                    defaults: new { controller = "Cities", action = "Create" });

                routes.MapRoute(
                    name: "create_country",
                    template: "createCountry/{newAddress}",
                    defaults: new { controller = "Countries", action = "Create" });

                routes.MapRoute(
                    name: "Admin",
                    template: "{area:exists}/{controller=StoreConfigurations}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
