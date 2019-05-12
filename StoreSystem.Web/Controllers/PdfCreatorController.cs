using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Web.Controllers.Utils;
using System.IO;

namespace StoreSystem.Web.Controllers
{
    public class PdfCreatorController : Controller
    {
        private IConverter _converter;

        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;
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
        [Route("ToPlain")]
        [HttpPost]
        public IActionResult Plain([FromBody] HtmlSource htmlSource) //string htmlContent
        {
            // var myObject = JsonConvert.DeserializeObject<string>(htmlSource.Source);

            return this.View("Plain.cshtml");
        }

        [Route("PdfFromURL")]
        [HttpPost]
        public void PdfUrl([FromBody] HtmlSource htmlSource) //string htmlContent
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
                        //HtmlContent = htmlSource.Source,
                        Page = htmlSource.Source,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            converter.Convert(doc);
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
            return this.View("../Sales/Details",2);
        }
    }

    public class HtmlSource
    {
        public string Source { get; set; }
    }
}
