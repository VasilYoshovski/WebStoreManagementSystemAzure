using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Services.Dto;
using StoreSystem.Services.Providers;
using System;
using System.Collections.Generic;

namespace ServicesManualTest
{
    class Program
    {
        private static (string name, IReadOnlyList<string> args) Spliter(String input)
        {
            string command;
            var res = new List<string>();
            var nextChar = " ";
            var emptyS = input.IndexOf(" ", 0);
            if (emptyS == -1) emptyS = input.Length + 1;
            var quoteS = input.IndexOf(" '", 0);
            if (quoteS == -1) quoteS = input.Length + 1;

            if (quoteS == input.Length + 1 && emptyS == input.Length + 1)
            {
                //res.Add(input);
                return (input, new List<string>());
            }
            int cur = 0;
            if (quoteS <= emptyS)
            {
                nextChar = " '";
                cur = quoteS;
            }
            else
            {
                nextChar = " ";
                cur = emptyS;
            }
            //res.Add(input.Substring(0, cur));
            command = input.Substring(0, cur);
            while (true)
            {
                if (nextChar == " '")
                {
                    var next = input.IndexOf("' ", cur + 2);
                    if (next == -1) next = input.Length - 1;
                    res.Add(input.Substring(cur + 2, next - cur - 2));
                    if (next >= input.Length - 1) break;
                    cur = next + 1;
                }
                else
                {
                    var next = input.IndexOf(" ", cur + 1);
                    if (next == -1) next = input.Length;
                    res.Add(input.Substring(cur + 1, next - cur - 1));
                    if (next >= input.Length - 1) break;
                    cur = next;
                }
                emptyS = input.IndexOf(" ", cur);
                if (emptyS == -1) emptyS = input.Length + 1;
                quoteS = input.IndexOf(" '", cur);
                if (quoteS == -1) quoteS = input.Length + 1;

                nextChar = quoteS <= emptyS ? " '" : " ";

                if (quoteS == input.Length + 1 && emptyS == input.Length + 1)
                {
                    throw new ArgumentException("Parse error");
                }
            }
            return (command, res);
        }

        static void Main(string[] args)
        {
            var context = new StoreSystemDbContext();
            var dateNow = new DateTimeNowProvider();
            var addressService = new AddressService(context);
            var cityService = new CityService(context);
            var countryService = new CountryService(context);
            var saleService = new SaleService(context, new DateTimeNowProvider());
            var clientService = new ClientService(context);
            var productService = new ProductService(context);
            var offerService = new OfferService(context, dateNow);
            var supplierService = new SupplierService(context);
            var purchaseService = new PurchaseService(context, new DateTimeNowProvider());
            var warehouseService = new WarehouseService(context);

            addressService.GetListOfAllClientsbyID(1);

            cityService.GetListOfAllClientsbyName("Sofia");
            countryService.GetListOfAllClientsbyName("Bulgaria");
            clientService.FindClientWithAddress(1);
            clientService.GetAllClients(0, 100, "tash");
            clientService.UpdateClient(1, null, "321654987", null, null, null, null, null);
            clientService.GetClientSales(4);
            


























            try
            {
                //var a = clientService.CreateClient("Pesho", "123456789", "my@gmail.com", "0888500050",
                //    new Address() { Name = "Nova Strasse" }, new City() { Name = "Berlin" }, new Country() { Name = "Germany" });
                //Console.WriteLine(a?.ClientID + " " +a?.Name);
                //addressService.CreateAddress("Malinova dolina 2");
                //addressService.CreateAddress("Ovcha kupel otzad");
                //addressService.CreateAddress("Krasna polqna bai cig");
                //addressService.CreateAddress("ul. Krasnorech");
                //addressService.CreateAddress("jk. Liulin 11");
                //addressService.CreateAddress("Cheroshova gradina 5");

                //cityService.CreateCity("Sofia");
                //cityService.CreateCity("Dolno Uino");
                //cityService.CreateCity("Pelnik");

                //productService.CreateProduct("Piron 1.2", "pcs", 100, 1.50m, 1.80m);
                //productService.CreateProduct("Nailon 2mk", "pcs", 1200, 2.60m, 3.80m);
                //productService.CreateProduct("Konop", "pcs", 45, 3.50m, 5.80m);
                //productService.CreateProduct("Lager 6.19", "pcs", 55, 8.20m, 10.70m);
                //productService.CreateProduct("Bager JCB", "pcs", 5, 50000.00m, 60000.00m);
                //productService.CreateProduct("Fadroma Liebherr", "pcs", 2, 61000.00m, 66000.00m);
                //productService.CreateProduct("Lopata prava", "pcs", 35, 10.30m, 13.80m);
                //productService.CreateProduct("Chuk kofrajen", "pcs", 190, 18.90m, 22.80m);

                //saleService.CreateSale(
                //    clientService.FindClientByName("Pesho"),
                //    7,
                //    addressService.FindAddressByName("Nova Strasse"),
                //    cityService.FindCityByName("Berlin"),
                //    countryService.FindCountryByName("Germany")
                //    );

                //saleService.AddProductsToSale(1, new KeyValuePair<string, decimal>("Konop", 10),
                //                                 new KeyValuePair<string, decimal>("Lager 6.19", 20),
                //                                 new KeyValuePair<string, decimal>("Lopata prava", 8));

                //var a =saleService.GetSaleQuantityByDate(new DateTime(2018, 1, 1), new DateTime(2019, 12, 1));
                //Console.WriteLine(a.ToString("f2"));

                //var (a, b) = Spliter(Console.ReadLine());
                //Console.WriteLine(a);
                //Console.WriteLine(String.Join(", ",b));

                saleService.AddProductsToSale(4, new ProductQuantityDto("pironche", 1),
                    new ProductQuantityDto("lopata prava", 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
