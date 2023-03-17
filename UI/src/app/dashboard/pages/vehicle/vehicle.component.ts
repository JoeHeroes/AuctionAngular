import { IVehicle } from './../../../common/services/auction-api.generated.service';
import { Component, OnInit } from '@angular/core';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { AuctionSystemApiClient } from 'src/app/common/services/auction-api.generated.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
})
export class VehicleComponent implements OnInit {
  datasource: any;

  constructor(private api: AuctionSystemApiClient,
    private router: Router) {
    this.api.getVehicle().subscribe(res => {
      this.datasource = res;
    });
  }
  ngOnInit(): void {
  }

  onExporting() {

  }

  handleRowDoubleClick(event: RowDblClickEvent) {
    const template = event.data as IVehicle;
    this.router.navigate(['/Vehicle', template.id].filter(v => !!v));
  }

}
