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
    public class DeleteSale_Should
    {
        [TestMethod]
        public async Task DeleteProperSale_WhenValidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validSaleId;
            int totalCountOfSales;

            using (var getContext = new StoreSystemDbContext(options))
            {
                validSaleId = getContext.Sales.First().SaleID;
                totalCountOfSales = getContext.Sales.Count();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var isExecxuted = await sut.DeleteSaleAsync(validSaleId);

                //Assert
                Assert.IsTrue(isExecxuted);
                Assert.IsTrue(context.Sales.Find(validSaleId) == null);
                Assert.AreEqual(totalCountOfSales - 1, context.Sales.Count());
            }
        }

        [TestMethod]
        public async Task DeleteProperProductSaleRecords_WhenValidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);
            Sale sale;
            int totalRecordsInProductSale;

            using (var getContext = new StoreSystemDbContext(options))
            {
                sale = getContext.Sales.Include(x => x.ProductsInSale).First();
                totalRecordsInProductSale = getContext.ProductSales.Count();    
            }

            int validSaleId = sale.SaleID;
            var countOfProductsInSale = sale.ProductsInSale.Count();

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var isExecxuted = await sut.DeleteSaleAsync(validSaleId);

                //Assert
                Assert.IsTrue(isExecxuted);
                Assert.AreEqual(totalRecordsInProductSale - countOfProductsInSale, context.ProductSales.Count());
            }
        }

        [TestMethod]
        public async Task ThrowsArgumentException_WhenInvalidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int invalidSaleID = 100;

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.DeleteSaleAsync(invalidSaleID));
            }
        }
    }
}


