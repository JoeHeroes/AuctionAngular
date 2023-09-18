import { Component } from '@angular/core';
import { Router } from '@angular/router';
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
    private router: Router) {
    this.vehicleService.getVehiclesAuctionEnd().subscribe(res => {
      this.datasource = res;
    });
  }

  editClick(vehicleId: any)  {
    this.router.navigate(['/vehicle/edit', vehicleId].filter(v => !!v));
  }

  confirmClick(vehicleId: any)  {

    this.vehicleService.getVehicle(vehicleId).subscribe(res => {

    });
  }
}
