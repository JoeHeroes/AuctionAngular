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
    this.vehicleService.deleteVehicle(vehicleId).subscribe(res => {
      this.vehicleService.getVehiclesWaiting().subscribe(res => {
        this.datasource = res;
      });
    })
  }

  acceptClick(vehicleId: any)  {
    this.vehicleService.confirmVehicle(vehicleId).subscribe(res => {
      this.vehicleService.getVehiclesWaiting().subscribe(res => {
        this.datasource = res;
      });
    })
  }










  sellClick(vehicleId: any)  {
    
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
}
