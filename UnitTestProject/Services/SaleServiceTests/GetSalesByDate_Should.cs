//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using StoreSystem.Data.DbContext;
//using StoreSystem.Data.Models;
//using StoreSystem.Services;
//using StoreSystem.Services.Providers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StoreSystem.Tests.Services.SaleServiceTests
//{
//    [TestClass]
//    public class GetSalesByDate_Should
//    {
//        [TestMethod]
//        public async Task ShouldReturnCollectionWithSales_WhenValidDatesArePassed()
//        {
//            //Arrange
//            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

//            var options = DbSeed.GetOptions(databaseName);

//            DbSeed.SeedDatabase(options);

//            var validStartDate = new DateTime(2018, 12, 31);
//            var validEndDate = new DateTime(2019, 2, 3);

//            using (var context = new StoreSystemDbContext(options))
//            {
//                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
//                var sut = new SaleService(context, dateTimeNowProvider.Object);

//                //Act
//                var actualSales = sut.GetSalesByDat(validStartDate, validEndDate);

//                //Assert
//                Assert.AreEqual(2, actualSales.Count);
//                Assert.IsTrue(actualSales.All(x => validStartDate <= x.OrderDate && x.OrderDate <= validEndDate));
//            }
//        }
//    }
//}
