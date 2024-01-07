import { Component } from "@angular/core";
import { Router } from '@angular/router';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { AuthenticationService } from "src/app/services/authentication.service";
import { VehicleService } from "src/app/services/vehicle.service";

@Component({
  selector: 'app-vehicle-lost',
  templateUrl: './vehicle-lost.component.html',
  styleUrls: ['./vehicle-lost.component.css']
})
export class VehicleLostComponent {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private vehicleService: VehicleService,
    private authenticationService: AuthenticationService,
    private router: Router) {
    this.authenticationService.loggedUserId().subscribe(res => {
      this.vehicleService.getLostVehicles(res.userId).subscribe(res => {
        this.datasource = res;
      });
    })
  }

  handleRowDoubleClick(event: RowDblClickEvent) {
    const template = event.data
    this.router.navigate(['/vehicle/lot', template.lotNumber].filter(v => !!v));
  }
}