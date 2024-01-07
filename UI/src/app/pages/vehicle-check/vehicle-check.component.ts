import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { TranslocoService } from '@ngneat/transloco';
import { AuthResponseDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-vehicle-check',
  templateUrl: './vehicle-check.component.html',
  styleUrls: ['./vehicle-check.component.css']
})
export class VehicleCheckComponent {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];
 
  displayMode = 'full';

  constructor(private vehicleService: VehicleService,
    private notificationService: NotificationService,
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
}