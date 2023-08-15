import { Component } from "@angular/core";
import { Router } from '@angular/router';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { PaymentService } from "src/app/common/services/payment.service";

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent{
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private paymentService: PaymentService,
    private authenticationService: AuthenticationService,
    private router: Router) {
    

    this.paymentService.getPayments().subscribe(res => {
      this.datasource = res;
    });
    
    this.authenticationService.loggedUserId().subscribe(res => {
      
    })
  }


  handleRowDoubleClick(event: RowDblClickEvent) {
    const template = event.data
    this.router.navigate(['/vehicle/lot', template.lotNumber].filter(v => !!v));
  }

}