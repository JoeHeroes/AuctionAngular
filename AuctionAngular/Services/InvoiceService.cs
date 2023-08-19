using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using AuctionAngular.Services.NewFolder;
using Database;
using Database.Entities;
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
            string htmlcontent = "<style>body { font-family: Arial, sans-serif; margin: 0; padding: 20px; width:100%;} .invoice {max-width: 800px;margin-bottom: 20px; padding: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);} .item-list { width: 100%; border-collapse: collapse; margin-bottom: 20px; } .item-list th, .item-list td { border: 1px solid #ccc; padding: 8px; text-align: center; } .total { text-align: right; }</style>\r\n";
            htmlcontent += "<body><img style='width:80px;height:80%' src='" + imgeurl + "'   />";
            htmlcontent += "<h1>Receipt</h1><p>Receipt number: 12345</p>";
            htmlcontent += "<div class='invoice'>";


            htmlcontent += "<table style='margin-bottom: 20px;'><tr><td><h3>Nadawca:</h3><p>Nazwa firmy</p><p>Adres</p><p>NIP: 123-456-789</p></td>";
            htmlcontent += "<td style='width:150px'><div></div></td>";
            htmlcontent += "<td><h3>Odbiorca:</h3><p>Nazwa klienta</p><p>Adres</p><p>NIP: 987-654-321</p></td></tr></table>";


            htmlcontent += "<table class='item-list'><thead>";
            htmlcontent += "<tr><th>Product</th><th>Psc</th><th>Price</th><th>Tax</th><th>Total</th></tr>";
            htmlcontent += "</thead><tbody>";
            htmlcontent += "<tr><td>Produkt 1</td><td>2</td><td>50.00 zł</td><td>100.00 zł</td><td>100.00 zł</td></tr>";
            htmlcontent += "<tr><td>Produkt 2</td><td>3</td><td>30.00 zł</td><td>90.00 zł</td><td>100.00 zł</td></tr>";
            htmlcontent += "</tbody></table>";
            htmlcontent += "<div style='text-align: left; margin-bottom: 40px;'> <p>Linked to Vehicle 50227047</p></div>";
            htmlcontent += "<div style='text-align: right;'><p>Razem: 190.00 zł</p></div>";
            htmlcontent += "<table>";
            htmlcontent += "<tr style='margin:20px'><td>VAT 24 %:</td><td></td><td>829,55</td></tr>";
            htmlcontent += "<tr style='margin:20px'><td>Total tax:</td><td></td><td>829,55</td></tr>";
            htmlcontent += "<tr style='margin:20px'><td>Tax free price:</td><td></td><td>3456,45</td></tr>";
            htmlcontent += "</table></br></br></br>";
            htmlcontent += "<table>";
            htmlcontent += "<tr><td>Payment methods:</td><td style='width:80px'></td><td>Bank</td><td style='width:80px'></td><td>1321,00</td></tr>";
            htmlcontent += "<tr style='margin:20px'><td>Payment reference:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td><b>Payment Date</b></td><td></td><td><b>27.06.2022</b></td></tr>";
            htmlcontent += "</table>";
            htmlcontent += "<div style='text-align: left; margin-top: 80px;'><p><b>Vehicle information</b></p></div>";
            htmlcontent += "<table><tr><td>Selling price:</td><td style='width:250px'></td><td>3950,00 EUR</td></tr></table></br></br>";
            htmlcontent += "<table>";
            htmlcontent += "<tr><td>Location:</td><td style='width:30px'></td><td>Bank</td></tr>";
            htmlcontent += "<tr><td>Vehicle category:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>Make:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>Registration number:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>Primary Damage:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>Secondary Damage:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>VIN:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>Sale group:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>Model:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>Vehicle category:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "<tr><td>Registration year:</td><td></td><td>200001743108</td></tr>";
            htmlcontent += "</table></div></body>";
            























            PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);

            PDFResponseDto dto = new PDFResponseDto();

            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                dto.Response = ms.ToArray();
            }

            dto.Filename = "Invoice_" + InvoiceNo + ".pdf";

            return dto;

            ////////////////////////////////////////
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
