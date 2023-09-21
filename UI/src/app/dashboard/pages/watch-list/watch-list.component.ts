import { Component } from "@angular/core";
import { Router } from '@angular/router';
import { VehicleService, WatchDto } from 'src/app/common/services/vehicle.service';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { NotificationService } from "src/app/common/services/notification.service";
import { TranslocoService } from "@ngneat/transloco";

@Component({
  selector: 'app-watch-list',
  templateUrl: './watch-list.component.html',
  styleUrls: ['./watch-list.component.css']
})
export class WatchListComponent {

  datasource: any;

  watchDto: WatchDto = {
    vehicleId: 0,
    userId: 0
  };

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private vehicleService: VehicleService,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private transloco: TranslocoService,
    private router: Router) {

    this.authenticationService.loggedUserId().subscribe(res => {
      this.vehicleService.getAllWatch(res.userId).subscribe(res => {
        this.datasource = res;
      });
    });
  }

  handleRowDoubleClick(event: RowDblClickEvent) {
    const template = event.data
    this.router.navigate(['/vehicle/lot', template.lotNumber].filter(v => !!v));
  }

  removeWatchClick(vehicleId: any)  {
    this.authenticationService.loggedUserId().subscribe(res => {
      this.watchDto.userId = res.userId;
      this.watchDto.vehicleId = vehicleId;
      this.vehicleService.removeWatch(this.watchDto)
      .subscribe({
        next: () => {
          this.notificationService.showSuccess( this.transloco.translate('notification.vehicleUnwatchCorrect'), "Success");
          this.vehicleService.getAllWatch(res.userId).subscribe(res => {
            this.datasource = res;
          });
        },
        error: () => {
          this.notificationService.showError( this.transloco.translate('notification.vehicleUnwatchFail'), "Failed");
        }
      })
    });
  }
}