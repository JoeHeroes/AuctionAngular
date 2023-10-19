import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { InvoiceInfo, InvoiceService } from 'src/app/services/invoice.service';

@Component({
  selector: 'app-admin-panel-invoices',
  templateUrl: './admin-panel-invoices.component.html',
  styleUrls: ['./admin-panel-invoices.component.css']
})
export class AdminPanelInvoicesComponent {
  datasource: any;

  invoiceInfo: InvoiceInfo  = {
    userId: 0,
    vehicleId: 0,
    locationId: 1,
  };

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private invoiceService: InvoiceService,
    private authenticationService: AuthenticationService) {

      this.invoiceService.getInvoices().subscribe(res => {
        this.datasource = res;
      });
  }

  downloadClick(vehicleId: any)  {
    this.authenticationService.loggedUserId().subscribe(res => {
      this.invoiceInfo.userId = res.userId;
      this.invoiceInfo.vehicleId = vehicleId;
      this.invoiceService.downloadInvoice(this.invoiceInfo).subscribe(
        (pdfBlob: Blob) => {
          const url = window.URL.createObjectURL(pdfBlob);
          const a = document.createElement('a');
          a.href = url;
          a.download = 'invoice.pdf';
          a.click();
          window.URL.revokeObjectURL(url);
        },
        error => {
          console.error('Error generating PDF:', error);
        }
      );
    })
  }
}