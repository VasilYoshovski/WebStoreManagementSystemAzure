using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Services.Dto;
using StoreSystem.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{


    public class ProductSaleService : IProductSaleService
    {
        private readonly StoreSystemDbContext context;

        public ProductSaleService(StoreSystemDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<IGrouping<int, ProductTotalDto>>> GetProductsTotalSaleQuantityAsync(string alies = Consts.AllText)
        {
            var query = this.context.ProductSales.Include(x => x.Product).Include(x => x.Sale).AsQueryable();
            if (alies != Consts.AllText)
            {
                query = query.Where(x => x.Product.Name.Contains(alies));
            }

            var groupObject = (from sale in
                         from saleproduct in query
                         orderby saleproduct.Product.Name ascending
                         select new ProductTotalDto()
                         {
                             Product = saleproduct.Product.Name,
                             ProductID = saleproduct.ProductID,
                             Quantity = saleproduct.Quantity,
                             Total = saleproduct.Quantity * saleproduct.Product.RetailPrice * (1 - saleproduct.Sale.ProductDiscount)
                         }
                     group sale by sale.ProductID).ToListAsync();
            return await groupObject;
        }
    }
}
