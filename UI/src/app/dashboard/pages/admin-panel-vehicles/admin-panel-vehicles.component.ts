import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PaymentInfo, PaymentService } from 'src/app/common/services/payment.service';
import { VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-admin-panel-vehicles',
  templateUrl: './admin-panel-vehicles.component.html',
  styleUrls: ['./admin-panel-vehicles.component.css']
})
export class AdminPanelVehiclesComponent {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private vehicleService: VehicleService,
    private paymentService: PaymentService,
    private router: Router) {
    this.vehicleService.getVehiclesAuctionEnd().subscribe(res => {
      this.datasource = res;
    });
  }

  sellClick(vehicleId: any)  {
    this.vehicleService.sellVehicle(vehicleId).subscribe(res => {
      this.vehicleService.getVehiclesAuctionEnd().subscribe(res => {
        this.datasource = res;
      });
    })
  }

  editClick(vehicleId: any)  {
    this.router.navigate(['/vehicle/edit', vehicleId].filter(v => !!v));
  }

  deleteClick(vehicleId: any)  {
    this.vehicleService.deleteVehicle(vehicleId).subscribe(res => {
      this.vehicleService.getVehiclesAuctionEnd().subscribe(res => {
        this.datasource = res;
      });
    })
  }

  invoiceClick(vehicleId: any)  {

    this.vehicleService.getVehicle(vehicleId).subscribe(res => {

      const paymentInfo: PaymentInfo = {
        lotId: vehicleId,
        auctionId: res.auctionId,
        description: "",
        invoiceAmount: res.currentBid,
        lotLeftLocationDate: new Date,
      }
  
      this.paymentService.createPayment(paymentInfo).subscribe(res => {
      })
    });
  }
}
