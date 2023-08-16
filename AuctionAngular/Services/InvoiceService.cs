using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using Database;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace AuctionAngular.Services
{
    public class InvoiceService: IInvoiceService
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IWebHostEnvironment _webHost;

        public InvoiceService(AuctionDbContext dbContext, IWebHostEnvironment webHost)
        {
            _dbContext = dbContext;
            _webHost = webHost;
        }

        public async Task<PDFResponseDto> GeneratePDFAsync(string InvoiceNo)
        {
            var document = new PdfDocument();
            string imgeurl = "data:image/png;base64, " + Getbase64string() + "";


            InvoiceHeader header = await this._container.GetAllInvoiceHeaderbyCode(InvoiceNo);
            List<InvoiceDetail> detail = await this._container.GetAllInvoiceDetailbyCode(InvoiceNo);
            string htmlcontent = "<div style='width:100%; text-align:center'>";
            htmlcontent += "<img style='width:80px;height:80%' src='" + imgeurl + "'   />";
            htmlcontent += "<h2>Welcome to Nihira Techiees</h2>";



            if (header != null)
            {
                htmlcontent += "<h2> Invoice No:" + header.InvoiceNo + " & Invoice Date:" + header.InvoiceDate + "</h2>";
                htmlcontent += "<h3> Customer : " + header.CustomerName + "</h3>";
                htmlcontent += "<p>" + header.DeliveryAddress + "</p>";
                htmlcontent += "<h3> Contact : 9898989898 & Email :ts@in.com </h3>";
                htmlcontent += "<div>";
            }



            htmlcontent += "<table style ='width:100%; border: 1px solid #000'>";
            htmlcontent += "<thead style='font-weight:bold'>";
            htmlcontent += "<tr>";
            htmlcontent += "<td style='border:1px solid #000'> Product Code </td>";
            htmlcontent += "<td style='border:1px solid #000'> Description </td>";
            htmlcontent += "<td style='border:1px solid #000'>Qty</td>";
            htmlcontent += "<td style='border:1px solid #000'>Price</td >";
            htmlcontent += "<td style='border:1px solid #000'>Total</td>";
            htmlcontent += "</tr>";
            htmlcontent += "</thead >";

            htmlcontent += "<tbody>";
            if (detail != null && detail.Count > 0)
            {
                detail.ForEach(item =>
                {
                    htmlcontent += "<tr>";
                    htmlcontent += "<td>" + item.ProductCode + "</td>";
                    htmlcontent += "<td>" + item.ProductName + "</td>";
                    htmlcontent += "<td>" + item.Qty + "</td >";
                    htmlcontent += "<td>" + item.SalesPrice + "</td>";
                    htmlcontent += "<td> " + item.Total + "</td >";
                    htmlcontent += "</tr>";
                });
            }
            htmlcontent += "</tbody>";

            htmlcontent += "</table>";
            htmlcontent += "</div>";

            htmlcontent += "<div style='text-align:right'>";
            htmlcontent += "<h1> Summary Info </h1>";
            htmlcontent += "<table style='border:1px solid #000;float:right' >";
            htmlcontent += "<tr>";
            htmlcontent += "<td style='border:1px solid #000'> Summary Total </td>";
            htmlcontent += "<td style='border:1px solid #000'> Summary Tax </td>";
            htmlcontent += "<td style='border:1px solid #000'> Summary NetTotal </td>";
            htmlcontent += "</tr>";
            if (header != null)
            {
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border: 1px solid #000'> " + header.Total + " </td>";
                htmlcontent += "<td style='border: 1px solid #000'>" + header.Tax + "</td>";
                htmlcontent += "<td style='border: 1px solid #000'> " + header.NetTotal + "</td>";
                htmlcontent += "</tr>";
            }
            htmlcontent += "</table>";
            htmlcontent += "</div>";
            htmlcontent += "</div>";

            PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);

            PDFResponseDto dto = new PDFResponseDto();

            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                dto.Response = ms.ToArray();
            }

            dto.Filename = "Invoice_" + InvoiceNo + ".pdf";

            return dto;
        }

        public Task GeneratePDFAddressAsync()
        {
            throw new NotImplementedException();
        }

        public Task GeneratePDFWithImageAsync()
        {
            throw new NotImplementedException();
        }
        public string Getbase64string()
        {
            string filepath = _webHost.WebRootPath + "\\Uploads\\common\\logo.jpeg";
            byte[] imgarray = System.IO.File.ReadAllBytes(filepath);
            string base64 = Convert.ToBase64String(imgarray);
            return base64;
        }
    }
}
