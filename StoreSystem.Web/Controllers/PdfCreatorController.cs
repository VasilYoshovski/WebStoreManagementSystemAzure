using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Web.Controllers.Utils;
using StoreSystem.Web.Models.SaleViewModels;
using System.IO;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class PdfCreatorController : Controller
    {
        private IConverter _converter;
        private readonly ISaleService saleService;
        private readonly UserManager<StoreUser> userManager;

        public PdfCreatorController(
            IConverter converter,
            ISaleService saleService,
            UserManager<StoreUser> userManager
            )
        {
            _converter = converter;
            this.saleService = saleService ?? throw new System.ArgumentNullException(nameof(saleService));
            this.userManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
        }

        public IActionResult CreatePDF(string id)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = @"D:\Report.pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = id,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);

            return Ok("Successfully created PDF document.");
        }
        
        public async Task<IActionResult> SaleIndex()
        {
            int? clientId = null;
            if (this.User.IsInRole(ROLES.Client))
            {
                clientId = (await this.userManager.GetUserAsync(this.User))?.ClientId;
                if (clientId == null)
                {
                    return this.NotFound();
                }
            }
            var sales = await this.saleService.GetSalesWithTotalAsync(clientID: clientId);
            var notClosed = await this.saleService.GetNotClosedSalesAsync();
            return this.View(new SaleIndexViewModel() { SalesList = sales, NotClosedSales = notClosed, CanEdit = false });
        }

        [HttpGet]
        public IActionResult PlainPost([FromBody] HtmlSource htmlSource) //string htmlContent
        {
            // var myObject = JsonConvert.DeserializeObject<string>(htmlSource.Source);

            //return this.View("Plain",htmlSource);
            return RedirectToAction(nameof(SaleIndex), new { htmlSource });
        }

        [Route("PdfFromURL")]
        [HttpPost]
        public IActionResult PdfUrl([FromBody] HtmlSource htmlSource) //string htmlContent
        {
            var converter = new BasicConverter(new PdfTools());
            //CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            //context.LoadUnmanagedLibrary(@"C:\Users\stani\Desktop\storemanagementsystemweb\StoreSystem.Web\libwkhtmltox.dll");

            //var converter = new SynchronizedConverter(new PdfTools());


            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings() { Top = 30 },
                        Out = @"D:\test.pdf",
                    },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        //HtmlContent = htmlSource.Source,
                        Page = htmlSource.Source,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            converter.Convert(doc);
            return StatusCode(200, "success");

        }

        [Route("PdfFromSource")]
        [HttpPost]
        public IActionResult PdfSource([FromBody] HtmlSource htmlSource) //string htmlContent
        {
            //var converter = new BasicConverter(new PdfTools());
            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(@"C:\Users\stani\Desktop\storemanagementsystemweb\StoreSystem.Web\libwkhtmltox.dll");

            var converter = new SynchronizedConverter(new PdfTools());


            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings() { Top = 10 },
                        Out = @"D:\test.pdf",
                    },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlSource.Source,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };


            converter.Convert(doc);
            return StatusCode(200,"success");
        }
    }

    public class HtmlSource
    {
        public string Source { get; set; }
    }
}
