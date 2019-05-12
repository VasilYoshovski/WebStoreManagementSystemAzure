using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Services;
using StoreSystem.Services.Dto;
using StoreSystem.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.ProductSaleServiceTests
{
    [TestClass]
    public class GetProductsTotalSaleQuantity_Should
    {
        [TestMethod]
        public async Task ReturnProperQuantity_WhenStarParameterIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeedProductSale.GetOptions(databaseName);

            DbSeedProductSale.SeedDatabase(options);

            int productValidId1 = 1;
            int productValidId2 = 2;
            int productValidId3 = 3;

            decimal expectedTotal1 = (decimal)(1 * 1 * (1 - 0.1));
            decimal expectedTotal2 = (decimal)(1 * 2 * (1 - 0.1));
            decimal expectedTotal3 = (decimal)(1 * 3 * (1 - 0.1));

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new ProductSaleService(context);

                //Act
                var a = await sut.GetProductsTotalSaleQuantityAsync(Consts.AllText);

                //Assert
                Assert.AreEqual(2, a[0].Count());
                Assert.AreEqual(1, a[1].Count());
                Assert.AreEqual(1, a[2].Count());
                Assert.AreEqual(productValidId1, a[0].Key);
                Assert.AreEqual(productValidId2, a[1].Key);
                Assert.AreEqual(productValidId3, a[2].Key);
                CollectionAssert.AreEquivalent(new List<decimal> { expectedTotal1, expectedTotal1 }, a[0].Select(x => x.Total).ToList());
                CollectionAssert.AreEquivalent(new List<decimal> { expectedTotal2 }, a[1].Select(x => x.Total).ToList());
                CollectionAssert.AreEquivalent(new List<decimal> { expectedTotal3 }, a[2].Select(x => x.Total).ToList());
            }
        }
    }
}
