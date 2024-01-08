import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuctionService } from 'src/app/services/auction.service';
import { DataService } from 'src/app/services/data.service';
import { EditVehicleDto, VehicleService } from 'src/app/services/vehicle.service';


@Component({
  selector: 'app-vehicle-edit',
  templateUrl: './vehicle-edit.component.html',
  styleUrls: ['./vehicle-edit.component.css']
})
export class VehicleEditComponent implements OnInit {

  urlSubscription?: Subscription;
  returnUrl: string = "/vehicle/panel";
  id: any;
  auctions: any;

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
      auctionValue!: string;
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
      saleTermValue!: string;
      CategoryValue!: string;


  constructor(private vehicleService: VehicleService,
    private auctionService: AuctionService,
    private router: Router,
    private route: ActivatedRoute,
    private dataService: DataService) {
      this.auctionService.getAuctionList().subscribe(res => {
        this.auctions = res;
      });
  }
 
  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');
  }

  ngOnInit(): void {

    this.urlSubscription = this.route.url.subscribe(segments => {
      this.loadData(segments);
    });

    this.editForm = new FormGroup({
      producer: new FormControl("", [Validators.required]),
      modelSpecifer: new FormControl("", [Validators.required]),
      modelGeneration: new FormControl("", [Validators.required]),
      registrationYear: new FormControl("", [Validators.required]),
      color: new FormControl("", [Validators.required]),
      auction: new FormControl("", [Validators.required]),
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
      Category: new FormControl("", [Validators.required]),
    })
    

    this.vehicleService.getVehicle(this.id).subscribe(res => {
      this.producerValue = res.producer;
      this.modelSpeciferValue = res.modelSpecifer;
      this.modelGenerationValue = res.modelGeneration;
      this.registrationYearValue = res.registrationYear;
      this.colorValue = res.color;
      this.auctionValue = res.auctionId;
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
      this.CategoryValue = res.Category;
    });
  }


  editVehicle = (vehicleFormValue: any) => {
    this.showError = false;

    const vehicle = { ...vehicleFormValue };

    if(vehicle.producer==""){
      vehicle.producer = this.producerValue;
    }
    if(vehicle.modelSpecifer==""){
      vehicle.modelSpecifer = this.modelSpeciferValue;
    }
    if(vehicle.modelGeneration==""){
      vehicle.modelGeneration = this.modelGenerationValue;
    }
    if(vehicle.registrationYear==""){
      vehicle.registrationYear = this.registrationYearValue;
    }
    if(vehicle.color==""){
      vehicle.color = this.colorValue;
    }
    if(vehicle.auction==""){
      vehicle.auction = this.auctionValue;
    }
    if(vehicle.bodyType==""){
      vehicle.bodyType = this.bodyTypeValue;
    }
    if(vehicle.transmission==""){
      vehicle.transmission = this.transmissionValue;
    }
    if(vehicle.drive==""){
      vehicle.drive = this.driveValue;
    }
    if(vehicle.meterReadout==""){
      vehicle.meterReadout = this.meterReadoutValue;
    }
    if(vehicle.fuel==""){
      vehicle.fuel = this.fuelValue;
    }
    if(vehicle.primaryDamage==""){
      vehicle.primaryDamage = this.primaryDamageValue;
    }
    if(vehicle.secondaryDamage==""){
      vehicle.secondaryDamage = this.secondaryDamageValue;
    }
    if(vehicle.engineCapacity==""){
      vehicle.engineCapacity = this.engineCapacityValue;
    }
    if(vehicle.engineOutput==""){
      vehicle.engineOutput = this.engineOutputValue;
    }
    if(vehicle.numberKeys==""){
      vehicle.numberKeys = this.numberKeysValue;
    }
    if(vehicle.serviceManual==""){
      vehicle.serviceManual = this.serviceManualValue;
    }
    if(vehicle.secondTireSet==""){
      vehicle.secondTireSet = this.secondTireSetValue;
    }
    if(vehicle.VIN==""){
      vehicle.VIN = this.VINValue;
    }
    if(vehicle.saleTerm==""){
      vehicle.saleTerm = this.saleTermValue;
    }
    if(vehicle.Category==""){
      vehicle.Category = this.CategoryValue;
    }

    const editVehicle: EditVehicleDto = {
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
      Category: vehicle.Category,
    }
    this.vehicleService.editVehicle(this.id, editVehicle)
      .subscribe({
        next: (res: any) => {
          this.dataService.userId = res;
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
  Pickup = 'Pickup',
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