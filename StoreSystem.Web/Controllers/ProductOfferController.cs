using Microsoft.AspNetCore.Mvc;
using StoreSystem.Services;
using StoreSystem.Services.Dto;
using StoreSystem.Web.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class ProductOfferController : Controller
    {
        private readonly IOfferService offerService;

        public ProductOfferController(IOfferService offerService)
        {
            this.offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
        }

        [HttpPost]
        public async Task<ActionResult> AddProductsToOffer([FromBody] ProductsLinesDto model)
        {
            try
            {
                var res = await this.offerService.AddProductsByIdToOfferAsync(model.SOId, model.Products.ToArray());

                if (res)
                {
                    var ret = $"Success! {model.Products.Count} products added to offer with Id {model.SOId}";
                    return this.Json(new ResultAjax() { IsSucceded = true, Text = ret });
                }
                else
                {
                    var ret = $"Failed to add products to offer"; ;
                    return this.Json(new ResultAjax() { IsSucceded = false, Text = ret });
                };
            }
            catch (ExecutionEngineException ex)
            {
                var ret = $"Failed to add products to offer. " + ex.Message;
                return this.Json(new ResultAjax() { IsSucceded = false, Text = ret });
            }
        }
    }
}