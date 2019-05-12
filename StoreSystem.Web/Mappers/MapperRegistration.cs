using Microsoft.Extensions.DependencyInjection;
using StoreSystem.Data.Models;
using StoreSystem.Web.Models.ClientViewModels;
using StoreSystem.Web.Models.OfferViewModels;
using StoreSystem.Web.Models.ProductViewModels;
using StoreSystem.Web.Models.PurchaseViewModels;
using StoreSystem.Web.Models.SaleViewModels;
using StoreSystem.Web.Models.StoreUserViewModels;
using StoreSystem.Web.Models.SupplierViewModels;
using System.Collections.Generic;

namespace StoreSystem.Web.Mappers
{
    public static class MapperRegistration
    {
        public static IServiceCollection AddCustomMappers(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelMapper<Product, ProductViewModel>, ProductViewModelMapper>();
            services.AddSingleton<IViewModelMapper<IReadOnlyCollection<Product>, ProductsCollectionViewModel>, ProductsCollectionModelMapper>();
            services.AddSingleton<IViewModelMapper<Sale, SaleInfoViewModel>, SaleInfoViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Sale, SaleCUViewModel>, SaleCUViewModelMapper>();
            services.AddSingleton<IViewModelMapper<ProductSale, ProductLineInfoViewModel>, ProductInSaleInfoViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Offer, OfferCUViewModel>, OfferCUViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Offer, OfferInfoViewModel>, OfferInfoViewModelMapper>();
            services.AddSingleton<IViewModelMapper<ProductOffer, ProductLineInfoViewModel>, ProductInOfferInfoViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Client, ClientInfoViewModel>, ClientInfoViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Client, ClientCUViewModel>, ClientCUViewModelMapper>();
            services.AddSingleton<IViewModelMapper<StoreUser, StoreUserViewModel>, StoreUserViewModelMapper >();
            services.AddSingleton<IViewModelMapper<List<StoreUser>, StoreUsersCollectionViewModel>, StoreUsersCollectionViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Supplier, SupplierViewModel>, SupplierViewModelMapper>();
            services.AddSingleton<IViewModelMapper<List<Supplier>, SuppliersCollectionViewModel>, SuppliersCollectionViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Purchase, PurchaseViewModel>, PurchaseViewModelMapper>();
            services.AddSingleton<IViewModelMapper<List<Purchase>, PurchasesCollectionViewModel>, PurchasesCollectionViewModelMapper>();

            return services;
        }
    }
}
