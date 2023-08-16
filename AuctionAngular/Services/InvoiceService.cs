using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using AuctionAngular.Services.NewFolder;
using Database;
using Database.Entities;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using SixLabors.Fonts.Tables.AdvancedTypographic;
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


            InvoiceHeader header = new InvoiceHeader()
            {
                InvoiceNumber ="Test",
                InvoiceDate = new DateTime(),
                ReceiptNumber = 1234,
                CustomerId = 1,
                LocationId = 1,
                CustomerAddressId = 1,
                DeliveryAddressId = 1,
                Product = "Test",
                Psc = 1,
                Tax = 1,
                Total = 1,
                NetTotal = 1,
                CreateDate = new DateTime(),
            };

            Location location = new Location()
            {
                Name = "Espoo",
                Phone = "358401776000",
                Email = "Espoo@Copart.fi",
                City = "Espoo",
                Street = "Pieni teollisuuskatu 7",
                PostalCode = "Uusimaa 02920",
                County = "Finland",
                Picture = "Espoo.png"

            };


            Location delivery = new Location()
            {
                Name = "Espoo1",
                Phone = "3584017760001",
                Email = "Espoo@Copart.fi1",
                City = "Espoo1",
                Street = "Pieni teollisuuskatu 71",
                PostalCode = "Uusimaa 029201",
                County = "Finland1",
                Picture = "Espoo.png"

            };


            Location place = new Location()
            {
                Name = "Espoo21",
                Phone = "3584017760321001",
                Email = "Espoo@Copart.fi1312",
                City = "Espoo3121",
                Street = "Pieni teollisuuskatu 73121",
                PostalCode = "Uusimaa 029231201",
                County = "Finland1",
                Picture = "Espoo.png"

            };


            User user = new User()
            {
                Name = "Joe",
                SureName = "Heros",
                Phone = "3584017760321001",
                Email = "Espoo@Copart.fi1312",
            };

            List<InvoiceDetail> detail = new List<InvoiceDetail>()
            {
                new InvoiceDetail()
                {
                    InvoiceNo = "Test1",
                    Product = "Test1",
                    Pcs = "Test1",
                    Price  = "Test1",
                    Tax = "Test1",
                    Total = 1,
                },
                new InvoiceDetail()
                {
                    InvoiceNo = "Test2",
                    Product = "Test2",
                    Pcs = "Test2",
                    Price  = "Test2",
                    Tax = "Test2",
                    Total = 2,
                },
                new InvoiceDetail()
                {
                    InvoiceNo = "Test3",
                    Product = "Test3",
                    Pcs = "Test3",
                    Price  = "Test3",
                    Tax = "Test3",
                    Total = 3,
                },
                new InvoiceDetail()
                {
                    InvoiceNo = "Test3",
                    Product = "Test3",
                    Pcs = "Test4",
                    Price  = "Test4",
                    Tax = "Test4",
                    Total = 4,
                },
            };
            string htmlcontent = "<div style='width:100%; text-align:center'>";
            htmlcontent += "<img style='width:80px;height:80%' src='" + imgeurl + "'   />";
            htmlcontent += "<b>Receipt</b>";


            
            

            if (header != null)
            {
                htmlcontent += "<p>Receipt number:" +header.ReceiptNumber+ "</p>";
                htmlcontent += "<p>Date:" +header.CreateDate+ "</p>";
                htmlcontent += "<p>Location:" +place.Name+ "</p>";


                htmlcontent += "<p>Buyer:</p>";
                htmlcontent += "<p>Customer id:" +header.CustomerId+ "</p>";
                htmlcontent += "<p>"+user.Name + " " +user.SureName+ "</p>";
                htmlcontent += "<p>" +location.Street+ "</p>";
                htmlcontent += "<p>" +location.PostalCode+ " " +location.City+ " " +location.County+ "</p>";



                htmlcontent += "<p>Delivery address:</p>";
                htmlcontent += "<p>" +user.Name+ " " +user.SureName+ "</p>";
                htmlcontent += "<p>" +delivery.Street+ "</p>";
                htmlcontent += "<p>" +delivery.PostalCode+ " " +delivery.City+ " " +delivery.County+ "</p>";
                
                
                htmlcontent += "<p>Information:</p>";



            }



            htmlcontent += "<table style ='width:100%; border: 1px solid black'>";
            htmlcontent += "<thead style='font-weight:bold'>";
            htmlcontent += "<tr>";
            htmlcontent += "<td style='border:1px solid black'> Product </td>";
            htmlcontent += "<td style='border:1px solid black'> Pcs </td>";
            htmlcontent += "<td style='border:1px solid black'> Price </td>";
            htmlcontent += "<td style='border:1px solid black'> Tax </td >";
            htmlcontent += "<td style='border:1px solid black'> Total </td>";
            htmlcontent += "</tr>";
            htmlcontent += "</thead >";

            htmlcontent += "<tbody>";
            if (detail != null && detail.Count > 0)
            {
                detail.ForEach(item =>
                {
                    htmlcontent += "<tr>";
                    htmlcontent += "<td>" + item.Product + "</td>";
                    htmlcontent += "<td>" + item.Pcs + "</td>";
                    htmlcontent += "<td>" + item.Price + "</td >";
                    htmlcontent += "<td>" + item.Tax + "</td>";
                    htmlcontent += "<td> " + item.Total + "</td >";
                    htmlcontent += "</tr>";
                });
            }
            htmlcontent += "</tbody>";

            htmlcontent += "</table>";
            htmlcontent += "</div>";

            htmlcontent += "<div style='text-align:right'>";
            htmlcontent += "<h1> Summary Info </h1>";
            htmlcontent += "<table style='border:1px solid  black;float:right' >";
            htmlcontent += "<tr>";
            htmlcontent += "<td style='border:1px solid black'> Summary Total </td>";
            htmlcontent += "<td style='border:1px solid  black'> Summary Tax </td>";
            htmlcontent += "<td style='border:1px solid  black'> Summary NetTotal </td>";
            htmlcontent += "</tr>";
            if (header != null)
            {
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border: 1px solid  black'> " + header.Total + " </td>";
                htmlcontent += "<td style='border: 1px solid  black'>" + header.Tax + "</td>";
                htmlcontent += "<td style='border: 1px solid  black'> " + header.NetTotal + "</td>";
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
            string filepath = "D:\\Logo\\404_learnmore.png";
            byte[] imgarray = System.IO.File.ReadAllBytes(filepath);
            string base64 = Convert.ToBase64String(imgarray);
            return base64;
        }
    }
}
