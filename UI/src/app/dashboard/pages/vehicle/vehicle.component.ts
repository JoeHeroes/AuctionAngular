import { VehicleService } from './../../../common/services/vehicle.service';
import { Component } from "@angular/core";
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { Router } from '@angular/router';


@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
})
export class VehicleComponent {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private vehicleService: VehicleService,
    private router: Router) {
    this.vehicleService.getVehicles().subscribe(res => {
      this.datasource = res;
    });
  }

  handleRowDoubleClick(event: RowDblClickEvent) {
    const template = event.data
    this.router.navigate(['/vehicle/lot', template.lotNumber].filter(v => !!v));
  }

}
