import { VehicleService } from './../../../common/services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { Router } from '@angular/router';


@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
})
export class VehicleComponent implements OnInit {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  customizeColumns(columns: any) {
    columns[0].width = 70;
  }


  constructor(private service: VehicleService,
    private router: Router) {
    this.service.getVehicles().subscribe(res => {
      this.datasource = res;
    });
  }
  ngOnInit(): void {
  }

  onExporting() {

  }


  handleRowDoubleClick(event: RowDblClickEvent) {
    const template = event.data
    this.router.navigate(['/vehicle/lot', template.lotNumber].filter(v => !!v));
  }

}
