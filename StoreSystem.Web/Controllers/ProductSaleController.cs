using Microsoft.AspNetCore.Mvc;
using StoreSystem.Data;
using StoreSystem.Services;
using StoreSystem.Web.Models.Dtos;
using StoreSystem.Web.Utils;
using System;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
    public class ProductSaleController : Controller
    {
        private readonly ISaleService saleService;

        public ProductSaleController(ISaleService saleService)
        {
            this.saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
        }

        [HttpPost]
        public async Task<ActionResult> AddProductsToSale([FromBody] ProductsLinesDto model)
        {
            try
            {
                var res = await this.saleService.AddProductsByIdToSaleAsync(model.SOId, model.Products.ToArray());

                if (res)
                {
                    var ret = $"Success! {model.Products.Count} products added to sale with Id {model.SOId}";
                    return this.Json(new ResultAjax() { IsSucceded = true, Text = ret });
                }
                else
                {
                    var ret = $"Failed to add products to sale"; ;
                    return this.Json(new ResultAjax() { IsSucceded = false, Text = ret });
                };
            }
            catch (Exception ex)
            {
                var ret = $"Failed to add products to sale. " + ex.Message;
                return this.Json(new ResultAjax() { IsSucceded = false, Text = ret });
            }
        }
    }
}