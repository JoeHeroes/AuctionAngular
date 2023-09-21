import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuthResponseDto } from 'src/app/common/services/authentication.service';
import { NotificationService } from 'src/app/common/services/notification.service';
import { VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-verification-vehicles',
  templateUrl: './verification-vehicles.component.html',
  styleUrls: ['./verification-vehicles.component.css']
})
export class VerificationVehiclesComponent {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private vehicleService: VehicleService,
    private notificationService: NotificationService,
    private router: Router,
    private transloco: TranslocoService) {
    this.vehicleService.getVehiclesWaiting().subscribe(res => {
      this.datasource = res;
    });
  }

  setClick(vehicleId: any)  {
    this.router.navigate(['/vehicle/set', vehicleId].filter(v => !!v));
  }

  detailClick(vehicleId: any)  {
    this.router.navigate(['/vehicle/detail', vehicleId].filter(v => !!v));
  }

  cancelClick(vehicleId: any)  {
    this.vehicleService.deleteVehicle(vehicleId).subscribe({
      next: (res: AuthResponseDto) => {
        this.vehicleService.getVehiclesWaiting().subscribe(res => {
          this.datasource = res;
        });
        this.notificationService.showSuccess( this.transloco.translate('notification.deleteVehicleCorrect'), "Success");
      },
      error: (err: HttpErrorResponse) => {
        this.notificationService.showError( this.transloco.translate('notification.deleteVehicleFail'), "Failed");
      }
    })
  }

  acceptClick(vehicleId: any)  {
    this.vehicleService.confirmVehicle(vehicleId).subscribe({
      next: (res: AuthResponseDto) => {
        this.vehicleService.getVehiclesWaiting().subscribe(res => {
          this.datasource = res;
        });
        this.notificationService.showSuccess( this.transloco.translate('notification.confirmVehicleCorrect'), "Success");
      },
      error: (err: HttpErrorResponse) => {
        this.notificationService.showError( this.transloco.translate('notification.confirmVehicleFail'), "Failed");
      }
    })
  }

  editClick(vehicleId: any)  {
    this.router.navigate(['/vehicle/edit', vehicleId].filter(v => !!v));
  }
}
