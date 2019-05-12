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
    public class GetNotCLosedSales_Should
    {
        [TestMethod]
        public async Task ReturnAllNotClosedSales_WhenIsInvolkedPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);
            var expectedCountOfNotClosed = 1;
            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualSales = await sut.GetNotClosedSalesAsync();

                //Assert
                Assert.AreEqual(expectedCountOfNotClosed, actualSales.Count);
            }
        }
    }
}
