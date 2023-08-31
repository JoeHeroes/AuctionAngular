import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DataService } from 'src/app/common/services/data.service';
import { LocationService } from 'src/app/common/services/location.service';
import { CreateVehicleDto, VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-vehicle-edit',
  templateUrl: './vehicle-edit.component.html',
  styleUrls: ['./vehicle-edit.component.css']
})
export class VehicleEditComponent implements OnInit {

  urlSubscription?: Subscription;
  returnUrl!: string;
  id: any;

  editForm !: FormGroup;
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




      producerValue!: string;
      modelSpeciferValue!: string;
      modelGenerationValue!: string;
      registrationYearValue!: string;
      colorValue!: string;
      locationIdValue!: string;
      bodyTypeValue!: string;
      transmissionValue!: string;
      driveValue!: string;
      meterReadoutValue!: string;
      fuelValue!: string;
      primaryDamageValue!: string;
      secondaryDamageValue!: string;
      engineCapacityValue!: string;
      engineOutputValue!: string;
      numberKeysValue!: string;
      serviceManualValue!: string;
      secondTireSetValue!: string;
      VINValue!: string;
      dateTimeValue!: string;
      saleTermValue!: string;
      highlightsValue!: string;

      value!: string;

  constructor(private vehicleService: VehicleService,
    private router: Router,
    private route: ActivatedRoute,
    private dataService: DataService) {
  }

  ngOnInit(): void {

    this.id = 1;

    this.editForm = new FormGroup({
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
      dateTime: new FormControl("", [Validators.required, this.validateMinDate.bind(this)]),
      saleTerm: new FormControl("", [Validators.required]),
      highlights: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/vehicle/picture';


    

    this.vehicleService.getVehicle(this.id).subscribe(res => {
      this.producerValue = res.producer;
      this.modelSpeciferValue = res.modelSpecifer;
      this.modelGenerationValue = res.modelGeneration;
      this.registrationYearValue = res.registrationYear;
      this.colorValue = res.color;
      this.locationIdValue = res.locationId;
      this.bodyTypeValue = res.bodyType;
      this.transmissionValue = res.transmission;
      this.driveValue = res.drive;
      this.meterReadoutValue = res.meterReadout;
      this.fuelValue = res.fuel;
      this.primaryDamageValue = res.primaryDamage;
      this.secondaryDamageValue = res.secondaryDamage;
      this.engineCapacityValue = res.engineCapacity;
      this.engineOutputValue = res.engineOutput;
      this.numberKeysValue = res.numberKeys;
      this.serviceManualValue = res.serviceManual;
      this.secondTireSetValue = res.secondTireSet;
      this.VINValue = res.vin;
      this.saleTermValue = res.saleTerm;
      this.highlightsValue = res.highlights;
    });
  }

  validateMinDate(control: FormControl): { [key: string]: any } | null {
    const selectedDate = new Date(control.value);
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);

    if (selectedDate > currentDate) {
      return { minDate: true };
    }
    return null;
  }

  editVehicle = (vehicleFormValue: any) => {
    this.showError = false;


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
  All_Over = 'All_Over',
  Burn = 'Burn',
  Burn_Engine = 'Burn_Engine',
  Front_End = 'Front_End',
  Hail = 'Hail',
  Mechanical = 'Mechanical',
  Minor_Dents_Scratch = 'Minor_Dents_Scratch',
  Normal_Wear = 'Normal_Wear',
  Rear_End = 'Rear_End',
  Rollover = 'Rollover',
  Side = 'Side',
  Top_Roof = 'Top_Roof',
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

