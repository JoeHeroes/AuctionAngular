import { Subscription } from 'rxjs';
import { TranslocoService } from '@ngneat/transloco';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { BidDto, VehicleService, WatchDto } from 'src/app/services/vehicle.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-lot',
  templateUrl: './lot.component.html',
  styleUrls: ['./lot.component.css']
})
export class LotComponent implements OnInit {
  urlSubscription?: Subscription;
  id: any;
  datasource: any = {
    Producer: "",
    ModelSpecifer: "",
    ModelGeneration: "",
    RegistrationYear: 0,
    Color: "",
    BodyType: "",
    EngineCapacity: 0,
    EngineOutput: 0,
    Transmission: "",
    Drive: "",
    MeterReadout: 0,
    Fuel: "",
    NumberKeys: 0,
    ServiceManual: "",
    SecondTireSet: "",
    CurrentBid: 0,
    PrimaryDamage: "",
    SecondaryDamage: "",
    DateTime: Date,
    VIN: "",
    LocationId: 0,
    SalesFinised: false,
    SaleTerm: "",
    Category: "",
    WaitingForConfirm: false,
  };
  pictures: any;
  bidForm!: FormGroup;
  watchLot: boolean = false;
  isUserAuthenticated: boolean = false;
  auctionEnd: boolean = false;
  currentDate!: Date;

  watchDto: WatchDto = {
    vehicleId: 0,
    userId: 0
  };

  constructor(private vehicleService: VehicleService,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) {

    this.isUserAuthenticated = false;

    this.authenticationService.loggedUserId().subscribe({
      next: (res) => {
        this.isUserAuthenticated = true;
      },
      error: () => {
      }
    })
  }

  ngOnInit(): void { 
    this.urlSubscription = this.route.url.subscribe(segments => {
      this.loadData(segments);
    });
  }

  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');

    this.vehicleService.getVehicle(this.id).subscribe(res => {
      this.datasource = res;

      this.currentDate = new Date();

      if(formatDate(res.dateTime, 'yyyy-MM-dd', 'en-US') < formatDate(this.currentDate, 'yyyy-MM-dd', 'en-US')){
        this.auctionEnd = true
      }
    });

    this.bidForm = new FormGroup({
      bidNow: new FormControl("", [Validators.required])
    })

    this.authenticationService.loggedUserId().subscribe(res => {
      this.watchDto.userId = res.userId;
      this.watchDto.vehicleId = this.id;
      this.vehicleService.checkWatch(this.watchDto)
        .subscribe({
          next: (res) => {
            this.watchLot = res;
          },
          error: () => {
          }
        })
    });
  }

  watch() {
    this.vehicleService.watch(this.watchDto)
      .subscribe({
        next: () => {
          this.watchLot = true;
          this.notificationService.showSuccess( this.transloco.translate('notification.vehicleWatchCorrect'), "Success");
        },
        error: () => {
          this.notificationService.showError( this.transloco.translate('notification.vehicleWatchFail'), "Failed");
        }
      })
  }

  removeWatch() {
    this.vehicleService.removeWatch(this.watchDto)
      .subscribe({
        next: () => {
          this.watchLot = false;
          this.notificationService.showSuccess( this.transloco.translate('notification.vehicleUnwatchCorrect'), "Success");
        },
        error: () => {
          this.notificationService.showError( this.transloco.translate('notification.vehicleUnwatchFail'), "Failed");
        }
      })
  }

  bidCar(bidValue: any) {
    const bid = { ...bidValue };
    const bidDto: BidDto = {
      lotNumber: this.datasource.id,
      bidNow: bid.bidNow,
      userId: this.watchDto.userId
    }

    this.vehicleService.bidVehicle(bidDto)
      .subscribe({
        next: () => {
          this.vehicleService.getVehicle(this.id).subscribe(res => {
            this.datasource = res;
            this.notificationService.showSuccess( this.transloco.translate('notification.bidVehicleCorrect'), "Success");
          });
        },
        error: () => {
          this.notificationService.showError( this.transloco.translate('notification.bidVehicleFail'), "Failed");
        }
      })
  }

  contact()  {
    this.router.navigate(['/vehicle/contact', this.id].filter(v => !!v));
  }

  opinion()  {
    this.router.navigate(['/vehicle/opinion', this.id].filter(v => !!v));
  }
}