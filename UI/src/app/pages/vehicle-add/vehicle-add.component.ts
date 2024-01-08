import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuctionService } from 'src/app/services/auction.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';
import { CreateVehicleDto, VehicleService, WatchDto } from 'src/app/services/vehicle.service';


@Component({
  selector: 'app-vehicle-add',
  templateUrl: './vehicle-add.component.html',
  styleUrls: ['./vehicle-add.component.css']
})
export class VehicleAddComponent implements OnInit {
  private returnUrl!: string;

  auctions: any;
  userId: number = 0;
  addForm !: FormGroup;
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


  constructor(private vehicleService: VehicleService,
    private auctionService: AuctionService,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) {

    this.auctionService.getAuctionList().subscribe(res => {
      this.auctions = res;
    });

    this.authenticationService.loggedUserId().subscribe(res => {
      this.userId = res.userId;
	  })
  }

  ngOnInit(): void {
    this.addForm = new FormGroup({
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
      Category: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/vehicle/picture';
  }

  addVehicle = (vehicleFormValue: any) => {
    this.showError = true;
    const vehicle = { ...vehicleFormValue };

    if (vehicle.serviceManual == "") {
      vehicle.serviceManual = true;
    }

    if (vehicle.secondTireSet == "") {
      vehicle.secondTireSet = true;
    }

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
      Category: vehicle.Category,
      confirm: false,
      ownerId : this.userId,
    }

    if (vehicle.producer == "") {
      this.errorMessage = this.transloco.translate('message.producerRequired');
      this.showError = true;
    }
    else if (vehicle.modelSpecifer == "") {
      this.errorMessage = this.transloco.translate('message.modelSpeciferRequired');
      this.showError = true;
    }
    else if (vehicle.modelGeneration == "") {
      this.errorMessage = this.transloco.translate('message.modelGenerationRequired');
      this.showError = true;
    }
    else if (vehicle.registrationYear == "") {
      this.errorMessage = this.transloco.translate('message.registrationYearRequired');
      this.showError = true;
    }
    else if (vehicle.color == "") {
      this.errorMessage = this.transloco.translate('message.colorRequired');
      this.showError = true;
    }
    else if (vehicle.auction == "") {
      this.errorMessage = this.transloco.translate('message.auctionRequired');
      this.showError = true;
    }
    else if (vehicle.bodyType == "") {
      this.errorMessage = this.transloco.translate('message.bodyTypeRequired');
      this.showError = true;
    }
    else if (vehicle.transmission == "") {
      this.errorMessage = this.transloco.translate('message.transmissionRequired');
      this.showError = true;
    }
    else if (vehicle.drive == "") {
      this.errorMessage = this.transloco.translate('message.driveRequired');
      this.showError = true;
    }
    else if (vehicle.meterReadout == "") {
      this.errorMessage = this.transloco.translate('message.meterReadoutRequired');
      this.showError = true;
    }
    else if (vehicle.fuel == "") {
      this.errorMessage = this.transloco.translate('message.fuelRequired');
      this.showError = true;
    }
    else if (vehicle.primaryDamage == "") {
      this.errorMessage = this.transloco.translate('message.primaryDamageRequired');
      this.showError = true;
    }
    else if (vehicle.secondaryDamage == "") {
      this.errorMessage = this.transloco.translate('message.secondaryDamageRequired');
      this.showError = true;
    }
    else if (vehicle.engineCapacity == "") {
      this.errorMessage = this.transloco.translate('message.engineCapacityRequired');
      this.showError = true;
    }
    else if (vehicle.engineOutput == "") {
      this.errorMessage = this.transloco.translate('message.engineOutputRequired');
      this.showError = true;
    }
    else if (vehicle.numberKeys == "") {
      this.errorMessage = this.transloco.translate('message.numberKeysRequired');
      this.showError = true;
    }
    else if (vehicle.serviceManual == "") {
      this.errorMessage = this.transloco.translate('message.serviceManualRequired');
      this.showError = true;
    }
    else if (vehicle.secondTireSet == "") {
      this.errorMessage = this.transloco.translate('message.secondTireSetRequired');
      this.showError = true;
    }
    else if (vehicle.VIN == "") {
      this.errorMessage = this.transloco.translate('message.vinRequired');
      this.showError = true;
    }
    else if (vehicle.saleTerm == "") {
      this.errorMessage = this.transloco.translate('message.saleTermRequired');
      this.showError = true;
    }
    else if (vehicle.Category == "") {
      this.errorMessage = this.transloco.translate('message.categoryRequired');
      this.showError = true;
    }

    this.vehicleService.addVehicle(createVehicle)
      .subscribe({
        next: (res: any) => {
          this.notificationService.showSuccess( this.transloco.translate('notification.vehicleAddCorrect'), "Success");
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => { 
          this.notificationService.showError( this.transloco.translate('notification.vehicleAddFail'), "Failed");
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
  AWD = 'AWD',
  FWD = 'FWD',
  RWD = 'RWD',
}

enum Fuel {
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
  Conditional_repair = 'Conditional Repair',
  Used_vehicle = 'Used Vehicle',
  To_be_desmantle = 'To be desmantle',
  Classic = 'Classic',
}

enum Transmission {
  Manual = 'Manual',
  Automatic = 'Automatic',
  DualClutch = 'DualClutch',
  CVT = 'CVT',
}