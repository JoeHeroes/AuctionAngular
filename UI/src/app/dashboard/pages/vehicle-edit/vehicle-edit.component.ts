import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/common/services/data.service';
import { CreateVehicleDto, VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-vehicle-edit',
  templateUrl: './vehicle-edit.component.html',
  styleUrls: ['./vehicle-edit.component.css']
})
export class VehicleEditComponent implements OnInit {
  private returnUrl!: string;

  vehicleForm !: FormGroup;
  errorMessage: string = '';
  showError: boolean = false;

  constructor(private vehicleService: VehicleService,
    private router: Router,
    private route: ActivatedRoute,
    private dataService: DataService) { }

  ngOnInit(): void {
    this.vehicleForm = new FormGroup({
      producer: new FormControl("", [Validators.required]),
      modelSpecifer: new FormControl("", [Validators.required]),
      modelGeneration: new FormControl("", [Validators.required]),
      registrationYear: new FormControl("", [Validators.required]),
      color: new FormControl("", [Validators.required]),
      locationId: new FormControl("", [Validators.required]),
      bodyType: new FormControl("", [Validators.required]),
      transmission: new FormControl("", [Validators.required]),
      drive: new FormControl("", [Validators.required]),
      meterReadout: new FormControl("", [Validators.required]),
      fuel: new FormControl("", [Validators.required]),
      primaryDamage: new FormControl("", [Validators.required]),
      secondaryDamage: new FormControl("", [Validators.required]),
      engineCapacity: new FormControl("", [Validators.required]),
      engineOutput: new FormControl("", [Validators.required]),
      numberKeys: new FormControl("", [Validators.required]),
      serviceManual: new FormControl("", [Validators.required]),
      secondTireSet: new FormControl("", [Validators.required]),
      VIN: new FormControl("", [Validators.required]),
      dateTime: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/vehicle/picture';
  }

  addVehicle = (vehicleFormValue: any) => {
    this.showError = true;
    const vehicle = { ...vehicleFormValue };
    const createVehicle: CreateVehicleDto = {
      producer: vehicle.producer,
      modelSpecifer: vehicle.modelSpecifer,
      modelGeneration: vehicle.modelGeneration,
      registrationYear: vehicle.registrationYear,
      color: vehicle.color,
      locationId: vehicle.locationId,
      bodyType: vehicle.bodyType,
      transmission: vehicle.transmission,
      drive: vehicle.drive,
      meterReadout: vehicle.meterReadout,
      fuel: vehicle.fuel,
      primaryDamage: vehicle.primaryDamage,
      secondaryDamage: vehicle.secondaryDamage,
      engineCapacity: vehicle.engineCapacity,
      engineOutput: vehicle.engineOutput,
      numberKeys: vehicle.numberKeys,
      serviceManual: vehicle.serviceManua,
      secondTireSet: vehicle.secondTireSet,
      VIN: vehicle.VIN,
      dateTime: vehicle.dateTime,
    }
    this.vehicleService.addVehicle(createVehicle)
      .subscribe({
        next: (res: any) => {
          this.dataService.id = res;
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.name;
          this.showError = true;
        }
      })
  }
}
