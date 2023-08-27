import { Component } from "@angular/core";
import { Router } from '@angular/router';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { InvoiceInfo, InvoiceService } from "src/app/common/services/invoice.service";
import { PaymentService } from "src/app/common/services/payment.service";

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent{
  datasource: any;

  invoiceInfo: InvoiceInfo  = {
    userId: 1,
    vehicleId: 1,
    locationId: 1
  };

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private paymentService: PaymentService,
    private invoiceService: InvoiceService,
    private authenticationService: AuthenticationService,
    private router: Router) {
    this.paymentService.getPayments().subscribe(res => {
      this.datasource = res;
    });
    
    this.authenticationService.loggedUserId().subscribe(res => {
      
    })

    
  }

  downloadIconClick()  {
    this.invoiceService.downloadInvoice(this.invoiceInfo).subscribe(
      (pdfBlob: Blob) => {
        const url = window.URL.createObjectURL(pdfBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'generated-pdf.pdf';
        a.click();
        window.URL.revokeObjectURL(url);
      },
      error => {
        console.error('Error generating PDF:', error);
      }
    );
  }

}

