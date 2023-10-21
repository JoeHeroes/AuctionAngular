﻿using AuctionAngular.Dtos;
using AuctionAngular.Dtos.Invoice;
using AuctionAngular.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace AuctionAngular.Services
{
    public class InvoiceService: IInvoiceService
    {
        private readonly AuctionDbContext _dbContext;

        public InvoiceService(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PDFResponseDto> GeneratePDFAsync(InfoDto info)
        {
            var document = new PdfDocument();
            //string imgeurl = "data:image/png;base64, " + Getbase64string() + "";

            var user = await  _dbContext.Users.FirstOrDefaultAsync(x => x.Id == info.UserId);

            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == info.VehicleId);

            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == info.LocationId);

            var locationUser = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == info.LocationId);
            
            var delivery = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == info.LocationId);

            Random rand = new Random();

            HeaderDto header = new HeaderDto()
            {
                InvoiceNumber = rand.Next(100000, 1000000).ToString(),
                CustomerId = user!.Id,
                CustomerAddressId = locationUser!.Id,
                DeliveryAddressId = delivery!.Id,
                LocationId = location!.Id,
                Tax = 23,
                TaxFreePrice = 0,
                TaxTotal = 0,
                Total = 0,
                PaymentMethod = "Bank",
                InvoiceDate = DateTime.Now.ToString("MM.dd.yyyy"),
            };

            TaxDto tax = new TaxDto()
            {
                VAT = 0,
                TotalTax = 0,
                TaxFreeFrice = 0
            };

            List<DetailDto> detail = new List<DetailDto>()
            {
                new DetailDto()
                {
                    Product = vehicle.Producer +" "+vehicle.ModelSpecifer,
                    Pcs = 1,
                    Price  = vehicle.CurrentBid,
                    Tax = "VAT "+ header.Tax +"%",
                    Total = vehicle.CurrentBid,
                },
                new DetailDto()
                {
                    Product = "Lot Retrieval Fee",
                    Pcs = 1,
                    Price  = 15.00,
                    Tax = "VAT "+ header.Tax +"%",
                    Total = 15.00,
                },
                new DetailDto()
                {
                    Product = "Buyer fee",
                    Pcs = 1,
                    Price  = 150.00,
                    Tax = "VAT "+ header.Tax +"%",
                    Total = 150.00,
                },
                new DetailDto()
                {
                    Product = "Virtual Bid Fee",
                    Pcs = 1,
                    Price  = 30.00,
                    Tax = "VAT "+ header.Tax +"%",
                    Total = 30.00,
                },
                new DetailDto()
                {
                    Product = "Green papers fee",
                    Pcs = 1,
                    Price  = 50.00,
                    Tax = "VAT "+ header.Tax +"%",
                    Total = 50.00,
                },
            };


            foreach(var d in detail)
            {
                header.Total += d.Total;
            }

            tax.TaxFreeFrice = (header.Total - vehicle.CurrentBid)/1.23;


            tax.VAT = (header.Total - vehicle.CurrentBid) - tax.TaxFreeFrice;
            tax.TotalTax = tax.VAT;

            string htmlcontent = "<style>body { font-family: Arial, sans-serif; margin: 0; padding: 20px; width:100%;} .invoice {max-width: 800px;margin-bottom: 20px; padding: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);} .item-list { width: 100%; border-collapse: collapse; margin-bottom: 20px; } .item-list th, .item-list td { border: 1px solid #ccc; padding: 8px; text-align: center; } .total { text-align: right; }</style>\r\n";

            htmlcontent += "<table style='margin-bottom: 20px;'><tr><td></td>";
            //htmlcontent += "<table style='margin-bottom: 20px;'><tr><td><img style='width:80px;height:80%' src='" + imgeurl + "'/></td>";

            htmlcontent += "<td style='width:150px'><div></div></td>";
            htmlcontent += "<td><h1>Receipt</h1><p>Receipt number: "+header.InvoiceNumber+"</p></td></tr></table>";
            htmlcontent += "<div class='invoice'>";


            htmlcontent += "<table style='margin-bottom: 20px;'><tr>";
            htmlcontent += "<td><h3>Buyer:</h3><p>Customer id: "+ user.Id +" </p><p> "+user.Name + " "+ user.SureName +"</p><p>"+ location.Street + "</p><p>"+location.PostalCode +" " + location.City+ "</p></td>";
            htmlcontent += "<td style='width:120px'><div></div></td>";
            htmlcontent += "<td><h3>Delivery address</h3><p> "+user.Name + " "+ user.SureName + "</p><p>" + location.Street + "</p><p>" + location.PostalCode + " " + location.City + "</p></td></tr></table>";
            htmlcontent += "<table class='item-list'><thead>";

            htmlcontent += "<tr>";
            htmlcontent += "<th>Product</th>";
            htmlcontent += "<th>Psc</th>";
            htmlcontent += "<th>Price</th>";
            htmlcontent += "<th>Tax</th>";
            htmlcontent += "<th>Total</th>";
            htmlcontent += "</tr>";

            htmlcontent += "</thead><tbody>";

            if (detail != null && detail.Count > 0)
            {
                detail.ForEach(item =>
                {
                    htmlcontent += "<tr>";
                    htmlcontent += "<td>" + item.Product + "</td>";
                    htmlcontent += "<td>" + item.Pcs + "</td >";
                    htmlcontent += "<td>" + item.Price + "</td>";
                    htmlcontent += "<td>" + item.Tax + "</td>"; 
                    htmlcontent += "<td> " + item.Total + "</td >";
                    htmlcontent += "</tr>";
                });
            }

            htmlcontent += "</tbody></table>";
            htmlcontent += "<div style='text-align: left; margin-bottom: 40px;'> <p>Linked to Vehicle"+header.Product+"</p></div>";
            htmlcontent += "<div style='text-align: right;'><p>Total:  "+ header.Total + " EUR </p></div>";
            htmlcontent += "<table>";
            htmlcontent += "<tr style='margin:20px'><td>VAT 24 %:</td><td></td><td>"+ Math.Round(tax.VAT, 2) + "</td></tr>";
            htmlcontent += "<tr style='margin:20px'><td>Total tax:</td><td></td><td>"+ Math.Round(tax.TotalTax, 2) + "</td></tr>";
            htmlcontent += "<tr style='margin:20px'><td>Tax free price:</td><td></td><td>"+ Math.Round(tax.TaxFreeFrice, 2) + "</td></tr>";
            htmlcontent += "</table></br></br></br>";
            htmlcontent += "<table>";
            htmlcontent += "<tr><td>Payment methods:</td><td style='width:80px'></td><td>"+ header.PaymentMethod + "</td><td style='width:80px'></td><td>"+ header.Total + "</td></tr>";
            htmlcontent += "<tr style='margin:20px'><td>Payment reference:</td><td></td><td>"+ header.InvoiceNumber + "</td></tr>";
            htmlcontent += "<tr><td><b>Payment Date</b></td><td></td><td><b>"+ header.InvoiceDate +"</b></td></tr>";
            htmlcontent += "</table>";
            htmlcontent += "<div style='text-align: left; margin-top: 80px;'><p><b>Vehicle information</b></p></div>";
            htmlcontent += "<table><tr><td>Selling price:</td><td style='width:250px'></td><td>"+ vehicle.CurrentBid +" EUR</td></tr></table></br></br>";
            htmlcontent += "<table>";
            htmlcontent += "<tr><td>Location:</td><td style='width:30px'></td><td>"+ location.Name +"</td></tr>";
            htmlcontent += "<tr><td>Vehicle category:</td><td></td><td>"+ vehicle.BodyType + "</td></tr>";
            htmlcontent += "<tr><td>Make:</td><td></td><td>"+ vehicle.Producer + "</td></tr>";
            htmlcontent += "<tr><td>Registration number:</td><td></td><td>"+ vehicle.RegistrationYear + "</td></tr>";
            htmlcontent += "<tr><td>Primary Damage:</td><td></td><td>"+ vehicle.PrimaryDamage + "</td></tr>";
            htmlcontent += "<tr><td>Secondary Damage:</td><td></td><td>"+ vehicle.SecondaryDamage  + "</td></tr>";
            htmlcontent += "<tr><td>VIN:</td><td></td><td>"+ vehicle.VIN + "</td></tr>";
            htmlcontent += "<tr><td>Model:</td><td></td><td>"+ vehicle.ModelSpecifer + "</td></tr>";
            htmlcontent += "<tr><td>Registration year:</td><td></td><td>" + vehicle.RegistrationYear + "</td></tr>";
            htmlcontent += "</table></div></body>";
            
            PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);

            PDFResponseDto dto = new PDFResponseDto();

            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                dto.Response = ms.ToArray();
            }

            dto.Filename = "Invoice_" + header.InvoiceNumber + ".pdf";


            var payment = await _dbContext
                                 .Payments
                                 .FirstOrDefaultAsync(x => x.LotId == info.VehicleId);

            payment!.isInvoiceGenereted = !payment.isInvoiceGenereted;

            _dbContext.SaveChanges();

            return dto;
        }

        public string Getbase64string()
        {
            string filepath = "D:\\Logo\\404_learnmore.png";
            byte[] imgarray = File.ReadAllBytes(filepath);
            string base64 = Convert.ToBase64String(imgarray);
            return base64;
        }
    }
}
