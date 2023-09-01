import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/common/services/data.service';
import { LocationService } from 'src/app/common/services/location.service';
import { CreateVehicleDto, VehicleService } from 'src/app/common/services/vehicle.service';


@Component({
  selector: 'app-vehicle-add',
  templateUrl: './vehicle-add.component.html',
  styleUrls: ['./vehicle-add.component.css']
})
export class VehicleAddComponent implements OnInit {
  private returnUrl!: string;

  vehicleForm !: FormGroup;
  errorMessage: string = '';
  showError: boolean = false;
  enumBodyCar = Object.values(BodyCar);
  enumPrimaryDamage = Object.values(Damage);
  enumSecondaryDamage = Object.values(Damage);
  enumDrive = Object.values(Drive);
  enumFuel = Object.values(Fuel);
  enumHighlight = Object.values(Highlight);
  enumProducer = Object.values(Producer);
  enumSaleTerm = Object.values(SaleTerm);
  enumTransmission = Object.values(Transmission);
  locations: any;



  constructor(private vehicleService: VehicleService,
    private locationService: LocationService,
    private router: Router,
    private route: ActivatedRoute,
    private dataService: DataService) {

    this.locationService.getLocations().subscribe(res => {
      this.locations = res;
    });

  }

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
      saleTerm: new FormControl("", [Validators.required]),
      highlights: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/vehicle/picture';
  }

  addVehicle = (vehicleFormValue: any) => {
    this.showError = true;
    const vehicle = { ...vehicleFormValue };

    alert(vehicle.secondTireSet);

    const createVehicle: CreateVehicleDto = {
      producer: vehicle.producer,
      modelSpecifer: vehicle.modelSpecifer,
      modelGeneration: vehicle.modelGeneration,
      registrationYear: vehicle.registrationYear,
      color: vehicle.color,
      auction: vehicle.auction,
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
      serviceManual: vehicle.serviceManual,
      secondTireSet: vehicle.secondTireSet,
      VIN: vehicle.VIN,
      saleTerm: vehicle.saleTerm,
      highlights: vehicle.highlights,
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

enum BodyCar {
  none = '',
  Micro = 'Micro',
  Sedan = 'Sedan',
  Liftback = 'Liftback',
  Combi = 'Combi',
  Coupe = 'Coupe',
  Hatchback = 'Hatchback',
  Van = 'Van',
  SUV = 'SUV',
  Picup = 'Picup',
  Cabrio = 'Cabrio',
}



enum Damage {
  none = '',
  All_Over = 'All Over',
  Burn = 'Burn',
  Burn_Engine = 'Burn Engine',
  Front_End = 'Front End',
  Hail = 'Hail',
  Mechanical = 'Mechanical',
  Minor_Dents_Scratch = 'Minor Dents Scratch',
  Normal_Wear = 'Normal Wear',
  Rear_End = 'Rear End',
  Rollover = 'Rollover',
  Side = 'Side',
  Top_Roof = 'Top Roof',
  Undercarriage = 'Undercarriage',
  Unknown = 'Unknown',
  Vandalism = 'Vandalism'
}


enum Drive {
  none = '',
  AWD = 'AWD',
  FWD = 'FWD',
  RWD = 'RWD',
}


enum Fuel {
  none = '',
  Diesel = 'Diesel',
  Petrol = 'Petrol',
  Gas = 'Gas',
  Hybrid = 'Hybrid',
  Electric = 'Electric',
}


enum Highlight {
  NonOperational = 'Non Operational',
  RunAndDrive = 'Run and Drive',
}


enum Producer {
  none = '',
  Alfa_Romeo = 'Alfa Romeo',
  Audi = 'Audi',
  BMW = 'BMW',
  Cupra = 'Cupra',
  Chevrolet = 'Chevrolet',
  Citroen = 'Citroen',
  Dacia = 'Dacia',
  Dodge = 'Dodge',
  FIAT = 'FIAT',
  Ford = 'Ford',
  Honda = 'Honda',
  Hyundai = 'Hyundai',
  Kia = 'Kia',
  Land_Rover = 'Land Rover',
  Jeep = 'Jeep',
  Lexus = 'Lexus',
  Mazda = 'Mazda',
  Mercedes = 'Mercedes',
  Mini = 'Mini',
  Mitsubishi = 'Mitsubishi',
  Nissan = 'Nissan',
  Opel = 'Opel',
  Peugeot = 'Peugeot',
  Renault = 'Renault',
  Seat = 'Seat',
  Skoda = 'Skoda',
  Subaru = 'Subaru',
  Suzuki = 'Suzuki',
  Tesla = 'Tesla',
  Toyota = 'Toyota',
  Volkswagen = 'Volkswagen',
  Volvo = 'Volvo',
}

enum SaleTerm {
  none = '',
  Conditional_repair = 'Conditional Repair',
  Used_vehicle = 'Used Vehicle',
  To_be_desmantle = 'To be desmantle',
  Classic = 'Classic',
}



enum Transmission {
  none = '',
  Manual = 'Manual',
  Automatic = 'Automatic',
  DualClutch = 'DualClutch',
  CVT = 'CVT',
}

