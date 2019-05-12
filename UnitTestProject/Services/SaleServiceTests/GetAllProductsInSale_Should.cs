using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.SaleServiceTests
{
    [TestClass]
    public class GetAllProductsInOffer_Should
    {
        [TestMethod]
        public async Task ShouldReturnCollectionWithProductInSale_WhenValidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validSaleId = 1;
            int productsInSaleCount = 2;
            ICollection<ProductSale> products = new List<ProductSale>
                        {
                            new ProductSale{ProductID = 1, Quantity =1 },
                            new ProductSale{ProductID = 2, Quantity =1 }
                        };

            //using (var getContext = new StoreSystemDbContext(options))
            //{
            //    var sale = getContext.Sales.Include(x => x.ProductsInSale).First(x => x.ProductsInSale.Count > 0);
            //    validSaleId = sale.SaleID;
            //    products = sale.ProductsInSale;
            //    productsInSaleCount = products.Count;
            //}

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualProductsInSale = await sut.GetAllProductsInSaleAsync(validSaleId);

                //Assert
                Assert.AreEqual(productsInSaleCount, actualProductsInSale.Count);
                CollectionAssert.AreEquivalent(products.Select(x => x.ProductID).ToList(), actualProductsInSale.Select(x => x.ProductID).ToList());
            }
        }

        [TestMethod]
        public async Task ShouldThrowsArgumentException_WhenInvalidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int invalidSaleID = 100;

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(() =>sut.GetAllProductsInSaleAsync(invalidSaleID));
            }
        }
    }
}

