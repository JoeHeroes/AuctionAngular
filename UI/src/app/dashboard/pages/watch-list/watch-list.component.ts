import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { VehicleService } from 'src/app/common/services/vehicle.service';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { AuthenticationService } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'app-watch-list',
  templateUrl: './watch-list.component.html',
  styleUrls: ['./watch-list.component.css']
})
export class WatchListComponent {

  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private vehicleService: VehicleService,
    private authenticationService: AuthenticationService,
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

}