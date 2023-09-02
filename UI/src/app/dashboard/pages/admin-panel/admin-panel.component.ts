import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent  {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private vehicleService: VehicleService,
    private router: Router) {
    this.vehicleService.getVehicles().subscribe(res => {
      this.datasource = res;
    });
  }

  sellClick(vehicleId: any)  {
    this.vehicleService.sellVehicle(vehicleId).subscribe(res => {
      this.vehicleService.getVehicles().subscribe(res => {
        this.datasource = res;
      });
    })
  }

  editClick(vehicleId: any)  {
    this.router.navigate(['/vehicle/edit', vehicleId].filter(v => !!v));
  }

  deleteClick(vehicleId: any)  {
    this.vehicleService.deleteVehicle(vehicleId).subscribe(res => {
      this.vehicleService.getVehicles().subscribe(res => {
        this.datasource = res;
      });
    })
  }

  invoiceClick(vehicleId: any)  {
    
  }

}
