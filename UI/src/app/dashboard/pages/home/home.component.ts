import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { marker as _ } from '@ngneat/transloco-keys-manager/marker';
import { VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  datasource: any;
  count: number = 0;



  constructor(private vehicleService: VehicleService,
    private router: Router) {
    this.vehicleService.getVehicles().subscribe(res => {
      this.datasource = res;
      this.count = this.datasource.length;
    });
  }
}
