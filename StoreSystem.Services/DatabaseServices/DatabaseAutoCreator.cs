//using Microsoft.AspNetCore.Identity;
//using StoreSystem.Data.DbContext;
//using StoreSystem.Data.Models;
//using StoreSystem.Services;
//using StoreSystem.Services.DatabaseServices.Contracts;
//using StoreSystem.Services.Dto;
//using StoreSystem.Services.Providers;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace StoreSystem.Services.DatabaseServices
//{
//    public class DatabaseAutoCreator
//    {
//        private readonly UserManager<StoreUser> userManager;
//        private readonly IDatabaseJSONConnectivity<string> jSONConnectivityService;
//        private readonly StoreSystemDbContext context;
//        private readonly IDatabaseService databaseService;

//        public DatabaseAutoCreator(
//            StoreSystemDbContext context,
//            IDatabaseService databaseService,
//            IDateTimeNowProvider dateTimeProvider,
//            UserManager<StoreUser> userManager,
//            IDatabaseJSONConnectivity<string> JSONConnectivityService)
//        {
//            this.dateTimeProvider = dateTimeProvider;
//            this.userManager = userManager;
//            jSONConnectivityService = JSONConnectivityService;
//            this.context = context;
//            this.databaseService = databaseService;
//        }

//        public void PopulateDatabase()
//        {
//            context.Database.EnsureCreated();

//            // !!! countriesCount must be grater than warehousesCount because of the one to one relation between the 
//            // warehause and the address , city and country
//            int countriesCount = 4;
//            int citiesCount = 10;
//            int addressesCount = 20;
//            int warehousesCount = 3;
//            int productsCount = 100;
//            int suppliersCount = 10;
//            int clientsCount = 15;
//            int offersCount = 50;
//            int salesCount = 50;
//            int purchasesCount = 20;

//            PopulateCountries(countriesCount, context);
//            PopulateCities(citiesCount, context);
//            PopulateAddresses(addressesCount, context);
//            PopulateWarehouses(warehousesCount, addressesCount, citiesCount, countriesCount, context);
//            PopulateProducts(productsCount, context);
//            PopulateSuppliers(suppliersCount, addressesCount, citiesCount, countriesCount, context);
//            PopulateClients(clientsCount, addressesCount, citiesCount, countriesCount, context);
//            PopulateOffers(offersCount, clientsCount, addressesCount, citiesCount, countriesCount, context);
//            PopulateSales(salesCount, offersCount, clientsCount, addressesCount, citiesCount, countriesCount, context);
//            PopulatePurchases(purchasesCount, suppliersCount, warehousesCount, context);
//            PopulateSupplierProducts(suppliersCount, productsCount, context);
//            PopulateSupplierPurchases(suppliersCount, purchasesCount, context);
//            PopulateOfferProducts(offersCount, productsCount, context);
//            PopulateSaleProducts(salesCount, productsCount, context);
//            PopulatePurchaseProducts(purchasesCount, productsCount, context);
//        }

//        private readonly IDateTimeNowProvider dateTimeProvider;

//        private string GetNumericString(int length)
//        {
//            Random r = new Random();
//            string tmpString = "";
//            for (int i = 0; i < length; i++)
//            {
//                tmpString = tmpString + (r.Next() % 10).ToString();
//            }
//            return tmpString;
//        }

//        private bool PopulateAddresses(int addressesCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Address";
//            while (addressesCount > 0)
//            {
//                var addressService = new AddressService(context);
//                var result = addressService.CreateAddressAsync($"{param}{addressesCount}", true);
//                if (result == null)
//                {
//                    arePopulated = false;
//                    addressesCount--;
//                    continue;
//                }
//                //Console.WriteLine($"{param} with name {result?.Name} has ID {result?.AddressID}");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();
//                addressesCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateCities(int citiesCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "City";
//            while (citiesCount > 0)
//            {
//                var cityService = new CityService(this.context, this.jSONConnectivityService);
//                var result = cityService.CreateCityAsync($"{param}{citiesCount}", true);
//                if (result == null)
//                {
//                    arePopulated = false;
//                    citiesCount--;
//                    continue;
//                }
//                //Console.WriteLine($"{param} with name {result?.Name} has ID {result?.CityID}");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();
//                citiesCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateCountries(int countriesCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Country";
//            while (countriesCount > 0)
//            {
//                var countryService = new CountryService(this.context, this.jSONConnectivityService);
//                var result = countryService.CreateCountryAsync($"{param}{countriesCount}");
//                if (result == null)
//                {
//                    arePopulated = false;
//                    countriesCount--;
//                    continue;
//                }
//                //Console.WriteLine($"{param} with name {result?.Name} has ID {result?.CountryID}");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();
//                countriesCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateWarehouses(int warehousesCount, int addressCount, int cityCount, int countryCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Warehouse";
//            while (warehousesCount > 0)
//            {
//                var warehouseService = new WarehouseService(context);
//                var warehouseTempName = $"{param}{warehousesCount}";

//                if(null != warehouseService.FindWarehouseByName(warehouseTempName))
//                {
//                    warehousesCount--;
//                    continue;
//                }
//                Random r = new Random(); 
//                var result = warehouseService.CreateWarehouse(
//                    warehouseTempName,
//                    warehousesCount,
//                    warehousesCount,
//                    warehousesCount);
//                if (result == null)
//                {
//                    arePopulated = false;
//                    warehousesCount--;
//                    continue;
//                }
//                //Console.WriteLine($"{param} with name {result?.Name} has ID {result?.AddressID}");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();
//                warehousesCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateProducts(int productCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Product";
//            while (productCount > 0)
//            {
//                var productService = new ProductService(context);
//                var productTempName = $"{param}{productCount}";
//                var result = productService.FindProductByNameAsync(productTempName);
//                if (result != null)
//                {
//                    //Console.WriteLine($"{param} with name {result?.Name} has ID = {result?.ProductID}");
//                    //Console.WriteLine("Press ENTER");
//                    //Console.ReadLine();
//                    productCount--;
//                    continue;
//                }

//                Random r = new Random();
//                result = productService.CreateProductAsync(
//                    productTempName,
//                    "pcs",
//                    20000 + (r.Next() % 100),
//                    (r.Next() % 10000) / 100,
//                    (r.Next() % 10000) / 100);
//                //Console.WriteLine($"{param} with name {result?.Name} has ID {result?.ProductID}");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                productCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateSuppliers(int suppliersCount, int addressesCount, int citiesCount, int countriesCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Supplier";
//            while (suppliersCount > 0)
//            {
//                var supplierService = new SupplierService(this.context, this.userManager, this.databaseService);
//                var supplierTempName = $"{param}{suppliersCount}";
//                var result = supplierService.FindSupplierByName(supplierTempName);
//                if (result != null)
//                {
//                    //Console.WriteLine($"{param} with name {result?.Name} has ID = {result?.SupplierID}");
//                    //Console.WriteLine("Press ENTER");
//                    //Console.ReadLine();
//                    suppliersCount--;
//                    continue;
//                }

//                Random r = new Random();
//                result = supplierService.CreateSupplierAsync(
//                    supplierTempName,
//                    GetNumericString(9),
//                    //$"{supplierTempName}@gmail.com",
//                    //$"0888{GetNumericString(6)}",
//                    r.Next() % countriesCount + 1,
//                    r.Next() % citiesCount + 1,
//                    r.Next() % addressesCount + 1,
//                    "userId").Result;
//                //Console.WriteLine($"{param} with name {result?.Name} has ID {result?.SupplierID}");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                suppliersCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateSupplierPurchases(int supplierCount, int purchaseCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;

//            Random r = new Random();
//            var supplierService = new SupplierService(this.context, this.userManager, this.databaseService);
//            for (int supplierID = 1; supplierID <= supplierCount; supplierID++)
//            {
//                var result = supplierService.FindSupplierByID(supplierID);

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                else
//                {
//                    for (int i = 0; i < (purchaseCount / 4 + 1); i++)
//                    {
//                        var purchaseIdTmp = r.Next() % (purchaseCount) + 1;
//                        if (context.Purchases.All(p => p.PurchaseID != purchaseIdTmp))
//                        {
//                            continue;
//                        }
//                        if(result.Purchases.Any(p => p.PurchaseID == purchaseIdTmp))
//                        {
//                            continue;
//                        }
//                        var resultAddPurchaseToSupplier = supplierService.AddPurchaseToSupplier(result.SupplierID, purchaseIdTmp);
//                    }
//                }
//            }
//            return arePopulated;
//        }

//        private bool PopulateSupplierProducts(int supplierCount, int productCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;

//            Random r = new Random();
//            var supplierService = new SupplierService(this.context, this.userManager, this.databaseService);
//            for (int supplierID = 1; supplierID <= supplierCount; supplierID++)
//            {
//                var result = supplierService.FindSupplierByID(supplierID);

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                else
//                {
//                    for (int i = 0; i < (productCount / 4 + 1); i++)
//                    {
//                        var productIdTmp = r.Next() % productCount + 1;
//                        if (context.Products.All(p => p.ProductID != productIdTmp))
//                        {
//                            continue;
//                        }
//                        if (result.ProductsOfSupplier.Any(p => p.ProductID == productIdTmp))
//                        {
//                            continue;
//                        }
//                        var resultAddProductToSupplier = supplierService.AddProductToSupplier(result.SupplierID, productIdTmp);
//                    }
//                }
//            }
//            return arePopulated;
//        }

//        private bool PopulateClients(
//            int clientsCount,
//            int addressCount,
//            int cityCount,
//            int countryCount,
//            StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Client";
//            while (clientsCount > 0)
//            {
//                var clientService = new ClientService(this.context, this.userManager, this.databaseService);
//                var clientTempName = $"{param}{clientsCount}";
//                var result = clientService.FindClientByNameAsync(clientTempName);
//                if (result != null)
//                {
//                    //Console.WriteLine($"{param} with name {result?.Name} has ID = {result?.ClientID}");
//                    //Console.WriteLine("Press ENTER");
//                    //Console.ReadLine();
//                    clientsCount--;
//                    continue;
//                }

//                Random r = new Random();
//                //result = clientService.CreateClientAsync(
//                //    clientTempName,
//                //    GetNumericString(9),
//                //    //$"{clientTempName}@gmail.com",
//                //    //"0888" + GetNumericString(6),
//                //    r.Next() % addressCount + 1,
//                //    r.Next() % cityCount + 1,
//                //    r.Next() % countryCount + 1,
//                //    "userId").Result;
//                //Console.WriteLine($"{param} with name {result?.Name} has ID {result?.ClientID}");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                clientsCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateSales(
//            int salesCount,
//            int offersCount,
//            int clientCount,
//            int addressesCount,
//            int citiesCount,
//            int countriesCount,
//            StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Sale";
//            while (salesCount > 0)
//            {
//                var saleService = new SaleService(context, this.dateTimeProvider);
//                var result = saleService.GetSaleWithProductsByIDAsync(salesCount).Result;
//                if (result != null)
//                {
//                    //Console.WriteLine($"{param} with ID = {result?.SaleID} exists");
//                    //Console.WriteLine("Press ENTER");
//                    //Console.ReadLine();
//                    salesCount--;
//                    continue;
//                }

//                Random r = new Random();
//                result = saleService.CreateSaleAsync(
//                    r.Next() % clientCount + 1,
//                    (r.Next() % 100) / 100,
//                    r.Next() % 100,
//                    r.Next() % addressesCount + 1,
//                    r.Next() % citiesCount + 1,
//                    r.Next() % countriesCount + 1,
//                    r.Next() % offersCount + 1).Result;
//                //Console.WriteLine($"{param} with ID = {result?.SaleID} created");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                salesCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateSaleProducts(int saleCount, int productCount, StoreSystemDbContext context)
//        {
//            Random r = new Random();
//            bool arePopulated = true;
//            var saleService = new SaleService(context, this.dateTimeProvider);
//            for (int saleID = 1; saleID <= saleCount; saleID++)
//            {
//                var result = saleService.GetSaleWithProductsByIDAsync(saleID).Result;
//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                else
//                {
//                    for (int i = 0; i < (productCount / 4 + 1); i++)
//                    {
//                        var productResult = context.Products.Find(r.Next() % productCount + 1);
//                        var resultAddProductToSupplier = saleService.AddProductsToSaleAsync(result.SaleID, new ProductQuantityDto(productResult.Name, 2m)).Result;
//                    }
//                }
//            }
//            return arePopulated;
//        }

//        private bool PopulatePurchases(int purchasesCount, int suppliersCount, int warehousesCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Purchase";
//            while (purchasesCount > 0)
//            {
//                var purchaseService = new PurchaseService(context, this.dateTimeProvider);
//                var result = purchaseService.FindPurchaseByID(purchasesCount);
//                if (result != null)
//                {
//                    //Console.WriteLine($"{param} with ID = {result?.PurchaseID} exists");
//                    //Console.WriteLine("Press ENTER");
//                    //Console.ReadLine();
//                    purchasesCount--;
//                    continue;
//                }
//                Random r = new Random();
//                var warehouseTmp = 1 + (r.Next() % warehousesCount);
//                if (context.Warehouses.All(w => w.WarehouseID != warehouseTmp))
//                {
//                    return false;
//                }
//                result = purchaseService.CreatePurchase(
//                    1 + (r.Next() % suppliersCount),
//                    warehouseTmp,
//                    r.Next() % 100);
//                //Console.WriteLine($"{param} with ID = {result?.PurchaseID} created");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                purchasesCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulatePurchaseProducts(int purchasesCount, int productCount, StoreSystemDbContext context)
//        {
//            Random r = new Random();
//            bool arePopulated = true;
//            var purchaseService = new PurchaseService(context, this.dateTimeProvider);

//            for (int purchaseID = 1; purchaseID <= purchasesCount; purchaseID++)
//            {
//                var result = purchaseService.FindPurchaseByID(purchaseID);


//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                else
//                {
//                    for (int i = 0; i < (productCount / 4 + 1); i++)
//                    {
//                        var productResult = context.Products.Find(r.Next() % productCount + 1);
//                        var resultAddProductToSupplier = purchaseService.AddProductsToPurchase(result.PurchaseID, new KeyValuePair<string, decimal>(productResult.Name, 2m));
//                    }
//                }
//            }
//            return arePopulated;
//        }

//        private bool PopulateOffers(
//            int offersCount,
//            int clientCount,
//            int addressCount,
//            int cityCount,
//            int countryCount,
//            StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var param = "Offer";
//            while (offersCount > 0)
//            {
//                var offerService = new OfferService(context, this.dateTimeProvider);
//                var result = offerService.GetOfferWithProductsByIDAsync(offersCount);
//                if (result != null)
//                {
//                    //Console.WriteLine($"{param} with ID = {result?.OfferID} exists");
//                    //Console.WriteLine("Press ENTER");
//                    //Console.ReadLine();
//                    offersCount--;
//                    continue;
//                }

//                Random r = new Random();
//                var clientIdTmp = r.Next() % clientCount + 1;

//                result = offerService.CreateOfferAsync(
//                    clientIdTmp,
//                    10,
//                    r.Next() % addressCount + 1,
//                    r.Next() % cityCount + 1,
//                    r.Next() % countryCount + 1,
//                    (r.Next() % 100) / 100);
//                //Console.WriteLine($"{param} with ID = {result?.OfferID} created");
//                //Console.WriteLine("Press ENTER");
//                //Console.ReadLine();

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                offersCount--;
//            }
//            return arePopulated;
//        }

//        private bool PopulateOfferProducts(int offerCount, int productCount, StoreSystemDbContext context)
//        {
//            bool arePopulated = true;
//            var offerService = new OfferService(context, this.dateTimeProvider);

//            Random r = new Random();
//            for (int offerID = 1; offerID <= offerCount; offerID++)
//            {
//                var result = offerService.GetOfferWithProductsByIDAsync(offerID);

//                if (result == null)
//                {
//                    arePopulated = false;
//                }
//                else
//                {
//                    for (int i = 0; i < (productCount / 4 + 1); i++)
//                    {
//                        var productResult = context.Products.Find(r.Next() % productCount + 1);
//                        //var resultAddProductToSupplier = offerService.AddProductsToOfferAsync(result.OfferID, new KeyValuePair<string, decimal>(productResult.Name, 2m));
//                    }
//                }
//            }
//            return arePopulated;
//        }
//    }
//}
