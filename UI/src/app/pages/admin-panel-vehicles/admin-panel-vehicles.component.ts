import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuthResponseDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';
import { PaymentInfo, PaymentService } from 'src/app/services/payment.service';
import { VehicleService } from 'src/app/services/vehicle.service';

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
    private notificationService: NotificationService,
    private router: Router,
    private transloco: TranslocoService) {
    this.vehicleService.getVehiclesAuctionEnd().subscribe(res => {
      this.datasource = res;
    });
  }

  sellClick(vehicleId: any)  {
    this.vehicleService.sellVehicle(vehicleId).subscribe({
      next: (res: AuthResponseDto) => {
        this.vehicleService.getVehiclesAuctionEnd().subscribe(res => {
          this.datasource = res;
        });
        this.notificationService.showSuccess( this.transloco.translate('notification.sellVehicleCorrect'), "Success");
      },
      error: (err: HttpErrorResponse) => {
        this.notificationService.showError( this.transloco.translate('notification.sellVehicleFail'), "Failed");
      }
    })
  }

  editClick(vehicleId: any)  {
    this.router.navigate(['/vehicle/edit', vehicleId].filter(v => !!v));
  }

  deleteClick(vehicleId: any)  {
    this.vehicleService.deleteVehicle(vehicleId).subscribe({
      next: (res: AuthResponseDto) => {
        this.vehicleService.getVehiclesAuctionEnd().subscribe(res => {
          this.datasource = res;
        });
        this.notificationService.showSuccess( this.transloco.translate('notification.deleteVehicleCorrect'), "Success");
      },
      error: (err: HttpErrorResponse) => {
        this.notificationService.showError( this.transloco.translate('notification.deleteVehicleFail'), "Failed");
      }
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